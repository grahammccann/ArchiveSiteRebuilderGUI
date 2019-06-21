namespace ArchiveSiteReBuilder
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Linq;
    using ArchiveSiteReBuilder.Lib;

    public partial class RebuiltForm : Form
    {
        private WebSites _webSites;

        public RebuiltForm(WebSites webSites)
        {
            InitializeComponent();
            InitializeGrid();
            statusLabel.Text = string.Empty;

            _webSites = webSites;

            FillGrid();
        }

        private void InitializeGrid()
        {
            var namesCol = new DataGridViewTextBoxColumn()
            {
                Name = "domainsNames",
                HeaderText = @"Domain Name"
            };
            var filesCount = new DataGridViewTextBoxColumn()
            {
                Name = "filesCount",
                HeaderText = @"Files"
            };

            rebuiltDgv.Columns.AddRange(namesCol, filesCount);

            if (rebuiltDgv.Columns["filesCount"] != null)
                rebuiltDgv.Columns["filesCount"].Width = 50;
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            ShowPreview(sender.ToString());
        }

        private void FillGrid()
        {
            var namesList = _webSites.GetNamesList("rebuilt");
            var count = namesList.Count;
            var filesCount = new List<int>();
            namesList.ForEach(name => filesCount.Add(_webSites.GetWebSiteByName(name).DomainFilesCount));

            var rebuilt = namesList.Zip(filesCount, (name, files) => new { Name = name, Files = files });
            
            statusLabel.Text = @"You have " + count + (count == 1 ? @" rebuilt website." : @" rebuilt websites.");

            if (count == 0) return;

            rebuiltDgv.Rows.Clear();
            foreach (var webSite in rebuilt)
                rebuiltDgv.Rows.Add(webSite.Name, webSite.Files);
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

                var webSite = _webSites.GetWebSiteByName(rebuiltDgv.CurrentRow.Cells[0].Value.ToString());
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
    }
}
