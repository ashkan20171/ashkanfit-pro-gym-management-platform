using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AshkanFitPro {
 public partial class LoginForm : Form {
  readonly AccountService accounts=new AccountService();
  readonly Color background=Color.FromArgb(9,16,30),panel=Color.FromArgb(20,32,51),accent=Color.FromArgb(42,218,185);
  TextBox username,password;Label heading,subtitle,message;Button submit,language;bool persian=true;bool setup;
  public LoginForm(){InitializeComponent();setup=accounts.NeedsSetup;Text="AshkanFit Pro | Secure Access";StartPosition=FormStartPosition.CenterScreen;ClientSize=new Size(1000,630);MinimumSize=new Size(880,560);BackColor=background;Font=new Font("Segoe UI",10);DoubleBuffered=true;Build();}
  Label L(string text,int size,Color color,bool bold=false){return new Label{Text=text,AutoSize=false,ForeColor=color,Font=new Font("Segoe UI",size,bold?FontStyle.Bold:FontStyle.Regular),TextAlign=ContentAlignment.MiddleLeft};}
  Button B(string text,Color color){var b=new Button{Text=text,BackColor=color,ForeColor=background,FlatStyle=FlatStyle.Flat,Font=new Font("Segoe UI",11,FontStyle.Bold),Cursor=Cursors.Hand};b.FlatAppearance.BorderSize=0;return b;}
  void Build(){
   designerContentHost.BackColor=background;
   loginCard.BackColor=panel;
   titleLabel.Text=setup?Tr("ایجاد حساب مدیر","Create admin account"):Tr("خوش آمدید","Welcome back");
   subtitleLabel.Text=setup?Tr("برای شروع، یک حساب امن بسازید.","Create your secure account to get started."):Tr("برای ورود اطلاعات خود را وارد کنید.","Sign in to your gym workspace.");
   usernameLabel.Text=Tr("نام کاربری","Username");passwordLabel.Text=Tr("رمز عبور","Password");
   showPassword.Text=Tr("نمایش رمز عبور","Show password");
   submitButton.Text=setup?Tr("ساخت حساب و ورود","Create account & continue"):Tr("ورود به داشبورد","Sign in to dashboard");
   languageButton.Text=persian?"ENGLISH":"فارسی";
   helpLabel.Text=setup?Tr("رمز عبور باید حداقل ۱۰ کاراکتر باشد.","Use a password with at least 10 characters."):(SqlStore.Enabled?Tr("حساب‌ها در SQL Server نگهداری می‌شوند.","Accounts are stored in SQL Server."):Tr("اطلاعات ورود فقط روی همین دستگاه نگهداری می‌شود.","Credentials are stored locally on this device."));
   usernameBox.RightToLeft=persian?RightToLeft.Yes:RightToLeft.No;
   titleLabel.TextAlign=persian?ContentAlignment.MiddleRight:ContentAlignment.MiddleLeft;
   subtitleLabel.TextAlign=titleLabel.TextAlign;
   usernameLabel.TextAlign=titleLabel.TextAlign;passwordLabel.TextAlign=titleLabel.TextAlign;
   username=usernameBox;password=passwordBox;message=helpLabel;submit=submitButton;language=languageButton;
   AcceptButton=submitButton;
  }
  void ChangeLanguage(object sender,EventArgs e){persian=!persian;Build();}
  void TogglePassword(object sender,EventArgs e){passwordBox.UseSystemPasswordChar=!showPassword.Checked;}
  string Tr(string fa,string en){return persian?fa:en;}
  void Submit(object sender,EventArgs e){try{if(setup){accounts.Create(username.Text,password.Text);setup=false;}else if(!accounts.Validate(username.Text,password.Text)){message.ForeColor=Color.FromArgb(255,145,143);message.Text=Tr("نام کاربری یا رمز عبور نادرست است.","Incorrect username or password.");password.Clear();return;}DialogResult=DialogResult.OK;Close();}catch(Exception ex){message.ForeColor=Color.FromArgb(255,145,143);message.Text=ex is ArgumentException?Tr("نام کاربری حداقل ۳ و رمز عبور حداقل ۱۰ کاراکتر باشد.","Username: 3+ characters; password: 10+ characters."):Tr("خطا در ورود یا ذخیره حساب: ","Account error: ")+ex.Message;}}
  void DrawHero(object sender,PaintEventArgs e){var g=e.Graphics;g.SmoothingMode=SmoothingMode.AntiAlias;var r=((Panel)sender).ClientRectangle;using(var brush=new LinearGradientBrush(r,Color.FromArgb(15,63,81),Color.FromArgb(11,25,46),45f))g.FillRectangle(brush,r);
   using(var p=new Pen(Color.FromArgb(55,72,124,135),2))for(int i=0;i<12;i++)g.DrawEllipse(p,r.Width/2-180-i*17,r.Height/2-180-i*17,360+i*34,360+i*34);
   using(var glow=new SolidBrush(Color.FromArgb(34,42,218,185)))g.FillEllipse(glow,r.Width-170,55,210,210);
   using(var line=new Pen(Color.FromArgb(100,42,218,185),4)){g.DrawArc(line,48,64,96,96,20,285);g.DrawLine(line,65,115,127,115);g.DrawLine(line,78,99,78,131);g.DrawLine(line,114,99,114,131);}
   using(var font=new Font("Segoe UI",32,FontStyle.Bold))using(var b=new SolidBrush(Color.White))g.DrawString("ASHKANFIT",font,b,36,r.Height/2-90);
   using(var font=new Font("Segoe UI",32,FontStyle.Bold))using(var b=new SolidBrush(accent))g.DrawString("PRO",font,b,36,r.Height/2-38);
   using(var font=new Font("Segoe UI",12))using(var b=new SolidBrush(Color.FromArgb(188,220,225)))g.DrawString("TRAIN SMART. MANAGE BETTER.",font,b,39,r.Height/2+34);
   using(var font=new Font("Segoe UI",10))using(var b=new SolidBrush(Color.FromArgb(148,189,201)))g.DrawString("MEMBERS  /  CLASSES  /  FINANCE  /  INSIGHTS",font,b,39,r.Height-65);
  }
 }
}
