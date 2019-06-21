using System.Threading;

namespace ArchiveSiteReBuilder.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class WebSites
    {
        /// <summary>
        /// The websites lists. <para>Keys: rebuilt, checked, toRebuild.</para>
        /// </summary>
        public ConcurrentDictionary<string, List<WebSite>> Lists { get; private set; }

        /// <summary>
        /// The current website instance
        /// </summary>
        public WebSite Current { get; private set; }

        /// <summary>
        /// True if server is started
        /// </summary>
        public bool IsServerStarted
        {
            get { return _server.IsStarted; }
        }

        /// <summary>
        /// The settings instance
        /// </summary>
        public Settings AsrSettings { get; set; }        

        /// <summary>
        /// The server instance
        /// </summary>
        private HttpServer _server;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public WebSites()
        {
            AsrSettings = new Settings();
            Lists = new ConcurrentDictionary<string, List<WebSite>>();

            _server = new HttpServer();
            _server.Start();    
        }

        /// <summary>
        /// The function checks if we already have some rebuilt websites
        /// </summary>
        public async Task CheckForRebuiltWebSitesAsync()
        {
            if (File.Exists(Constants.Common.WebsitesLocalBase))
                await Task.Run(() => LoadList());
            else
            {
                if (!Directory.Exists(Constants.Common.DomainsDir)) return;

                var key = "rebuilt";
                var directories = await Task.Run(() => Directory.EnumerateDirectories(Constants.Common.DomainsDir).ToList());

                await Task.Run(() =>
                {
                    for (var index = 0; index < directories.Count; index++)
                        directories[index] = directories[index].Replace(Constants.Common.DomainsDir, "");

                    try { directories.ForEach(directory => AddNew(key, directory, true)); }
                    catch (Exception) { }
                });

                if (Lists.Count < 1) return;

                if (Lists.ContainsKey(key))
                    UpdateLocalBase();
            }
        }

        /// <summary>
        /// The function gets the list of the names of the domains
        /// </summary>
        /// <returns>List of the domain names</returns>
        public List<string> GetNamesList(string listName = "")
        {
            try
            {
                return string.IsNullOrEmpty(listName) ? Lists.Values.SelectMany(list => list.Select(webSite => webSite.DomainName)).ToList()
                                                  : Lists[listName].Select(webSite => webSite.DomainName).ToList();
            }
            catch (Exception) { return new List<string>(); }
        }

        /// <summary>
        /// The function gets the website instance by the specified name
        /// </summary>
        /// <param name="domainName">Domain name</param>
        /// <returns>Website instance</returns>
        public WebSite GetWebSiteByName(string domainName)
        {
            return Lists.Values.Select(list => list.Find(site => site.DomainName.Equals(domainName))).FirstOrDefault(webSite => webSite != null);
        }

        /// <summary>
        /// The function sets the root directory of the server
        /// </summary>
        /// <param name="rootDirectory">New root directory</param>
        /// <returns>True if successful</returns>
        public bool SetCurrentRootDirectory(string rootDirectory)
        {
            _server.RootDirectory = rootDirectory;

            return true;
        }

        /// <summary>
        /// The function adds domain to the domains list and set it current
        /// </summary>
        /// <param name="listName">In which list we should add this domain</param>
        /// <param name="domain">Domain name</param>
        /// <param name="isInit">Needed on the application startup to increase check time</param>
        public async Task AddNew(string listName, string domain, bool isInit = false)
        {
            await Task.Run(() =>
            {
                Current = new WebSite(domain, isInit);

                if (Lists.ContainsKey(listName))
                {
                    if (!Lists[listName].Contains(Current))
                        Lists[listName].Add(Current);
                    else
                        Current = Lists[listName].Find(webSite => webSite.Equals(Current));
                }
                else
                {
                    try
                    {
                        if (!Lists.TryAdd(listName, new List<WebSite> { Current }))
                            Lists[listName].Add(Current);
                    }
                    catch (Exception) { }
                }

                Current.SetPreviewUrl(_server.BaseUrl.ToString());
                SetCurrentRootDirectory(Current.DomainDirectory);
            });
        }

        /// <summary>
        /// The function changes websites's list.
        /// <para>For example: from "toRebuild" list to the "rebuilt" list</para>
        /// </summary>
        /// <param name="newListKey">New list name where we should move the website</param>
        /// <param name="webSite">Current website to move</param>
        /// <returns>True if website was added to the new list and removed from the previous list</returns>
        public bool ChangeWebSiteList(string newListKey, WebSite webSite)
        {
            var currentListName = Lists.FirstOrDefault(kvp => kvp.Value.Any(value => value.Equals(webSite))).Key;

            var isRemoved = Lists[currentListName].Remove(webSite);

            try
            {
                if (Lists.ContainsKey(newListKey))
                    Lists[newListKey].Add(webSite);
                else
                {
                    try
                    {
                        if (!Lists.TryAdd(newListKey, new List<WebSite> { webSite }))
                            Lists[newListKey].Add(webSite);
                    }
                    catch (Exception) { }
                }
                
            }
            catch (Exception) { return false; }

            return isRemoved;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task RebuildList(IProgress<KeyValuePair<string, KeyValuePair<string, int>>> progress, CancellationToken ct, Constants.Settings.OverwriteMode overwriteMode = Constants.Settings.OverwriteMode.IgnoreExisting)
        {
            if (Lists["toRebuild"].Count > 0)
            {
                var tasks = new ConcurrentBag<Task>();

                Parallel.ForEach(Lists["toRebuild"], webSite =>
                {
                    webSite.SetProgressAndCancellation(progress, ct);
                    tasks.Add(Task.Run(() => webSite.Rebuild(overwriteMode)));
                });

                await Task.WhenAll(tasks);
            }       
        }

        /// <summary>
        /// The function updates local database.
        /// <para>Maybe some logic will be added in future</para>
        /// </summary>
        /// <returns></returns>
        public bool UpdateLocalBase()
        {
            return SaveList();
        }

        /// <summary>
        /// DO NOTHING FOR NOW!
        /// <para>The function saves websites list to the local database</para>
        /// </summary>
        /// <returns>True if successfully saved</returns>
        private bool SaveList()
        {
            var filePath = Constants.Common.WebsitesLocalBase;
            try
            {
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// DO NOTHING FOR NOW!
        /// <para>The function loads saved websites from the local database</para>
        /// </summary>
        /// <returns>True if successfully loaded</returns>
        private bool LoadList()
        {
            var filePath = Constants.Common.WebsitesLocalBase;
            try
            {
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}
