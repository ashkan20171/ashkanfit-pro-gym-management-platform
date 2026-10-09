using System;using System.Windows.Forms;
namespace AshkanFitPro { static class Program { [STAThread] static void Main() { Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);while(true){using(var login=new LoginForm()){if(login.ShowDialog()!=DialogResult.OK)return;}using(var main=new MainForm()){Application.Run(main);if(!main.LoggedOut)return;}} } } }
