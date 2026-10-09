namespace AshkanFitPro
{
 partial class LoginForm
 {
  private System.ComponentModel.IContainer components = null;
  private System.Windows.Forms.Panel designerContentHost, heroPanel, loginCard;
  private System.Windows.Forms.TableLayoutPanel splitLayout, fieldsLayout;
  private System.Windows.Forms.Label titleLabel, subtitleLabel, usernameLabel, passwordLabel, helpLabel;
  private System.Windows.Forms.TextBox usernameBox, passwordBox;
  private System.Windows.Forms.Button submitButton, languageButton;
  private System.Windows.Forms.CheckBox showPassword;
  private void InitializeComponent()
  {
   this.designerContentHost=new System.Windows.Forms.Panel();
   this.splitLayout=new System.Windows.Forms.TableLayoutPanel();
   this.heroPanel=new System.Windows.Forms.Panel();
   this.loginCard=new System.Windows.Forms.Panel();
   this.fieldsLayout=new System.Windows.Forms.TableLayoutPanel();
   this.titleLabel=new System.Windows.Forms.Label();this.subtitleLabel=new System.Windows.Forms.Label();
   this.usernameLabel=new System.Windows.Forms.Label();this.passwordLabel=new System.Windows.Forms.Label();
   this.helpLabel=new System.Windows.Forms.Label();this.usernameBox=new System.Windows.Forms.TextBox();
   this.passwordBox=new System.Windows.Forms.TextBox();this.submitButton=new System.Windows.Forms.Button();
   this.languageButton=new System.Windows.Forms.Button();this.showPassword=new System.Windows.Forms.CheckBox();
   this.SuspendLayout();this.designerContentHost.SuspendLayout();this.splitLayout.SuspendLayout();
   this.loginCard.SuspendLayout();this.fieldsLayout.SuspendLayout();
   this.designerContentHost.Dock=System.Windows.Forms.DockStyle.Fill;
   this.designerContentHost.Name="designerContentHost";
   this.designerContentHost.Padding=new System.Windows.Forms.Padding(18);
   this.designerContentHost.BackColor=System.Drawing.Color.FromArgb(9,16,30);
   this.splitLayout.Dock=System.Windows.Forms.DockStyle.Fill;this.splitLayout.ColumnCount=2;
   this.splitLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,51F));
   this.splitLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,49F));
   this.heroPanel.Dock=System.Windows.Forms.DockStyle.Fill;this.heroPanel.Margin=new System.Windows.Forms.Padding(0,0,10,0);
   this.heroPanel.Paint+=new System.Windows.Forms.PaintEventHandler(this.DrawHero);
   this.loginCard.Dock=System.Windows.Forms.DockStyle.Fill;this.loginCard.Padding=new System.Windows.Forms.Padding(32);
   this.loginCard.BackColor=System.Drawing.Color.FromArgb(20,32,51);
   this.fieldsLayout.Dock=System.Windows.Forms.DockStyle.Fill;this.fieldsLayout.ColumnCount=1;this.fieldsLayout.RowCount=10;
   int[] heights={44,72,60,28,48,28,48,38,58,55};
   foreach(int h in heights)this.fieldsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,h));
   this.languageButton.Name="languageButton";this.languageButton.Text="ENGLISH";this.languageButton.Dock=System.Windows.Forms.DockStyle.Right;
   this.languageButton.Width=112;this.languageButton.FlatStyle=System.Windows.Forms.FlatStyle.Flat;
   this.languageButton.BackColor=System.Drawing.Color.FromArgb(42,58,78);this.languageButton.ForeColor=System.Drawing.Color.White;
   this.languageButton.Click+=new System.EventHandler(this.ChangeLanguage);
   this.titleLabel.Name="titleLabel";this.titleLabel.Dock=System.Windows.Forms.DockStyle.Fill;
   this.titleLabel.Font=new System.Drawing.Font("Segoe UI",23F,System.Drawing.FontStyle.Bold);
   this.titleLabel.ForeColor=System.Drawing.Color.White;
   this.subtitleLabel.Name="subtitleLabel";this.subtitleLabel.Dock=System.Windows.Forms.DockStyle.Fill;
   this.subtitleLabel.ForeColor=System.Drawing.Color.FromArgb(168,190,207);
   this.subtitleLabel.Font=new System.Drawing.Font("Segoe UI",10F);
   this.usernameLabel.Name="usernameLabel";this.usernameLabel.Dock=System.Windows.Forms.DockStyle.Fill;
   this.passwordLabel.Name="passwordLabel";this.passwordLabel.Dock=System.Windows.Forms.DockStyle.Fill;
   foreach(System.Windows.Forms.Label label in new[]{this.usernameLabel,this.passwordLabel}){
    label.ForeColor=System.Drawing.Color.White;label.Font=new System.Drawing.Font("Segoe UI",10F,System.Drawing.FontStyle.Bold);
   }
   this.usernameBox.Name="usernameBox";this.usernameBox.Dock=System.Windows.Forms.DockStyle.Fill;
   this.passwordBox.Name="passwordBox";this.passwordBox.Dock=System.Windows.Forms.DockStyle.Fill;
   foreach(System.Windows.Forms.TextBox box in new[]{this.usernameBox,this.passwordBox}){
    box.Font=new System.Drawing.Font("Segoe UI",13F);box.BackColor=System.Drawing.Color.FromArgb(33,49,69);
    box.ForeColor=System.Drawing.Color.White;box.BorderStyle=System.Windows.Forms.BorderStyle.FixedSingle;
    box.Margin=new System.Windows.Forms.Padding(0,2,0,10);
   }
   this.passwordBox.UseSystemPasswordChar=true;
   this.showPassword.Name="showPassword";this.showPassword.AutoSize=true;
   this.showPassword.ForeColor=System.Drawing.Color.FromArgb(168,190,207);
   this.showPassword.CheckedChanged+=new System.EventHandler(this.TogglePassword);
   this.submitButton.Name="submitButton";this.submitButton.Dock=System.Windows.Forms.DockStyle.Fill;
   this.submitButton.BackColor=System.Drawing.Color.FromArgb(42,218,185);
   this.submitButton.ForeColor=System.Drawing.Color.FromArgb(9,16,30);
   this.submitButton.FlatStyle=System.Windows.Forms.FlatStyle.Flat;
   this.submitButton.Font=new System.Drawing.Font("Segoe UI",11F,System.Drawing.FontStyle.Bold);
   this.submitButton.Click+=new System.EventHandler(this.Submit);
   this.helpLabel.Name="helpLabel";this.helpLabel.Dock=System.Windows.Forms.DockStyle.Fill;
   this.helpLabel.AutoEllipsis=true;this.helpLabel.ForeColor=System.Drawing.Color.FromArgb(168,190,207);
   this.helpLabel.Font=new System.Drawing.Font("Segoe UI",9F);
   this.fieldsLayout.Controls.Add(this.languageButton,0,0);
   this.fieldsLayout.Controls.Add(this.titleLabel,0,1);this.fieldsLayout.Controls.Add(this.subtitleLabel,0,2);
   this.fieldsLayout.Controls.Add(this.usernameLabel,0,3);this.fieldsLayout.Controls.Add(this.usernameBox,0,4);
   this.fieldsLayout.Controls.Add(this.passwordLabel,0,5);this.fieldsLayout.Controls.Add(this.passwordBox,0,6);
   this.fieldsLayout.Controls.Add(this.showPassword,0,7);this.fieldsLayout.Controls.Add(this.submitButton,0,8);
   this.fieldsLayout.Controls.Add(this.helpLabel,0,9);
   this.loginCard.Controls.Add(this.fieldsLayout);
   this.splitLayout.Controls.Add(this.heroPanel,0,0);this.splitLayout.Controls.Add(this.loginCard,1,0);
   this.designerContentHost.Controls.Add(this.splitLayout);
   this.ClientSize=new System.Drawing.Size(1000,630);this.MinimumSize=new System.Drawing.Size(880,560);
   this.Controls.Add(this.designerContentHost);this.Name="LoginForm";
   this.Text="AshkanFit Pro | Secure Access";this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
   this.fieldsLayout.ResumeLayout(false);this.fieldsLayout.PerformLayout();this.loginCard.ResumeLayout(false);
   this.splitLayout.ResumeLayout(false);this.designerContentHost.ResumeLayout(false);this.ResumeLayout(false);
  }
  protected override void Dispose(bool disposing){if(disposing && components!=null)components.Dispose();base.Dispose(disposing);}
 }
}
