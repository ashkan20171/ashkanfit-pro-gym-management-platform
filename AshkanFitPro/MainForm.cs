using System;using System.Drawing;using System.Linq;using System.Windows.Forms;using System.IO;using System.Text;using System.Globalization;using System.Drawing.Drawing2D;
namespace AshkanFitPro { public partial class MainForm:Form {
public bool LoggedOut {get;private set;} readonly Repository repo=new Repository();bool fa=true;string page="dashboard";Panel sidebar,body,header;TextBox search;ComboBox memberStatusFilter;DataGridView grid;readonly Color bg=Color.FromArgb(8,15,28),surface=Color.FromArgb(19,32,51),muted=Color.FromArgb(166,190,209),accent=Color.FromArgb(34,218,186);Font regular=new Font("Segoe UI",10);
public MainForm(){InitializeComponent();if(AccountService.CurrentRole=="Trainer")page="coach";Text="AshkanFit Pro | Gym Management";MinimumSize=new Size(1050,700);WindowState=FormWindowState.Maximized;BackColor=bg;Font=regular;KeyPreview=true;KeyDown+=MainFormKeyboardShortcuts;Build();Shown+=(sender,args)=>ShowMembershipAlerts();}
string T(string p,string e){return fa?p:e;}
// Persian users see Jalali dates; English users see Gregorian dates.
string DateText(DateTime value){
 if(!fa)return value.ToString("yyyy/MM/dd",CultureInfo.InvariantCulture);
 var pc=new PersianCalendar();
 return string.Format(CultureInfo.InvariantCulture,"{0:0000}/{1:00}/{2:00}",pc.GetYear(value),pc.GetMonth(value),pc.GetDayOfMonth(value));
}

// Keyboard navigation stays in the form logic rather than generated Designer code.
void MainFormKeyboardShortcuts(object sender,KeyEventArgs e){
 if(e.KeyCode==Keys.F5){Render();e.Handled=true;}
 else if(e.Control && e.KeyCode==Keys.D1){if(CanAccess("dashboard")){page="dashboard";Build();}e.SuppressKeyPress=true;}
 else if(e.Control && e.KeyCode==Keys.D2){if(CanAccess("members")){page="members";Build();}e.SuppressKeyPress=true;}
 else if(e.Control && e.KeyCode==Keys.L){AccountService.SignOut();LoggedOut=true;Close();e.SuppressKeyPress=true;}
}

Label Label(string s,int size=11,Color? color=null){return new Label{Text=s,AutoSize=true,ForeColor=color??Color.White,Font=new Font("Segoe UI",size,size>=20?FontStyle.Bold:FontStyle.Regular)};}
Button Button(string text,EventHandler click,Color? color=null){var b=new Button{Text=text,Height=46,Dock=DockStyle.Top,Margin=new Padding(0,0,0,10),FlatStyle=FlatStyle.Flat,BackColor=color??surface,ForeColor=Color.White,Font=new Font("Segoe UI",10,FontStyle.Bold),Cursor=Cursors.Hand};b.FlatAppearance.BorderSize=0;b.FlatAppearance.MouseOverBackColor=Color.FromArgb(35,87,103);b.Click+=click;return b;}
void Build(){
designerMainHost.Controls.Clear();
designerSidebarHost.Controls.Clear();
designerSidebarHost.Dock=fa?DockStyle.Right:DockStyle.Left;
designerSidebarHost.Width=Math.Max(210,Math.Min(270,ClientSize.Width/5));
designerMainHost.BringToFront();
designerSidebarHost.BringToFront();
sidebar=new Panel{Dock=DockStyle.Fill,BackColor=Color.FromArgb(13,25,42),Padding=new Padding(16,24,16,16),AutoScroll=true};sidebar.Paint+=(sender,args)=>{using(var pen=new Pen(Color.FromArgb(37,65,83)))args.Graphics.DrawLine(pen,fa?0:sidebar.Width-1,0,fa?0:sidebar.Width-1,sidebar.Height);};var sideStack=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,WrapContents=false,AutoScroll=true,BackColor=Color.FromArgb(13,25,42),Padding=new Padding(0,0,4,8)};sidebar.Controls.Add(sideStack);sideStack.Resize+=(s,e)=>{foreach(Control c in sideStack.Controls)c.Width=Math.Max(170,sideStack.ClientSize.Width-24);};var brand=Label("◆  ASHKANFIT PRO",17,accent);brand.Height=68;sideStack.Controls.Add(brand);sideStack.RightToLeft=fa?RightToLeft.Yes:RightToLeft.No;
if(AccountService.CurrentRole!="Trainer")Nav(sideStack,"dashboard",T("داشبورد","Dashboard"));Nav(sideStack,"members",T("مدیریت اعضا","Members"));if(AccountService.CurrentRole!="Trainer")Nav(sideStack,"payments",T("پرداخت‌ها","Payments"));Nav(sideStack,"expiring",T("اشتراک‌های رو به پایان","Expiring plans"));if(AccountService.IsAdmin)Nav(sideStack,"health51",T("سلامت داده‌ها","Data quality"));if(AccountService.IsAdmin)Nav(sideStack,"executive50",T("داشبورد مدیریتی ویژه","Executive dashboard"));if(AccountService.IsAdmin)Nav(sideStack,"alerts49",T("مرکز هشدار تمدید","Renewal alerts"));if(AccountService.IsAdmin)Nav(sideStack,"reports",T("گزارش‌ها","Reports"));if(AccountService.IsAdmin)Nav(sideStack,"insights",T("تحلیل درآمد","Revenue insights"));if(AccountService.IsAdmin)Nav(sideStack,"financial47",T("گزارش مالی پیشرفته","Financial reporting"));if(AccountService.IsAdmin)Nav(sideStack,"trends48",T("روند مالی ۱۲ ماهه","12-month finance trends"));Nav(sideStack,"attendance",T("حضور و غیاب","Attendance"));Nav(sideStack,"trainers",T("مربیان","Trainers"));Nav(sideStack,"coach",T("داشبورد مربیان","Coach dashboard"));Nav(sideStack,"workouts",T("برنامه تمرینی","Workout plans"));Nav(sideStack,"progress",T("پیشرفت ورزشی","Body progress"));Nav(sideStack,"classes",T("کلاس‌های گروهی","Group classes"));if(AccountService.IsAdmin)Nav(sideStack,"finance",T("شهریه و تمدید","Billing & renewals"));if(AccountService.IsAdmin)Nav(sideStack,"expenses",T("هزینه‌های باشگاه","Club expenses"));if(AccountService.IsAdmin)Nav(sideStack,"invoices",T("صورتحساب اعضا","Member invoices"));if(AccountService.IsAdmin)Nav(sideStack,"backup",T("پشتیبان‌گیری","Backup & restore"));if(AccountService.IsAdmin)Nav(sideStack,"audit",T("گزارش فعالیت‌ها","Activity log"));if(AccountService.IsAdmin){var usersButton=Button(T("مدیریت کاربران","User management"),(s,e)=>{using(var dialog=new UsersForm())dialog.ShowDialog(this);});sideStack.Controls.Add(usersButton);}
 var passwordButton=Button(T("تغییر رمز عبور","Change password"),(s,e)=>{using(var dialog=new ChangePasswordForm())dialog.ShowDialog(this);});sideStack.Controls.Add(passwordButton);
 var switcher=Button(fa?"English  →":"←  فارسی",(s,e)=>{fa=!fa;Build();});switcher.Margin=new Padding(0,30,0,0);sideStack.Controls.Add(switcher);var logout=Button(T("خروج از حساب","Sign out"),(s,e)=>{AccountService.SignOut();LoggedOut=true;Close();});sideStack.Controls.Add(logout);sideStack.Controls.Add(Label(T("F5: تازه‌سازی  |  Ctrl+1: داشبورد","F5: Refresh  |  Ctrl+1: Dashboard"),9,muted));sideStack.Controls.Add(Label("AshkanFit Pro  •  .NET Framework 4.8",9,muted));
var main=new Panel{Dock=DockStyle.Fill,Padding=new Padding(24,18,24,18),BackColor=bg};header=new Panel{Dock=DockStyle.Top,Height=84};main.Controls.Add(header);body=new Panel{Dock=DockStyle.Fill,AutoScroll=true};main.Controls.Add(body);body.BringToFront();header.BringToFront();designerMainHost.Controls.Add(main);designerSidebarHost.Controls.Add(sidebar);Render();}
bool CanAccess(string key){
 if(AccountService.IsAdmin)return true;
 if(AccountService.CurrentRole=="Trainer")return key=="coach"; // Other pages are not row-scoped; deny access until secured.
 if(AccountService.CurrentRole=="Reception")return key=="dashboard"||key=="coach"||key=="members"||key=="payments"||key=="expiring"||key=="attendance"||key=="trainers"||key=="workouts"||key=="progress"||key=="classes";
 return false;
}
void Nav(FlowLayoutPanel stack,string key,string label){if(!CanAccess(key))return;var b=Button((page==key?"  ●  ":"      ")+label,(s,e)=>{page=key;Build();},page==key?Color.FromArgb(24,91,101):surface);b.TextAlign=fa?ContentAlignment.MiddleRight:ContentAlignment.MiddleLeft;stack.Controls.Add(b);}
void Render(){designerBrandLabel.Text=fa?"ASHKANFIT PRO  /  مدیریت باشگاه":"ASHKANFIT PRO  /  GYM MANAGEMENT";designerBrandLabel.TextAlign=fa?ContentAlignment.MiddleRight:ContentAlignment.MiddleLeft;if(!CanAccess(page)){page=AccountService.CurrentRole=="Trainer"?"coach":"dashboard";}body.Controls.Clear();header.Controls.Clear();string title=page=="dashboard"?T("نمای کلی باشگاه","Club overview"):page=="members"?T("مدیریت اعضا","Member management"):page=="payments"?T("مدیریت پرداخت‌ها","Payments"):page=="expiring"?T("اشتراک‌های در حال انقضا","Expiring memberships"):page=="attendance"?T("حضور و غیاب","Attendance"):page=="trainers"?T("مدیریت مربیان","Trainer management"):page=="coach"?T("داشبورد مربیان","Coach dashboard"):page=="workouts"?T("برنامه‌های تمرینی","Workout plans"):page=="progress"?T("پیشرفت ورزشی اعضا","Member body progress"):page=="classes"?T("کلاس‌های گروهی","Group classes"):page=="finance"?T("شهریه و تمدید اشتراک","Billing and renewals"):page=="expenses"?T("هزینه‌های باشگاه","Club expenses"):page=="insights"?T("تحلیل درآمد و هزینه","Revenue and expense insights"):page=="financial47"?T("گزارش مالی پیشرفته","Financial reporting"):page=="trends48"?T("روند درآمد و هزینه","Revenue & expense trends"):page=="alerts49"?T("مرکز هشدار تمدید","Membership renewal alerts"):page=="health51"?T("سلامت داده‌ها","Data quality"):page=="executive50"?T("داشبورد مدیریتی ویژه","Executive dashboard"):page=="invoices"?T("صورتحساب و چاپ","Invoices & printing"):page=="backup"?T("پشتیبان‌گیری و بازیابی","Backup and restore"):page=="audit"?T("تاریخچه فعالیت‌ها","Activity history"):T("گزارش‌های مدیریتی","Management reports");var heading=Label(title,24);heading.Location=new Point(0,4);header.Controls.Add(heading);var date=Label(DateText(DateTime.Now),10,muted);date.Location=new Point(3,49);header.Controls.Add(date);var who=Label((AccountService.CurrentUser??"Guest")+"  ·  "+(AccountService.CurrentRole??""),10,accent);who.AutoSize=true;who.Anchor=AnchorStyles.Top|AnchorStyles.Right;who.Location=new Point(Math.Max(0,header.Width-240),14);header.Controls.Add(who);header.Resize+=(sender,args)=>who.Left=Math.Max(0,header.Width-who.Width-8);if(page=="dashboard")Dashboard();else if(page=="members")Members(false);else if(page=="expiring")Members(true);else if(page=="payments")Payments();else if(page=="attendance")AttendancePage();else if(page=="trainers")TrainersPage();else if(page=="coach")CoachDashboard();else if(page=="workouts")WorkoutsPage();else if(page=="progress")ProgressPage();else if(page=="classes")ClassesPage();else if(page=="finance")FinancePage();else if(page=="expenses")ExpensesPage();else if(page=="invoices")InvoicesPage();else if(page=="backup")BackupPage();else if(page=="insights")InsightsPage();else if(page=="trends48")FinanceTrendsPage();else if(page=="alerts49")MembershipAlertsPage();else if(page=="health51")DataHealthPage();else if(page=="executive50")ExecutiveDashboardPage();else if(page=="financial47")FinancialReportingPage();else if(page=="audit")AuditPage();else Reports();}
Panel Panel(){return new PremiumCard{BackColor=surface,Margin=new Padding(0,0,16,14),Padding=new Padding(20),Radius=18};}
void Dashboard(){var layout=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=4,AutoScroll=true};layout.RowStyles.Add(new RowStyle(SizeType.Absolute,128));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,150));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,78));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));body.Controls.Add(layout);layout.MinimumSize=new Size(640,470);var hero=new PremiumHeroPanel{Dock=DockStyle.Fill,Margin=new Padding(0,0,16,14),RightToLeft=fa?RightToLeft.Yes:RightToLeft.No};hero.TitleText=T("قدرت مدیریت، انرژی پیشرفت","POWER YOUR GYM");hero.SubtitleText=T("اعضا، برنامه‌ها و امور مالی؛ همه در یک فضای حرفه‌ای","Members, training and finances — beautifully connected");layout.Controls.Add(hero,0,0);var stats=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=4};for(int i=0;i<4;i++)stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,25));layout.Controls.Add(stats,0,1);int active=repo.Data.Members.Count(m=>m.Status=="Active"),exp=repo.Data.Members.Count(m=>m.Expiry.Date>=DateTime.Today&&m.Expiry.Date<=DateTime.Today.AddDays(7));decimal total=repo.Data.Payments.Sum(p=>p.Amount);Stat(stats,0,T("کل اعضا","Total members"),repo.Data.Members.Count.ToString(),"◉");Stat(stats,1,T("اعضای فعال","Active members"),active.ToString(),"✓");Stat(stats,2,T("انقضا تا ۷ روز","Expiring in 7 days"),exp.ToString(),"!");Stat(stats,3,T("درآمد ثبت‌شده","Recorded revenue"),total.ToString("N0"),"$");var actions=new FlowLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(8,12,0,0)};layout.Controls.Add(actions,0,2);var add=Button(T("+ ثبت عضو جدید","+ New member"),(s,e)=>AddMember(),accent);add.ForeColor=bg;add.Width=180;add.Dock=DockStyle.None;actions.Controls.Add(add);var payment=Button(T("+ ثبت پرداخت","+ Record payment"),(s,e)=>{page="payments";Render();},Color.FromArgb(43,61,87));payment.Width=180;payment.Dock=DockStyle.None;actions.Controls.Add(payment);var recent=Panel();recent.Dock=DockStyle.Fill;layout.Controls.Add(recent,0,3);var label=Label(T("آخرین اعضای ثبت‌شده","Recently registered members"),16);label.Dock=DockStyle.Top;label.Height=48;recent.Controls.Add(label);var g=Grid();g.DataSource=repo.Data.Members.OrderByDescending(m=>m.Start).Take(8).Select(m=>new{ Name=m.Name,Phone=m.Phone,Plan=m.Plan,Expiry=m.Expiry.ToString("yyyy/MM/dd"),Status=m.Status}).ToList();recent.Controls.Add(g);g.BringToFront();}
void Stat(TableLayoutPanel target,int col,string title,string value,string symbol){var p=Panel();p.Dock=DockStyle.Fill;p.Paint+=(sender,args)=>{using(var pen=new Pen(Color.FromArgb(43,75,96)))args.Graphics.DrawRectangle(pen,0,0,Math.Max(0,p.Width-1),Math.Max(0,p.Height-1));};target.Controls.Add(p,col,0);var icon=Label(symbol,18,accent);icon.Dock=DockStyle.Top;icon.Height=36;p.Controls.Add(icon);var v=Label(value,23);v.Dock=DockStyle.Top;v.Height=51;p.Controls.Add(v);var t=Label(title,10,muted);t.Dock=DockStyle.Top;p.Controls.Add(t);t.BringToFront();v.BringToFront();icon.BringToFront();}
DataGridView Grid(){var g=new DataGridView{Dock=DockStyle.Fill,BackgroundColor=surface,BorderStyle=BorderStyle.None,ReadOnly=true,AllowUserToAddRows=false,AllowUserToDeleteRows=false,SelectionMode=DataGridViewSelectionMode.FullRowSelect,MultiSelect=false,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill,RowHeadersVisible=false,EnableHeadersVisualStyles=false,ColumnHeadersHeight=44,RowTemplate={Height=42},GridColor=Color.FromArgb(38,52,70),RightToLeft=fa?RightToLeft.Yes:RightToLeft.No};g.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(23,39,59);g.DefaultCellStyle.BackColor=surface;g.DefaultCellStyle.ForeColor=Color.White;g.DefaultCellStyle.SelectionBackColor=Color.FromArgb(23,108,111);g.DefaultCellStyle.SelectionForeColor=Color.White;g.DefaultCellStyle.Font=regular;g.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(29,45,65);g.ColumnHeadersDefaultCellStyle.ForeColor=accent;return g;}
void ShowMembershipAlerts(){
 var expired=repo.Data.Members.Count(m=>m.Expiry.Date<DateTime.Today);
 var upcoming=repo.Data.Members.Count(m=>m.Expiry.Date>=DateTime.Today&&m.Expiry.Date<=DateTime.Today.AddDays(7));
 if(expired==0&&upcoming==0)return;
 var message=T("اشتراک‌های منقضی‌شده: ","Expired memberships: ")+expired+Environment.NewLine+
 T("اشتراک‌های در آستانه انقضا (۷ روز): ","Expiring within 7 days: ")+upcoming;
 if(MessageBox.Show(message+Environment.NewLine+T("لیست اشتراک‌های رو به پایان باز شود؟","Open expiring memberships?"),"AshkanFit Pro",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes){page="expiring";Build();}
}
void Members(bool expiring){var wrap=new Panel{Dock=DockStyle.Fill};body.Controls.Add(wrap);var bar=new FlowLayoutPanel{Dock=DockStyle.Top,Height=62};wrap.Controls.Add(bar);if(!expiring){var add=Button(T("+ عضو جدید","+ Add member"),(s,e)=>AddMember(),accent);add.ForeColor=bg;add.Dock=DockStyle.None;add.Width=155;bar.Controls.Add(add);}var edit=Button(T("ویرایش","Edit"),(s,e)=>EditMember());edit.Dock=DockStyle.None;edit.Width=105;bar.Controls.Add(edit);var del=Button(T("حذف","Delete"),(s,e)=>DeleteMember());del.Dock=DockStyle.None;del.Width=105;bar.Controls.Add(del);var export=Button("CSV",(s,e)=>ExportMembers());export.Dock=DockStyle.None;export.Width=85;bar.Controls.Add(export);search=new TextBox{Width=220,Font=regular,Margin=new Padding(8,10,0,0)};bar.Controls.Add(search);memberStatusFilter=new ComboBox{Width=165,DropDownStyle=ComboBoxStyle.DropDownList,Margin=new Padding(8,10,0,0)};memberStatusFilter.Items.AddRange(new object[]{T("همه وضعیت‌ها","All statuses"),T("فعال","Active"),T("منقضی‌شده","Expired"),T("انقضا تا ۷ روز","Expiring soon")});memberStatusFilter.SelectedIndex=expiring?3:0;bar.Controls.Add(memberStatusFilter);memberStatusFilter.SelectedIndexChanged+=(s,e)=>FillMembers(expiring);grid=Grid();wrap.Controls.Add(grid);grid.BringToFront();bar.BringToFront();search.TextChanged+=(s,e)=>FillMembers(expiring);FillMembers(expiring);}
void FillMembers(bool expiring){string q=search==null?"":search.Text.Trim();int statusFilter=memberStatusFilter==null?0:memberStatusFilter.SelectedIndex;var list=repo.Data.Members.Where(m=>(!expiring||(m.Expiry.Date>=DateTime.Today&&m.Expiry.Date<=DateTime.Today.AddDays(7)))&&(statusFilter==0||(statusFilter==1&&m.Expiry.Date>=DateTime.Today)||(statusFilter==2&&m.Expiry.Date<DateTime.Today)||(statusFilter==3&&m.Expiry.Date>=DateTime.Today&&m.Expiry.Date<=DateTime.Today.AddDays(7)))&&(string.IsNullOrEmpty(q)||((m.Name??"")+(m.Phone??"")+(m.Plan??"")).IndexOf(q,StringComparison.OrdinalIgnoreCase)>=0)).OrderBy(m=>m.Expiry).Select(m=>new{Id=m.Id,Name=m.Name,Phone=m.Phone,Plan=m.Plan,Start=m.Start.ToString("yyyy/MM/dd"),Expiry=m.Expiry.ToString("yyyy/MM/dd"),Fee=m.Fee,Status=m.Status}).ToList();grid.DataSource=list;if(grid.Columns.Contains("Id"))grid.Columns["Id"].Visible=false;}
Member Selected(){if(grid==null||grid.SelectedRows.Count==0)return null;return repo.Data.Members.FirstOrDefault(m=>m.Id==(Guid)grid.SelectedRows[0].Cells["Id"].Value);}
void AddMember(){using(var dlg=new MemberDialog(null,fa)){if(dlg.ShowDialog(this)==DialogResult.OK){repo.Data.Members.Add(dlg.Value);repo.Save();Render();}}}
void EditMember(){var m=Selected();if(m==null)return;using(var dlg=new MemberDialog(m,fa)){if(dlg.ShowDialog(this)==DialogResult.OK){int idx=repo.Data.Members.IndexOf(m);repo.Data.Members[idx]=dlg.Value;repo.Save();Render();}}}
void DeleteMember(){var m=Selected();if(m==null)return;if(MessageBox.Show(T("عضو حذف شود؟","Delete selected member?"),"AshkanFit Pro",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)return;repo.Data.Members.Remove(m);repo.Data.Payments.RemoveAll(p=>p.MemberId==m.Id);repo.Data.AttendanceRecords.RemoveAll(a=>a.MemberId==m.Id);repo.Data.WorkoutPlans.RemoveAll(w=>w.MemberId==m.Id);repo.Data.BodyProgressRecords.RemoveAll(r=>r.MemberId==m.Id);repo.Save();Render();}
void Payments(){var wrap=new Panel{Dock=DockStyle.Fill};body.Controls.Add(wrap);var bar=new FlowLayoutPanel{Dock=DockStyle.Top,Height=65};wrap.Controls.Add(bar);var combo=new ComboBox{Width=270,DropDownStyle=ComboBoxStyle.DropDownList,Margin=new Padding(0,10,8,0)};var members=repo.Data.Members.OrderBy(m=>m.Name).ToList();combo.DataSource=members;combo.DisplayMember="Name";bar.Controls.Add(combo);var amount=new NumericUpDown{Maximum=1000000000,ThousandsSeparator=true,Width=155,Margin=new Padding(0,10,8,0)};bar.Controls.Add(amount);var add=Button(T("ثبت پرداخت","Add payment"),(s,e)=>{var m=combo.SelectedItem as Member;if(m==null||amount.Value<=0){MessageBox.Show(T("عضو و مبلغ معتبر انتخاب کنید","Select a member and valid amount"));return;}repo.Data.Payments.Add(new Payment{Id=Guid.NewGuid(),MemberId=m.Id,Amount=amount.Value,Date=DateTime.Now,Note="Membership payment"});repo.Save();Render();},accent);add.Dock=DockStyle.None;add.Width=130;add.ForeColor=bg;bar.Controls.Add(add);var export=Button("CSV",(s,e)=>ExportPayments());export.Dock=DockStyle.None;export.Width=80;bar.Controls.Add(export);var g=Grid();g.DataSource=repo.Data.Payments.OrderByDescending(p=>p.Date).Select(p=>new{Member=repo.Data.Members.FirstOrDefault(m=>m.Id==p.MemberId)?.Name??"Unknown",Amount=p.Amount,Date=p.Date.ToString("yyyy/MM/dd HH:mm"),Note=p.Note}).ToList();wrap.Controls.Add(g);g.BringToFront();bar.BringToFront();}
void DataHealthPage(){if(!AccountService.IsAdmin)return;using(var dialog=new DataHealthForm(repo.Data,fa))dialog.ShowDialog(this);page="dashboard";Render();}
void ExecutiveDashboardPage(){if(!AccountService.IsAdmin)return;using(var dialog=new ExecutiveDashboardForm(repo.Data,fa))dialog.ShowDialog(this);page="dashboard";Render();}
void MembershipAlertsPage(){if(!AccountService.IsAdmin)return;using(var dialog=new MembershipAlertsForm(repo.Data,fa))dialog.ShowDialog(this);page="dashboard";Render();}
void FinancialReportingPage(){if(!AccountService.IsAdmin)return;using(var dialog=new FinancialReportForm(repo.Data,fa))dialog.ShowDialog(this);page="reports";Render();}
void Reports(){var panel=Panel();panel.Dock=DockStyle.Fill;body.Controls.Add(panel);var summary=Label(T("گزارش عملکرد باشگاه","Gym performance summary"),19);summary.Dock=DockStyle.Top;summary.Height=70;panel.Controls.Add(summary);int active=repo.Data.Members.Count(m=>m.Status=="Active");decimal revenue=repo.Data.Payments.Sum(p=>p.Amount);var report=Label(string.Format(T("اعضای کل: {0}\nاعضای فعال: {1}\nاشتراک‌های منقضی: {2}\nتعداد تراکنش‌ها: {3}\nمجموع پرداخت‌ها: {4:N0}","Total members: {0}\nActive members: {1}\nExpired plans: {2}\nPayment transactions: {3}\nTotal revenue: {4:N0}"),repo.Data.Members.Count,active,repo.Data.Members.Count(m=>m.Expiry.Date<DateTime.Today),repo.Data.Payments.Count,revenue),15,muted);report.Text=report.Text.Replace("\\n",Environment.NewLine);report.Dock=DockStyle.Top;report.Height=260;panel.Controls.Add(report);report.BringToFront();summary.BringToFront();}
void AttendancePage(){var wrap=new Panel{Dock=DockStyle.Fill};body.Controls.Add(wrap);var bar=new FlowLayoutPanel{Dock=DockStyle.Top,Height=64};wrap.Controls.Add(bar);var combo=new ComboBox{Width=280,DropDownStyle=ComboBoxStyle.DropDownList,Margin=new Padding(0,10,8,0)};combo.DataSource=repo.Data.Members.OrderBy(m=>m.Name).ToList();combo.DisplayMember="Name";bar.Controls.Add(combo);var check=Button(T("ثبت ورود","Check in"),(sender,args)=>{var member=combo.SelectedItem as Member;if(member==null)return;if(member.Start.Date>DateTime.Today||member.Expiry.Date<DateTime.Today){MessageBox.Show(T("اشتراک این عضو منقضی شده است","Membership has expired"));return;}repo.Data.AttendanceRecords.Add(new Attendance{Id=Guid.NewGuid(),MemberId=member.Id,CheckedInAt=DateTime.Now});repo.Save();Render();},accent);check.ForeColor=bg;check.Dock=DockStyle.None;check.Width=155;bar.Controls.Add(check);var g=Grid();g.DataSource=repo.Data.AttendanceRecords.OrderByDescending(a=>a.CheckedInAt).Select(a=>new{Member=repo.Data.Members.FirstOrDefault(m=>m.Id==a.MemberId)?.Name??"Unknown",CheckIn=a.CheckedInAt.ToString("yyyy/MM/dd HH:mm")}).ToList();wrap.Controls.Add(g);g.BringToFront();bar.BringToFront();}

void CoachDashboard(){
 var root=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=3,AutoScroll=true,BackColor=bg};
 root.RowStyles.Add(new RowStyle(SizeType.Absolute,74));
 root.RowStyles.Add(new RowStyle(SizeType.Absolute,145));
 root.RowStyles.Add(new RowStyle(SizeType.Percent,100));
 body.Controls.Add(root);
 var toolbar=new FlowLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(6,8,6,8),WrapContents=false,AutoScroll=true};root.Controls.Add(toolbar,0,0);
 var trainerLabel=Label(T("مربی:","Trainer:"),11,muted);trainerLabel.AutoSize=true;trainerLabel.Margin=new Padding(8,12,8,0);toolbar.Controls.Add(trainerLabel);
 var selector=new ComboBox{Width=270,DropDownStyle=ComboBoxStyle.DropDownList,Margin=new Padding(4,8,12,0)};
 var trainers=repo.Data.Trainers.OrderBy(t=>t.Name).ToList();
 if(AccountService.CurrentRole=="Trainer")trainers=trainers.Where(t=>AccountService.CurrentTrainerId.HasValue && t.Id==AccountService.CurrentTrainerId.Value).ToList();
 selector.DisplayMember="Name";selector.DataSource=trainers;selector.Enabled=AccountService.IsAdmin || AccountService.CurrentRole=="Reception";toolbar.Controls.Add(selector);
 if(AccountService.CurrentRole=="Trainer" && trainers.Count==0){var warning=Label(T("حساب شما هنوز به پروفایل مربی متصل نشده است.","Your account has not been linked to a trainer profile."),11,Color.Orange);warning.AutoSize=true;toolbar.Controls.Add(warning);}
 var stats=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=3};for(int i=0;i<3;i++)stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,33.333f));root.Controls.Add(stats,0,1);
 var grid=Grid();root.Controls.Add(grid,0,2);
 Action update=()=>{
  var coach=selector.SelectedItem as Trainer;
  var plans=coach==null?new System.Collections.Generic.List<WorkoutPlan>():repo.Data.WorkoutPlans.Where(w=>w.TrainerId==coach.Id).ToList();
  // SQL mode: enforce the authenticated trainer's server-side assignment, not a client-provided trainer ID.
  if(SqlStore.Enabled && AccountService.CurrentRole=="Trainer") {
   var allowed=new System.Collections.Generic.HashSet<Guid>(new TrainerScopedQueries().GetAssignedMembers(AccountService.CurrentUser).Select(m=>m.MemberId));
   plans=plans.Where(w=>allowed.Contains(w.MemberId)).ToList();
  }
  var memberIds=plans.Select(w=>w.MemberId).Distinct().ToList();
  stats.Controls.Clear();
  Stat(stats,0,T("ورزشکاران تحت نظارت","Assigned athletes"),memberIds.Count.ToString(),"◉");
  Stat(stats,1,T("برنامه‌های تمرینی","Workout plans"),plans.Count.ToString(),"✓");
  Stat(stats,2,T("کلاس‌های مربی","Coach classes"),(coach==null?0:repo.Data.Classes.Count(c=>c.TrainerId==coach.Id)).ToString(),"★");
  grid.DataSource=memberIds.Select(id=>{var m=repo.Data.Members.FirstOrDefault(x=>x.Id==id);var latest=repo.Data.BodyProgressRecords.Where(x=>x.MemberId==id).OrderByDescending(x=>x.RecordedAt).FirstOrDefault();return new{Member=m==null?"—":m.Name,Phone=m==null?"":m.Phone,WorkoutPlans=plans.Count(x=>x.MemberId==id),LastWeightKg=latest==null?"—":latest.WeightKg.ToString("N1"),BMI=latest==null?"—":latest.Bmi.ToString("N1"),LastCheck=latest==null?"—":DateText(latest.RecordedAt)};}).ToList();
 };
 selector.SelectedIndexChanged+=(sender,args)=>update();update();
}

