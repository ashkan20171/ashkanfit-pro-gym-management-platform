namespace AshkanFitPro {
partial class FinanceTrendsForm {
 private System.ComponentModel.IContainer components=null;
 private System.Windows.Forms.TableLayoutPanel layout;
 private System.Windows.Forms.FlowLayoutPanel summary;
 private System.Windows.Forms.Label titleLabel,incomeLabel,expenseLabel,netLabel;
 private System.Windows.Forms.Panel chartPanel;
 private System.Windows.Forms.FlowLayoutPanel footer;
 private System.Windows.Forms.Button exportButton,closeButton;
 protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
 private void InitializeComponent(){
 this.layout=new System.Windows.Forms.TableLayoutPanel();this.summary=new System.Windows.Forms.FlowLayoutPanel();this.titleLabel=new System.Windows.Forms.Label();this.incomeLabel=new System.Windows.Forms.Label();this.expenseLabel=new System.Windows.Forms.Label();this.netLabel=new System.Windows.Forms.Label();this.chartPanel=new System.Windows.Forms.Panel();this.footer=new System.Windows.Forms.FlowLayoutPanel();this.exportButton=new System.Windows.Forms.Button();this.closeButton=new System.Windows.Forms.Button();
 this.SuspendLayout();this.BackColor=System.Drawing.Color.FromArgb(9,18,32);this.ForeColor=System.Drawing.Color.White;this.Font=new System.Drawing.Font("Segoe UI",10F);this.MinimumSize=new System.Drawing.Size(800,540);this.ClientSize=new System.Drawing.Size(1050,680);this.StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;
 this.layout.Dock=System.Windows.Forms.DockStyle.Fill;this.layout.Padding=new System.Windows.Forms.Padding(24);this.layout.ColumnCount=1;this.layout.RowCount=4;this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,65));this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,75));this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent,100F));this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,75));
 this.titleLabel.Dock=System.Windows.Forms.DockStyle.Fill;this.titleLabel.Font=new System.Drawing.Font("Segoe UI",19F,System.Drawing.FontStyle.Bold);this.titleLabel.ForeColor=System.Drawing.Color.FromArgb(42,220,186);this.layout.Controls.Add(this.titleLabel,0,0);
 this.summary.Dock=System.Windows.Forms.DockStyle.Fill;this.summary.WrapContents=false;this.summary.AutoScroll=true;this.incomeLabel.AutoSize=true;this.expenseLabel.AutoSize=true;this.netLabel.AutoSize=true;this.incomeLabel.ForeColor=System.Drawing.Color.FromArgb(42,220,186);this.expenseLabel.ForeColor=System.Drawing.Color.FromArgb(255,123,145);this.netLabel.ForeColor=System.Drawing.Color.White;this.incomeLabel.Margin=new System.Windows.Forms.Padding(8,12,30,4);this.expenseLabel.Margin=new System.Windows.Forms.Padding(8,12,30,4);this.netLabel.Margin=new System.Windows.Forms.Padding(8,12,8,4);this.summary.Controls.Add(this.incomeLabel);this.summary.Controls.Add(this.expenseLabel);this.summary.Controls.Add(this.netLabel);this.layout.Controls.Add(this.summary,0,1);
 this.chartPanel.Dock=System.Windows.Forms.DockStyle.Fill;this.chartPanel.BackColor=System.Drawing.Color.FromArgb(19,32,51);this.layout.Controls.Add(this.chartPanel,0,2);
 this.footer.Dock=System.Windows.Forms.DockStyle.Fill;this.footer.FlowDirection=System.Windows.Forms.FlowDirection.RightToLeft;this.exportButton.Size=new System.Drawing.Size(155,45);this.closeButton.Size=new System.Drawing.Size(120,45);this.exportButton.FlatStyle=System.Windows.Forms.FlatStyle.Flat;this.closeButton.FlatStyle=System.Windows.Forms.FlatStyle.Flat;this.exportButton.BackColor=System.Drawing.Color.FromArgb(42,160,143);this.closeButton.BackColor=System.Drawing.Color.FromArgb(41,56,75);this.exportButton.ForeColor=System.Drawing.Color.White;this.closeButton.ForeColor=System.Drawing.Color.White;this.footer.Controls.Add(this.closeButton);this.footer.Controls.Add(this.exportButton);this.layout.Controls.Add(this.footer,0,3);
 this.Controls.Add(this.layout);this.ResumeLayout(false);
 }
}
}
