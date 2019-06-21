namespace ArchiveSiteReBuilder
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebuiltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reBuildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reBuildOneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reBuildListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overwriteModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreExistingFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overwriteExistingFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysCheckFilesAfterParsingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatesLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkButton = new System.Windows.Forms.Button();
            this.domainTextBox = new System.Windows.Forms.TextBox();
            this.domainLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPages = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.previewButton = new System.Windows.Forms.Button();
            this.bgWorkerRebuild = new System.ComponentModel.BackgroundWorker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.timeMapList = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.previewBrowserButton = new System.Windows.Forms.Button();
            this.bgWorkerGetFilesList = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerCheck = new System.ComponentModel.BackgroundWorker();
            this.pagesCurrentLabel = new System.Windows.Forms.Label();
            this.pagesSlash = new System.Windows.Forms.Label();
            this.pagesMaxLabel = new System.Windows.Forms.Label();
            this.pagesStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.currentStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.allStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.allCurrentLabel = new System.Windows.Forms.Label();
            this.allSlash = new System.Windows.Forms.Label();
            this.allMaxLabel = new System.Windows.Forms.Label();
            this.cssStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.cssCurrentLabel = new System.Windows.Forms.Label();
            this.cssSlash = new System.Windows.Forms.Label();
            this.cssMaxLabel = new System.Windows.Forms.Label();
            this.jsStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.jsCurrentLabel = new System.Windows.Forms.Label();
            this.jsSlash = new System.Windows.Forms.Label();
            this.jsMaxLabel = new System.Windows.Forms.Label();
            this.imagesStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.imagesCurrentLabel = new System.Windows.Forms.Label();
            this.imagesSlash = new System.Windows.Forms.Label();
            this.imagesMaxLabel = new System.Windows.Forms.Label();
            this.bgWorkerInitRebuiltSites = new System.ComponentModel.BackgroundWorker();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.getFilesListButton = new System.Windows.Forms.Button();
            this.addToListButton = new System.Windows.Forms.Button();
            this.rebuildButton = new System.Windows.Forms.Button();
            this.mainMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPages)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pagesStatusGroupBox.SuspendLayout();
            this.currentStatusGroupBox.SuspendLayout();
            this.allStatusGroupBox.SuspendLayout();
            this.cssStatusGroupBox.SuspendLayout();
            this.jsStatusGroupBox.SuspendLayout();
            this.imagesStatusGroupBox.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.updateToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(684, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rebuiltToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // rebuiltToolStripMenuItem
            // 
            this.rebuiltToolStripMenuItem.Name = "rebuiltToolStripMenuItem";
            this.rebuiltToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.rebuiltToolStripMenuItem.Text = "ReBuilt WebSites";
            this.rebuiltToolStripMenuItem.Click += new System.EventHandler(this.rebuiltToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reBuildToolStripMenuItem,
            this.overwriteModeToolStripMenuItem,
            this.alwaysCheckFilesAfterParsingToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // reBuildToolStripMenuItem
            // 
            this.reBuildToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reBuildOneToolStripMenuItem,
            this.reBuildListToolStripMenuItem});
            this.reBuildToolStripMenuItem.Name = "reBuildToolStripMenuItem";
            this.reBuildToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.reBuildToolStripMenuItem.Text = "ReBuild Mode";
            // 
            // reBuildOneToolStripMenuItem
            // 
            this.reBuildOneToolStripMenuItem.Name = "reBuildOneToolStripMenuItem";
            this.reBuildOneToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.reBuildOneToolStripMenuItem.Text = "ReBuild One";
            // 
            // reBuildListToolStripMenuItem
            // 
            this.reBuildListToolStripMenuItem.Name = "reBuildListToolStripMenuItem";
            this.reBuildListToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.reBuildListToolStripMenuItem.Text = "ReBuild List";
            // 
            // overwriteModeToolStripMenuItem
            // 
            this.overwriteModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ignoreExistingFilesToolStripMenuItem,
            this.overwriteExistingFilesToolStripMenuItem});
            this.overwriteModeToolStripMenuItem.Name = "overwriteModeToolStripMenuItem";
            this.overwriteModeToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.overwriteModeToolStripMenuItem.Text = "Overwrite Mode";
            // 
            // ignoreExistingFilesToolStripMenuItem
            // 
            this.ignoreExistingFilesToolStripMenuItem.CheckOnClick = true;
            this.ignoreExistingFilesToolStripMenuItem.Name = "ignoreExistingFilesToolStripMenuItem";
            this.ignoreExistingFilesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.ignoreExistingFilesToolStripMenuItem.Text = "Ignore Existing Files";
            // 
            // overwriteExistingFilesToolStripMenuItem
            // 
            this.overwriteExistingFilesToolStripMenuItem.CheckOnClick = true;
            this.overwriteExistingFilesToolStripMenuItem.Name = "overwriteExistingFilesToolStripMenuItem";
            this.overwriteExistingFilesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.overwriteExistingFilesToolStripMenuItem.Text = "Overwrite Existing Files";
            // 
            // alwaysCheckFilesAfterParsingToolStripMenuItem
            // 
            this.alwaysCheckFilesAfterParsingToolStripMenuItem.CheckOnClick = true;
            this.alwaysCheckFilesAfterParsingToolStripMenuItem.Name = "alwaysCheckFilesAfterParsingToolStripMenuItem";
            this.alwaysCheckFilesAfterParsingToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.alwaysCheckFilesAfterParsingToolStripMenuItem.Text = "Always check files after parsing";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdateToolStripMenuItem,
            this.updatesLogToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.updateToolStripMenuItem.Text = "Help";
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.checkForUpdateToolStripMenuItem.Text = "Check for Update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // updatesLogToolStripMenuItem
            // 
            this.updatesLogToolStripMenuItem.Name = "updatesLogToolStripMenuItem";
            this.updatesLogToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.updatesLogToolStripMenuItem.Text = "Change Log";
            this.updatesLogToolStripMenuItem.Click += new System.EventHandler(this.updatesLogToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkButton);
            this.groupBox1.Controls.Add(this.domainTextBox);
            this.groupBox1.Controls.Add(this.domainLabel);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 55);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Domain Settings:";
            // 
            // checkButton
            // 
            this.checkButton.Location = new System.Drawing.Point(566, 19);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(88, 23);
            this.checkButton.TabIndex = 2;
            this.checkButton.Text = "SCAN!";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // domainTextBox
            // 
            this.domainTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.domainTextBox.Location = new System.Drawing.Point(63, 20);
            this.domainTextBox.Name = "domainTextBox";
            this.domainTextBox.Size = new System.Drawing.Size(497, 22);
            this.domainTextBox.TabIndex = 1;
            this.domainTextBox.Text = "example.com";
            // 
            // domainLabel
            // 
            this.domainLabel.AutoSize = true;
            this.domainLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.domainLabel.Location = new System.Drawing.Point(4, 24);
            this.domainLabel.Name = "domainLabel";
            this.domainLabel.Size = new System.Drawing.Size(53, 15);
            this.domainLabel.TabIndex = 0;
            this.domainLabel.Text = "Domain:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPages);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 218);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(660, 260);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Domain Pages:";
            // 
            // dgvPages
            // 
            this.dgvPages.AllowUserToAddRows = false;
            this.dgvPages.AllowUserToDeleteRows = false;
            this.dgvPages.AllowUserToResizeColumns = false;
            this.dgvPages.AllowUserToResizeRows = false;
            this.dgvPages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPages.Location = new System.Drawing.Point(3, 19);
            this.dgvPages.MultiSelect = false;
            this.dgvPages.Name = "dgvPages";
            this.dgvPages.ReadOnly = true;
            this.dgvPages.RowHeadersVisible = false;
            this.dgvPages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPages.Size = new System.Drawing.Size(654, 238);
            this.dgvPages.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 559);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(684, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(38, 17);
            this.statusLabel.Text = "status";
            // 
            // previewButton
            // 
            this.previewButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewButton.Location = new System.Drawing.Point(84, 91);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(94, 25);
            this.previewButton.TabIndex = 6;
            this.previewButton.Text = "Preview Box";
            this.previewButton.UseVisualStyleBackColor = true;
            this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.timeMapList);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 115);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(660, 100);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Snapshots:";
            // 
            // timeMapList
            // 
            this.timeMapList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeMapList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeMapList.FormattingEnabled = true;
            this.timeMapList.Location = new System.Drawing.Point(3, 19);
            this.timeMapList.MultiColumn = true;
            this.timeMapList.Name = "timeMapList";
            this.timeMapList.Size = new System.Drawing.Size(654, 78);
            this.timeMapList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Preview in:";
            // 
            // previewBrowserButton
            // 
            this.previewBrowserButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewBrowserButton.Location = new System.Drawing.Point(184, 91);
            this.previewBrowserButton.Name = "previewBrowserButton";
            this.previewBrowserButton.Size = new System.Drawing.Size(109, 25);
            this.previewBrowserButton.TabIndex = 16;
            this.previewBrowserButton.Text = "External Browser";
            this.previewBrowserButton.UseVisualStyleBackColor = true;
            this.previewBrowserButton.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // pagesCurrentLabel
            // 
            this.pagesCurrentLabel.AutoSize = true;
            this.pagesCurrentLabel.Location = new System.Drawing.Point(10, 16);
            this.pagesCurrentLabel.Margin = new System.Windows.Forms.Padding(0);
            this.pagesCurrentLabel.Name = "pagesCurrentLabel";
            this.pagesCurrentLabel.Size = new System.Drawing.Size(49, 15);
            this.pagesCurrentLabel.TabIndex = 16;
            this.pagesCurrentLabel.Text = "999999";
            // 
            // pagesSlash
            // 
            this.pagesSlash.AutoSize = true;
            this.pagesSlash.Location = new System.Drawing.Point(59, 16);
            this.pagesSlash.Margin = new System.Windows.Forms.Padding(0);
            this.pagesSlash.Name = "pagesSlash";
            this.pagesSlash.Size = new System.Drawing.Size(13, 15);
            this.pagesSlash.TabIndex = 17;
            this.pagesSlash.Text = "/";
            // 
            // pagesMaxLabel
            // 
            this.pagesMaxLabel.AutoSize = true;
            this.pagesMaxLabel.Location = new System.Drawing.Point(72, 16);
            this.pagesMaxLabel.Margin = new System.Windows.Forms.Padding(0);
            this.pagesMaxLabel.Name = "pagesMaxLabel";
            this.pagesMaxLabel.Size = new System.Drawing.Size(49, 15);
            this.pagesMaxLabel.TabIndex = 18;
            this.pagesMaxLabel.Text = "999999";
            // 
            // pagesStatusGroupBox
            // 
            this.pagesStatusGroupBox.Controls.Add(this.pagesCurrentLabel);
            this.pagesStatusGroupBox.Controls.Add(this.pagesSlash);
            this.pagesStatusGroupBox.Controls.Add(this.pagesMaxLabel);
            this.pagesStatusGroupBox.Location = new System.Drawing.Point(7, 19);
            this.pagesStatusGroupBox.Name = "pagesStatusGroupBox";
            this.pagesStatusGroupBox.Size = new System.Drawing.Size(124, 36);
            this.pagesStatusGroupBox.TabIndex = 30;
            this.pagesStatusGroupBox.TabStop = false;
            this.pagesStatusGroupBox.Text = "Pages:";
            // 
            // currentStatusGroupBox
            // 
            this.currentStatusGroupBox.Controls.Add(this.allStatusGroupBox);
            this.currentStatusGroupBox.Controls.Add(this.cssStatusGroupBox);
            this.currentStatusGroupBox.Controls.Add(this.jsStatusGroupBox);
            this.currentStatusGroupBox.Controls.Add(this.imagesStatusGroupBox);
            this.currentStatusGroupBox.Controls.Add(this.pagesStatusGroupBox);
            this.currentStatusGroupBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.currentStatusGroupBox.Location = new System.Drawing.Point(12, 484);
            this.currentStatusGroupBox.Name = "currentStatusGroupBox";
            this.currentStatusGroupBox.Size = new System.Drawing.Size(660, 65);
            this.currentStatusGroupBox.TabIndex = 17;
            this.currentStatusGroupBox.TabStop = false;
            this.currentStatusGroupBox.Text = "Current status:";
            // 
            // allStatusGroupBox
            // 
            this.allStatusGroupBox.Controls.Add(this.allCurrentLabel);
            this.allStatusGroupBox.Controls.Add(this.allSlash);
            this.allStatusGroupBox.Controls.Add(this.allMaxLabel);
            this.allStatusGroupBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.allStatusGroupBox.Location = new System.Drawing.Point(527, 19);
            this.allStatusGroupBox.Name = "allStatusGroupBox";
            this.allStatusGroupBox.Size = new System.Drawing.Size(124, 36);
            this.allStatusGroupBox.TabIndex = 33;
            this.allStatusGroupBox.TabStop = false;
            this.allStatusGroupBox.Text = "All:";
            // 
            // allCurrentLabel
            // 
            this.allCurrentLabel.AutoSize = true;
            this.allCurrentLabel.Location = new System.Drawing.Point(10, 16);
            this.allCurrentLabel.Margin = new System.Windows.Forms.Padding(0);
            this.allCurrentLabel.Name = "allCurrentLabel";
            this.allCurrentLabel.Size = new System.Drawing.Size(49, 15);
            this.allCurrentLabel.TabIndex = 16;
            this.allCurrentLabel.Text = "999999";
            // 
            // allSlash
            // 
            this.allSlash.AutoSize = true;
            this.allSlash.Location = new System.Drawing.Point(59, 16);
            this.allSlash.Margin = new System.Windows.Forms.Padding(0);
            this.allSlash.Name = "allSlash";
            this.allSlash.Size = new System.Drawing.Size(13, 15);
            this.allSlash.TabIndex = 17;
            this.allSlash.Text = "/";
            // 
            // allMaxLabel
            // 
            this.allMaxLabel.AutoSize = true;
            this.allMaxLabel.Location = new System.Drawing.Point(72, 16);
            this.allMaxLabel.Margin = new System.Windows.Forms.Padding(0);
            this.allMaxLabel.Name = "allMaxLabel";
            this.allMaxLabel.Size = new System.Drawing.Size(49, 15);
            this.allMaxLabel.TabIndex = 18;
            this.allMaxLabel.Text = "999999";
            // 
            // cssStatusGroupBox
            // 
            this.cssStatusGroupBox.Controls.Add(this.cssCurrentLabel);
            this.cssStatusGroupBox.Controls.Add(this.cssSlash);
            this.cssStatusGroupBox.Controls.Add(this.cssMaxLabel);
            this.cssStatusGroupBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.cssStatusGroupBox.Location = new System.Drawing.Point(397, 19);
            this.cssStatusGroupBox.Name = "cssStatusGroupBox";
            this.cssStatusGroupBox.Size = new System.Drawing.Size(124, 36);
            this.cssStatusGroupBox.TabIndex = 31;
            this.cssStatusGroupBox.TabStop = false;
            this.cssStatusGroupBox.Text = ".css files:";
            // 
            // cssCurrentLabel
            // 
            this.cssCurrentLabel.AutoSize = true;
            this.cssCurrentLabel.Location = new System.Drawing.Point(10, 16);
            this.cssCurrentLabel.Margin = new System.Windows.Forms.Padding(0);
            this.cssCurrentLabel.Name = "cssCurrentLabel";
            this.cssCurrentLabel.Size = new System.Drawing.Size(49, 15);
            this.cssCurrentLabel.TabIndex = 16;
            this.cssCurrentLabel.Text = "999999";
            // 
            // cssSlash
            // 
            this.cssSlash.AutoSize = true;
            this.cssSlash.Location = new System.Drawing.Point(59, 16);
            this.cssSlash.Margin = new System.Windows.Forms.Padding(0);
            this.cssSlash.Name = "cssSlash";
            this.cssSlash.Size = new System.Drawing.Size(13, 15);
            this.cssSlash.TabIndex = 17;
            this.cssSlash.Text = "/";
            // 
            // cssMaxLabel
            // 
            this.cssMaxLabel.AutoSize = true;
            this.cssMaxLabel.Location = new System.Drawing.Point(72, 16);
            this.cssMaxLabel.Margin = new System.Windows.Forms.Padding(0);
            this.cssMaxLabel.Name = "cssMaxLabel";
            this.cssMaxLabel.Size = new System.Drawing.Size(49, 15);
            this.cssMaxLabel.TabIndex = 18;
            this.cssMaxLabel.Text = "999999";
            // 
            // jsStatusGroupBox
            // 
            this.jsStatusGroupBox.Controls.Add(this.jsCurrentLabel);
            this.jsStatusGroupBox.Controls.Add(this.jsSlash);
            this.jsStatusGroupBox.Controls.Add(this.jsMaxLabel);
            this.jsStatusGroupBox.Location = new System.Drawing.Point(267, 19);
            this.jsStatusGroupBox.Name = "jsStatusGroupBox";
            this.jsStatusGroupBox.Size = new System.Drawing.Size(124, 36);
            this.jsStatusGroupBox.TabIndex = 32;
            this.jsStatusGroupBox.TabStop = false;
            this.jsStatusGroupBox.Text = ".js files:";
            // 
            // jsCurrentLabel
            // 
            this.jsCurrentLabel.AutoSize = true;
            this.jsCurrentLabel.Location = new System.Drawing.Point(10, 16);
            this.jsCurrentLabel.Margin = new System.Windows.Forms.Padding(0);
            this.jsCurrentLabel.Name = "jsCurrentLabel";
            this.jsCurrentLabel.Size = new System.Drawing.Size(49, 15);
            this.jsCurrentLabel.TabIndex = 16;
            this.jsCurrentLabel.Text = "999999";
            // 
            // jsSlash
            // 
            this.jsSlash.AutoSize = true;
            this.jsSlash.Location = new System.Drawing.Point(59, 16);
            this.jsSlash.Margin = new System.Windows.Forms.Padding(0);
            this.jsSlash.Name = "jsSlash";
            this.jsSlash.Size = new System.Drawing.Size(13, 15);
            this.jsSlash.TabIndex = 17;
            this.jsSlash.Text = "/";
            // 
            // jsMaxLabel
            // 
            this.jsMaxLabel.AutoSize = true;
            this.jsMaxLabel.Location = new System.Drawing.Point(72, 16);
            this.jsMaxLabel.Margin = new System.Windows.Forms.Padding(0);
            this.jsMaxLabel.Name = "jsMaxLabel";
            this.jsMaxLabel.Size = new System.Drawing.Size(49, 15);
            this.jsMaxLabel.TabIndex = 18;
            this.jsMaxLabel.Text = "999999";
            // 
            // imagesStatusGroupBox
            // 
            this.imagesStatusGroupBox.Controls.Add(this.imagesCurrentLabel);
            this.imagesStatusGroupBox.Controls.Add(this.imagesSlash);
            this.imagesStatusGroupBox.Controls.Add(this.imagesMaxLabel);
            this.imagesStatusGroupBox.Location = new System.Drawing.Point(137, 19);
            this.imagesStatusGroupBox.Name = "imagesStatusGroupBox";
            this.imagesStatusGroupBox.Size = new System.Drawing.Size(124, 36);
            this.imagesStatusGroupBox.TabIndex = 31;
            this.imagesStatusGroupBox.TabStop = false;
            this.imagesStatusGroupBox.Text = "Images:";
            // 
            // imagesCurrentLabel
            // 
            this.imagesCurrentLabel.AutoSize = true;
            this.imagesCurrentLabel.Location = new System.Drawing.Point(10, 16);
            this.imagesCurrentLabel.Margin = new System.Windows.Forms.Padding(0);
            this.imagesCurrentLabel.Name = "imagesCurrentLabel";
            this.imagesCurrentLabel.Size = new System.Drawing.Size(49, 15);
            this.imagesCurrentLabel.TabIndex = 16;
            this.imagesCurrentLabel.Text = "999999";
            // 
            // imagesSlash
            // 
            this.imagesSlash.AutoSize = true;
            this.imagesSlash.Location = new System.Drawing.Point(59, 16);
            this.imagesSlash.Margin = new System.Windows.Forms.Padding(0);
            this.imagesSlash.Name = "imagesSlash";
            this.imagesSlash.Size = new System.Drawing.Size(13, 15);
            this.imagesSlash.TabIndex = 17;
            this.imagesSlash.Text = "/";
            // 
            // imagesMaxLabel
            // 
            this.imagesMaxLabel.AutoSize = true;
            this.imagesMaxLabel.Location = new System.Drawing.Point(72, 16);
            this.imagesMaxLabel.Margin = new System.Windows.Forms.Padding(0);
            this.imagesMaxLabel.Name = "imagesMaxLabel";
            this.imagesMaxLabel.Size = new System.Drawing.Size(49, 15);
            this.imagesMaxLabel.TabIndex = 18;
            this.imagesMaxLabel.Text = "999999";
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.getFilesListButton);
            this.flowLayoutPanel.Controls.Add(this.addToListButton);
            this.flowLayoutPanel.Controls.Add(this.rebuildButton);
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel.Location = new System.Drawing.Point(307, 88);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(365, 30);
            this.flowLayoutPanel.TabIndex = 3;
            // 
            // getFilesListButton
            // 
            this.getFilesListButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getFilesListButton.Location = new System.Drawing.Point(247, 3);
            this.getFilesListButton.Name = "getFilesListButton";
            this.getFilesListButton.Size = new System.Drawing.Size(115, 25);
            this.getFilesListButton.TabIndex = 3;
            this.getFilesListButton.Text = "Parse Files";
            this.getFilesListButton.UseVisualStyleBackColor = true;
            this.getFilesListButton.Click += new System.EventHandler(this.getFilesListButton_Click);
            // 
            // addToListButton
            // 
            this.addToListButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addToListButton.Location = new System.Drawing.Point(126, 3);
            this.addToListButton.Name = "addToListButton";
            this.addToListButton.Size = new System.Drawing.Size(115, 25);
            this.addToListButton.TabIndex = 4;
            this.addToListButton.Text = "Add to list";
            this.addToListButton.UseVisualStyleBackColor = true;
            this.addToListButton.Click += new System.EventHandler(this.addToListButton_Click);
            // 
            // rebuildButton
            // 
            this.rebuildButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rebuildButton.Location = new System.Drawing.Point(5, 3);
            this.rebuildButton.Name = "rebuildButton";
            this.rebuildButton.Size = new System.Drawing.Size(115, 25);
            this.rebuildButton.TabIndex = 5;
            this.rebuildButton.Text = "ReBuild!";
            this.rebuildButton.UseVisualStyleBackColor = true;
            this.rebuildButton.Click += new System.EventHandler(this.rebuildButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 581);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.currentStatusGroupBox);
            this.Controls.Add(this.previewBrowserButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.previewButton);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Archive Site ReBuilder";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPages)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.pagesStatusGroupBox.ResumeLayout(false);
            this.pagesStatusGroupBox.PerformLayout();
            this.currentStatusGroupBox.ResumeLayout(false);
            this.allStatusGroupBox.ResumeLayout(false);
            this.allStatusGroupBox.PerformLayout();
            this.cssStatusGroupBox.ResumeLayout(false);
            this.cssStatusGroupBox.PerformLayout();
            this.jsStatusGroupBox.ResumeLayout(false);
            this.jsStatusGroupBox.PerformLayout();
            this.imagesStatusGroupBox.ResumeLayout(false);
            this.imagesStatusGroupBox.PerformLayout();
            this.flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button checkButton;
        private System.Windows.Forms.TextBox domainTextBox;
        private System.Windows.Forms.Label domainLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvPages;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Button previewButton;
        private System.ComponentModel.BackgroundWorker bgWorkerRebuild;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox timeMapList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button previewBrowserButton;
        private System.ComponentModel.BackgroundWorker bgWorkerGetFilesList;
        private System.ComponentModel.BackgroundWorker bgWorkerCheck;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatesLogToolStripMenuItem;
        private System.Windows.Forms.Label pagesCurrentLabel;
        private System.Windows.Forms.Label pagesSlash;
        private System.Windows.Forms.Label pagesMaxLabel;
        private System.Windows.Forms.GroupBox pagesStatusGroupBox;
        private System.Windows.Forms.GroupBox currentStatusGroupBox;
        private System.Windows.Forms.GroupBox allStatusGroupBox;
        private System.Windows.Forms.Label allCurrentLabel;
        private System.Windows.Forms.Label allSlash;
        private System.Windows.Forms.Label allMaxLabel;
        private System.Windows.Forms.GroupBox cssStatusGroupBox;
        private System.Windows.Forms.Label cssCurrentLabel;
        private System.Windows.Forms.Label cssSlash;
        private System.Windows.Forms.Label cssMaxLabel;
        private System.Windows.Forms.GroupBox jsStatusGroupBox;
        private System.Windows.Forms.Label jsCurrentLabel;
        private System.Windows.Forms.Label jsSlash;
        private System.Windows.Forms.Label jsMaxLabel;
        private System.Windows.Forms.GroupBox imagesStatusGroupBox;
        private System.Windows.Forms.Label imagesCurrentLabel;
        private System.Windows.Forms.Label imagesSlash;
        private System.Windows.Forms.Label imagesMaxLabel;
        private System.Windows.Forms.ToolStripMenuItem rebuiltToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgWorkerInitRebuiltSites;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overwriteModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoreExistingFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overwriteExistingFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reBuildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reBuildOneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reBuildListToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button addToListButton;
        private System.Windows.Forms.Button getFilesListButton;
        private System.Windows.Forms.Button rebuildButton;
        private System.Windows.Forms.ToolStripMenuItem alwaysCheckFilesAfterParsingToolStripMenuItem;
    }
}