void TrainersPage(){
 var wrap=new Panel{Dock=DockStyle.Fill,BackColor=bg};body.Controls.Add(wrap);
 var bar=new FlowLayoutPanel{Dock=DockStyle.Top,Height=66,AutoScroll=true,WrapContents=false,Padding=new Padding(8,8,8,8),BackColor=surface};wrap.Controls.Add(bar);
 var table=Grid();table.MultiSelect=false;table.DataSource=repo.Data.Trainers.OrderBy(t=>t.Name).Select(t=>new{Id=t.Id,Name=t.Name,Specialty=t.Specialty,Phone=t.Phone}).ToList();
 if(table.Columns.Contains("Id"))table.Columns["Id"].Visible=false;
 wrap.Controls.Add(table);table.BringToFront();bar.BringToFront();
 Func<Trainer> selected=()=>{if(table.SelectedRows.Count==0)return null;object id=table.SelectedRows[0].Cells["Id"].Value;return id is Guid?repo.Data.Trainers.FirstOrDefault(t=>t.Id==(Guid)id):null;};
 var add=Button(T("+ مربی جدید","+ Add trainer"),(sender,args)=>{using(var dialog=new TrainerDialog(null,fa,repo.Data.Trainers)){if(dialog.ShowDialog(this)!=DialogResult.OK)return;repo.Data.Trainers.Add(dialog.Value);repo.Save();Render();}},accent);add.Dock=DockStyle.None;add.ForeColor=bg;add.Width=165;bar.Controls.Add(add);
 var edit=Button(T("ویرایش مربی","Edit trainer"),(sender,args)=>{var current=selected();if(current==null){MessageBox.Show(T("یک مربی انتخاب کنید.","Select a trainer."));return;}using(var dialog=new TrainerDialog(current,fa,repo.Data.Trainers)){if(dialog.ShowDialog(this)!=DialogResult.OK)return;current.Name=dialog.Value.Name;current.Specialty=dialog.Value.Specialty;current.Phone=dialog.Value.Phone;repo.Save();Render();}});edit.Dock=DockStyle.None;edit.Width=150;bar.Controls.Add(edit);
 var delete=Button(T("حذف مربی","Delete trainer"),(sender,args)=>{var current=selected();if(current==null){MessageBox.Show(T("یک مربی انتخاب کنید.","Select a trainer."));return;}if(repo.Data.WorkoutPlans.Any(w=>w.TrainerId==current.Id)||repo.Data.Classes.Any(c=>c.TrainerId==current.Id)){MessageBox.Show(T("این مربی به برنامه تمرینی یا کلاس متصل است و قابل حذف نیست.","This trainer has assigned workouts or classes and cannot be deleted."));return;}if(MessageBox.Show(T("مربی حذف شود؟","Delete this trainer?"),"AshkanFit Pro",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes)return;repo.Data.Trainers.Remove(current);repo.Save();Render();});delete.Dock=DockStyle.None;delete.Width=145;bar.Controls.Add(delete);
}
void WorkoutsPage(){var wrap=new Panel{Dock=DockStyle.Fill};body.Controls.Add(wrap);var bar=new FlowLayoutPanel{Dock=DockStyle.Top,Height=66,AutoScroll=true};wrap.Controls.Add(bar);var members=repo.Data.Members.OrderBy(m=>m.Name).ToList();var trainers=repo.Data.Trainers.OrderBy(t=>t.Name).ToList();var member=new ComboBox{Width=190,DropDownStyle=ComboBoxStyle.DropDownList,DisplayMember="Name",DataSource=members,Margin=new Padding(0,9,8,0)};bar.Controls.Add(member);var trainer=new ComboBox{Width=170,DropDownStyle=ComboBoxStyle.DropDownList,DisplayMember="Name",DataSource=trainers,Margin=new Padding(0,9,8,0)};bar.Controls.Add(trainer);var add=Button(T("+ برنامه جدید","+ New workout"),(sender,args)=>{if(!(member.SelectedItem is Member m)||!(trainer.SelectedItem is Trainer t)){MessageBox.Show(T("ابتدا عضو و مربی ثبت کنید","Create a member and trainer first"));return;}using(var dialog=new WorkoutDialog(fa)){if(dialog.ShowDialog(this)!=DialogResult.OK)return;repo.Data.WorkoutPlans.Add(new WorkoutPlan{Id=Guid.NewGuid(),MemberId=m.Id,TrainerId=t.Id,Goal=dialog.Goal,Exercises=dialog.Exercises,CreatedAt=DateTime.Now});repo.Save();Render();}},accent);add.Dock=DockStyle.None;add.ForeColor=bg;add.Width=165;bar.Controls.Add(add);var g=Grid();g.DataSource=repo.Data.WorkoutPlans.OrderByDescending(w=>w.CreatedAt).Select(w=>new{Member=repo.Data.Members.FirstOrDefault(m=>m.Id==w.MemberId)?.Name??"—",Trainer=repo.Data.Trainers.FirstOrDefault(t=>t.Id==w.TrainerId)?.Name??"—",Goal=w.Goal,Exercises=w.Exercises,Date=DateText(w.CreatedAt)}).ToList();wrap.Controls.Add(g);g.BringToFront();bar.BringToFront();}
void ClassesPage(){var wrap=new Panel{Dock=DockStyle.Fill};body.Controls.Add(wrap);var bar=new FlowLayoutPanel{Dock=DockStyle.Top,Height=66,AutoScroll=true};wrap.Controls.Add(bar);var add=Button(T("+ کلاس جدید","+ New class"),(sender,args)=>{using(var d=new ClassDialog(repo.Data.Trainers,fa)){if(d.ShowDialog(this)!=DialogResult.OK)return;repo.Data.Classes.Add(d.Value);repo.Save();Render();}},accent);add.Dock=DockStyle.None;add.ForeColor=bg;add.Width=160;bar.Controls.Add(add);var g=Grid();g.DataSource=repo.Data.Classes.Select(c=>new{Title=c.Title,Trainer=repo.Data.Trainers.FirstOrDefault(t=>t.Id==c.TrainerId)?.Name??"—",Day=c.Day.ToString(),Time=c.Time,Capacity=c.Capacity}).ToList();wrap.Controls.Add(g);g.BringToFront();bar.BringToFront();}

void FinanceTrendsPage(){
 var host=new Panel{Dock=DockStyle.Fill,BackColor=bg,Padding=new Padding(8)};
 body.Controls.Add(host);
 var launch=Button(T("نمایش نمودار ۱۲ ماهه و خروجی CSV","Open 12-month chart and CSV export"),(sender,args)=>{
  using(var dialog=new FinanceTrendsForm(repo.Data,fa))dialog.ShowDialog(this);
 },accent);
 launch.Dock=DockStyle.Top;launch.Height=60;launch.ForeColor=bg;
 host.Controls.Add(launch);
 var tip=Label(T("نمودار درآمد، هزینه و مانده خالص را در پنجره جداگانه بررسی کنید.","Explore receipts, expenses and net balance in the dedicated analytics window."),12,muted);
 tip.Dock=DockStyle.Top;tip.Height=75;host.Controls.Add(tip);tip.BringToFront();
}
void FinancePage(){
 var layout=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=3};
 layout.RowStyles.Add(new RowStyle(SizeType.Absolute,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,78));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));body.Controls.Add(layout);
 var stats=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=3};for(int i=0;i<3;i++)stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,33.333f));layout.Controls.Add(stats,0,0);
 decimal due=repo.Data.Members.Sum(m=>Math.Max(0,m.Fee-repo.Data.Payments.Where(p=>p.MemberId==m.Id).Sum(p=>p.Amount)));
 Stat(stats,0,T("مانده شهریه","Outstanding balance"),due.ToString("N0"),"$");
 Stat(stats,1,T("درآمد این ماه","This month's receipts"),repo.Data.Payments.Where(p=>p.Date.Year==DateTime.Today.Year&&p.Date.Month==DateTime.Today.Month).Sum(p=>p.Amount).ToString("N0"),"+");
 Stat(stats,2,T("اشتراک منقضی","Expired memberships"),repo.Data.Members.Count(m=>m.Expiry.Date<DateTime.Today).ToString(),"!");
 var bar=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=false,AutoScroll=true};layout.Controls.Add(bar,0,1);
 var memberBox=new ComboBox{Width=245,DropDownStyle=ComboBoxStyle.DropDownList,DisplayMember="Name",DataSource=repo.Data.Members.OrderBy(m=>m.Name).ToList(),Margin=new Padding(4,12,8,0)};bar.Controls.Add(memberBox);
 var months=new NumericUpDown{Minimum=1,Maximum=24,Value=1,Width=65,Margin=new Padding(0,12,8,0)};bar.Controls.Add(months);
 var renew=Button(T("تمدید (ماه)","Renew (months)"),(sender,args)=>{
 var m=memberBox.SelectedItem as Member;if(m==null)return;
 var from=m.Expiry.Date>DateTime.Today?m.Expiry.Date:DateTime.Today;
 m.Expiry=from.AddMonths((int)months.Value);
 repo.Save();Render();},accent);renew.Dock=DockStyle.None;renew.ForeColor=bg;renew.Width=155;bar.Controls.Add(renew);
 var csv=Button(T("خروجی بدهکاران","Export balances"),(sender,args)=>ExportBalances());csv.Dock=DockStyle.None;csv.Width=155;bar.Controls.Add(csv);
 var invoice=Button(T("رسید عضو (CSV)","Member receipt (CSV)"),(sender,args)=>ExportMemberReceipt(memberBox.SelectedItem as Member));invoice.Dock=DockStyle.None;invoice.Width=165;bar.Controls.Add(invoice);
 var g=Grid();g.DataSource=repo.Data.Members.OrderBy(m=>m.Name).Select(m=>new{Member=m.Name,Phone=m.Phone,Plan=m.Plan,Expiry=DateText(m.Expiry),Fee=m.Fee,Paid=repo.Data.Payments.Where(p=>p.MemberId==m.Id).Sum(p=>p.Amount),Balance=Math.Max(0,m.Fee-repo.Data.Payments.Where(p=>p.MemberId==m.Id).Sum(p=>p.Amount))}).ToList();layout.Controls.Add(g,0,2);
}
void InsightsPage(){
 var layout=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=2};layout.RowStyles.Add(new RowStyle(SizeType.Absolute,145));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));body.Controls.Add(layout);
 var stats=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=3};for(int i=0;i<3;i++)stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,33.333f));layout.Controls.Add(stats,0,0);
 decimal revenue=repo.Data.Payments.Sum(p=>p.Amount),cost=repo.Data.Expenses.Sum(e=>e.Amount);
 Stat(stats,0,T("کل دریافتی‌ها","Total receipts"),revenue.ToString("N0"),"+");Stat(stats,1,T("کل هزینه‌ها","Total expenses"),cost.ToString("N0"),"-");Stat(stats,2,T("جریان نقدی خالص","Net cash flow"),(revenue-cost).ToString("N0"),"=");
 var chart=new PremiumCard{Dock=DockStyle.Fill,BackColor=surface,Padding=new Padding(24),Radius=18};layout.Controls.Add(chart,0,1);
 chart.Paint+=(sender,args)=>{
  var gr=args.Graphics;gr.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
  int left=65,top=76,right=24,bottom=65;int width=Math.Max(10,chart.ClientSize.Width-left-right),height=Math.Max(10,chart.ClientSize.Height-top-bottom);
  using(var titleFont=new Font("Segoe UI",15,FontStyle.Bold))using(var titleBrush=new SolidBrush(Color.White))gr.DrawString(T("دریافتی و هزینه‌های شش ماه اخیر","Receipts and expenses · last 6 months"),titleFont,titleBrush,24,20);
  var months=Enumerable.Range(0,6).Select(i=>DateTime.Today.AddMonths(i-5)).ToArray();
  var income=months.Select(m=>repo.Data.Payments.Where(p=>p.Date.Year==m.Year&&p.Date.Month==m.Month).Sum(p=>p.Amount)).ToArray();
  var expenses=months.Select(m=>repo.Data.Expenses.Where(e=>e.Date.Year==m.Year&&e.Date.Month==m.Month).Sum(e=>e.Amount)).ToArray();
  decimal maximum=Math.Max(1,Math.Max(income.Max(),expenses.Max()));
  using(var gridPen=new Pen(Color.FromArgb(50,72,92)))for(int k=0;k<=4;k++){int y=top+height-k*height/4;gr.DrawLine(gridPen,left,y,left+width,y);}
  int slot=width/6;int barWidth=Math.Max(8,Math.Min(36,slot/4));
  using(var incomeBrush=new SolidBrush(accent))using(var expenseBrush=new SolidBrush(Color.FromArgb(249,159,92)))using(var textBrush=new SolidBrush(muted))using(var axisFont=new Font("Segoe UI",9)){
   for(int i=0;i<6;i++){int x=left+i*slot+slot/2;int h1=(int)(height*income[i]/maximum),h2=(int)(height*expenses[i]/maximum);gr.FillRectangle(incomeBrush,x-barWidth-2,top+height-h1,barWidth,h1);gr.FillRectangle(expenseBrush,x+2,top+height-h2,barWidth,h2);gr.DrawString(months[i].ToString("MMM yy",CultureInfo.InvariantCulture),axisFont,textBrush,x-barWidth-9,top+height+14);}
   gr.FillRectangle(incomeBrush,left,top-25,12,12);gr.DrawString(T("دریافتی","Receipts"),axisFont,textBrush,left+18,top-29);gr.FillRectangle(expenseBrush,left+130,top-25,12,12);gr.DrawString(T("هزینه","Expenses"),axisFont,textBrush,left+148,top-29);
  }
 };
}
void ExportMemberReceipt(Member member){
 if(member==null){MessageBox.Show(T("ابتدا عضو را انتخاب کنید.","Select a member first."));return;}
 var payments=repo.Data.Payments.Where(p=>p.MemberId==member.Id).OrderBy(p=>p.Date).ToList();
 var rows=new System.Collections.Generic.List<string>{Line("AshkanFit Pro - Member Statement"),Line("Member",member.Name),Line("Phone",member.Phone),Line("Plan",member.Plan),Line("Expiry",member.Expiry.ToString("yyyy-MM-dd")),Line("Membership fee",member.Fee.ToString(CultureInfo.InvariantCulture)),Line("Total paid",payments.Sum(p=>p.Amount).ToString(CultureInfo.InvariantCulture)),Line("Outstanding",Math.Max(0,member.Fee-payments.Sum(p=>p.Amount)).ToString(CultureInfo.InvariantCulture)),"",Line("Payment date","Amount","Note")};
 rows.AddRange(payments.Select(p=>Line(p.Date.ToString("yyyy-MM-dd"),p.Amount.ToString(CultureInfo.InvariantCulture),p.Note)));
 Csv("ashkanfit-member-statement.csv",rows);
}
void ExportBalances(){Csv("ashkanfit-balances.csv",new[]{"Member,Phone,Plan,Fee,Paid,Balance"}.Concat(repo.Data.Members.Select(m=>{var paid=repo.Data.Payments.Where(p=>p.MemberId==m.Id).Sum(p=>p.Amount);return Line(m.Name,m.Phone,m.Plan,m.Fee.ToString(CultureInfo.InvariantCulture),paid.ToString(CultureInfo.InvariantCulture),Math.Max(0,m.Fee-paid).ToString(CultureInfo.InvariantCulture));})));}

