namespace ArchiveSiteReBuilder
{
    using System;
    using System.Windows.Forms;

    public partial class PreviewForm : Form
    {
        private string _homeUrl = string.Empty;

        public PreviewForm(string url)
        {
            InitializeComponent();

            _homeUrl = url;
            GoToUrl(_homeUrl);
            //
            backButton.Enabled = false;
            //
            webBrowser.CanGoBackChanged += webBrowser_CanGoBackChanged;
            webBrowser.Navigated += webBrowser_Navigated;
            webBrowser.ProgressChanged += webBrowser_ProgressChanged;
            //
            ResizeRedraw = true;
            ResizeEnd += PreviewForm_ResizeEnd;
        }

        private void PreviewForm_ResizeEnd(object sender, EventArgs e)
        {
            navigateTextBox.Width = webBrowser.Width - homeButton.Width - backButton.Width - goButton.Width - progressPanel.Width;
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            navigateTextBox.Text = e.Url.AbsoluteUri;
        }

        private void webBrowser_CanGoBackChanged(object sender, EventArgs e)
        {
            backButton.Enabled = webBrowser.CanGoBack;
        }

        private void webBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            progressBar.Maximum = (int) e.MaximumProgress;

            try { progressBar.Value = e.CurrentProgress > 0 ? (int) e.CurrentProgress : 0; }
            catch (Exception) { progressBar.Value = (int) e.MaximumProgress; }

            maxLabel.Text = e.MaximumProgress.ToString();
            currentLabel.Text = e.CurrentProgress.ToString();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            GoToUrl(navigateTextBox.Text);
            webBrowser.Focus();
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            webBrowser.GoBack();
        }
        private void homeButton_Click(object sender, EventArgs e)
        {
            GoToUrl(_homeUrl);
            webBrowser.Focus();
        }

        private void GoToUrl(string url)
        {
            var nextUrl = url.Contains(_homeUrl) ? new Uri(url) : new Uri(_homeUrl + url);            

            webBrowser.Navigate(nextUrl);
        }
    }
}
