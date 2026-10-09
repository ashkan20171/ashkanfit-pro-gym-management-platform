using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace AshkanFitPro {
 public partial class DataHealthForm : Form {
  readonly bool fa;
  readonly List<DataHealthFinding> findings;
  public DataHealthForm(Store data,bool persian) {
   if(!AccountService.IsAdmin)throw new UnauthorizedAccessException("Administrator role required.");
   fa=persian;InitializeComponent();RightToLeft=fa?RightToLeft.Yes:RightToLeft.No;RightToLeftLayout=fa;
   findings=DataHealthReport.Analyze(data,DateTime.Today);
   Text=fa?"اشکان فیت | سلامت داده‌ها":"AshkanFit Pro | Data health";
   heading.Text=fa?"مرکز بررسی سلامت داده‌ها":"DATA QUALITY CENTER";
   summary.Text=(fa?"خطا: ":"Errors: ")+findings.Count(x=>x.Severity=="Error")+"   |   "+(fa?"هشدار: ":"Warnings: ")+findings.Count(x=>x.Severity=="Warning")+"   |   "+(fa?"اطلاع: ":"Info: ")+findings.Count(x=>x.Severity=="Info");
   exportButton.Text=fa?"خروجی CSV":"Export CSV";closeButton.Text=fa?"بستن":"Close";
   grid.Columns.Add("severity",fa?"سطح":"Severity");grid.Columns.Add("category",fa?"بخش":"Category");grid.Columns.Add("detail",fa?"جزئیات":"Details");
   foreach(var f in findings)grid.Rows.Add(f.Severity,f.Category,f.Detail);
   exportButton.Click+=(s,e)=>Export();closeButton.Click+=(s,e)=>Close();
  }
  static string Csv(string s){return "\""+(s??"").Replace("\"","\"\"")+"\"";}
  void Export(){using(var dialog=new SaveFileDialog{Filter="CSV (*.csv)|*.csv",FileName="ashkanfit-data-health.csv"}){
   if(dialog.ShowDialog(this)!=DialogResult.OK)return;
   var lines=new List<string>{"Severity,Category,Detail"};lines.AddRange(findings.Select(x=>Csv(x.Severity)+","+Csv(x.Category)+","+Csv(x.Detail)));
   File.WriteAllText(dialog.FileName,string.Join(Environment.NewLine,lines),new UTF8Encoding(true));
   MessageBox.Show(this,fa?"گزارش ذخیره شد.":"Report exported.");
  }}
 }
}
