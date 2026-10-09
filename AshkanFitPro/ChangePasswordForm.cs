using System;
using System.Windows.Forms;
namespace AshkanFitPro {
 public partial class ChangePasswordForm : Form {
  public ChangePasswordForm(){InitializeComponent();userLabel.Text="User: "+AccountService.CurrentUser;}
  private void SaveClick(object sender,EventArgs e){
   if(newBox.Text!=confirmBox.Text){MessageBox.Show(this,"New passwords do not match.");return;}
   try {new AccountService().ChangePassword(AccountService.CurrentUser,oldBox.Text,newBox.Text);MessageBox.Show(this,"Password updated.");DialogResult=DialogResult.OK;Close();}
   catch(Exception ex){MessageBox.Show(this,ex.Message,"AshkanFit Pro",MessageBoxButtons.OK,MessageBoxIcon.Warning);}
  }
 }
}
