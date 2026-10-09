using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace AshkanFitPro {
 public partial class MainForm {
  void InvoicesPage(){
   var host=new Panel{Dock=DockStyle.Fill};body.Controls.Add(host);
   var actions=new FlowLayoutPanel{Dock=DockStyle.Top,Height=76,Padding=new Padding(0,10,0,0)};
   host.Controls.Add(actions);
   var members=repo.Data.Members.OrderBy(m=>m.Name).ToList();
   var selector=new ComboBox{Width=310,DropDownStyle=ComboBoxStyle.DropDownList,Font=regular,Margin=new Padding(0,5,12,0)};
   selector.DataSource=members;selector.DisplayMember="Name";actions.Controls.Add(selector);
   var print=Button(T("صدور صورتحساب HTML","Export printable invoice"),(sender,args)=>{
    var member=selector.SelectedItem as Member;
    if(member==null){MessageBox.Show(T("ابتدا یک عضو ثبت کنید.","Please add a member first."));return;}
    using(var dlg=new SaveFileDialog{Filter="HTML document (*.html)|*.html",FileName="AshkanFit-Invoice-"+member.Id.ToString("N").Substring(0,8)+".html"}){
     if(dlg.ShowDialog(this)!=DialogResult.OK)return;
     try{File.WriteAllText(dlg.FileName,CreateInvoiceHtml(member),new UTF8Encoding(true));MessageBox.Show(T("صورتحساب آماده چاپ ذخیره شد.","Printable invoice saved."));}
     catch(Exception ex){MessageBox.Show(ex.Message,T("خطای ذخیره","Save error"));}
    }
   },accent);print.ForeColor=bg;print.Dock=DockStyle.None;print.Width=240;actions.Controls.Add(print);
   var info=Label(T("عضو را انتخاب کنید؛ صورتحساب شامل پرداخت‌ها، شهریه و مانده خواهد بود. فایل HTML را در مرورگر باز کرده و Print / Save as PDF را بزنید.","Choose a member to export a printable invoice with payment history and outstanding balance. Open the HTML in a browser and use Print / Save as PDF."),11,muted);
   info.Dock=DockStyle.Top;info.Height=70;host.Controls.Add(info);
   var table=Grid();table.DataSource=members.Select(m=>new {Member=m.Name,Plan=m.Plan,Fee=m.Fee,Paid=repo.Data.Payments.Where(p=>p.MemberId==m.Id).Sum(p=>p.Amount),Balance=Math.Max(0,m.Fee-repo.Data.Payments.Where(p=>p.MemberId==m.Id).Sum(p=>p.Amount))}).ToList();host.Controls.Add(table);table.BringToFront();info.BringToFront();actions.BringToFront();
  }
  string CreateInvoiceHtml(Member member){
   Func<string,string> esc=value=>WebUtility.HtmlEncode(value??"");
   var payments=repo.Data.Payments.Where(p=>p.MemberId==member.Id).OrderBy(p=>p.Date).ToList();
   decimal paid=payments.Sum(p=>p.Amount);var html=new StringBuilder();
   html.Append("<!doctype html><html lang='en'><head><meta charset='utf-8'><meta name='viewport' content='width=device-width,initial-scale=1'><title>AshkanFit Pro | Invoice</title><style>body{font:15px Segoe UI,Arial,sans-serif;background:#f3f7fb;color:#152337;padding:32px}main{max-width:820px;margin:auto;background:white;padding:40px;border-radius:18px;box-shadow:0 10px 40px #d9e2ec}h1{color:#007f7d}table{border-collapse:collapse;width:100%;margin-top:25px}td,th{padding:12px;border-bottom:1px solid #e3eaf1;text-align:left}th{background:#eef8f7}.total{font-size:20px;font-weight:700;margin-top:25px}.muted{color:#62758b}@media print{body{background:white;padding:0}main{box-shadow:none;padding:0}.no-print{display:none}}</style></head><body><main>");
   html.Append("<h1>◆ AshkanFit Pro</h1><p class='muted'>Member statement · ").Append(DateTime.Now.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture)).Append("</p>");
   html.Append("<h2>").Append(esc(member.Name)).Append("</h2><p>Phone: ").Append(esc(member.Phone)).Append("<br>Plan: ").Append(esc(member.Plan)).Append("<br>Expires: ").Append(member.Expiry.ToString("yyyy-MM-dd")).Append("</p>");
   html.Append("<table><thead><tr><th>Date</th><th>Note</th><th>Amount</th></tr></thead><tbody>");
   foreach(var payment in payments)html.Append("<tr><td>").Append(payment.Date.ToString("yyyy-MM-dd")).Append("</td><td>").Append(esc(payment.Note)).Append("</td><td>").Append(payment.Amount.ToString("N0",CultureInfo.InvariantCulture)).Append("</td></tr>");
   html.Append("</tbody></table><p class='total'>Membership fee: ").Append(member.Fee.ToString("N0",CultureInfo.InvariantCulture)).Append("<br>Total paid: ").Append(paid.ToString("N0",CultureInfo.InvariantCulture)).Append("<br>Outstanding: ").Append(Math.Max(0,member.Fee-paid).ToString("N0",CultureInfo.InvariantCulture)).Append("</p><p class='muted'>This statement reflects recorded payments; it is not a tax invoice.</p><button class='no-print' onclick='window.print()'>Print / Save as PDF</button></main></body></html>");
   return html.ToString();
  }
 }
}
