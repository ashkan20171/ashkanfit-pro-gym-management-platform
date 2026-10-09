namespace AshkanFitPro
{
    partial class WorkoutDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel workoutLayout;
        private System.Windows.Forms.Label goalLabel;
        private System.Windows.Forms.TextBox goalBox;
        private System.Windows.Forms.Label exercisesLabel;
        private System.Windows.Forms.TextBox exercisesBox;
        private System.Windows.Forms.Button saveButton;
        private void InitializeComponent()
        {
            this.workoutLayout = new System.Windows.Forms.TableLayoutPanel();
            this.goalLabel = new System.Windows.Forms.Label();
            this.goalBox = new System.Windows.Forms.TextBox();
            this.exercisesLabel = new System.Windows.Forms.Label();
            this.exercisesBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.workoutLayout.SuspendLayout();
            this.workoutLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workoutLayout.Padding = new System.Windows.Forms.Padding(24);
            this.workoutLayout.ColumnCount = 1;
            this.workoutLayout.RowCount = 5;
            this.workoutLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.workoutLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.workoutLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.workoutLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.workoutLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.goalLabel.Text = "Training goal";
            this.goalLabel.ForeColor = System.Drawing.Color.White;
            this.goalLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goalBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exercisesLabel.Text = "Exercises and sets";
            this.exercisesLabel.ForeColor = System.Drawing.Color.White;
            this.exercisesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exercisesBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exercisesBox.Multiline = true;
            this.exercisesBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.saveButton.Text = "Save";
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(42,218,185);
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.workoutLayout.Controls.Add(this.goalLabel,0,0);
            this.workoutLayout.Controls.Add(this.goalBox,0,1);
            this.workoutLayout.Controls.Add(this.exercisesLabel,0,2);
            this.workoutLayout.Controls.Add(this.exercisesBox,0,3);
            this.workoutLayout.Controls.Add(this.saveButton,0,4);
            this.ClientSize = new System.Drawing.Size(560,460);
            this.Controls.Add(this.workoutLayout);
            this.BackColor = System.Drawing.Color.FromArgb(20,31,50);
            this.ForeColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI",10F);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "WorkoutDialog";
            this.workoutLayout.ResumeLayout(false);
            this.workoutLayout.PerformLayout();
            this.ResumeLayout(false);

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
    }
}
