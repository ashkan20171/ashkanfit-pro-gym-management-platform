using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
namespace AshkanFitPro {
public partial class MainForm {
void ExpensesPage(){
 var layout=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=3};
 layout.RowStyles.Add(new RowStyle(SizeType.Absolute,112));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,66));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));body.Controls.Add(layout);
 var stats=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=3};for(int i=0;i<3;i++)stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,33.333f));layout.Controls.Add(stats,0,0);
 decimal income=repo.Data.Payments.Sum(p=>p.Amount),cost=repo.Data.Expenses.Sum(e=>e.Amount);
 Stat(stats,0,T("مجموع دریافتی","Total receipts"),income.ToString("N0"),"+");
 Stat(stats,1,T("مجموع هزینه","Total expenses"),cost.ToString("N0"),"−");
 Stat(stats,2,T("خالص جریان نقدی","Net cash flow"),(income-cost).ToString("N0"),"=");
 var bar=new FlowLayoutPanel{Dock=DockStyle.Fill,AutoScroll=true,WrapContents=false};layout.Controls.Add(bar,0,1);
 var add=Button(T("+ ثبت هزینه","+ Add expense"),(sender,args)=>AddExpense(),accent);add.ForeColor=bg;add.Width=160;add.Dock=DockStyle.None;bar.Controls.Add(add);
 var remove=Button(T("حذف هزینه انتخابی","Delete selected"),(sender,args)=>{
 if(grid==null||grid.SelectedRows.Count==0)return;
 Guid id=(Guid)grid.SelectedRows[0].Cells["Id"].Value;
 if(MessageBox.Show(T("این هزینه حذف شود؟","Delete this expense?"),"AshkanFit Pro",MessageBoxButtons.YesNo)!=DialogResult.Yes)return;
 repo.Data.Expenses.RemoveAll(x=>x.Id==id);repo.Save();Render();});remove.Width=165;remove.Dock=DockStyle.None;bar.Controls.Add(remove);
 var export=Button("CSV",(sender,args)=>Csv("expenses.csv",new[]{"Date,Category,Description,Amount"}.Concat(repo.Data.Expenses.Select(x=>Line(x.Date.ToString("yyyy-MM-dd"),x.Category,x.Description,x.Amount.ToString(CultureInfo.InvariantCulture))))));export.Width=90;export.Dock=DockStyle.None;bar.Controls.Add(export);
 grid=Grid();grid.DataSource=repo.Data.Expenses.OrderByDescending(x=>x.Date).Select(x=>new{Id=x.Id,Date=DateText(x.Date),Category=x.Category,Description=x.Description,Amount=x.Amount}).ToList();layout.Controls.Add(grid,0,2);if(grid.Columns.Contains("Id"))grid.Columns["Id"].Visible=false;
}
void AddExpense(){
 using(var d=new Form{Text=T("ثبت هزینه","New expense"),Width=480,Height=370,StartPosition=FormStartPosition.CenterParent,BackColor=surface,ForeColor=Color.White,FormBorderStyle=FormBorderStyle.FixedDialog,MaximizeBox=false,MinimizeBox=false,RightToLeft=fa?RightToLeft.Yes:RightToLeft.No}){
 var panel=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(22),ColumnCount=1,RowCount=7};d.Controls.Add(panel);
 var category=new ComboBox{Dock=DockStyle.Top,DropDownStyle=ComboBoxStyle.DropDownList};category.Items.AddRange(new object[]{T("اجاره","Rent"),T("حقوق","Payroll"),T("تجهیزات","Equipment"),T("قبوض","Utilities"),T("سایر","Other")});category.SelectedIndex=0;
 var description=new TextBox{Dock=DockStyle.Top};var amount=new NumericUpDown{Dock=DockStyle.Top,Maximum=1000000000000M,DecimalPlaces=0,ThousandsSeparator=true};
 panel.Controls.Add(Label(T("دسته‌بندی","Category")));panel.Controls.Add(category);panel.Controls.Add(Label(T("توضیحات","Description")));panel.Controls.Add(description);panel.Controls.Add(Label(T("مبلغ","Amount")));panel.Controls.Add(amount);
 var save=Button(T("ذخیره هزینه","Save expense"),(sender,args)=>{if(amount.Value<=0){MessageBox.Show(T("مبلغ باید بیشتر از صفر باشد","Amount must be greater than zero"));return;}repo.Data.Expenses.Add(new Expense{Id=Guid.NewGuid(),Date=DateTime.Now,Category=category.Text,Description=description.Text.Trim(),Amount=amount.Value});repo.Save();d.DialogResult=DialogResult.OK;d.Close();},accent);save.ForeColor=bg;panel.Controls.Add(save);
 if(d.ShowDialog(this)==DialogResult.OK)Render();
 }
}
}
}
