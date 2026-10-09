using System;
using System.Windows.Forms;
namespace AshkanFitPro
{
    public partial class WorkoutDialog : Form
    {
        public string Goal { get; private set; }
        public string Exercises { get; private set; }
        public WorkoutDialog(bool fa)
        {
            InitializeComponent();
            Text = fa ? "برنامه تمرینی" : "Workout plan";
            RightToLeft = fa ? RightToLeft.Yes : RightToLeft.No;
            goalLabel.Text = fa ? "هدف تمرین" : "Training goal";
            exercisesLabel.Text = fa ? "حرکات و تعداد ست‌ها" : "Exercises and sets";
            saveButton.Text = fa ? "ذخیره" : "Save";
            saveButton.Click += (s,e) =>
            {
                if (string.IsNullOrWhiteSpace(goalBox.Text) || string.IsNullOrWhiteSpace(exercisesBox.Text))
                {
                    MessageBox.Show(fa ? "تمام فیلدها الزامی هستند" : "Both fields are required");
                    return;
                }
                Goal = goalBox.Text.Trim();
                Exercises = exercisesBox.Text.Trim();
                DialogResult = DialogResult.OK;
            };
        }
    }
}
