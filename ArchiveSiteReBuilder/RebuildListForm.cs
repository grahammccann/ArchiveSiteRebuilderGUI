namespace ArchiveSiteReBuilder
{
    using System;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Threading;
    using System.Diagnostics;
    using ArchiveSiteReBuilder.Lib;

    public partial class RebuildListForm : Form
    {
        Progress<KeyValuePair<string, KeyValuePair<string, int>>> _progressIndicator;
        CancellationTokenSource _cancellationTokenSource;

        private WebSites _webSites;

        private bool _isRebuildFinished = false;

        public RebuildListForm(WebSites webSites)
        {
            InitializeComponent();
            InitializeDgv();

            AcceptButton = backButton;
            DialogResult = DialogResult.None;
            FormClosed += RebuildListForm_FormClosed;

            _progressIndicator = new Progress<KeyValuePair<string, KeyValuePair<string, int>>>(ReportProgress);
            _cancellationTokenSource = new CancellationTokenSource();
            _webSites = webSites;

            _webSites.GetNamesList("toRebuild").ForEach(name =>
                {
                    var allCount = _webSites.GetWebSiteByName(name).DomainLists.HtmlFilesList["available"].Count +
                                   _webSites.GetWebSiteByName(name).DomainLists.ImgsList["available"].Count +
                                   _webSites.GetWebSiteByName(name).DomainLists.JsFilesList["available"].Count +
                                   _webSites.GetWebSiteByName(name).DomainLists.CssFilesList["available"].Count;
                    dashboardDgv.Rows.Add(name,
                        0, _webSites.GetWebSiteByName(name).DomainLists.HtmlFilesList["available"].Count,
                        0, _webSites.GetWebSiteByName(name).DomainLists.ImgsList["available"].Count,
                        0, _webSites.GetWebSiteByName(name).DomainLists.JsFilesList["available"].Count,
                        0, _webSites.GetWebSiteByName(name).DomainLists.CssFilesList["available"].Count,
                        0, allCount);
                });

            UpdateStatusCount(dashboardDgv.Rows.Count);

            previewButton.Enabled = false;
            previewBrowserButton.Enabled = false;
            previewButton.Click += previewButton_Click;
            previewBrowserButton.Click += previewButton_Click;
        }
        private void RebuildListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = _isRebuildFinished ? DialogResult.OK : DialogResult.Cancel;
        }
        private void previewButton_Click(object sender, EventArgs e)
        {
            ShowPreview(sender.ToString());
        }

        /// <summary>
        /// The function opens the website in the preview box or in the browser.
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

                var webSite = _webSites.GetWebSiteByName(dashboardDgv.CurrentRow.Cells[0].Value.ToString());
                _webSites.SetCurrentRootDirectory(webSite.DomainDirectory);

                if (handler.Contains("Browser"))
                    Process.Start(webSite.DomainPreviewUrl);
                else
                {
                    using (var form = new PreviewForm(webSite.DomainPreviewUrl))
                        form.ShowDialog();
                    Focus();
                }
            }
            catch (Exception) { statusLabel.Text = @"Something went wrong! Can't open site preview."; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeAndValue">
        /// Key is the type of the file
        /// <para>Value.Key is the row index</para>
        /// <para>Value.Value is the progress value</para>
        /// </param>
        private void ReportProgress(KeyValuePair<string, KeyValuePair<string, int>> typeAndValue)
        {
            var index = -1;
            for (var i = 0; i < dashboardDgv.Rows.Count; i++)
            {
                if (!dashboardDgv.Rows[i].Cells["names"].Value.ToString().Equals(typeAndValue.Key)) continue;

                index = i;
                break;
            }
            if (index == -1) return;

            if (typeAndValue.Value.Key.Equals("html"))
                dashboardDgv.Rows[index].Cells["pagesCurrent"].Value = typeAndValue.Value.Value.ToString();
            else if (typeAndValue.Value.Key.Equals("js"))
                dashboardDgv.Rows[index].Cells["jsCurrent"].Value = typeAndValue.Value.Value.ToString();
            else if (typeAndValue.Value.Key.Equals("css"))
                dashboardDgv.Rows[index].Cells["cssCurrent"].Value = typeAndValue.Value.Value.ToString();
            else if (typeAndValue.Value.Key.Equals("images"))
                dashboardDgv.Rows[index].Cells["imagesCurrent"].Value = typeAndValue.Value.Value.ToString();

            dashboardDgv.Rows[index].Cells["allCurrent"].Value = (
                                        Int32.Parse(dashboardDgv.Rows[index].Cells["pagesCurrent"].Value.ToString()) +
                                        Int32.Parse(dashboardDgv.Rows[index].Cells["jsCurrent"].Value.ToString()) +
                                        Int32.Parse(dashboardDgv.Rows[index].Cells["cssCurrent"].Value.ToString()) +
                                        Int32.Parse(dashboardDgv.Rows[index].Cells["imagesCurrent"].Value.ToString())
                                        ).ToString();
        }

        private void InitializeDgv()
        {
            var namesCol = new DataGridViewTextBoxColumn()
            {
                Name = "names",
                HeaderText = @"Domain Name"
            };
            var pagesCurrentCol = new DataGridViewTextBoxColumn()
            {
                Name = "pagesCurrent",
                HeaderText = @"Pages Current"
            };
            var pagesMaxCol = new DataGridViewTextBoxColumn()
            {
                Name = "pagesMax",
                HeaderText = @"Pages Max"
            };
            var imagesCurrentCol = new DataGridViewTextBoxColumn()
            {
                Name = "imagesCurrent",
                HeaderText = @"Images Current"
            };
            var imagesMaxCol = new DataGridViewTextBoxColumn()
            {
                Name = "imagesMax",
                HeaderText = @"Images Max"
            };
            var jsCurrentCol = new DataGridViewTextBoxColumn()
            {
                Name = "jsCurrent",
                HeaderText = @".js Current"
            };
            var jsMaxCol = new DataGridViewTextBoxColumn()
            {
                Name = "jsMax",
                HeaderText = @".js Max"
            };
            var cssCurrentCol = new DataGridViewTextBoxColumn()
            {
                Name = "cssCurrent",
                HeaderText = @".css Current"
            };
            var cssMaxCol = new DataGridViewTextBoxColumn()
            {
                Name = "cssMax",
                HeaderText = @".css Max"
            };
            var allCurrentCol = new DataGridViewTextBoxColumn()
            {
                Name = "allCurrent",
                HeaderText = @"All Current"
            };
            var allMaxCol = new DataGridViewTextBoxColumn()
            {
                Name = "allMax",
                HeaderText = @"All Max"
            };

            dashboardDgv.Columns.AddRange(namesCol, 
                                            pagesCurrentCol, pagesMaxCol, 
                                            imagesCurrentCol, imagesMaxCol, 
                                            jsCurrentCol, jsMaxCol, 
                                            cssCurrentCol, cssMaxCol,
                                            allCurrentCol, allMaxCol);

            dashboardDgv.Columns["pagesCurrent"].Width = 40;
            dashboardDgv.Columns["pagesMax"].Width = 40;
            dashboardDgv.Columns["imagesCurrent"].Width = 40;
            dashboardDgv.Columns["imagesMax"].Width = 40;
            dashboardDgv.Columns["jsCurrent"].Width = 40;
            dashboardDgv.Columns["jsMax"].Width = 40;
            dashboardDgv.Columns["cssCurrent"].Width = 40;
            dashboardDgv.Columns["cssMax"].Width = 40;
            dashboardDgv.Columns["allCurrent"].Width = 40;
            dashboardDgv.Columns["allMax"].Width = 40;
        }

        private void UpdateStatusCount(int count)
        {
            statusLabel.Text = @"You have selected " + count + @" websites to rebuild.";
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            DialogResult = _isRebuildFinished ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        private async void rebuildButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn == null) return;
            if (btn.Text.Equals("Cancel"))
            {
                _cancellationTokenSource.Cancel();
                rebuildButton.Text = @"ReBuild!";
                statusLabel.Text = @"ReBuild was canceled!";
            }
            else
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
                rebuildButton.Text = @"Cancel";

                var count = _webSites.Lists.ContainsKey("toRebuild") ? _webSites.Lists["toRebuild"].Count : 0;
                statusLabel.Text = @"ReBuild " + count + (count == 1 ? @" domain." : @" domains.");

                await _webSites.RebuildList(_progressIndicator, _cancellationTokenSource.Token, _webSites.AsrSettings.OverwriteMode);

                if (_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    _isRebuildFinished = false;
                    rebuildButton.Text = @"ReBuild!";
                    statusLabel.Text = @"ReBuild was canceled!";
                    return;
                }
                    
                try
                {
                    await Task.Run(() =>
                    {
                        for (var i = count - 1; i >= 0; i--)
                            _webSites.ChangeWebSiteList("rebuilt", _webSites.Lists["toRebuild"][i]);
                    });
                }
                catch (Exception) { }

                previewButton.Enabled = true;
                previewBrowserButton.Enabled = true;
                _isRebuildFinished = true;
                rebuildButton.Text = @"ReBuild!";
                statusLabel.Text = count + (count == 1 ? @" domain was " : @" domains were ") + @"rebuilt.";
            }
        }

    }
}
