namespace AshkanFitPro {
 partial class HistoryBrowserForm {
  private System.ComponentModel.IContainer components=null;
  private System.Windows.Forms.DataGridView grid;
  private System.Windows.Forms.Button btnRefresh;
  private System.Windows.Forms.Button btnExport;
  private System.Windows.Forms.Button btnRestore;
  protected override void Dispose(bool disposing) {if(disposing && components!=null)components.Dispose();base.Dispose(disposing);}
  private void InitializeComponent() {
   this.grid=new System.Windows.Forms.DataGridView();
   this.btnRefresh=new System.Windows.Forms.Button();
   this.btnExport=new System.Windows.Forms.Button();
   this.btnRestore=new System.Windows.Forms.Button();
   ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
   this.SuspendLayout();
   this.grid.AllowUserToAddRows=false;
   this.grid.AllowUserToDeleteRows=false;
   this.grid.AutoSizeColumnsMode=System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
   this.grid.BackgroundColor=System.Drawing.Color.FromArgb(22,36,54);
   this.grid.Dock=System.Windows.Forms.DockStyle.Fill;
   this.grid.MultiSelect=false;
   this.grid.ReadOnly=true;
   this.grid.RowHeadersVisible=false;
   this.grid.SelectionMode=System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
   this.grid.Name="grid";
   this.btnRefresh.Dock=System.Windows.Forms.DockStyle.Left;
   this.btnRefresh.Width=170;
   this.btnRefresh.Name="btnRefresh";
   this.btnRestore.Dock=System.Windows.Forms.DockStyle.Right;
   this.btnRestore.Width=190;
   this.btnRestore.Name="btnRestore";
   this.btnExport.Dock=System.Windows.Forms.DockStyle.Right;
   this.btnExport.Width=240;
   this.btnExport.Name="btnExport";
   var footer=new System.Windows.Forms.Panel();
   footer.Dock=System.Windows.Forms.DockStyle.Bottom;
   footer.Height=54;
   footer.Padding=new System.Windows.Forms.Padding(10);
   footer.Controls.Add(this.btnRefresh);
   footer.Controls.Add(this.btnExport);
   footer.Controls.Add(this.btnRestore);
   this.ClientSize=new System.Drawing.Size(920,560);
   this.MinimumSize=new System.Drawing.Size(650,380);
   this.StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;
   this.Controls.Add(this.grid);
   this.Controls.Add(footer);
   ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
   this.ResumeLayout(false);
  }
 }
}
