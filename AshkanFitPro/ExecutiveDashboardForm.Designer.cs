namespace AshkanFitPro {
partial class ExecutiveDashboardForm {
 private System.ComponentModel.IContainer components=null;
 private System.Windows.Forms.TableLayoutPanel layout;
 private System.Windows.Forms.Label titleLabel,chartTitle,noteLabel;
 private System.Windows.Forms.FlowLayoutPanel cards;
 private System.Windows.Forms.Panel chartPanel;
 private System.Windows.Forms.Button closeButton;
 protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
 private void InitializeComponent(){
 layout=new System.Windows.Forms.TableLayoutPanel();titleLabel=new System.Windows.Forms.Label();chartTitle=new System.Windows.Forms.Label();noteLabel=new System.Windows.Forms.Label();cards=new System.Windows.Forms.FlowLayoutPanel();chartPanel=new System.Windows.Forms.Panel();closeButton=new System.Windows.Forms.Button();
 SuspendLayout();BackColor=System.Drawing.Color.FromArgb(9,18,32);ForeColor=System.Drawing.Color.White;Font=new System.Drawing.Font("Segoe UI",10F);StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;MinimumSize=new System.Drawing.Size(850,650);ClientSize=new System.Drawing.Size(1120,780);
 layout.Dock=System.Windows.Forms.DockStyle.Fill;layout.Padding=new System.Windows.Forms.Padding(24);layout.ColumnCount=1;layout.RowCount=6;layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,65));layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,260));layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,43));layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent,100F));layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,40));layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,55));
 titleLabel.Dock=System.Windows.Forms.DockStyle.Fill;titleLabel.Font=new System.Drawing.Font("Segoe UI",22F,System.Drawing.FontStyle.Bold);titleLabel.ForeColor=System.Drawing.Color.FromArgb(42,220,186);layout.Controls.Add(titleLabel,0,0);
 cards.Dock=System.Windows.Forms.DockStyle.Fill;cards.AutoScroll=true;cards.WrapContents=true;cards.BackColor=System.Drawing.Color.FromArgb(14,27,44);layout.Controls.Add(cards,0,1);
 chartTitle.Dock=System.Windows.Forms.DockStyle.Fill;chartTitle.Font=new System.Drawing.Font("Segoe UI",11F,System.Drawing.FontStyle.Bold);chartTitle.ForeColor=System.Drawing.Color.White;layout.Controls.Add(chartTitle,0,2);
 chartPanel.Dock=System.Windows.Forms.DockStyle.Fill;chartPanel.BackColor=System.Drawing.Color.FromArgb(19,32,51);layout.Controls.Add(chartPanel,0,3);
 noteLabel.Dock=System.Windows.Forms.DockStyle.Fill;noteLabel.ForeColor=System.Drawing.Color.FromArgb(171,196,212);layout.Controls.Add(noteLabel,0,4);
 closeButton.Anchor=System.Windows.Forms.AnchorStyles.Right;closeButton.Size=new System.Drawing.Size(130,42);closeButton.BackColor=System.Drawing.Color.FromArgb(32,150,133);closeButton.FlatStyle=System.Windows.Forms.FlatStyle.Flat;closeButton.ForeColor=System.Drawing.Color.White;layout.Controls.Add(closeButton,0,5);
 Controls.Add(layout);ResumeLayout(false);
 }
}
}
