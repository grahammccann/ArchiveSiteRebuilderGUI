namespace ArchiveSiteReBuilder
{
    partial class RebuiltForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RebuiltForm));
            this.rebuiltDgv = new System.Windows.Forms.DataGridView();
            this.previewBrowserButton = new System.Windows.Forms.Button();
            this.previewBoxButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.rebuiltDgv)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rebuiltDgv
            // 
            this.rebuiltDgv.AllowUserToAddRows = false;
            this.rebuiltDgv.AllowUserToDeleteRows = false;
            this.rebuiltDgv.AllowUserToResizeColumns = false;
            this.rebuiltDgv.AllowUserToResizeRows = false;
            this.rebuiltDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.rebuiltDgv.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.rebuiltDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rebuiltDgv.Dock = System.Windows.Forms.DockStyle.Top;
            this.rebuiltDgv.Location = new System.Drawing.Point(0, 0);
            this.rebuiltDgv.Name = "rebuiltDgv";
            this.rebuiltDgv.RowHeadersVisible = false;
            this.rebuiltDgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rebuiltDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rebuiltDgv.Size = new System.Drawing.Size(353, 424);
            this.rebuiltDgv.TabIndex = 0;
            // 
            // previewBrowserButton
            // 
            this.previewBrowserButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewBrowserButton.Location = new System.Drawing.Point(232, 430);
            this.previewBrowserButton.Name = "previewBrowserButton";
            this.previewBrowserButton.Size = new System.Drawing.Size(109, 35);
            this.previewBrowserButton.TabIndex = 1;
            this.previewBrowserButton.Text = "External Browser";
            this.previewBrowserButton.UseVisualStyleBackColor = true;
            this.previewBrowserButton.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // previewBoxButton
            // 
            this.previewBoxButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewBoxButton.Location = new System.Drawing.Point(12, 430);
            this.previewBoxButton.Name = "previewBoxButton";
            this.previewBoxButton.Size = new System.Drawing.Size(109, 35);
            this.previewBoxButton.TabIndex = 2;
            this.previewBoxButton.Text = "Preview Box";
            this.previewBoxButton.UseVisualStyleBackColor = true;
            this.previewBoxButton.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 471);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(353, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(38, 17);
            this.statusLabel.Text = "status";
            // 
            // RebuiltForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 493);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.previewBoxButton);
            this.Controls.Add(this.previewBrowserButton);
            this.Controls.Add(this.rebuiltDgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RebuiltForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReBuilt Web Sites";
            ((System.ComponentModel.ISupportInitialize)(this.rebuiltDgv)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView rebuiltDgv;
        private System.Windows.Forms.Button previewBrowserButton;
        private System.Windows.Forms.Button previewBoxButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}