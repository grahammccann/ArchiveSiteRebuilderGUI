namespace ArchiveSiteReBuilder.Lib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The class with a functions for the URL correction.
    /// </summary>
    public static class Correction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public static void RemoveWbmCodeFromHtml(string filePath)
        {
            try
            {
                var htmlPage = File.ReadAllText(filePath);
                string header = string.Empty, body = string.Empty;
                // get header before the wayback toolbar
                var headerIndex = htmlPage.IndexOf(Constants.Patterns.WaybackToolbarBegin, StringComparison.Ordinal);
                if (headerIndex > 0)
                    header = htmlPage.Substring(0, headerIndex);
                // get body after the wayback toolbar
                var bodyIndex = htmlPage.LastIndexOf(Constants.Patterns.WaybackToolbarEnd, StringComparison.Ordinal);
                if (bodyIndex > 0)
                    body = htmlPage.Substring(bodyIndex).Replace(Constants.Patterns.WaybackToolbarEnd, "");
                // remove the wayback machine copyright
                var htmlEndIndex = body.LastIndexOf("</html>", StringComparison.OrdinalIgnoreCase);
                if (htmlEndIndex > 0)
                    body = body.Substring(0, htmlEndIndex) + "</html>";
                // remove the static js/css files from the header
                var regex = new Regex(Constants.Patterns.WaybackAnalyticsJsCssPattern);
                header = regex.Replace(header, "");
                regex = new Regex(Constants.Patterns.WaybackAnalyticsServerPattern);
                header = regex.Replace(header, "");

                if (!string.IsNullOrEmpty(header) && !string.IsNullOrEmpty(body))
                    htmlPage = header + body;

                File.WriteAllText(filePath, htmlPage);
            }
            catch (Exception) { /* if something goes wrong */ }
        }

        /// <summary>
        /// The function corrects a list of the urls which will be used for the downloading
        /// </summary>
        /// <param name="list">List of the urls</param>
        /// <param name="url">Url to add before the list items</param>
        /// <param name="type">Sets the type of the urls list. Could be: js | css | html | img | files</param>
        /// <param name="currentDomain"></param>
        /// <returns>Corrected list</returns>
        public static List<string> UrlsInList(List<string> list, string url, string type, string currentDomain)
        {
            // remove foreign urls from list
            list.RemoveAll(item => ((item.Contains("http://") || item.Contains("https://")) && !item.Contains(currentDomain)));
            // remove mailto urls
            list.RemoveAll(item => item.Contains("mailto"));
            // 
            switch (type)
            {
                case "js":
                    list.RemoveAll(item => !item.Contains(".js"));
                    break;
                case "css":
                    list.RemoveAll(item => !item.Contains(".css"));
                    break;
            }

            list = list.Select(item =>
            {
                if (item.Contains("/web/") && !item.Contains(Constants.ArchiveUrls.ArchiveUrl))
                    item = Constants.ArchiveUrls.ArchiveUrl + item;
                else if (item.Contains(Constants.ArchiveUrls.ArchiveUrl))
                    item = item;
                else
                {
                    if (item.StartsWith("/")) item = item.Substring(item.IndexOf("/") + 1);

                    item = url + item;
                }

                item = item.Replace("&amp;", "&");

                return item;
            }).ToList();

            // remove urls which contain /#
            list.RemoveAll(item => item.Contains(currentDomain + "/#"));
            // and # generally
            list.RemoveAll(item => item.Contains("#"));

            return list;
        }

        /// <summary>
        /// <para>The function corrects all internal website urls in the file</para>
        /// <para>From: web/20140517051400/http://example.com/index.php?route=common/home </para>
        /// <para>To: /index.php/route=common/home.html</para>
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <param name="currentDomain">Current domain name</param>
        public static void AllUrlsInFile(string filePath, string currentDomain)
        {
            try
            {
                var file = File.ReadAllText(filePath);
                var newFile = string.Empty;

                var regex = new Regex(Constants.Patterns.FullUrlArchivePattern);
                if (filePath.EndsWith("css") || filePath.EndsWith("js"))
                {
                    newFile = regex.Replace(file, m => ("/" + m.Groups["path"].Value + m.Groups["name"].Value));
                    try { newFile = newFile.Substring(newFile.IndexOf("*/", StringComparison.Ordinal) + 2); }
                    catch (Exception) { }
                }
                else
                {
                    newFile = regex.Replace(file, m =>
                    {
                        // correct urls like facebook.com, twitter.com etc.
                        if (!m.Groups["url"].Value.Contains(currentDomain))
                            return (m.Groups["url"].Value + "/" + m.Groups["path"].Value + m.Groups["name"].Value).Replace("&amp;", "&");

                        if (string.IsNullOrEmpty(m.Groups["path"].Value) && string.IsNullOrEmpty(m.Groups["name"].Value))
                            return ("/");

                        var path = Helpers.CheckPathNameLength(m.Groups["path"].Value);
                        var name = Helpers.CheckPathNameLength(m.Groups["name"].Value);

                        if (!string.IsNullOrEmpty(path) && string.IsNullOrEmpty(name))
                        {                            
                            path = path.Substring(0, path.LastIndexOf("/", StringComparison.Ordinal));
                            path = Helpers.CheckPathNameLength(path);
                            return (path + ".html").Replace("?", "_").Replace("|", "/");
                        }

                        // correct files and dirs urls in the file
                        if (Helpers.IsFile(name))
                        {
                            var url = ("/" + path + name);
                            try
                            {
                                url = url.Replace("?", "_");
                                url = url.Replace("&amp;", "_").Replace("&", "_");
                                url = url.Replace(",", "/");

                                if (url.Contains(".js") && !url.EndsWith(".js"))
                                    url += ".js";
                                else if (url.Contains(".css") && !url.EndsWith(".css"))
                                    url += ".css";
                            }
                            catch (Exception) { }
                            return url;
                        }
                        if (Helpers.IsWebPage(name))
                        {
                            if (!name.Substring(name.LastIndexOf(".", StringComparison.Ordinal)).Equals(".htm"))
                            {
                                try { name = name.Replace(name.Substring(name.LastIndexOf(".", StringComparison.Ordinal)), ".html"); }
                                catch (Exception) { }
                            }
                            
                            return ("/" + path + name).Replace("?", "_").Replace("|", "/");
                        }

                        return ("/" + path + name + ".html").Replace("?", "_").Replace("|", "/");
                    }).Replace("&amp;", "_");
                    // 
                    regex = new Regex(Constants.Patterns.UrlsPattern);
                    foreach (Match m in regex.Matches(newFile))
                    {
                        var tag = m.Groups["tag"].Value;
                        var path = Helpers.CheckPathNameLength(m.Groups["path"].Value);
                        var name = Helpers.CheckPathNameLength(m.Groups["name"].Value);
                        if (path.StartsWith("/")) continue;
                        if (path.StartsWith("http://") || path.StartsWith("https://")) continue;
                        if (string.IsNullOrEmpty(path))
                        {
                            if (string.IsNullOrEmpty(name)) continue;
                        }
                        // TODO: this is fast fix for urls that don't contain web archive trash part
                        if (Helpers.IsWebPage(name))
                        {
                            if (!name.Substring(name.LastIndexOf(".", StringComparison.Ordinal)).Equals(".htm"))
                            {
                                try { name = name.Replace(name.Substring(name.LastIndexOf(".", StringComparison.Ordinal)), ".html"); }
                                catch (Exception) { }
                            }
                        }

                        newFile = newFile.Replace(tag + m.Groups["path"].Value + m.Groups["name"].Value, tag + "/" + path + name);
                    }
                }

                File.WriteAllText(filePath, newFile);
            }
            catch (Exception) { }
        }
    }
}
