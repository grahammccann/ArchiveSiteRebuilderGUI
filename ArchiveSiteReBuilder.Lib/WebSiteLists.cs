namespace ArchiveSiteReBuilder.Lib
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains lists of the files that are available for the website
    /// </summary>
    public class WebSiteLists
    {
        /// <summary>
        /// Gets or sets the list of the html files
        /// </summary>
        public Dictionary<string, List<string>> HtmlFilesList { get; set; }
        
        /// <summary>
        /// Gets or sets the list of the css files
        /// </summary>
        public Dictionary<string, List<string>> CssFilesList { get; set; }
        
        /// <summary>
        /// Gets or sets the list of the js files
        /// </summary>
        public Dictionary<string, List<string>> JsFilesList { get; set; }
        
        /// <summary>
        /// Gets or sets the list of the images files
        /// </summary>
        public Dictionary<string, List<string>> ImgsList { get; set; }
        
        /// <summary>
        /// Gets or sets the timemap list
        /// </summary>
        public List<string[]> TimeMapList { get; set; }
        
        /// <summary>
        /// Gets or sets the list of the downloaded files
        /// </summary>
        public List<string[]> DownloadedFilesList { get; set; }

        /// <summary>
        /// Gets or sets the key-value pairs 
        /// <para>the key is the type of the files</para> 
        /// <para>the value is the count of the files</para>
        /// </summary>
        public Dictionary<string, int> DownloadedFilesCountersList { get; set; }

        /// <summary>
        /// Gets or sets the list of the not available files
        /// </summary>
        public List<string> NotAvailableList { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebSiteLists()
        {
            HtmlFilesList = new Dictionary<string, List<string>>();
            CssFilesList = new Dictionary<string, List<string>>();
            JsFilesList = new Dictionary<string, List<string>>();
            ImgsList = new Dictionary<string, List<string>>();
            TimeMapList = new List<string[]>();
            DownloadedFilesList = new List<string[]>();
            DownloadedFilesCountersList = new Dictionary<string, int>();
            NotAvailableList = new List<string>();

            InitFilesLists();
        }
        
        public void ClearFilesLists()
        {
            HtmlFilesList["available"].Clear();
            HtmlFilesList["notAvailable"].Clear();

            CssFilesList["available"].Clear();
            CssFilesList["notAvailable"].Clear();

            JsFilesList["available"].Clear();
            JsFilesList["notAvailable"].Clear();

            ImgsList["available"].Clear();
            ImgsList["notAvailable"].Clear();
        }

        private void InitFilesLists()
        {
            HtmlFilesList.Add("available", new List<string>());
            CssFilesList.Add("available", new List<string>());
            JsFilesList.Add("available", new List<string>());
            ImgsList.Add("available", new List<string>());

            HtmlFilesList.Add("notAvailable", new List<string>());
            CssFilesList.Add("notAvailable", new List<string>());
            JsFilesList.Add("notAvailable", new List<string>());
            ImgsList.Add("notAvailable", new List<string>());
        }

    }
}
