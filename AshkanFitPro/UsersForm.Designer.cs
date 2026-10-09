namespace AshkanFitPro
{
    partial class UsersForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label roleLabel;
        private System.Windows.Forms.TextBox usernameInput;
        private System.Windows.Forms.TextBox passwordInput;
        private System.Windows.Forms.ComboBox roleInput;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.DataGridView usersGrid;
        private System.Windows.Forms.FlowLayoutPanel actionsPanel;
        private System.Windows.Forms.TableLayoutPanel fieldsLayout;
        private void InitializeComponent()
        {
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.roleLabel = new System.Windows.Forms.Label();
            this.usernameInput = new System.Windows.Forms.TextBox();
            this.passwordInput = new System.Windows.Forms.TextBox();
            this.roleInput = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.usersGrid = new System.Windows.Forms.DataGridView();
            this.actionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.fieldsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.rootLayout.SuspendLayout();
            this.fieldsLayout.SuspendLayout();
            this.actionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usersGrid)).BeginInit();
            this.SuspendLayout();
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Padding = new System.Windows.Forms.Padding(24);
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.RowCount = 4;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.titleLabel.Text = "USER ACCESS  /  مدیریت کاربران";
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(42, 218, 185);
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldsLayout.ColumnCount = 3;
            this.fieldsLayout.RowCount = 2;
            this.fieldsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.fieldsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.fieldsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.fieldsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.fieldsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.usernameLabel.Text = "Username / نام کاربری";
            this.passwordLabel.Text = "Password / رمز عبور";
            this.roleLabel.Text = "Role / نقش";
            foreach (System.Windows.Forms.Label label in new[] { this.usernameLabel, this.passwordLabel, this.roleLabel })
            {
                label.Dock = System.Windows.Forms.DockStyle.Fill;
                label.ForeColor = System.Drawing.Color.FromArgb(180, 205, 219);
            }
            this.usernameInput.Name = "usernameInput";
            this.usernameInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usernameInput.MaxLength = 100;
            this.passwordInput.Name = "passwordInput";
            this.passwordInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passwordInput.UseSystemPasswordChar = true;
            this.roleInput.Name = "roleInput";
            this.roleInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roleInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roleInput.Items.AddRange(new object[] { "Admin", "Trainer", "Reception" });
            this.roleInput.SelectedIndex = 2;
            this.fieldsLayout.Controls.Add(this.usernameLabel, 0, 0);
            this.fieldsLayout.Controls.Add(this.passwordLabel, 1, 0);
            this.fieldsLayout.Controls.Add(this.roleLabel, 2, 0);
            this.fieldsLayout.Controls.Add(this.usernameInput, 0, 1);
            this.fieldsLayout.Controls.Add(this.passwordInput, 1, 1);
            this.fieldsLayout.Controls.Add(this.roleInput, 2, 1);
            this.actionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.addButton.Name = "addButton";
            this.addButton.Text = "Add user / افزودن";
            this.addButton.Width = 170;
            this.addButton.Height = 37;
            this.addButton.BackColor = System.Drawing.Color.FromArgb(42, 218, 185);
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeButton.Name = "removeButton";
            this.removeButton.Text = "Remove selected / حذف";
            this.removeButton.Width = 185;
            this.removeButton.Height = 37;
            this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeButton.ForeColor = System.Drawing.Color.White;
            this.removeButton.BackColor = System.Drawing.Color.FromArgb(56, 72, 92);
            this.actionsPanel.Controls.Add(this.addButton);
            this.actionsPanel.Controls.Add(this.removeButton);
            this.usersGrid.Name = "usersGrid";
            this.usersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersGrid.ReadOnly = true;
            this.usersGrid.AllowUserToAddRows = false;
            this.usersGrid.AllowUserToDeleteRows = false;
            this.usersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.usersGrid.MultiSelect = false;
            this.usersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.usersGrid.BackgroundColor = System.Drawing.Color.FromArgb(20, 32, 51);
            this.rootLayout.Controls.Add(this.titleLabel, 0, 0);
            this.rootLayout.Controls.Add(this.fieldsLayout, 0, 1);
            this.rootLayout.Controls.Add(this.actionsPanel, 0, 2);
            this.rootLayout.Controls.Add(this.usersGrid, 0, 3);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.MinimumSize = new System.Drawing.Size(650, 450);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.FromArgb(13, 23, 39);
            this.ForeColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Text = "AshkanFit Pro | User Management";
            this.Controls.Add(this.rootLayout);
            ((System.ComponentModel.ISupportInitialize)(this.usersGrid)).EndInit();
            this.actionsPanel.ResumeLayout(false);
            this.fieldsLayout.ResumeLayout(false);
            this.fieldsLayout.PerformLayout();
            this.rootLayout.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
    }
}
