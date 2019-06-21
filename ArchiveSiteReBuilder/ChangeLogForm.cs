namespace ArchiveSiteReBuilder
{
    using System;
    using System.IO;
    using System.Net;
    using System.Windows.Forms;
    using ArchiveSiteReBuilder.Lib;

    public partial class ChangeLogForm : Form
    {
        public ChangeLogForm()
        {
            InitializeComponent();
        }

        private void ChangeLogForm_Load(object sender, EventArgs e)
        {
            var fileName = "change.log";

            try
            {
                using (var client = new WebClient())
                    client.DownloadFile(new Uri(Constants.Common.AppChangeLogUrl), fileName);
            }
            catch (Exception) { }

            try { changeLogTextBox.Text = File.ReadAllText(fileName); }
            catch (Exception) { changeLogTextBox.Text = @"There are no changes!"; }
        }
    }
}
