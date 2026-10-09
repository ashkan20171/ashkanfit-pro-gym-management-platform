namespace AshkanFitPro
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel designerContentHost;
        private System.Windows.Forms.Panel designerSidebarHost;
        private System.Windows.Forms.Panel designerMainHost;
        private System.Windows.Forms.Panel designerBrandBar;
        private System.Windows.Forms.Label designerBrandLabel;
        private System.Windows.Forms.Panel designerAccentLine;

        private void InitializeComponent()
        {
            this.designerContentHost = new System.Windows.Forms.Panel();
            this.designerSidebarHost = new System.Windows.Forms.Panel();
            this.designerMainHost = new System.Windows.Forms.Panel();
            this.designerBrandBar = new System.Windows.Forms.Panel();
            this.designerBrandLabel = new System.Windows.Forms.Label();
            this.designerAccentLine = new System.Windows.Forms.Panel();
            this.designerBrandBar.SuspendLayout();
            this.SuspendLayout();
            //
            // designerContentHost — all functional runtime controls are hosted here.
            //
            this.designerContentHost.BackColor = System.Drawing.Color.FromArgb(10, 19, 33);
            this.designerContentHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designerContentHost.Location = new System.Drawing.Point(0, 42);
            this.designerContentHost.Name = "designerContentHost";
            this.designerContentHost.Size = new System.Drawing.Size(960, 598);
            this.designerContentHost.TabIndex = 1;
            this.designerSidebarHost.Name = "designerSidebarHost";
            this.designerSidebarHost.BackColor = System.Drawing.Color.FromArgb(13, 25, 42);
            this.designerSidebarHost.Dock = System.Windows.Forms.DockStyle.Right;
            this.designerSidebarHost.Width = 250;
            this.designerSidebarHost.TabIndex = 0;
            this.designerMainHost.Name = "designerMainHost";
            this.designerMainHost.BackColor = System.Drawing.Color.FromArgb(8, 15, 28);
            this.designerMainHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designerMainHost.TabIndex = 1;
            this.designerContentHost.Controls.Add(this.designerMainHost);
            this.designerContentHost.Controls.Add(this.designerSidebarHost);
            //
            // designerBrandBar — editable in Visual Studio Form Designer.
            //
            this.designerBrandBar.BackColor = System.Drawing.Color.FromArgb(16, 29, 47);
            this.designerBrandBar.Controls.Add(this.designerBrandLabel);
            this.designerBrandBar.Controls.Add(this.designerAccentLine);
            this.designerBrandBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.designerBrandBar.Location = new System.Drawing.Point(0, 0);
            this.designerBrandBar.Name = "designerBrandBar";
            this.designerBrandBar.Size = new System.Drawing.Size(960, 42);
            this.designerBrandBar.TabIndex = 0;
            //
            // designerBrandLabel
            //
            this.designerBrandLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designerBrandLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.designerBrandLabel.ForeColor = System.Drawing.Color.FromArgb(42, 218, 185);
            this.designerBrandLabel.Location = new System.Drawing.Point(0, 0);
            this.designerBrandLabel.Name = "designerBrandLabel";
            this.designerBrandLabel.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.designerBrandLabel.Size = new System.Drawing.Size(960, 39);
            this.designerBrandLabel.TabIndex = 0;
            this.designerBrandLabel.Text = "ASHKANFIT PRO  /  MainForm";
            this.designerBrandLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // designerAccentLine
            //
            this.designerAccentLine.BackColor = System.Drawing.Color.FromArgb(42, 218, 185);
            this.designerAccentLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.designerAccentLine.Location = new System.Drawing.Point(0, 39);
            this.designerAccentLine.Name = "designerAccentLine";
            this.designerAccentLine.Size = new System.Drawing.Size(960, 3);
            this.designerAccentLine.TabIndex = 1;
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(10, 19, 33);
            this.ClientSize = new System.Drawing.Size(960, 640);
            this.Controls.Add(this.designerContentHost);
            this.Controls.Add(this.designerBrandBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AshkanFit Pro — MainForm";
            this.designerBrandBar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
    }
}