void ExportMembers(){Csv("members.csv",new[]{"Name,Phone,Plan,Start,Expiry,Fee,Status"}.Concat(repo.Data.Members.Select(m=>Line(m.Name,m.Phone,m.Plan,m.Start.ToString("yyyy-MM-dd"),m.Expiry.ToString("yyyy-MM-dd"),m.Fee.ToString(),m.Status))));}
void ExportPayments(){Csv("payments.csv",new[]{"Member,Amount,Date,Note"}.Concat(repo.Data.Payments.Select(p=>Line(repo.Data.Members.FirstOrDefault(m=>m.Id==p.MemberId)?.Name??"",p.Amount.ToString(),p.Date.ToString("s"),p.Note))));}
string Line(params string[] cells){return string.Join(",",cells.Select(c=>"\""+(c??"").Replace("\"","\"\"")+"\""));}
void Csv(string name,System.Collections.Generic.IEnumerable<string> lines){using(var dialog=new SaveFileDialog{FileName=name,Filter="CSV files|*.csv"})if(dialog.ShowDialog(this)==DialogResult.OK){File.WriteAllLines(dialog.FileName,lines,new UTF8Encoding(true));MessageBox.Show(T("فایل ذخیره شد","Export completed")); }}

void AuditPage(){
 var wrapper=new PremiumCard{Dock=DockStyle.Fill,Padding=new Padding(16),BackColor=surface,Radius=18};
 body.Controls.Add(wrapper);
 var intro=Label(T("ثبت خودکار تغییرات اطلاعات باشگاه (از نسخه ۹)","Automatic change history (from version 9)"),13,muted);
 intro.Dock=DockStyle.Top;intro.Height=52;wrapper.Controls.Add(intro);
 var export=Button(T("خروجی CSV","Export CSV"),(sender,args)=>ExportAudit(),accent);
 export.Dock=DockStyle.Top;export.Height=45;wrapper.Controls.Add(export);
 var table=Grid();table.Dock=DockStyle.Fill;
 table.DataSource=repo.Data.AuditEntries.OrderByDescending(a=>a.At).Select(a=>new{
  Date=DateText(a.At), Action=a.Action, Details=a.Details
 }).ToList();
 wrapper.Controls.Add(table);table.BringToFront();
}
void ExportAudit(){
 using(var dlg=new SaveFileDialog{Filter="CSV files (*.csv)|*.csv",FileName="ashkanfit-activity.csv"}){
  if(dlg.ShowDialog()!=DialogResult.OK)return;
  Func<string,string> esc=value=>"\""+(value??"").Replace("\"","\"\"")+"\"";
  var rows=new System.Collections.Generic.List<string>{"Date,Action,Details"};
  rows.AddRange(repo.Data.AuditEntries.Select(a=>string.Join(",",new[]{esc(a.At.ToString("o")),esc(a.Action),esc(a.Details)})));
  File.WriteAllLines(dlg.FileName,rows, new UTF8Encoding(true));
  MessageBox.Show(T("فایل ذخیره شد.","File saved."));
 }
}
// Backup operations are deliberately limited to administrators.
void BackupPage(){
 var stack=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,WrapContents=false,AutoScroll=true,Padding=new Padding(18),BackColor=bg};
 body.Controls.Add(stack);
 var hint=Label(T("نسخه پشتیبان از داده‌های باشگاه را در محل امن ذخیره کنید.","Export a copy of gym data and store it securely."),12,muted);hint.Margin=new Padding(0,0,0,20);stack.Controls.Add(hint);
 var export=Button(T("دریافت نسخه پشتیبان JSON","Export JSON backup"),(sender,args)=>{
  using(var dialog=new SaveFileDialog{Filter="JSON (*.json)|*.json",FileName="AshkanFitPro-Backup-"+DateTime.Now.ToString("yyyyMMdd-HHmmss",CultureInfo.InvariantCulture)+".json"}){
   if(dialog.ShowDialog(this)!=DialogResult.OK)return;
   try{
    using(var stream=File.Create(dialog.FileName))new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(Store)).WriteObject(stream,repo.Data);
    MessageBox.Show(T("فایل پشتیبان ذخیره شد.","Backup exported successfully."));
   }catch(Exception ex){MessageBox.Show(T("خطا در تهیه نسخه پشتیبان: ","Backup failed: ")+ex.Message);}
  }
 },accent);export.ForeColor=bg;export.Width=310;stack.Controls.Add(export);
 if(SqlStore.Enabled){
  var historyButton=Button(T("مشاهده و بررسی تاریخچه SQL","Inspect SQL snapshot history"),(sender,args)=>{
   try {using(var history=new HistoryBrowserForm(fa))history.ShowDialog(this);}
   catch(Exception ex){MessageBox.Show(ex.Message);}
  });
  historyButton.Width=310;stack.Controls.Add(historyButton);
 }
 stack.Controls.Add(Label(T("بازیابی داده‌ها باید با بررسی اعتبار فایل و تهیه نسخه ایمن از اطلاعات فعلی انجام شود.","Restore requires validating the file and safely backing up current data first."),10,muted));
}
void ProgressPage(){
var panel=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,AutoScroll=true,WrapContents=false,Padding=new Padding(16),BackColor=bg};body.Controls.Add(panel);
var add=Button(T("ثبت پیشرفت جدید","Record body progress"),(sender,args)=>{using(var dialog=new ProgressDialog(repo)){if(dialog.ShowDialog(this)==DialogResult.OK)Render();}});add.Width=260;panel.Controls.Add(add);
var grid=new DataGridView{Width=950,Height=490,ReadOnly=true,AllowUserToAddRows=false,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill,BackgroundColor=Color.FromArgb(18,33,51),RowHeadersVisible=false};
grid.Columns.Add("member",T("عضو","Member"));grid.Columns.Add("date",T("تاریخ","Date"));grid.Columns.Add("weight",T("وزن (کیلوگرم)","Weight kg"));grid.Columns.Add("bmi","BMI");grid.Columns.Add("waist",T("دور کمر","Waist cm"));grid.Columns.Add("fat",T("چربی بدن","Body fat %"));grid.Columns.Add("goal",T("هدف","Goal"));
foreach(var record in repo.Data.BodyProgressRecords.OrderByDescending(x=>x.RecordedAt)){var member=repo.Data.Members.FirstOrDefault(x=>x.Id==record.MemberId);grid.Rows.Add(member==null?"—":member.Name,DateText(record.RecordedAt),record.WeightKg,record.Bmi,record.WaistCm,record.BodyFatPercent,record.Goal);}
panel.Controls.Add(grid);
var analytics=Button(T("نمودار و گزارش پیشرفت","Progress charts & export"),(sender,args)=>{using(var dialog=new ProgressAnalyticsForm(repo,fa))dialog.ShowDialog(this);});analytics.Width=270;panel.Controls.Add(analytics);
}
}
}
