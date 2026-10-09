using System.Drawing;
using System.Windows.Forms;
namespace AshkanFitPro {partial class ProgressAnalyticsForm {
 private System.ComponentModel.IContainer components=null;
 private ComboBox memberBox;private Label memberLabel;private Label summaryLabel;private Button exportButton;private Panel chartPanel;private DataGridView recordsGrid;
 protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
 private void InitializeComponent(){
  memberBox=new ComboBox();memberLabel=new Label();summaryLabel=new Label();exportButton=new Button();chartPanel=new Panel();recordsGrid=new DataGridView();
  SuspendLayout();BackColor=Color.FromArgb(13,24,38);ForeColor=Color.White;Font=new Font("Segoe UI",10F);MinimumSize=new Size(740,650);Size=new Size(1000,760);StartPosition=FormStartPosition.CenterParent;
  memberLabel.Text="Member";memberLabel.Location=new Point(24,24);memberLabel.Size=new Size(100,28);
  memberBox.DropDownStyle=ComboBoxStyle.DropDownList;memberBox.Location=new Point(135,22);memberBox.Size=new Size(290,28);memberBox.Anchor=AnchorStyles.Top|AnchorStyles.Left;
  exportButton.Text="Export CSV";exportButton.Location=new Point(790,18);exportButton.Size=new Size(165,36);exportButton.Anchor=AnchorStyles.Top|AnchorStyles.Right;exportButton.FlatStyle=FlatStyle.Flat;exportButton.BackColor=Color.FromArgb(21,155,137);exportButton.ForeColor=Color.White;
  summaryLabel.Location=new Point(24,69);summaryLabel.Size=new Size(920,40);summaryLabel.Anchor=AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right;
  chartPanel.Location=new Point(24,118);chartPanel.Size=new Size(930,285);chartPanel.Anchor=AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right;chartPanel.BackColor=Color.FromArgb(19,33,49);
  recordsGrid.Location=new Point(24,421);recordsGrid.Size=new Size(930,275);recordsGrid.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;recordsGrid.ReadOnly=true;recordsGrid.AllowUserToAddRows=false;recordsGrid.AllowUserToDeleteRows=false;recordsGrid.RowHeadersVisible=false;recordsGrid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;recordsGrid.BackgroundColor=Color.FromArgb(19,33,49);recordsGrid.BorderStyle=BorderStyle.None;
  recordsGrid.Columns.Add("Date","Date");recordsGrid.Columns.Add("Weight","Weight kg");recordsGrid.Columns.Add("BMI","BMI");recordsGrid.Columns.Add("Waist","Waist cm");recordsGrid.Columns.Add("Fat","Body fat %");recordsGrid.Columns.Add("Goal","Goal");
  Controls.Add(memberLabel);Controls.Add(memberBox);Controls.Add(exportButton);Controls.Add(summaryLabel);Controls.Add(chartPanel);Controls.Add(recordsGrid);ResumeLayout(false);
 }
}}
