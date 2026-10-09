using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace AshkanFitPro {
public partial class FinanceTrendsForm : Form {
 readonly Store data; readonly bool fa;
 readonly Color teal=Color.FromArgb(42,220,186),rose=Color.FromArgb(255,123,145),muted=Color.FromArgb(168,190,208);
 public FinanceTrendsForm(Store store,bool persian){data=store??new Store();fa=persian;InitializeComponent();Text=fa?"تحلیل مالی باشگاه | اشکان فیت":"AshkanFit Pro | Finance Trends";titleLabel.Text=fa?"روند مالی ۱۲ ماه اخیر":"12-MONTH FINANCIAL TRENDS";exportButton.Text=fa?"خروجی CSV":"Export CSV";closeButton.Text=fa?"بستن":"Close";exportButton.Click+=(s,e)=>Export();closeButton.Click+=(s,e)=>Close();chartPanel.Paint+=DrawChart;Resize+=(s,e)=>chartPanel.Invalidate();UpdateTotals();}
 DateTime MonthAt(int offset){var today=DateTime.Today;return new DateTime(today.Year,today.Month,1).AddMonths(offset);}
 decimal Receipts(DateTime month){return data.Payments.Where(x=>x.Date.Year==month.Year&&x.Date.Month==month.Month).Sum(x=>x.Amount);}
 decimal Costs(DateTime month){return data.Expenses.Where(x=>x.Date.Year==month.Year&&x.Date.Month==month.Month).Sum(x=>x.Amount);}
 void UpdateTotals(){decimal income=0,cost=0;for(int i=-11;i<=0;i++){var m=MonthAt(i);income+=Receipts(m);cost+=Costs(m);}incomeLabel.Text=(fa?"درآمد: ":"RECEIPTS: ")+income.ToString("N0");expenseLabel.Text=(fa?"هزینه: ":"EXPENSES: ")+cost.ToString("N0");netLabel.Text=(fa?"خالص: ":"NET: ")+(income-cost).ToString("N0");}
 void DrawChart(object sender,PaintEventArgs e){var g=e.Graphics;g.SmoothingMode=SmoothingMode.AntiAlias;var rect=chartPanel.ClientRectangle;g.Clear(Color.FromArgb(19,32,51));if(rect.Width<280||rect.Height<180)return;int left=72,right=22,top=30,bottom=55;var width=rect.Width-left-right;var height=rect.Height-top-bottom;var values=Enumerable.Range(-11,12).Select(i=>new{Month=MonthAt(i),Income=Receipts(MonthAt(i)),Expense=Costs(MonthAt(i))}).ToArray();decimal max=Math.Max(1,values.Max(x=>Math.Max(x.Income,x.Expense)));using(var gridPen=new Pen(Color.FromArgb(50,80,100))){for(int j=0;j<=4;j++){int y=top+(height*j/4);g.DrawLine(gridPen,left,y,left+width,y);using(var f=new Font("Segoe UI",8))g.DrawString((max*(4-j)/4).ToString("N0"),f,Brushes.LightGray,4,y-8);}}float slot=width/12f;float barWidth=Math.Max(5,Math.Min(19,slot*.28f));using(var incomeBrush=new SolidBrush(teal))using(var expenseBrush=new SolidBrush(rose))using(var f=new Font("Segoe UI",8)){for(int i=0;i<values.Length;i++){float center=left+slot*(i+.5f);float a=(float)(values[i].Income/max)*height;float b=(float)(values[i].Expense/max)*height;g.FillRectangle(incomeBrush,center-barWidth-2,top+height-a,barWidth,a);g.FillRectangle(expenseBrush,center+2,top+height-b,barWidth,b);g.DrawString(values[i].Month.ToString("MM/yy",CultureInfo.InvariantCulture),f,Brushes.LightGray,center-17,top+height+12);}}}
 void Export(){using(var save=new SaveFileDialog{Filter="CSV files (*.csv)|*.csv",FileName="ashkanfit-finance-trends.csv"}){if(save.ShowDialog(this)!=DialogResult.OK)return;var sb=new StringBuilder("Month,Receipts,Expenses,Net\r\n");for(int i=-11;i<=0;i++){var m=MonthAt(i);var income=Receipts(m);var cost=Costs(m);sb.AppendFormat(CultureInfo.InvariantCulture,"{0:yyyy-MM},{1},{2},{3}\r\n",m,income,cost,income-cost);}File.WriteAllText(save.FileName,"\uFEFF"+sb,Encoding.UTF8);MessageBox.Show(fa?"گزارش ذخیره شد.":"Report exported.");}}
}
}
