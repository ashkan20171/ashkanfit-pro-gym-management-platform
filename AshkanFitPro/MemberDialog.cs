using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AshkanFitPro
{
    public partial class MemberDialog : Form
    {
        private readonly bool fa;
        public Member Value { get; private set; }
        private readonly Color background = Color.FromArgb(10, 19, 33);
        private readonly Color card = Color.FromArgb(23, 38, 58);
        private readonly Color accent = Color.FromArgb(43, 219, 187);

        public MemberDialog(Member existing, bool persian)
        {InitializeComponent();
            fa = persian;
            Value = existing == null ? new Member { Id = Guid.NewGuid(), Start = DateTime.Today, Expiry = DateTime.Today.AddMonths(1), Plan = "Monthly" } :
                new Member { Id = existing.Id, Name = existing.Name, Phone = existing.Phone, Plan = existing.Plan, Start = existing.Start, Expiry = existing.Expiry, Fee = existing.Fee };
            Text = fa ? "اشکان فیت | اطلاعات عضو" : "AshkanFit | Member details";
            ClientSize = new Size(570, 680);
            MinimumSize = new Size(480, 600);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = background;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10);
            FormBorderStyle = FormBorderStyle.Sizable;
            RightToLeft = fa ? RightToLeft.Yes : RightToLeft.No;
            RightToLeftLayout = fa;

            // All visible input controls and layout are declared in MemberDialog.Designer.cs.
            titleLabel.Text = fa ? "پروفایل عضو" : "MEMBER PROFILE";
            subtitleLabel.Text = fa ? "اطلاعات عضویت را تکمیل و بررسی کنید" : "Keep membership details accurate and up to date";
            labelName.Text = fa ? "نام و نام خانوادگی *" : "Full name *";
            labelPhone.Text = fa ? "شماره تماس" : "Phone number";
            labelPlan.Text = fa ? "نوع اشتراک" : "Membership plan";
            labelStart.Text = fa ? "تاریخ شروع" : "Start date";
            labelEnd.Text = fa ? "تاریخ پایان" : "Expiry date";
            labelFee.Text = fa ? "شهریه" : "Membership fee";
            saveButton.Text = fa ? "ذخیره تغییرات" : "Save member";
            cancelButton.Text = fa ? "انصراف" : "Cancel";
            foreach (Control c in fieldsPanel.Controls)
                if (c is Label) ((Label)c).TextAlign = fa ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft;
            saveButton.Click += (sender, args) => Save();
            start.ValueChanged += (sender, args) => RefreshWarning();
            end.ValueChanged += (sender, args) => RefreshWarning();
            AcceptButton = saveButton;
            CancelButton = cancelButton;
            name.Text = Value.Name ?? "";
            phone.Text = Value.Phone ?? "";
            plan.SelectedItem = Value.Plan;
            if (plan.SelectedIndex < 0) plan.SelectedIndex = 0;
            start.Value = ClampDate(Value.Start);
            end.Value = ClampDate(Value.Expiry);
            fee.Value = Math.Min(fee.Maximum, Math.Max(fee.Minimum, Value.Fee));
            RefreshWarning();
        }

        private DateTime ClampDate(DateTime date)
        {
            if (date < DateTimePicker.MinimumDateTime) return DateTime.Today;
            if (date > DateTimePicker.MaximumDateTime) return DateTime.Today;
            return date;
        }
        private void RefreshWarning()
        {
            warning.Text = end.Value.Date < start.Value.Date ? (fa ? "تاریخ پایان نمی‌تواند قبل از شروع باشد." : "Expiry date cannot be earlier than start date.") :
                end.Value.Date < DateTime.Today ? (fa ? "توجه: این اشتراک منقضی شده است." : "Note: this membership has expired.") : "";
        }
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(name.Text) || name.Text.Trim().Length < 2)
            {
                MessageBox.Show(fa ? "نام عضو باید حداقل دو حرف داشته باشد." : "Member name must contain at least two characters.");
                name.Focus(); return;
            }
            string normalizedPhone = Regex.Replace(phone.Text.Trim(), @"[\s\-()]", "");
            if (normalizedPhone.Length > 0 && !Regex.IsMatch(normalizedPhone, @"^\+?[0-9]{7,15}$"))
            {
                MessageBox.Show(fa ? "شماره تماس معتبر وارد کنید (۷ تا ۱۵ رقم)." : "Enter a valid phone number (7–15 digits).");
                phone.Focus(); return;
            }
            if (end.Value.Date < start.Value.Date)
            {
                MessageBox.Show(fa ? "تاریخ پایان نامعتبر است." : "Expiry must not be before start.");
                end.Focus(); return;
            }
            Value.Name = name.Text.Trim();
            Value.Phone = normalizedPhone;
            Value.Plan = plan.Text;
            Value.Start = start.Value.Date;
            Value.Expiry = end.Value.Date;
            Value.Fee = fee.Value;
            DialogResult = DialogResult.OK;
        }
    }
}
