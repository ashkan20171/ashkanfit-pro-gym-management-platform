using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AshkanFitPro {
 public partial class HistoryBrowserForm : Form {
  readonly bool fa;
  readonly SnapshotHistoryReader reader=new SnapshotHistoryReader();
  public HistoryBrowserForm(bool persian) {
   fa=persian;InitializeComponent();
   Text=fa?"تاریخچه نسخه‌های SQL Server":"SQL Server snapshot history";
   rightToLeftLayout(persian);
   btnRefresh.Text=fa?"بارگذاری تاریخچه":"Refresh history";
   btnExport.Text=fa?"خروجی نسخه انتخاب‌شده":"Export selected snapshot";
   btnRefresh.Click+=(s,e)=>RefreshHistory();
   btnExport.Click+=(s,e)=>ExportSelected();
   btnRestore.Text=fa?"بازیابی تأییدشده":"Verified restore";
   btnRestore.Click+=(s,e)=>RestoreSelected();
   grid.Columns.Add("id", "ID");grid.Columns.Add("date",fa?"تاریخ UTC":"Recorded (UTC)");
   grid.Columns.Add("size",fa?"حجم (بایت)":"Size (bytes)");
   grid.Columns.Add("integrity",fa?"اعتبار SHA-256":"SHA-256 check");
   var auditButton=new Button {Text=fa?"گزارش فعالیت‌ها":"Security audit",Dock=DockStyle.Bottom,Height=42,
    BackColor=Color.FromArgb(20,184,166),FlatStyle=FlatStyle.Flat};
   auditButton.Click+=(s,e)=>{using(var window=new AuditViewerForm(fa))window.ShowDialog(this);};
   Controls.Add(auditButton);
   Shown+=(s,e)=>RefreshHistory();
  }
  void rightToLeftLayout(bool persian) {RightToLeft=persian?RightToLeft.Yes:RightToLeft.No;RightToLeftLayout=persian;}
  void RefreshHistory() {
   try {
    grid.Rows.Clear();
    foreach(var record in reader.Recent(100))grid.Rows.Add(record.Id,record.RecordedAtUtc.ToString("yyyy-MM-dd HH:mm:ss"),record.ByteCount,record.HashMatches?"OK":"FAILED");
   }catch(Exception ex){MessageBox.Show(this,ex.Message,Text,MessageBoxButtons.OK,MessageBoxIcon.Error);}
  }
  void RestoreSelected() {
   if(grid.CurrentRow==null || grid.CurrentRow.IsNewRow)return;
   long id=Convert.ToInt64(grid.CurrentRow.Cells[0].Value);
   var warning=fa?"این عملیات اطلاعات زنده باشگاه را جایگزین می‌کند. نسخه فعلی در تاریخچه SQL ذخیره می‌شود. تمام نسخه‌های باز برنامه باید بسته شوند. ادامه می‌دهید؟":"This replaces live gym data. The current version will be saved to SQL history. All other app sessions must be closed. Continue?";
   if(MessageBox.Show(this,warning,Text,MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes)return;
   try {
    new VerifiedSnapshotRestorer().Restore(id);
    MessageBox.Show(this,fa?"بازیابی انجام شد. برنامه را ببندید و دوباره اجرا کنید.":"Restore committed. Close and restart the application before making any changes.",Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
    btnRestore.Enabled=false;
    RefreshHistory();
   }catch(Exception ex){MessageBox.Show(this,ex.Message,Text,MessageBoxButtons.OK,MessageBoxIcon.Error);}
  }
  void ExportSelected() {
   if(grid.CurrentRow==null || grid.CurrentRow.IsNewRow)return;
   long id=Convert.ToInt64(grid.CurrentRow.Cells[0].Value);
   try {
    // Verify before opening the save dialog; no live database data is modified.
    var payload=reader.ExportVerified(id);
    using(var dialog=new SaveFileDialog{Filter="JSON (*.json)|*.json",FileName="AshkanFitPro-History-"+id+".json"}) {
     if(dialog.ShowDialog(this)!=DialogResult.OK)return;
     File.WriteAllBytes(dialog.FileName,payload);
     MessageBox.Show(this,fa?"نسخه بررسی و ذخیره شد. فایل شامل اطلاعات شخصی است؛ آن را ایمن نگهداری کنید.":"Verified snapshot exported. This file contains personal data; store it securely.");
    }
   }catch(Exception ex){MessageBox.Show(this,ex.Message,Text,MessageBoxButtons.OK,MessageBoxIcon.Error);}
  }
 }
}
