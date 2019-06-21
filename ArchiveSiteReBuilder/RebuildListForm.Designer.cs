namespace ArchiveSiteReBuilder
{
    partial class RebuildListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RebuildListForm));
            this.rebuildButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dashboardDgv = new System.Windows.Forms.DataGridView();
            this.previewButton = new System.Windows.Forms.Button();
            this.previewBrowserButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // rebuildButton
            // 
            this.rebuildButton.Location = new System.Drawing.Point(12, 381);
            this.rebuildButton.Name = "rebuildButton";
            this.rebuildButton.Size = new System.Drawing.Size(100, 35);
            this.rebuildButton.TabIndex = 1;
            this.rebuildButton.Text = "ReBuild!";
            this.rebuildButton.UseVisualStyleBackColor = true;
            this.rebuildButton.Click += new System.EventHandler(this.rebuildButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(592, 381);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(100, 35);
            this.backButton.TabIndex = 2;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 423);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(704, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(38, 17);
            this.statusLabel.Text = "status";
            // 
            // dashboardDgv
            // 
            this.dashboardDgv.AllowUserToAddRows = false;
            this.dashboardDgv.AllowUserToDeleteRows = false;
            this.dashboardDgv.AllowUserToResizeColumns = false;
            this.dashboardDgv.AllowUserToResizeRows = false;
            this.dashboardDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dashboardDgv.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dashboardDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dashboardDgv.Dock = System.Windows.Forms.DockStyle.Top;
            this.dashboardDgv.Location = new System.Drawing.Point(0, 0);
            this.dashboardDgv.MultiSelect = false;
            this.dashboardDgv.Name = "dashboardDgv";
            this.dashboardDgv.ReadOnly = true;
            this.dashboardDgv.RowHeadersVisible = false;
            this.dashboardDgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dashboardDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dashboardDgv.Size = new System.Drawing.Size(704, 375);
            this.dashboardDgv.TabIndex = 0;
            // 
            // previewButton
            // 
            this.previewButton.Location = new System.Drawing.Point(256, 381);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(100, 35);
            this.previewButton.TabIndex = 4;
            this.previewButton.Text = "Preview Box";
            this.previewButton.UseVisualStyleBackColor = true;
            // 
            // previewBrowserButton
            // 
            this.previewBrowserButton.Location = new System.Drawing.Point(362, 381);
            this.previewBrowserButton.Name = "previewBrowserButton";
            this.previewBrowserButton.Size = new System.Drawing.Size(100, 35);
            this.previewBrowserButton.TabIndex = 5;
            this.previewBrowserButton.Text = "External Browser";
            this.previewBrowserButton.UseVisualStyleBackColor = true;
            // 
            // RebuildListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 445);
            this.Controls.Add(this.previewBrowserButton);
            this.Controls.Add(this.previewButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.rebuildButton);
            this.Controls.Add(this.dashboardDgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RebuildListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rebuild List";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardDgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button rebuildButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.DataGridView dashboardDgv;
        private System.Windows.Forms.Button previewButton;
        private System.Windows.Forms.Button previewBrowserButton;
    }
}