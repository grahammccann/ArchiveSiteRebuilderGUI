namespace ArchiveSiteReBuilder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Forms;
    using System.Net;
    using System.IO;
    using System.Drawing;
    using ArchiveSiteReBuilder.Lib;
    using System.Threading.Tasks;
    using System.Threading;

    public partial class MainForm : Form
    {        
        private bool _showUpdateMessage = false;

        private WebSites _webSites;
        private string _currentSnapshotDate = string.Empty;
        private bool _isFilesParsed = false;

        Progress<KeyValuePair<string, KeyValuePair<string, int>>> _progressIndicator;
        CancellationTokenSource _cancellationTokenSource;

        private void ReturnMessage(string message)
        {
            MessageBox.Show(message, Constants.Common.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void CheckForUpdates()
        {
            var wc = new WebClient();
            try
            {
                var newV = wc.DownloadString(Constants.Common.AppUpdateStatusUrl);
                if (Application.ProductVersion != newV)
                {
                    if (MessageBox.Show(@"A new update is available! latest version is: " + newV + Environment.NewLine +
                                        Environment.NewLine + @"Would you like to update now?",
                                        @"Wraith Archive Site ReBuilder Update Ready...", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;

                    wc.DownloadFile(Constants.Common.AppUpdateUrl, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\wraith-archive-site-rebuilder.rar");

                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\wraith-archive-site-rebuilder.rar"))
                    {
                        ReturnMessage(@"Update saved to: " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\wraith-archive-site-rebuilder.rar");
                    }
                }
                else
                {
                    if (_showUpdateMessage)
                        ReturnMessage(@"No new updates yet!");

                    _showUpdateMessage = true;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage(ex.ToString());
            }
        }

        public MainForm()
        {
            InitializeComponent();
            
            AcceptButton = checkButton;
            FormClosed += MainForm_FormClosed;

            _progressIndicator = new Progress<KeyValuePair<string, KeyValuePair<string, int>>>(ReportProgress);
            _cancellationTokenSource = new CancellationTokenSource();

            InitializeWebSites();
            InitializeForm();
            InitializeSettings();
            InitializePagesGrid();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            var softVersion = Application.ProductVersion;
            Text = @"Wraith Archive Site ReBuilder v" + softVersion + @" - graham23s@Hotmail.com";

            CheckForUpdates();
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_webSites.UpdateLocalBase())
                MessageBox.Show(@"Local websites database wasn't updated!", Constants.Common.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);   
        }

        #region Init functions
        private void InitializeForm()
        {
            statusLabel.Text = string.Empty;
            ResetCurrentCounters();
            ResetMaxCounters();

            getFilesListButton.Enabled = false;
            addToListButton.Enabled = false;
            rebuildButton.Enabled = false;
            previewButton.Enabled = false;
            previewBrowserButton.Enabled = false;

            timeMapList.SelectedIndexChanged += timeMapList_SelectedIndexChanged;
            timeMapList.CheckOnClick = true;
            timeMapList.Enabled = false;

            settingsToolStripMenuItem.Visible = false;
            aboutToolStripMenuItem.Visible = false;
#if DEBUG
            var autoCompleteStrings = new AutoCompleteStringCollection();

            autoCompleteStrings.AddRange(Constants.Test.TestDomains);

            domainTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            domainTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            domainTextBox.AutoCompleteCustomSource = autoCompleteStrings;
#endif
        }
        private void InitializeSettings()
        {
            reBuildOneToolStripMenuItem.Checked = true;
            reBuildListToolStripMenuItem.Checked = false;

            reBuildToolStripMenuItem.DropDownItemClicked += reBuildToolStripMenuItem_DropDownItemClicked;

            addToListButton.Visible = false;
            
            ignoreExistingFilesToolStripMenuItem.Checked = true;
            overwriteExistingFilesToolStripMenuItem.Checked = false;
            overwriteModeToolStripMenuItem.DropDownItemClicked += overwriteModeToolStripMenuItem_DropDownItemClicked;

            alwaysCheckFilesAfterParsingToolStripMenuItem.Checked = false;
            alwaysCheckFilesAfterParsingToolStripMenuItem.CheckStateChanged += alwaysCheckFilesAfterParsingToolStripMenuItem_CheckStateChanged;
        }
        private void InitializePagesGrid()
        {
            var linksCol = new DataGridViewTextBoxColumn()
            {
                Name = "pageLink",
                HeaderText = @"Page Link"
            };
            var savedCol = new DataGridViewTextBoxColumn()
            {
                Name = "saveStatus",
                HeaderText = @"Saved?"
            };

            dgvPages.Columns.AddRange(linksCol, savedCol);
            if (dgvPages.Columns["saveStatus"] != null) 
                dgvPages.Columns["saveStatus"].Width = 50;
        }
        private async void InitializeWebSites()
        {
            rebuiltToolStripMenuItem.Enabled = false;
            checkButton.Enabled = false;
            statusLabel.Text = @"Check for rebuilt websites...";

            _webSites = new WebSites();
            await Task.Run(() => _webSites.CheckForRebuiltWebSitesAsync());

            rebuiltToolStripMenuItem.Enabled = true;
            checkButton.Enabled = true;
            var count = _webSites.Lists.ContainsKey("rebuilt") ? _webSites.Lists["rebuilt"].Count : 0;
            statusLabel.Text = @"You have " + count + (count == 1 ? @" rebuilt website." : @" rebuilt websites.");

        }
        #endregion // Init functions

        #region ToolStripMenu
        private void rebuiltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var rebuiltForm = new RebuiltForm(_webSites))
                rebuiltForm.ShowDialog();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void reBuildToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var clickedItem = e.ClickedItem as ToolStripMenuItem;

            if (clickedItem == null) return;

            var rebuildMode = Constants.Settings.RebuildMode.RebuildOne;

            switch (clickedItem.Name)
            {
                case "reBuildListToolStripMenuItem":
                    reBuildOneToolStripMenuItem.CheckState = CheckState.Unchecked;
                    reBuildListToolStripMenuItem.CheckState = CheckState.Checked;
                    addToListButton.Visible = true;
                    rebuildMode = Constants.Settings.RebuildMode.RebuildList;
                    break;
                case "reBuildOneToolStripMenuItem":
                default:
                    reBuildOneToolStripMenuItem.CheckState = CheckState.Checked;
                    reBuildListToolStripMenuItem.CheckState = CheckState.Unchecked;
                    addToListButton.Visible = false;
                    break;
            }

            _webSites.AsrSettings.RebuildMode = rebuildMode;
        }
        private void overwriteModeToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var clickedItem = e.ClickedItem as ToolStripMenuItem;

            if (clickedItem == null) return;

            var overwriteMode = Constants.Settings.OverwriteMode.IgnoreExisting;

            switch (clickedItem.Name)
            {
                case "overwriteExistingFilesToolStripMenuItem":
                    ignoreExistingFilesToolStripMenuItem.CheckState = CheckState.Unchecked;
                    overwriteExistingFilesToolStripMenuItem.CheckState = CheckState.Checked;
                    overwriteMode = Constants.Settings.OverwriteMode.OverwriteExisting;
                    break;
                case "ignoreExistingFilesToolStripMenuItem":
                default:
                    ignoreExistingFilesToolStripMenuItem.CheckState = CheckState.Checked;
                    overwriteExistingFilesToolStripMenuItem.CheckState = CheckState.Unchecked;
                    break;
            }

            _webSites.AsrSettings.OverwriteMode = overwriteMode;
        }
        private void alwaysCheckFilesAfterParsingToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            _webSites.AsrSettings.AlwaysCheckFilesAfterParsing = alwaysCheckFilesAfterParsingToolStripMenuItem.Checked;
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckForUpdates();
        }
        private void updatesLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var changeLogForm = new ChangeLogForm())
                changeLogForm.ShowDialog();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion // ToolStripMenu

        #region Buttons clicks
        private async void checkButton_Click(object sender, EventArgs e)
        {
            dgvPages.Rows.Clear();
            ResetCurrentCounters();
            ResetMaxCounters();
            getFilesListButton.Text = @"Parse files";
            
            try
            {
                if (string.IsNullOrEmpty(domainTextBox.Text))
                    statusLabel.Text = @"The domain field can not be empty!";
                else
                {
                    AllControlsStatus(false);

                    await _webSites.AddNew("checked", domainTextBox.Text);

                    timeMapList.Items.Clear();

                    if (_webSites.Current.IsOnMaintenance)
                        statusLabel.Text = @"The Wayback Machine is offline for maintenance. Please try again later.";
                    else
                    {
                        statusLabel.Text = @"The domain " + _webSites.Current.DomainName;
                        if (_webSites.Current.IsAvailable)
                        {
                            statusLabel.Text += @" has snapshots available";
                            SetTimeMapList(_webSites.Current.DomainLists.TimeMapList);
                            timeMapList.Enabled = true;
                        }
                        else
                            statusLabel.Text += @" has no snapshots";

                        statusLabel.Text += (_webSites.Current.IsRebuilt) ? @" and has already been rebuilt!" : @"!";
                    }
                    domainTextBox.Enabled = true;
                    checkButton.Enabled = true;
                }
            }
            catch (Exception) { }
        }
        private async void getFilesListButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn == null) return;

            AllControlsStatus(false);
            if (btn.Text.Equals("Cancel"))
            {
                _cancellationTokenSource.Cancel();
                getFilesListButton.Text = (!_isFilesParsed) ? @"Parse files" : @"Check files";
                statusLabel.Text = (!_isFilesParsed) ? @"Parsing was canceled!" : @"Checking was canceled!";
            }
            else
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
                getFilesListButton.Text = @"Cancel";
                getFilesListButton.Enabled = true;
                try
                {
                    _webSites.Current.SetProgressAndCancellation(_progressIndicator, _cancellationTokenSource.Token);
                    if (_webSites.AsrSettings.AlwaysCheckFilesAfterParsing)
                    {
                        await ParseFilesAsync();
                        await CheckFilesAsync();
                    }
                    else
                    {
                        if (!_isFilesParsed)
                            await ParseFilesAsync();
                        else
                            await CheckFilesAsync();
                    }
                }
                catch (Exception) { }
            }
            AllControlsStatus(true);
        }
        private void addToListButton_Click(object sender, EventArgs e)
        {
            try
            {
                _webSites.ChangeWebSiteList("toRebuild", _webSites.Current);
                statusLabel.Text = @"The domain " + _webSites.Current.DomainName + @" was added to the rebuild list!";
            }
            catch (Exception) { statusLabel.Text = @"The domain " + _webSites.Current.DomainName + @" was not added to the rebuild list!"; }
        }
        private async void rebuildButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn == null) return;

            AllControlsStatus(false);

            if (btn.Text.Equals("Cancel"))
            {
                _cancellationTokenSource.Cancel();
                rebuildButton.Text = @"ReBuild!";
                statusLabel.Text = @"ReBuild was canceled!";
            }
            else
            {
                optionsToolStripMenuItem.Enabled = false;
                if (_webSites.AsrSettings.RebuildMode.Equals(Constants.Settings.RebuildMode.RebuildOne))
                {
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = new CancellationTokenSource();
                    rebuildButton.Text = @"Cancel";
                    rebuildButton.Enabled = true;

                    try
                    {
                        statusLabel.Text = @"ReBuild the domain " + _webSites.Current.DomainName + @" ...";
                        _webSites.Current.SetProgressAndCancellation(_progressIndicator, _cancellationTokenSource.Token);
                        await _webSites.Current.Rebuild(_webSites.AsrSettings.OverwriteMode);

                        if (!_cancellationTokenSource.Token.IsCancellationRequested)
                            UpdateViewAfterRebuild("one", true);
                        else
                            UpdateViewAfterRebuild("one");
                    }
                    catch (Exception)
                    {
                        statusLabel.Text = @"Error while rebuilding! Please try again.";
                        rebuildButton.Text = @"ReBuild!";
                    }
                }
                else
                {
                    using (var rebuildListForm = new RebuildListForm(_webSites))
                    {
                        if (rebuildListForm.ShowDialog().Equals(DialogResult.OK))
                            UpdateViewAfterRebuild("list", true);
                        else
                            UpdateViewAfterRebuild("list");
                    }
                }
            }
        }
        private void previewButton_Click(object sender, EventArgs e)
        {
            ShowPreview(sender.ToString());
        }
        #endregion // Buttons clicks

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isFinished"></param>
        private void UpdateViewAfterRebuild(string type, bool isFinished = false)
        {
            AllControlsStatus(true);
            optionsToolStripMenuItem.Enabled = true;

            if (!isFinished)
            {
                previewButton.Enabled = false;
                previewBrowserButton.Enabled = false;
                statusLabel.Text = @"ReBuild was canceled!";
                return;
            }

            if (type.Equals("one"))
            {
                // update dataGridView
                FillDataGridView(_webSites.Current.DomainLists.DownloadedFilesList);
                // check for rebuilt status
                if (_webSites.Current.IsRebuilt)
                {
                    _webSites.ChangeWebSiteList("rebuilt", _webSites.Current);

                    try
                    {
                        pagesCurrentLabel.Text = _webSites.Current.DomainLists.DownloadedFilesCountersList["pages"].ToString();
                        imagesCurrentLabel.Text = _webSites.Current.DomainLists.DownloadedFilesCountersList["images"].ToString();
                        jsCurrentLabel.Text = _webSites.Current.DomainLists.DownloadedFilesCountersList["js"].ToString();
                        cssCurrentLabel.Text = _webSites.Current.DomainLists.DownloadedFilesCountersList["css"].ToString();
                        allCurrentLabel.Text = _webSites.Current.DomainLists.DownloadedFilesCountersList["all"].ToString();
                    }
                    catch (Exception)
                    {
                        // if website was already rebuilt and overwrite mode set to "ignore existing"
                        // then DomainLists.DownloadedFilesCountersList will be empty
                    }

                    getFilesListButton.Enabled = false;

                    statusLabel.Text = @"The domain " + _webSites.Current.DomainName + @" was rebuilt.";
                }
                else // if something went wrong
                    statusLabel.Text = @"The domain " + _webSites.Current.DomainName + @" was not rebuilt.";

                rebuildButton.Enabled = false;
                rebuildButton.Text = @"ReBuild!";
            }
            else
            {
                statusLabel.Text = @"ReBuild was finished!";

                dgvPages.Rows.Clear();
                timeMapList.Items.Clear();
                domainTextBox.Clear();
                ResetCurrentCounters();
                ResetMaxCounters();

                getFilesListButton.Enabled = false;
                addToListButton.Enabled = false;
                rebuildButton.Enabled = false;
                previewButton.Enabled = false;
                previewBrowserButton.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task ParseFilesAsync()
        {
            statusLabel.Text = @"Parsing files ...";
            ResetCurrentCounters();

            _currentSnapshotDate = timeMapList.GetItemText(timeMapList.SelectedItem);

            try
            {
                await _webSites.Current.GetFilesListAsync(_currentSnapshotDate);

                if (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    FillDataGridView(_webSites.Current.GetAllFilesList());

                    pagesMaxLabel.Text = _webSites.Current.DomainLists.HtmlFilesList["available"].Count.ToString();
                    imagesMaxLabel.Text = _webSites.Current.DomainLists.ImgsList["available"].Count.ToString();
                    jsMaxLabel.Text = _webSites.Current.DomainLists.JsFilesList["available"].Count.ToString();
                    cssMaxLabel.Text = _webSites.Current.DomainLists.CssFilesList["available"].Count.ToString();
                    allMaxLabel.Text = _webSites.Current.GetAllFilesList().Count.ToString();

                    _isFilesParsed = true;
                    getFilesListButton.Text = @"Check files";
                    statusLabel.Text = @"Parsing was finished.";
                    return;
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception) { getFilesListButton.Text = @"Parse files"; }

            statusLabel.Text = @"Parsing was canceled.";
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task CheckFilesAsync()
        {
            statusLabel.Text = @"Checking files availability ...";
            ResetCurrentCounters();

            try
            {
                await _webSites.Current.CheckFilesListAsync();
                if (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await Task.Run(() => Parallel.ForEach(dgvPages.Rows.OfType<DataGridViewRow>(), row =>
                    {
                        // if operation was canceled throw an exception
                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                        var value = row.Cells[0].Value.ToString();
                        if (_webSites.Current.GetAllFilesList().All(item => item != value))
                            row.DefaultCellStyle.BackColor = Color.Red;
                    }), _cancellationTokenSource.Token);

                    _isFilesParsed = false;
                    getFilesListButton.Text = @"Parse files";

                    statusLabel.Text = @"Checking was finished.";
                    return;
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception) { getFilesListButton.Text = @"Check files"; }

            statusLabel.Text = @"Checking was canceled.";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeAndValue"></param>
        private void ReportProgress(KeyValuePair<string, KeyValuePair<string, int>> typeAndValue)
        {
            if (typeAndValue.Value.Key.Equals("html"))
                pagesCurrentLabel.Text = typeAndValue.Value.Value.ToString();
            else if (typeAndValue.Value.Key.Equals("js"))
                jsCurrentLabel.Text = typeAndValue.Value.Value.ToString();
            else if (typeAndValue.Value.Key.Equals("css"))
                cssCurrentLabel.Text = typeAndValue.Value.Value.ToString();
            else if (typeAndValue.Value.Key.Equals("images"))
                imagesCurrentLabel.Text = typeAndValue.Value.Value.ToString();

            allCurrentLabel.Text = (Int32.Parse(pagesCurrentLabel.Text) + Int32.Parse(jsCurrentLabel.Text) + 
                                    Int32.Parse(cssCurrentLabel.Text) + Int32.Parse(imagesCurrentLabel.Text)).ToString();
        }

        private void timeMapList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isFilesParsed = false;
            getFilesListButton.Text = @"Parse files";
            getFilesListButton.Enabled = true;
            
            if (timeMapList.CheckedItems.Count <= 1) return;
            // uncheck other items
            var selectedIndex = timeMapList.SelectedIndices[0];
            foreach (var checkedIndex in timeMapList.CheckedIndices.Cast<int>().Where(checkedIndex => checkedIndex != selectedIndex))
                timeMapList.SetItemChecked(checkedIndex, false);
        }

        /// <summary>
        /// The function enables or disables all controls on the form
        /// </summary>
        /// <param name="enabled">True or false</param>
        private void AllControlsStatus(bool enabled)
        {
            if (enabled)
            {
                checkButton.Enabled = true;
                domainTextBox.Enabled = true;
                timeMapList.Enabled = true;
                getFilesListButton.Enabled = true;
                addToListButton.Enabled = true;
                rebuildButton.Enabled = true;
                previewButton.Enabled = _webSites.Current.IsRebuilt;
                previewBrowserButton.Enabled = _webSites.Current.IsRebuilt;
            }
            else
            {
                checkButton.Enabled = false;
                getFilesListButton.Enabled = false;
                addToListButton.Enabled = false;
                rebuildButton.Enabled = false;
                previewButton.Enabled = false;
                domainTextBox.Enabled = false;
                timeMapList.Enabled = false;
                previewBrowserButton.Enabled = false;
            }
        }
        
        /// <summary>
        /// The function resets current counters
        /// </summary>
        private void ResetCurrentCounters()
        {
            pagesCurrentLabel.Text = @"0";
            jsCurrentLabel.Text = @"0";
            cssCurrentLabel.Text = @"0";
            imagesCurrentLabel.Text = @"0";
            allCurrentLabel.Text = @"0";
        }

        /// <summary>
        /// The function resets max counters
        /// </summary>
        private void ResetMaxCounters()
        {
            pagesMaxLabel.Text = @"0";
            imagesMaxLabel.Text = @"0";
            jsMaxLabel.Text = @"0";
            cssMaxLabel.Text = @"0";
            allMaxLabel.Text = @"0";
        }

        /// <summary>
        /// The function fills a dataGridView from the list that contains string arrays consisting of two columns.
        /// </summary>
        /// <param name="list">List of the strings array: [0] = url; [1] = status</param>
        private void FillDataGridView(List<string[]> list)
        {
            dgvPages.Rows.Clear();
            list.ForEach(item =>
            {
                try { dgvPages.Rows.Add(item[0], item[1]); }
                catch (Exception) { }
            }); 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        private void FillDataGridView(List<string> list)
        {
            dgvPages.Rows.Clear();
            list.ForEach(item => dgvPages.Rows.Add(item, ""));
        }

        /// <summary>
        /// The function fills a timeMapList
        /// </summary>
        /// <param name="list">Time map list</param>
        private void SetTimeMapList(List<string[]> list)
        {
            timeMapList.Items.Clear();
            if ((null == list) || (0 == list.Count)) return;
            list.ForEach(item => timeMapList.Items.Add(item.FirstOrDefault()));
            timeMapList.ColumnWidth = TextRenderer.MeasureText(timeMapList.Items[0].ToString(), timeMapList.Font).Width + 25;
        }
        
        /// <summary>
        /// The function starts a local http server and opens the website in the preview box or in the browser.
        /// </summary>
        /// <param name="handler">Browser or preview box</param>
        private void ShowPreview(string handler)
        {
            try
            {
                if (!_webSites.IsServerStarted)
                {
                    statusLabel.Text = @"Server was not started! Can't open site preview.";
                    return;
                }
                if (!_webSites.Current.IsRebuilt)
                {
                    statusLabel.Text = @"The website was not rebuilt correctly! Can't open site preview.";
                    return;
                }

                if (handler.Contains("Browser"))
                    Process.Start(_webSites.Current.DomainPreviewUrl);
                else
                {
                    using (var form = new PreviewForm(_webSites.Current.DomainPreviewUrl))
                        form.ShowDialog();
                    Focus();
                }
            }
            catch (Exception) { statusLabel.Text = @"Something went wrong! Can't open site preview."; }
        }
    }
}
