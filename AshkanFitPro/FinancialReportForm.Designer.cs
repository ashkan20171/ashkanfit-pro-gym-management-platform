namespace AshkanFitPro
{
    partial class FinancialReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel root;
        private System.Windows.Forms.FlowLayoutPanel toolbar;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Label summaryLabel;
        private System.Windows.Forms.DataGridView reportGrid;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.root = new System.Windows.Forms.TableLayoutPanel();
            this.toolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.refreshButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.summaryLabel = new System.Windows.Forms.Label();
            this.reportGrid = new System.Windows.Forms.DataGridView();
            this.SuspendLayout();
            this.root.Dock = System.Windows.Forms.DockStyle.Fill;
            this.root.ColumnCount = 1;
            this.root.RowCount = 3;
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.root.Padding = new System.Windows.Forms.Padding(20);
            this.root.BackColor = System.Drawing.Color.FromArgb(12, 23, 39);
            this.toolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolbar.Padding = new System.Windows.Forms.Padding(4, 9, 4, 0);
            this.toolbar.Controls.Add(this.dateFrom);
            this.toolbar.Controls.Add(this.dateTo);
            this.toolbar.Controls.Add(this.refreshButton);
            this.toolbar.Controls.Add(this.exportButton);
            this.dateFrom.Width = 150;
            this.dateTo.Width = 150;
            this.refreshButton.Size = new System.Drawing.Size(140, 32);
            this.exportButton.Size = new System.Drawing.Size(140, 32);
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(20, 166, 151);
            this.exportButton.BackColor = System.Drawing.Color.FromArgb(43, 68, 91);
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.exportButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.summaryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.summaryLabel.ForeColor = System.Drawing.Color.White;
            this.summaryLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.reportGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportGrid.ReadOnly = true;
            this.reportGrid.AllowUserToAddRows = false;
            this.reportGrid.AllowUserToDeleteRows = false;
            this.reportGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.reportGrid.BackgroundColor = System.Drawing.Color.FromArgb(22, 39, 59);
            this.reportGrid.RowHeadersVisible = false;
            this.root.Controls.Add(this.toolbar, 0, 0);
            this.root.Controls.Add(this.summaryLabel, 0, 1);
            this.root.Controls.Add(this.reportGrid, 0, 2);
            this.Controls.Add(this.root);
            this.ClientSize = new System.Drawing.Size(1050, 670);
            this.MinimumSize = new System.Drawing.Size(780, 490);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }
    }
}
