namespace AshkanFitPro {
 partial class DataHealthForm {
  private System.ComponentModel.IContainer components=null;
  private System.Windows.Forms.TableLayoutPanel layout;
  private System.Windows.Forms.Label heading,summary;
  private System.Windows.Forms.DataGridView grid;
  private System.Windows.Forms.Button exportButton,closeButton;
  protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
  private void InitializeComponent(){
   layout=new System.Windows.Forms.TableLayoutPanel();heading=new System.Windows.Forms.Label();summary=new System.Windows.Forms.Label();grid=new System.Windows.Forms.DataGridView();exportButton=new System.Windows.Forms.Button();closeButton=new System.Windows.Forms.Button();
   SuspendLayout();BackColor=System.Drawing.Color.FromArgb(12,23,38);ForeColor=System.Drawing.Color.White;Font=new System.Drawing.Font("Segoe UI",10F);StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;ClientSize=new System.Drawing.Size(990,670);MinimumSize=new System.Drawing.Size(740,500);
   layout.Dock=System.Windows.Forms.DockStyle.Fill;layout.Padding=new System.Windows.Forms.Padding(24);layout.RowCount=4;layout.ColumnCount=2;
   layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,70));layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,45));layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent,100F));layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,60));
   layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,100F));layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute,145));
   heading.Dock=System.Windows.Forms.DockStyle.Fill;heading.Font=new System.Drawing.Font("Segoe UI",21F,System.Drawing.FontStyle.Bold);heading.ForeColor=System.Drawing.Color.FromArgb(42,220,186);layout.Controls.Add(heading,0,0);layout.SetColumnSpan(heading,2);
   summary.Dock=System.Windows.Forms.DockStyle.Fill;summary.ForeColor=System.Drawing.Color.FromArgb(185,208,222);layout.Controls.Add(summary,0,1);layout.SetColumnSpan(summary,2);
   grid.Dock=System.Windows.Forms.DockStyle.Fill;grid.ReadOnly=true;grid.AllowUserToAddRows=false;grid.AllowUserToDeleteRows=false;grid.AutoSizeColumnsMode=System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;grid.BackgroundColor=System.Drawing.Color.FromArgb(20,35,53);grid.BorderStyle=System.Windows.Forms.BorderStyle.None;grid.RowHeadersVisible=false;grid.SelectionMode=System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;layout.Controls.Add(grid,0,2);layout.SetColumnSpan(grid,2);
   exportButton.Dock=System.Windows.Forms.DockStyle.Right;exportButton.Width=160;exportButton.FlatStyle=System.Windows.Forms.FlatStyle.Flat;exportButton.BackColor=System.Drawing.Color.FromArgb(32,150,133);exportButton.ForeColor=System.Drawing.Color.White;layout.Controls.Add(exportButton,0,3);
   closeButton.Dock=System.Windows.Forms.DockStyle.Fill;closeButton.FlatStyle=System.Windows.Forms.FlatStyle.Flat;closeButton.BackColor=System.Drawing.Color.FromArgb(44,59,77);closeButton.ForeColor=System.Drawing.Color.White;layout.Controls.Add(closeButton,1,3);
   Controls.Add(layout);ResumeLayout(false);
  }
 }
}
