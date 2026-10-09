namespace AshkanFitPro {
 partial class ChangePasswordForm {
  private System.ComponentModel.IContainer components=null;
  private System.Windows.Forms.Label userLabel;
  private System.Windows.Forms.TextBox oldBox,newBox,confirmBox;
  private System.Windows.Forms.Button saveButton;
  protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
  private void InitializeComponent(){
   this.userLabel=new System.Windows.Forms.Label();this.oldBox=new System.Windows.Forms.TextBox();this.newBox=new System.Windows.Forms.TextBox();this.confirmBox=new System.Windows.Forms.TextBox();this.saveButton=new System.Windows.Forms.Button();
   this.SuspendLayout();
   this.Text="AshkanFit Pro | Change password";this.StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;this.ClientSize=new System.Drawing.Size(440,325);this.MinimumSize=new System.Drawing.Size(440,325);this.BackColor=System.Drawing.Color.FromArgb(15,25,42);this.ForeColor=System.Drawing.Color.White;this.Font=new System.Drawing.Font("Segoe UI",10F);
   this.userLabel.SetBounds(30,20,370,30);this.userLabel.ForeColor=System.Drawing.Color.FromArgb(70,220,188);
   this.oldBox.SetBounds(30,82,370,32);this.oldBox.UseSystemPasswordChar=true;
   this.newBox.SetBounds(30,150,370,32);this.newBox.UseSystemPasswordChar=true;
   this.confirmBox.SetBounds(30,218,370,32);this.confirmBox.UseSystemPasswordChar=true;
   this.saveButton.SetBounds(230,270,170,36);this.saveButton.Text="Save password";this.saveButton.BackColor=System.Drawing.Color.FromArgb(42,218,185);this.saveButton.FlatStyle=System.Windows.Forms.FlatStyle.Flat;this.saveButton.Click+=new System.EventHandler(this.SaveClick);
   var oldLabel=new System.Windows.Forms.Label(){Text="Current password",Location=new System.Drawing.Point(30,57),AutoSize=true};
   var newLabel=new System.Windows.Forms.Label(){Text="New password (10+ chars)",Location=new System.Drawing.Point(30,125),AutoSize=true};
   var confirmLabel=new System.Windows.Forms.Label(){Text="Confirm new password",Location=new System.Drawing.Point(30,193),AutoSize=true};
   this.Controls.AddRange(new System.Windows.Forms.Control[]{this.userLabel,oldLabel,this.oldBox,newLabel,this.newBox,confirmLabel,this.confirmBox,this.saveButton});this.AcceptButton=this.saveButton;this.ResumeLayout(false);
  }
 }
}
