namespace ArchiveSiteReBuilder.Lib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Reflection;

    public static class Helpers
    {
        /// <summary>
        /// The function gets the user agent string from the file
        /// </summary>
        /// <returns>User agent string</returns>
        private static string GetUserAgent()
        {
            var rnd = new Random();
            var userAgents = string.Empty;

            var resourceName = "ArchiveSiteReBuilder.Lib.userAgents.txt";
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                userAgents = reader.ReadToEnd();
            }
            var lines = userAgents.Replace("\r", "").Split('\n');

            var userAgent = lines[rnd.Next(lines.Length) - 1];

            return userAgent;
        }
        
        /// <summary>
        /// The function creates a request to the server, receives a response and returns it as a string.
        /// </summary>
        /// <param name="url">Server url for the request</param>
        /// <returns>Response string</returns>
        public static string CreateRequest(string url)
        {
            string resFromServer;
            try
            {
                var req = WebRequest.Create(url);

                ((HttpWebRequest) req).UserAgent = GetUserAgent();
                ((HttpWebRequest) req).Accept = "text/html, application/xhtml+xml, */*";
                ((HttpWebRequest) req).Headers.Add("Accept-Language", "en-GB");
                ((HttpWebRequest) req).Headers.Add("Accept-Encoding", "gzip");
                ((HttpWebRequest) req).KeepAlive = true;
                ((HttpWebRequest) req).AutomaticDecompression = DecompressionMethods.GZip;
                ((HttpWebRequest) req).AllowAutoRedirect = true;

                using (var res = req.GetResponse() as HttpWebResponse)
                using (var stream = res.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    resFromServer = reader.ReadToEnd();
                    res.Close();
                }
            }
            catch (Exception) { resFromServer = string.Empty; }

            return resFromServer;
        }
        
        /// <summary>
        /// The function creates a request to the server and gets status code of the answer
        /// </summary>
        /// <param name="url">Url for the request</param>
        /// <returns>HttpStatusCode</returns>
        public static HttpStatusCode GetStatusCode(string url)
        {
            var result = default(HttpStatusCode);
            try
            {
                var req = WebRequest.Create(url);

                ((HttpWebRequest)req).Method = "HEAD";

                using (var res = req.GetResponse() as HttpWebResponse)
                {
                    if (res == null) throw new Exception("response is null.");

                    result = res.StatusCode;
                    res.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404"))
                    result = HttpStatusCode.NotFound;
                else if (ex.Message.Contains("503"))
                    result = HttpStatusCode.ServiceUnavailable;
                else if (ex.Message.Contains("403"))
                    result = HttpStatusCode.Forbidden;
                else
                    result = HttpStatusCode.BadRequest;
            }

            return result;
        }
        
        /// <summary>
        /// The function creates a list from matches of the source text.
        /// </summary>
        /// <param name="src">Source text</param>
        /// <param name="pattern">Regex pattern</param>
        /// <param name="group">Named part of the pattern</param>
        /// <param name="list">If you want to use your own list, you can send it as parameter</param>
        /// <returns>List of strings</returns>
        public static List<string> GetList(string src, string pattern, string group, List<string> list = null)
        {
            if (null == list)
                list = new List<string>();
            var regex = new Regex(pattern);

            // TODO: add replacer for all web characters like &amp;
            foreach (Match match in regex.Matches(src))
                list.Add(match.Groups[group].ToString().Replace("&amp;", "&"));

            return list;
        }
        
        /// <summary>
        /// The function creates a list of the string arrays from matches of the source text.
        /// </summary>
        /// <param name="src">Source text</param>
        /// <param name="pattern">Regex pattern</param>
        /// <param name="groups">Named parts of the pattern and columns of the string array</param>
        /// <param name="list">If you want to use your own list, you can send it as parameter</param>
        /// <returns>List of the string arrays</returns>
        public static List<string[]> GetList(string src, string pattern, string[] groups, List<string[]> list = null)
        {
            if (null == list)
                list = new List<string[]>();
            var regex = new Regex(pattern);

            foreach (Match match in regex.Matches(src))
                list.Add(groups.Select(group => match.Groups[group].ToString()).ToArray());

            return list;
        }

        /// <summary>
        /// The function gets a file from the specified URL
        /// </summary>
        /// <param name="url">Url to file</param>
        /// <param name="fileName">Local filename with path for saving</param>
        /// <returns>True if successful</returns>
        public static bool GetFile(string url, string fileName)
        {
            try
            {
                using (var client = new WebClient())
                    client.DownloadFile(new Uri(url), fileName);

                var status = new FileInfo(fileName).Length != 0;

                if (!status) File.Delete(fileName);

                return status;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// The function checks is this name of the file or not
        /// </summary>
        /// <param name="name">File name or path to the file</param>
        /// <returns>True if this is a file</returns>
        public static bool IsFile(string name)
        {
            var extensions = new List<string[]>
            {
                new string[] {".jpg", ".jpeg", ".png", ".gif", ".ico", ".bmp"},
                new string[] {".js", ".css", ".xml", ".htc"},
                new string[] {".doc", ".docx", ".xls", ".xlsx", ".csv", ".pdf"},
                new string[] {".zip", ".rar", ".7z", ".gz", ".tar"}
            };

            return extensions.SelectMany(type => type)
                                .Any(name.ToLowerInvariant().Contains);
        }
        
        /// <summary>
        /// The function checks is this file represents web page or not
        /// </summary>
        /// <param name="name">File name or path to the file</param>
        /// <returns>True if this is a web page file</returns>
        public static bool IsWebPage(string name)
        {
            var extensions = new List<string[]>
            {
                new string[] {"htm", "html", "shtml", "xhtml"},
                new string[] {"php", "cgi", "pl", "stm", "jsp"},
                new string[] {"asp", "aspx", "asmx", "ashx", "ascx"}
            };

            return extensions.SelectMany(type => type)
                                .Any(name.ToLowerInvariant().EndsWith);
        }
        
        /// <summary>
        /// The function obtains an attributes of the nodes from the HTML page
        /// </summary>
        /// <param name="htmlPage">HTML page from where we should get the attributes of the nodes</param>
        /// <param name="nodeName">Node name to obtain from the page</param>
        /// <param name="attribute">Attribute of the node to obtain</param>
        /// <returns>List of the attributes values</returns>
        public static List<string> GetNodesAttributes(string htmlPage, string nodeName, string attribute)
        {
            var list = new List<string>();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlPage);

            var nodes = doc.DocumentNode.SelectNodes(nodeName);
            try
            {
                foreach (var node in nodes)
                {
                    try { list.Add(attribute.Equals("innerText") ? node.InnerText : node.Attributes[attribute].Value); }
                    catch (Exception) { /* here could be a NullReferenceException */ }
                }
            }
            catch (Exception) { /* here could be a NullReferenceException */ }

            return list;
        }
        
        /// <summary>
        /// The function gets a part of the url using pattern.
        /// </summary>
        /// <param name="src">Source url</param>
        /// <param name="pattern">Regex pattern</param>
        /// <param name="group">Named part of pattern</param>
        /// <returns>Part of url</returns>
        public static string GetUrlPart(string src, string pattern, string group)
        {
            var part = string.Empty;
            src = src.Replace("&", "_");
            src = src.Replace("?", "_");
            src = src.Replace(",", "/");
            if (src.Contains(".js") && !src.EndsWith(".js"))
                src += ".js";
            else if (src.Contains(".css") && !src.EndsWith(".css"))
                src += ".css"; 

            var regex = new Regex(pattern);
            foreach (Match match in regex.Matches(src))
                part = match.Groups[group].ToString();

            return part;
        }

        public static Dictionary<string, string> GetPathAndNameFromUrl(string fileUrl, string startPage)
        {
            var pathAndName = new Dictionary<string, string>();

            var path = GetUrlPart(fileUrl, Constants.Patterns.FullUrlArchivePattern, "path");
            var name = GetUrlPart(fileUrl, Constants.Patterns.FullUrlArchivePattern, "name");

            if (string.IsNullOrEmpty(name))
            {
                if (string.IsNullOrEmpty(path))
                {
                    path = string.Empty;
                    name = startPage;
                }
                else
                {
                    try
                    {
                        var url = fileUrl.Substring(0, fileUrl.LastIndexOf("/", StringComparison.Ordinal));
                        path = GetUrlPart(url, Constants.Patterns.FullUrlArchivePattern, "path");
                        name = GetUrlPart(url, Constants.Patterns.FullUrlArchivePattern, "name");
                    }
                    catch (Exception)
                    {
                        path = string.Empty;
                        name = string.Empty;
                    }
                }
            }
            // the name of the directory can't be more than 248 symbols
            path = CheckPathNameLength(path);
            // the name of the file can't be more than 260 symbols
            name = CheckPathNameLength(name);

            if (!IsFile(name))
            {
                if (IsWebPage(name))
                {
                    if (!name.Substring(name.LastIndexOf(".", StringComparison.Ordinal)).Equals(".htm"))
                        name = name.Replace(name.Substring(name.LastIndexOf(".", StringComparison.Ordinal)), ".html");
                }
                else
                    name += ".html";
            }

            pathAndName.Add("path", path);
            pathAndName.Add("name", name);

            return pathAndName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CheckPathNameLength(string str)
        {
            return str.Length > 100 ? str.Substring(0, 33) + "~" + str.Substring(str.Length - 33) : str;
        }
    }
}
