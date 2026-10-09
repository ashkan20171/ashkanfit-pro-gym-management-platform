namespace AshkanFitPro { partial class ProgressDialog {
private System.ComponentModel.IContainer components = null;
private System.Windows.Forms.ComboBox memberBox;
private System.Windows.Forms.NumericUpDown weightBox,heightBox,waistBox,fatBox;
private System.Windows.Forms.DateTimePicker dateBox;
private System.Windows.Forms.TextBox goalBox;
private System.Windows.Forms.Button saveButton,closeButton;
private System.Windows.Forms.Label titleLabel;
protected override void Dispose(bool disposing){if(disposing && components!=null)components.Dispose();base.Dispose(disposing);}
private void InitializeComponent(){
this.components=new System.ComponentModel.Container();
this.memberBox=new System.Windows.Forms.ComboBox();this.weightBox=new System.Windows.Forms.NumericUpDown();this.heightBox=new System.Windows.Forms.NumericUpDown();this.waistBox=new System.Windows.Forms.NumericUpDown();this.fatBox=new System.Windows.Forms.NumericUpDown();this.dateBox=new System.Windows.Forms.DateTimePicker();this.goalBox=new System.Windows.Forms.TextBox();this.saveButton=new System.Windows.Forms.Button();this.closeButton=new System.Windows.Forms.Button();this.titleLabel=new System.Windows.Forms.Label();
((System.ComponentModel.ISupportInitialize)this.weightBox).BeginInit();((System.ComponentModel.ISupportInitialize)this.heightBox).BeginInit();((System.ComponentModel.ISupportInitialize)this.waistBox).BeginInit();((System.ComponentModel.ISupportInitialize)this.fatBox).BeginInit();
this.SuspendLayout();this.BackColor=System.Drawing.Color.FromArgb(13,25,42);this.ForeColor=System.Drawing.Color.White;this.ClientSize=new System.Drawing.Size(560,540);this.MinimumSize=new System.Drawing.Size(560,540);this.Font=new System.Drawing.Font("Segoe UI",10F);this.StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;this.Text="AshkanFit Pro | Body Progress";
this.titleLabel.Text="BODY PROGRESS  /  پیشرفت ورزشی";this.titleLabel.ForeColor=System.Drawing.Color.Turquoise;this.titleLabel.Font=new System.Drawing.Font("Segoe UI",16F,System.Drawing.FontStyle.Bold);this.titleLabel.Location=new System.Drawing.Point(28,18);this.titleLabel.Size=new System.Drawing.Size(500,44);this.Controls.Add(this.titleLabel);
System.Windows.Forms.Control[] controls={this.memberBox,this.dateBox,this.weightBox,this.heightBox,this.waistBox,this.fatBox,this.goalBox};string[] labels={"Member / عضو","Date / تاریخ","Weight (kg) / وزن","Height (cm) / قد","Waist (cm) / دور کمر","Body fat % / چربی بدن","Goal / هدف"};
for(int i=0;i<controls.Length;i++){var label=new System.Windows.Forms.Label();label.Text=labels[i];label.Location=new System.Drawing.Point(28,76+i*55);label.Size=new System.Drawing.Size(195,29);this.Controls.Add(label);controls[i].Location=new System.Drawing.Point(235,73+i*55);controls[i].Size=new System.Drawing.Size(286,30);controls[i].Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right;this.Controls.Add(controls[i]);}
this.memberBox.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;this.dateBox.Format=System.Windows.Forms.DateTimePickerFormat.Short;
System.Windows.Forms.NumericUpDown[] nums={this.weightBox,this.heightBox,this.waistBox,this.fatBox};foreach(var num in nums){num.DecimalPlaces=1;num.Increment=0.5m;num.Maximum=3000;num.Minimum=0;num.BackColor=System.Drawing.Color.White;}
this.weightBox.Value=75;this.heightBox.Value=175;this.fatBox.Maximum=100;this.waistBox.Value=80;
this.saveButton.Text="Save / ذخیره";this.saveButton.Location=new System.Drawing.Point(235,470);this.saveButton.Size=new System.Drawing.Size(135,42);this.saveButton.BackColor=System.Drawing.Color.FromArgb(20,184,166);this.saveButton.FlatStyle=System.Windows.Forms.FlatStyle.Flat;this.saveButton.Click+=new System.EventHandler(this.SaveClicked);this.Controls.Add(this.saveButton);
this.closeButton.Text="Close / بستن";this.closeButton.Location=new System.Drawing.Point(385,470);this.closeButton.Size=new System.Drawing.Size(135,42);this.closeButton.DialogResult=System.Windows.Forms.DialogResult.Cancel;this.closeButton.FlatStyle=System.Windows.Forms.FlatStyle.Flat;this.Controls.Add(this.closeButton);this.CancelButton=this.closeButton;
((System.ComponentModel.ISupportInitialize)this.weightBox).EndInit();((System.ComponentModel.ISupportInitialize)this.heightBox).EndInit();((System.ComponentModel.ISupportInitialize)this.waistBox).EndInit();((System.ComponentModel.ISupportInitialize)this.fatBox).EndInit();this.ResumeLayout(false);this.PerformLayout();}
} }
