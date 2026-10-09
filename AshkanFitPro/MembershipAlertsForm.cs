using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace AshkanFitPro {
public partial class MembershipAlertsForm : Form {
 readonly Store store; readonly bool fa;
 public MembershipAlertsForm(Store data,bool persian){store=data??new Store();fa=persian;InitializeComponent();Text=fa?"هشدار تمدید اشتراک | اشکان فیت":"AshkanFit Pro | Membership alerts";RightToLeft=fa?RightToLeft.Yes:RightToLeft.No;titleLabel.Text=fa?"مرکز هشدار اشتراک اعضا":"MEMBERSHIP RENEWAL CENTER";filterLabel.Text=fa?"بازه هشدار":"Alert window";daysBox.Items.AddRange(new object[]{fa?"۷ روز":"7 days",fa?"۳۰ روز":"30 days",fa?"۶۰ روز":"60 days"});daysBox.SelectedIndex=1;daysBox.SelectedIndexChanged+=(s,e)=>RefreshRows();exportButton.Text=fa?"خروجی CSV":"Export CSV";exportButton.Click+=(s,e)=>Export();closeButton.Text=fa?"بستن":"Close";closeButton.Click+=(s,e)=>Close();grid.Columns.Add("name",fa?"نام عضو":"Member");grid.Columns.Add("phone",fa?"تلفن":"Phone");grid.Columns.Add("plan",fa?"طرح":"Plan");grid.Columns.Add("expiry",fa?"انقضا":"Expiry");grid.Columns.Add("remaining",fa?"روز باقی‌مانده":"Days left");grid.Columns.Add("status",fa?"وضعیت":"Status");RefreshRows();}
 int WindowDays {get{return daysBox.SelectedIndex==0?7:daysBox.SelectedIndex==2?60:30;}}
 System.Collections.Generic.List<Member> Relevant(){var today=DateTime.Today;return (store.Members??new System.Collections.Generic.List<Member>()).Where(m=>m!=null&&m.Expiry.Date<=today.AddDays(WindowDays)).OrderBy(m=>m.Expiry).ThenBy(m=>m.Name).ToList();}
 void RefreshRows(){if(grid==null)return;grid.Rows.Clear();var items=Relevant();int expired=0,upcoming=0;foreach(var m in items){int days=(m.Expiry.Date-DateTime.Today).Days;bool old=days<0;if(old)expired++;else upcoming++;int row=grid.Rows.Add(m.Name??"",m.Phone??"",m.Plan??"",m.Expiry.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture),days,old?(fa?"منقضی":"Expired"):(fa?"نیازمند پیگیری":"Due soon"));grid.Rows[row].DefaultCellStyle.ForeColor=old?Color.FromArgb(255,130,145):Color.FromArgb(68,220,192);}summaryLabel.Text=fa?string.Format("منقضی: {0}   |   در آستانه تمدید: {1}   |   مجموع: {2}",expired,upcoming,items.Count):string.Format("Expired: {0}   |   Upcoming: {1}   |   Total: {2}",expired,upcoming,items.Count);}
 static string Csv(string s){return "\""+(s??"").Replace("\"","\"\"")+"\"";}
 void Export(){using(var dialog=new SaveFileDialog{Filter="CSV files (*.csv)|*.csv",FileName="ashkanfit-membership-alerts.csv"}){if(dialog.ShowDialog(this)!=DialogResult.OK)return;var sb=new StringBuilder("Member,Phone,Plan,Expiry,DaysLeft,Status\r\n");foreach(var m in Relevant()){int days=(m.Expiry.Date-DateTime.Today).Days;sb.Append(Csv(m.Name)).Append(',').Append(Csv(m.Phone)).Append(',').Append(Csv(m.Plan)).Append(',').Append(m.Expiry.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture)).Append(',').Append(days.ToString(CultureInfo.InvariantCulture)).Append(',').Append(days<0?"Expired":"Due soon").Append("\r\n");}try{File.WriteAllText(dialog.FileName,"\uFEFF"+sb,Encoding.UTF8);MessageBox.Show(this,fa?"فایل گزارش ذخیره شد.":"Report exported.");}catch(IOException ex){MessageBox.Show(this,ex.Message,fa?"خطای ذخیره":"Export error",MessageBoxButtons.OK,MessageBoxIcon.Error);}catch(UnauthorizedAccessException ex){MessageBox.Show(this,ex.Message,fa?"عدم دسترسی":"Access denied",MessageBoxButtons.OK,MessageBoxIcon.Error);}}}
}
}
