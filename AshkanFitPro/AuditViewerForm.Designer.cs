namespace AshkanFitPro {
 partial class AuditViewerForm {
  private System.ComponentModel.IContainer components=null;
  private System.Windows.Forms.DataGridView grid;
  private System.Windows.Forms.Button btnRefresh;
  protected override void Dispose(bool disposing){if(disposing && components!=null)components.Dispose();base.Dispose(disposing);}
  private void InitializeComponent(){
   this.grid=new System.Windows.Forms.DataGridView();this.btnRefresh=new System.Windows.Forms.Button();
   ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();this.SuspendLayout();
   this.BackColor=System.Drawing.Color.FromArgb(15,23,42);this.ForeColor=System.Drawing.Color.White;
   this.ClientSize=new System.Drawing.Size(1050,620);this.MinimumSize=new System.Drawing.Size(760,420);
   this.StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;
   this.btnRefresh.Dock=System.Windows.Forms.DockStyle.Top;this.btnRefresh.Height=54;
   this.btnRefresh.BackColor=System.Drawing.Color.FromArgb(20,184,166);this.btnRefresh.FlatStyle=System.Windows.Forms.FlatStyle.Flat;
   this.grid.Dock=System.Windows.Forms.DockStyle.Fill;this.grid.ReadOnly=true;this.grid.AllowUserToAddRows=false;
   this.grid.AllowUserToDeleteRows=false;this.grid.AutoSizeColumnsMode=System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
   this.grid.BackgroundColor=System.Drawing.Color.FromArgb(30,41,59);this.grid.RowHeadersVisible=false;
   this.grid.Columns.Add("id","ID");this.grid.Columns.Add("date","UTC");this.grid.Columns.Add("actor","Actor");
   this.grid.Columns.Add("action","Action");this.grid.Columns.Add("details","Details");
   this.Controls.Add(this.grid);this.Controls.Add(this.btnRefresh);
   ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();this.ResumeLayout(false);
  }
 }
}
