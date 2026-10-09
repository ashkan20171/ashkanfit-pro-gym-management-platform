namespace AshkanFitPro
{
    partial class ClassDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel classLayout;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox titleBox;
        private System.Windows.Forms.Label coachLabel;
        private System.Windows.Forms.ComboBox coachBox;
        private System.Windows.Forms.Label dayLabel;
        private System.Windows.Forms.ComboBox dayBox;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.DateTimePicker timePicker;
        private System.Windows.Forms.Label capacityLabel;
        private System.Windows.Forms.NumericUpDown capacityBox;
        private System.Windows.Forms.Button saveButton;
        private void InitializeComponent()
        {
            this.classLayout = new System.Windows.Forms.TableLayoutPanel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.titleBox = new System.Windows.Forms.TextBox();
            this.coachLabel = new System.Windows.Forms.Label();
            this.coachBox = new System.Windows.Forms.ComboBox();
            this.dayLabel = new System.Windows.Forms.Label();
            this.dayBox = new System.Windows.Forms.ComboBox();
            this.timeLabel = new System.Windows.Forms.Label();
            this.timePicker = new System.Windows.Forms.DateTimePicker();
            this.capacityLabel = new System.Windows.Forms.Label();
            this.capacityBox = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.capacityBox)).BeginInit();
            this.classLayout.SuspendLayout();
            this.classLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classLayout.Padding = new System.Windows.Forms.Padding(20);
            this.classLayout.ColumnCount = 2;
            this.classLayout.RowCount = 6;
            this.classLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute,125F));
            this.classLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,100F));
            for (int i=0;i<5;i++) this.classLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,55F));
            this.classLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent,100F));
            this.titleLabel.Text = "Class title";
            this.coachLabel.Text = "Trainer";
            this.dayLabel.Text = "Day of week";
            this.timeLabel.Text = "Time";
            this.capacityLabel.Text = "Capacity";
            this.titleBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coachBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coachBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dayBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dayBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePicker.CustomFormat = "HH:mm";
            this.timePicker.ShowUpDown = true;
            this.capacityBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capacityBox.Minimum = 1;
            this.capacityBox.Maximum = 500;
            this.capacityBox.Value = 15;
            this.saveButton.Text = "Save";
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.saveButton.Height = 44;
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(42,218,185);
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.classLayout.Controls.Add(this.titleLabel,0,0);
            this.classLayout.Controls.Add(this.titleBox,1,0);
            this.classLayout.Controls.Add(this.coachLabel,0,1);
            this.classLayout.Controls.Add(this.coachBox,1,1);
            this.classLayout.Controls.Add(this.dayLabel,0,2);
            this.classLayout.Controls.Add(this.dayBox,1,2);
            this.classLayout.Controls.Add(this.timeLabel,0,3);
            this.classLayout.Controls.Add(this.timePicker,1,3);
            this.classLayout.Controls.Add(this.capacityLabel,0,4);
            this.classLayout.Controls.Add(this.capacityBox,1,4);
            this.classLayout.Controls.Add(this.saveButton,1,5);
            this.ClientSize = new System.Drawing.Size(520,410);
            this.Controls.Add(this.classLayout);
            this.BackColor = System.Drawing.Color.FromArgb(20,31,50);
            this.ForeColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI",10F);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "ClassDialog";
            this.classLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.capacityBox)).EndInit();
            this.ResumeLayout(false);

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
    }
}
