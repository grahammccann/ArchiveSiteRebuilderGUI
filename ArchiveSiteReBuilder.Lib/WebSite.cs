namespace ArchiveSiteReBuilder.Lib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    public class WebSite
    {
        /// <summary>
        /// Gets the current domain name of the website.
        /// </summary>
        public string DomainName { get; private set; }
        
        /// <summary>
        /// Gets the current domain home page of the website.
        /// </summary>
        public string DomainHomePage { get; private set; }
        
        /// <summary>
        /// Gets the current domain directory of the website.
        /// </summary>
        public string DomainDirectory { get; private set; }
        
        /// <summary>
        /// Gets the lists of the web site. See WebSiteLists for more information.
        /// </summary>
        public WebSiteLists DomainLists { get; private set; }

        /// <summary>
        /// Gets the current domain local URL for preview.
        /// </summary>
        public string DomainPreviewUrl { get; private set; }

        /// <summary>
        /// The URL of the website's files list.
        /// </summary>
        public string DomainAllFilesUrl { get; private set; }

        /// <summary>
        /// Gets the availability of the website.
        /// </summary>
        public bool IsAvailable { get; private set; }
        
        /// <summary>
        /// Gets the maintenance status of the website.
        /// </summary>
        public bool IsOnMaintenance { get; private set; }
        
        /// <summary>
        /// Gets the rebuilt status of the website.
        /// </summary>
        public bool IsRebuilt { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int DomainFilesCount { get; private set; }

        /// <summary>
        /// Gets the start page of the website.
        /// </summary>
        public string StartPage 
        {
            get { return "index.html"; }
        }

        private Constants.Settings.OverwriteMode _overwriteMode;
        private IProgress<KeyValuePair<string, KeyValuePair<string, int>>> _progress;
        private CancellationToken _cancellationToken;
        
        /// <summary>
        /// The WebSite constructor.
        /// </summary>
        /// <param name="domain">Domain name of the website</param>
        /// <param name="isInit">Needed on the application startup to increase check time</param>
        public WebSite(string domain, bool isInit)
        {
            DomainLists = new WebSiteLists();

            CorrectUrl(domain);

            DomainDirectory = Constants.Common.DomainsDir + DomainName + @"\";
            DomainAllFilesUrl = Constants.ArchiveUrls.GetTimeGateUrl + "*/" + DomainName + "/*";
            
            UpdateRebuiltStatus();

            if (!isInit)
                IsAvailable = CheckAvailability();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="ct"></param>
        public void SetProgressAndCancellation(IProgress<KeyValuePair<string, KeyValuePair<string, int>>> progress, CancellationToken ct)
        {
            _progress = progress;
            _cancellationToken = ct;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFilesList()
        {
            var allFilesList = new List<string>();
            allFilesList.AddRange(DomainLists.HtmlFilesList["available"]);
            allFilesList.AddRange(DomainLists.JsFilesList["available"]);
            allFilesList.AddRange(DomainLists.CssFilesList["available"]);
            allFilesList.AddRange(DomainLists.ImgsList["available"]);

            return allFilesList;
        }

        /// <summary>
        /// The function gets files list for the specified snapshot date
        /// </summary>
        /// <returns></returns>
        public async Task GetFilesListAsync(string homePageDate)
        {
            UpdateHomePageUrl(homePageDate);

            if (!Directory.Exists(DomainDirectory)) Directory.CreateDirectory(DomainDirectory);

            var fullPathName = DomainDirectory + StartPage;
            var status = await Task.Run(() => Helpers.GetFile(DomainHomePage, fullPathName), _cancellationToken);

            if (!status) return;

            var htmlPage = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    // if operation was canceled throw an exception
                    _cancellationToken.ThrowIfCancellationRequested();

                    Correction.RemoveWbmCodeFromHtml(fullPathName);
                    htmlPage = File.ReadAllText(fullPathName);
                }
                catch (OperationCanceledException) { return; }
            }, _cancellationToken);

            if (_cancellationToken.IsCancellationRequested) return;

            // get files
            var htmlFilesList = Task.Run(() => GetFilesList("html", "html", htmlPage), _cancellationToken);
            var jsFilesList = Task.Run(() => GetFilesList("js", "js", htmlPage), _cancellationToken);
            var cssFilesList = Task.Run(() => GetFilesList("css", "css", htmlPage), _cancellationToken);
            var imgsFilesList = Task.Run(() => GetFilesList("imgs", "images"), _cancellationToken);

            await Task.WhenAll(htmlFilesList, jsFilesList, cssFilesList, imgsFilesList);

            if (_cancellationToken.IsCancellationRequested) return;

            DomainLists.ClearFilesLists();

            DomainLists.HtmlFilesList["available"].AddRange(htmlFilesList.Result);
            DomainLists.JsFilesList["available"].AddRange(jsFilesList.Result);
            DomainLists.CssFilesList["available"].AddRange(cssFilesList.Result);
            DomainLists.ImgsList["available"].AddRange(imgsFilesList.Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task CheckFilesListAsync()
        {
            // check files
            var htmlFilesDict = Task.Run(() => CheckListAvailability(DomainLists.HtmlFilesList["available"], "html"), _cancellationToken);
            var jsFilesDict = Task.Run(() => CheckListAvailability(DomainLists.JsFilesList["available"], "js"), _cancellationToken);
            var cssFilesDict = Task.Run(() => CheckListAvailability(DomainLists.CssFilesList["available"], "css"), _cancellationToken);
            var imgsDict = Task.Run(() => CheckListAvailability(DomainLists.ImgsList["available"], "images"), _cancellationToken);

            await Task.WhenAll(htmlFilesDict, jsFilesDict, cssFilesDict, imgsDict);

            if (_cancellationToken.IsCancellationRequested) return;

            DomainLists.ClearFilesLists();

            DomainLists.HtmlFilesList = htmlFilesDict.Result;
            DomainLists.JsFilesList = jsFilesDict.Result;
            DomainLists.CssFilesList = cssFilesDict.Result;
            DomainLists.ImgsList = imgsDict.Result;
        }

        /// <summary>
        /// The function starts the rebuild process
        /// </summary>
        /// <returns></returns>
        public async Task Rebuild(Constants.Settings.OverwriteMode overwriteMode = Constants.Settings.OverwriteMode.IgnoreExisting)
        {
            var all = 0;

            DomainLists.DownloadedFilesCountersList.Clear();
            _overwriteMode = overwriteMode;

            // save all files and pages locally
            var htmlCount = Task.Run(() => SaveList(DomainLists.HtmlFilesList["available"], "html"), _cancellationToken);
            var jsCount = Task.Run(() => SaveList(DomainLists.JsFilesList["available"], "js"), _cancellationToken);
            var cssCount = Task.Run(() => SaveList(DomainLists.CssFilesList["available"], "css"), _cancellationToken);
            var imgsCount = Task.Run(() => SaveList(DomainLists.ImgsList["available"], "images"), _cancellationToken);

            await Task.WhenAll(htmlCount, jsCount, cssCount, imgsCount);

            if (_cancellationToken.IsCancellationRequested) return;

            DomainLists.DownloadedFilesCountersList.Add("pages", htmlCount.Result);
            DomainLists.DownloadedFilesCountersList.Add("js", jsCount.Result);
            DomainLists.DownloadedFilesCountersList.Add("css", cssCount.Result);
            DomainLists.DownloadedFilesCountersList.Add("images", imgsCount.Result);

            all = htmlCount.Result + jsCount.Result + cssCount.Result + imgsCount.Result;
            DomainLists.DownloadedFilesCountersList.Add("all", all);

            UpdateRebuiltStatus();
        }

        /// <summary>
        /// The function sets the preview url for domain
        /// </summary>
        /// <param name="serverUrl"></param>
        public void SetPreviewUrl(string serverUrl)
        {
            DomainPreviewUrl = serverUrl + StartPage;
        }
        
        /// <summary>
        /// The function updates the rebuilt status of the website
        /// </summary>
        private void UpdateRebuiltStatus()
        {
            IsRebuilt = false;

            if (!Directory.Exists(DomainDirectory)) return;

            DomainFilesCount = Directory.EnumerateFiles(DomainDirectory, "*.*", SearchOption.AllDirectories).Count();

            if (File.Exists(DomainDirectory + StartPage))
                IsRebuilt = true;
        }
        
        /// <summary>
        /// The function removes uri scheme from the url
        /// </summary>
        /// <param name="domain"></param>
        private void CorrectUrl(string domain)
        {
            // check if domain name contains any Uri scheme
            if (!Uri.CheckSchemeName(domain))
            {
                // and if contains - remove it
                var regex = new Regex(@"^(\w+:(https?:)?(//)?)");
                domain = regex.Replace(domain, "");
            }

            DomainName = domain.Contains("www.") ? domain.Replace("www.", "") : domain;
        }
 
        /// <summary>
        /// The function checks a website for availability using JSON request
        /// </summary>
        /// <returns>True if the website has snapshots</returns>
        private bool CheckAvailability()
        {
            // ask wayback machine for the website snapshots
            var status = Helpers.GetStatusCode(Constants.ArchiveUrls.GetTimeMapUrl + DomainName).Equals(HttpStatusCode.OK);

            if (!status)
            {
                // check for maintenance
                if (Helpers.GetStatusCode(DomainAllFilesUrl).Equals(HttpStatusCode.ServiceUnavailable))
                    IsOnMaintenance = true;
            }
            else // get a snapshots time map
                DomainLists.TimeMapList = GetTimeMap(DomainName);

            return status;
        }

        /// <summary>
        /// The function gets a time map of the homepage from the archive.org server using @GetTimeMapUrl.
        /// </summary>
        /// <param name="url">Url of the website</param>
        private List<string[]> GetTimeMap(string url)
        {
            var res = Helpers.CreateRequest(Constants.ArchiveUrls.GetTimeMapUrl + url);
            var timeMap = Helpers.GetList(res, Constants.Patterns.MapPattern, new string[] { "date", "fullUrl" });
            try { timeMap.ForEach(item => item[1] = item[1].Replace(Constants.ArchiveUrls.ArchiveUrl, "")); }
            catch (Exception) { return null; }

            return timeMap;
        }

        /// <summary>
        /// The function gets urls from the timemap response
        /// </summary>
        /// <param name="url">Url for the timemap request</param>
        /// <returns>Working url or null</returns>
        private string GetUrlFromTimeMap(string url)
        {
            string result = null;
            var response = Helpers.CreateRequest(url);
            var list = Helpers.GetList(response, Constants.Patterns.MapPattern, "fullUrl");
            // start from the end of list
            list.Reverse();

            foreach (var item in list)
            {
                var status = Helpers.GetStatusCode(item);
                if (status.Equals(HttpStatusCode.OK))
                {
                    result = item;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// The function sets the url for a choosen snapshot date
        /// </summary>
        /// <param name="date"></param>
        private void UpdateHomePageUrl(string date)
        {
            DomainHomePage = Constants.ArchiveUrls.ArchiveUrl + DomainLists.TimeMapList.Where(item => item[0] == date)
                                                .ToArray().Select(item => item.LastOrDefault()).SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="listType"></param>
        /// <returns></returns>
        private async Task<Dictionary<string, List<string>>> CheckListAvailability(List<string> list, string listType)
        {
            var filesDict = Task.Run(() =>
            {
                var dict = new Dictionary<string, List<string>>
                {
                    { "available", new List<string>() },
                    { "notAvailable", new List<string>() }
                };
                var counter = 0;
                Parallel.ForEach(list, item =>
                {
                    try
                    {
                        // if operation was canceled throw an exception
                        _cancellationToken.ThrowIfCancellationRequested();

                        if (Helpers.GetStatusCode(item).Equals(HttpStatusCode.OK))
                            dict["available"].Add(item);
                        else
                            dict["notAvailable"].Add(item);

                        if (_progress != null)
                            _progress.Report(new KeyValuePair<string, KeyValuePair<string, int>>(DomainName, new KeyValuePair<string, int>(listType, ++counter)));
                    }
                    catch (OperationCanceledException) { return; }
                    catch (Exception) { }
                });
                if (_cancellationToken.IsCancellationRequested)
                {
                    dict["available"].Clear();
                    dict["notAvailable"].Clear();
                }
                return dict;
            }, _cancellationToken);

            await Task.WhenAll(filesDict);

            return filesDict.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="listType"></param>
        /// <param name="htmlPage"></param>
        /// <returns></returns>
        private List<string> GetFilesList(string type, string listType, string htmlPage = "")
        {
            var list = new List<string>();
            var typeCopy = type;
            switch (typeCopy)
            {
                case "html":
                    // create the sitemap
                    list = Helpers.GetNodesAttributes(htmlPage, "//a", "href");
                    list.AddRange(Helpers.GetNodesAttributes(htmlPage, "//area", "href"));
                    list.RemoveAll(item => (!item.Contains(DomainName) && item.Contains("/web/")));
                    list = Correction.UrlsInList(list, DomainHomePage, "html", DomainName);
                    // get the homepage from the current selected snapshot
                    list.Add(DomainHomePage);
                    // remove duplicates
                    list = list.Distinct().ToList();
                    if (_progress != null)
                        _progress.Report(new KeyValuePair<string, KeyValuePair<string, int>>(DomainName, new KeyValuePair<string, int>(listType, list.Count)));
                    break;
                case "js":
                    list = Helpers.GetNodesAttributes(htmlPage, "//script", "src").Distinct().ToList();
                    list = Correction.UrlsInList(list, DomainHomePage, "js", DomainName);
                    if (_progress != null)
                        _progress.Report(new KeyValuePair<string, KeyValuePair<string, int>>(DomainName, new KeyValuePair<string, int>(listType, list.Count)));
                    break;
                case "css":
                    list = Helpers.GetNodesAttributes(htmlPage, "//link", "href").Distinct().ToList();
                    list = Correction.UrlsInList(list, DomainHomePage, "css", DomainName);
                    if (_progress != null)
                        _progress.Report(new KeyValuePair<string, KeyValuePair<string, int>>(DomainName, new KeyValuePair<string, int>(listType, list.Count)));
                    break;
                case "imgs":
                    try
                    {
                        // if operation was canceled throw an exception
                        _cancellationToken.ThrowIfCancellationRequested();

                        var res = Helpers.CreateRequest(DomainAllFilesUrl);
                        var body = res.Substring(res.LastIndexOf("<table id=\"resultsUrl\">", StringComparison.Ordinal));
                        var imgsFilesUrls = Helpers.GetList(body, Constants.Patterns.ImgsFilesPattern, "url")
                            .Select(item => item.Contains("/web/*/") ? item.Replace("/web/*/", Constants.ArchiveUrls.GetTimeMapUrl) : item)
                            .ToList();
                        var imgsWithTimeMap = imgsFilesUrls.Where(item => item.Contains(Constants.ArchiveUrls.GetTimeMapUrl)).ToList();
                        var imgsWithoutTimeMap = imgsFilesUrls.Where(item => !item.Contains(Constants.ArchiveUrls.GetTimeMapUrl)).ToList();

                        list.AddRange(imgsWithoutTimeMap);
                        imgsFilesUrls.Clear();
                        imgsWithoutTimeMap.Clear();
                        // get the correct images urls
                        var imgsListCopy = list;
                        var counter = list.Count;
                        Parallel.ForEach(imgsWithTimeMap, imgUrl =>
                        {
                            try
                            {
                                // if operation was canceled throw an exception
                                _cancellationToken.ThrowIfCancellationRequested();

                                var urlCopy = imgUrl;
                                var item = GetUrlFromTimeMap(urlCopy);
                                if (!string.IsNullOrEmpty(item))
                                    imgsListCopy.Add(item);

                                if (_progress != null)
                                    _progress.Report(new KeyValuePair<string, KeyValuePair<string, int>>(DomainName,
                                        new KeyValuePair<string, int>(listType, ++counter)));
                            }
                            catch (OperationCanceledException) { return; }
                            catch (Exception) { }
                        });
                        if (!_cancellationToken.IsCancellationRequested)
                        {
                            if (_progress != null)
                                _progress.Report(new KeyValuePair<string, KeyValuePair<string, int>>(DomainName,
                                    new KeyValuePair<string, int>(listType, counter)));
                            list = imgsListCopy;
                            list = Correction.UrlsInList(list, DomainHomePage, "img", DomainName);
                        }
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception) { list = new List<string>(); }
                    break;
            }
            return list;
        }

        /// <summary>
        /// The function saves files from the list locally.
        /// </summary>
        /// <param name="list">List of files</param>        
        /// <param name="listType"></param>
        /// <returns>Number of the files that were successfully downloaded</returns>
        private int SaveList(List<string> list, string listType)
        {
            var counter = 0;

            Parallel.ForEach(list, fileUrl =>
            {
                try
                {
                    // if operation was canceled throw an exception
                    _cancellationToken.ThrowIfCancellationRequested();

                    var fileUrlCopy = fileUrl;
                    var isSaved = SaveFile(fileUrlCopy);
                    counter += isSaved ? 1 : 0;
                    if (_progress != null)
                        _progress.Report(new KeyValuePair<string, KeyValuePair<string, int>>(DomainName,
                            new KeyValuePair<string, int>(listType, counter)));
                }
                catch (OperationCanceledException) { return; }
                catch (Exception) { }
            });

            if (_cancellationToken.IsCancellationRequested) counter = 0;

            return counter;
        }

        /// <summary>
        /// The function saves a file from the url locally
        /// </summary>
        /// <param name="url">Url to a file or page</param>
        /// <returns>True if file successfully saved</returns>
        private bool SaveFile(string url)
        {
            var pathAndName = Helpers.GetPathAndNameFromUrl(url, StartPage);

            var fullPath = DomainDirectory + pathAndName["path"];
            var fullPathName = fullPath + pathAndName["name"];

            var isFileSaved = true;

            try
            {
                if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);                
                switch (_overwriteMode)
                {
                    case Constants.Settings.OverwriteMode.OverwriteExisting:
                        isFileSaved = Helpers.GetFile(url, fullPathName);
                        break;
                    case Constants.Settings.OverwriteMode.IgnoreExisting:
                    default:
                        if (!File.Exists(fullPathName) || fullPathName.Contains("index.html"))
                            isFileSaved = Helpers.GetFile(url, fullPathName);
                        break;
                }
            }
            catch (Exception) { isFileSaved = false; }

            var isWbmRedirectPage = false;
            if (isFileSaved && Helpers.IsWebPage(fullPathName))
            {
                try
                {
                    var htmlPage = File.ReadAllText(fullPathName);
                    var list = Helpers.GetNodesAttributes(htmlPage, "//p", "innerText");
                    isWbmRedirectPage = list.Select(type => type).Any(item => item.Equals(@"Got an HTTP 302 response at crawl time", StringComparison.OrdinalIgnoreCase));
                    if (isWbmRedirectPage)
                    {
                        var homePageRedirect = @"<!DOCTYPE html><html><head></head><body><script type=""text/javascript"">window.location = ""/"";</script></body></html>";
                        File.WriteAllText(fullPathName, homePageRedirect);
                    }
                }
                catch (Exception) { isWbmRedirectPage = false; }
            }

            if (isFileSaved && !isWbmRedirectPage && !string.IsNullOrEmpty(fullPathName))
            {
                if (fullPathName.EndsWith("htm") || fullPathName.EndsWith("html"))
                    Correction.RemoveWbmCodeFromHtml(fullPathName);
                if (fullPathName.EndsWith("css") || fullPathName.EndsWith("js") || fullPathName.EndsWith("htm") || fullPathName.EndsWith("html"))
                    Correction.AllUrlsInFile(fullPathName, DomainName);
            }

            var pageUrl = url;

            var isSaved = isFileSaved ? "Yes" : "No";
            var row = new string[] { pageUrl, isSaved };
            DomainLists.DownloadedFilesList.Add(row);

            return isFileSaved;
        }   
    }
}
