using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace AshkanFitPro
{
    public partial class ClassDialog : Form
    {
        public GymClass Value { get; private set; }
        public ClassDialog(List<Trainer> trainers, bool fa)
        {
            InitializeComponent();
            Text = fa ? "کلاس جدید" : "New class";
            RightToLeft = fa ? RightToLeft.Yes : RightToLeft.No;
            titleLabel.Text = fa ? "نام کلاس" : "Class title";
            coachLabel.Text = fa ? "مربی" : "Trainer";
            dayLabel.Text = fa ? "روز هفته" : "Day of week";
            timeLabel.Text = fa ? "ساعت" : "Time";
            capacityLabel.Text = fa ? "ظرفیت" : "Capacity";
            saveButton.Text = fa ? "ذخیره" : "Save";
            coachBox.DisplayMember = "Name";
            coachBox.DataSource = trainers.ToList();
            dayBox.DataSource = Enum.GetValues(typeof(DayOfWeek));
            saveButton.Click += (s,e) =>
            {
                var trainer = coachBox.SelectedItem as Trainer;
                if (string.IsNullOrWhiteSpace(titleBox.Text) || trainer == null)
                {
                    MessageBox.Show(fa ? "نام کلاس و مربی الزامی است" : "Class title and trainer required");
                    return;
                }
                Value = new GymClass { Id = Guid.NewGuid(), Title = titleBox.Text.Trim(), TrainerId = trainer.Id,
                    Day = (DayOfWeek)dayBox.SelectedItem, Time = timePicker.Value.ToString("HH:mm"), Capacity = (int)capacityBox.Value };
                DialogResult = DialogResult.OK;
            };
        }
    }
}
