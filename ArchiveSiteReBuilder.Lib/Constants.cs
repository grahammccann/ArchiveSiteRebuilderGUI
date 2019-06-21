namespace ArchiveSiteReBuilder.Lib
{
    using System.IO;

    /// <summary>
    /// Contains all constants used in the ASR.
    /// </summary>
    public static class Constants
    {
        public static class Settings
        {
            /// <summary>
            /// 
            /// </summary>
            public enum OverwriteMode
            {
                IgnoreExisting = 0,
                OverwriteExisting = 1
            }

            /// <summary>
            /// 
            /// </summary>
            public enum RebuildMode
            {
                RebuildOne = 0,
                RebuildList = 1
            }
        }

        /// <summary>
        /// Common ASR constants.
        /// </summary>
        public static class Common
        {
            /// <summary>
            /// Gets the application title.
            /// </summary>
            public static string AppTitle
            {
                get { return "Archive Site ReBuilder"; }
            }

            /// <summary>
            /// Gets the url for application change log
            /// </summary>
            public static string AppChangeLogUrl 
            {
                get { return @"http://www.x-catcher.com/archive-site-rebuilder/change.log"; }
            }

            /// <summary>
            /// Gets the url for application update status
            /// </summary>
            public static string AppUpdateStatusUrl
            {
                get { return @"http://www.x-catcher.com/archive-site-rebuilder/v.txt"; }
            }

            /// <summary>
            /// Gets the url for application update
            /// </summary>
            public static string AppUpdateUrl
            {
                get { return @"http://www.x-catcher.com/archive-site-rebuilder/dl/ASR.rar"; }
            }

            /// <summary>
            /// Gets the directory in which will be saved all domains.
            /// </summary>
            public static string DomainsDir
            {
                get { return Directory.GetCurrentDirectory() + @"\domains\"; }
            }

            /// <summary>
            /// Gets the name of the local rebuilt websites base.
            /// </summary>
            public static string WebsitesLocalBase
            {
                get { return DomainsDir + @"rebuiltWebsites.mdf"; }
            }
        }

        /// <summary>
        /// Contains all WBM URLs for the websites access.
        /// </summary>
        public static class ArchiveUrls
        {
            /// <summary>
            /// Gets the main WBM URL.
            /// </summary>
            public static string ArchiveUrl
            {
                get { return "http://web.archive.org"; }
            }

            /// <summary>
            /// Gets the links and dates in the response.
            /// </summary>
            public static string GetTimeMapUrl
            {
                get { return ArchiveUrl + "/web/timemap/link/"; }
            }

            /// <summary>
            /// Gets the snapshot of the site in the response.
            /// </summary>
            public static string GetTimeGateUrl
            {
                get { return ArchiveUrl + "/web/"; }
            }

            /// <summary>
            /// Gets the JSON with url status in the response.
            /// </summary>
            public static string GetStatusUrl
            {
                get { return "http://archive.org/wayback/available?url=http://"; }
            }
        }

        /// <summary>
        /// Patterns for Regular Expressions.
        /// </summary>
        public static class Patterns
        {
            /// <summary>
            /// Gets the beginning of the WBM Toolbar.
            /// </summary>
            public static string WaybackToolbarBegin
            {
                get { return @"<!-- BEGIN WAYBACK TOOLBAR INSERT -->"; }
            }

            /// <summary>
            /// Gets the end of the WBM Toolbar.
            /// </summary>
            public static string WaybackToolbarEnd
            {
                get { return @"<!-- END WAYBACK TOOLBAR INSERT -->"; }
            }

            /// <summary>
            /// Gets pattern for the WBM analytics code.
            /// </summary>
            public static string WaybackAnalyticsJsCssPattern
            {
                get { return @"<(link|script)\s*type=""text/(.*)""\s*(rel=""stylesheet"")?\s*(href|src)=""/static/(.*)""(/)?>(</script>)?"; }
            }

            /// <summary>
            /// Gets pattern for the WBM analytics code.
            /// </summary>
            public static string WaybackAnalyticsServerPattern
            {
                get { return @"<script type=""text/javascript"">archive_analytics.values.server_name=""(.*)"";archive_analytics.values.server_ms=(.*);</script>"; }
            }

            /// <summary>
            /// Gets the pattern for getting URLs and Dates from the time map.
            /// </summary>
            public static string MapPattern
            {
                get
                {
                    return @"<(?<fullUrl>" + ArchiveUrls.GetTimeGateUrl +
                           @"\d+/(.*?))>;(.*?)datetime=[""|'](?<date>.*?)[""|']";
                }
            }

            /// <summary>
            /// Gets the pattern for getting URLs that contains only JavaScript files.
            /// </summary>
            public static string JsFilesPattern
            {
                get { return FileUrlBeginPattern + @"(js)" + FileUrlEndPattern; }
            }

            /// <summary>
            /// Gets the pattern for getting URLs that contains only CSS files.
            /// </summary>
            public static string CssFilesPattern
            {
                get { return FileUrlBeginPattern + @"(css)" + FileUrlEndPattern; }
            }

            /// <summary>
            /// Gets the pattern for getting URLs that contains only images.
            /// </summary>
            public static string ImgsFilesPattern
            {
                get { return FileUrlBeginPattern + @"(png|jpe?g|ico|gif|bmp|tiff)" + FileUrlEndPattern; }
            }

            /// <summary>
            /// Gets the pattern for getting full url with archive prefix.
            /// </summary>
            public static string FullUrlArchivePattern
            {
                get
                {
                    return @"(?<fullUrl>" + @"(?<trash>/web/(([^/""'>])*)?/)" +
                           @"(?<url>https?://(([^/""'>])*))?/?" + @"(?<path>(([^><""'()])+/)*)?" +
                           @"((?<name>[a-zA-Z0-9\-\.\?\-\+=_,%&;:]{0,111}))?" + @")";
                }
            }

            /// <summary>
            /// Gets the pattern for searching URLs in the tags like a href|src|url.
            /// </summary>
            public static string UrlsPattern
            {
                get
                {
                    return
                        @"(?<tag>(href|src|url)(=|\()(""|\')?)(?<path>(([^><""'()])+/)*)(?<name>[a-zA-Z0-9\-\._]{0,64})";
                }
            }

            /// <summary>
            /// Gets the beginning of the files searching pattern.
            /// </summary>
            private static string FileUrlBeginPattern
            {
                get { return @"(src|href|url)(=|\()(""|\')?(?<url>([^><""'()])+\."; }
            }

            /// <summary>
            /// Gets the end of the files searching pattern.
            /// </summary>
            private static string FileUrlEndPattern
            {
                get { return @")(?=(""|\'|\)))"; }
            }
        }

        public static class Test
        {
            public static string[] TestDomains
            {
                get
                {
                    return new string[]
                    {
                        "buckwildbaitco.com",
                        "payday-dreams.co.uk",
                        "dglitigators.com",
                        "thebeastpageant.jubadaba.com",
                        "xkcd.com",
                        "billymilligan.ru",
                        "catlitterboxfurniture.net",
                        "thesmallesthouseingreatbritain.co.uk",
                        "hastingschess.org.uk",
                        "www.liveoakdatarecovery.com",
                        "www.timothygood.co.uk",
                        "e-homebank.com",
                        "epicdressupgames.com",
                        "smalldreams.com.au",
                        "desktopcatcher.com",
                        "www.faq4windowsphone.de",
                        "www.stura-erfurt.de"
                    };
                }
            }
        }
    }
}
