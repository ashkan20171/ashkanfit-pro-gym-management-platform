using System;
using System.Drawing;
using System.Windows.Forms;
namespace AshkanFitPro {
 public partial class AuditViewerForm : Form {
  readonly bool fa;
  public AuditViewerForm(bool persian) {
   fa=persian;InitializeComponent();
   Text=fa?"گزارش فعالیت‌های امنیتی":"Security audit events";
   RightToLeft=fa?RightToLeft.Yes:RightToLeft.No;
   RightToLeftLayout=fa;
   btnRefresh.Text=fa?"بارگذاری مجدد":"Refresh";
   btnRefresh.Click+=(s,e)=>RefreshEvents();
   Shown+=(s,e)=>RefreshEvents();
  }
  void RefreshEvents() {
   try {
    grid.Rows.Clear();
    foreach(var item in SqlAuditTrail.Recent(200))grid.Rows.Add(item.Id,item.AtUtc.ToString("yyyy-MM-dd HH:mm:ss"),item.Actor,item.Action,item.Details);
   }catch(Exception ex){MessageBox.Show(this,ex.Message,Text,MessageBoxButtons.OK,MessageBoxIcon.Error);}
  }
 }
}
