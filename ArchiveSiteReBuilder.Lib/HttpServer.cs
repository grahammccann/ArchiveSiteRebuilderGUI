namespace ArchiveSiteReBuilder
{
    using System;
    using System.IO;    
    using System.Net;
    using System.Net.Mime;
    using System.Text;
    using System.Threading;
    using System.Net.NetworkInformation;
    using ArchiveSiteReBuilder.Lib;

    /// <summary>
    /// The Http Server Class
    /// </summary>
    public class HttpServer
    {
        /// <summary>
        /// 
        /// </summary>
        private class CallbackState
        {
            /// <summary>
            /// The http listener
            /// </summary>
            private readonly HttpListener _listener;
            
            /// <summary>
            /// The auto reset event
            /// </summary>
            private readonly AutoResetEvent _listenForNextRequest;
            
            /// <summary>
            /// Constructor
            /// </summary>
            /// <exception cref="ArgumentNullException">
            /// Listener can not be null
            /// </exception>
            /// <param name="listener">Http listener</param>
            public CallbackState(HttpListener listener)
            {
                if (null == listener) throw new ArgumentNullException("listener");
                _listener = listener;
                _listenForNextRequest = new AutoResetEvent(false);
            }
            
            /// <summary>
            /// The public parameter to get access to the http listener
            /// </summary>
            public HttpListener Listener { get { return _listener; } }
            
            /// <summary>
            /// The public parameter to set listener to listen next request
            /// </summary>
            public AutoResetEvent ListenForNextRequest { get { return _listenForNextRequest; } }
        }

        /// <summary>
        /// 
        /// </summary>
        private ManualResetEvent _stopEvent = new ManualResetEvent(false);
        
        /// <summary>
        /// The port on which the server will be listening for the incoming requests
        /// </summary>
        private int _port;
        
        /// <summary>
        /// The base url on which the server will be listening for the incoming requests
        /// </summary>
        public Uri BaseUrl { get { return new Uri("http://" + "localhost:" + _port + "/"); } }
        
        /// <summary>
        /// True if server is started
        /// </summary>
        public bool IsStarted { get; private set; }
        
        /// <summary>
        /// The root directory of the current website
        /// </summary>
        public string RootDirectory { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">User can specify port, otherwise it will be randomly generated</param>
        public HttpServer(int port = -1)
        {
            RootDirectory = string.Empty;

            IsStarted = false;

            if (!port.Equals(-1) && !port.Equals(0))
                _port = port;
            else
            {
                _port = GetRandomPort();
            }
        }

        /// <summary>
        /// The function starts the web server
        /// </summary>
        public void Start()
        {
            if (IsStarted) return;

            RootDirectory = Constants.Common.DomainsDir;
            
            var tries = 0;
            while (tries < 5)
            {
                try
                {
                    var listener = new HttpListener();

                    listener.Prefixes.Add("http://" + "localhost:" + _port + "/");

                    listener.Start();
                    
                    var state = new CallbackState(listener);
                    ThreadPool.QueueUserWorkItem(Listen, state);

                    IsStarted = true;
                    break;
                }
                catch (Exception)
                {
                    IsStarted = false;
                    tries++;
                }
            }
        }
        
        /// <summary>
        /// The function stops the web server
        /// </summary>
        public void Stop()
        {
            IsStarted = false;

            _stopEvent.Set();
        }


        private int GetRandomPort()
        {
            var rnd = new Random();
            var isPortSet = false;
            var port = 0;

            while (!isPortSet)
            {
                port = rnd.Next(8080, 65535);
                isPortSet = IsPortAvailable(port);
            }

            return port;
        }

        private bool IsPortAvailable(int port)
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            var isAvailable = true;

            foreach (var tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == port)
                {
                    isAvailable = false;
                    break;
                }
            }

            return isAvailable;
        }

        /// <summary>
        /// Listener idle
        /// </summary>
        /// <param name="state">Callback state object</param>
        private void Listen(object state)
        {
            var callbackState = (CallbackState) state;

            while (callbackState.Listener.IsListening)
            {
                callbackState.Listener.BeginGetContext(ListenerCallback, callbackState);

                if (1 == WaitHandle.WaitAny(new WaitHandle[] {callbackState.ListenForNextRequest, _stopEvent}))
                {
                    callbackState.Listener.Stop();
                    break;
                }
            }
        }
        
        /// <summary>
        /// THe function gets the listener context and start processing of the request
        /// </summary>
        /// <param name="ar">Status of the asynchronous operation</param>
        private void ListenerCallback(IAsyncResult ar)
        {
            var callbackState = (CallbackState)ar.AsyncState;
            HttpListenerContext context = null;

            try { context = callbackState.Listener.EndGetContext(ar); }
            catch (Exception) { return; }
            finally { callbackState.ListenForNextRequest.Set(); }

            if (context == null) return;

            ProcessRequest(context);
        }
        
        /// <summary>
        /// The function processes a request and write it to the output stream
        /// </summary>
        /// <param name="context">Listener context</param>
        private void ProcessRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;
            response.StatusCode = 200;
            response.SendChunked = true;

            response.AddHeader("Cache-Control", "no-cache");

            try
            {
                var url = (request.Url.AbsolutePath == "/") ? "index.html" : request.Url.AbsolutePath.Substring(1);
                var path = Path.Combine(RootDirectory, url);

                if (!File.Exists(path))
                    FileNotFound(response);
                else
                    WriteResponse(response, path);
            }
            catch (Exception) { response.Abort(); }
        }
        
        /// <summary>
        /// The function writes the response into the output stream
        /// </summary>
        /// <param name="response">Listener response to write</param>
        /// <param name="path">Path to the page file or page content</param>
        private void WriteResponse(HttpListenerResponse response, string path)
        {
            try
            {
                byte[] data = null;
                if (!path.ToLowerInvariant().Contains("error"))
                {
                    var fileExtension = Path.GetExtension(path).ToLowerInvariant();
                    response.ContentType = GetContentType(fileExtension);

                    data = File.ReadAllBytes(path);
                }
                else
                {
                    response.ContentType = GetContentType(".html");
                    response.ContentEncoding = Encoding.UTF8;
                    // in path will be page content
                    data = Encoding.UTF8.GetBytes(path);
                }

                response.ContentLength64 = data.Length;
                using (var s = response.OutputStream)
                    s.Write(data, 0, data.Length);
            }
            catch (Exception) { InternalServerError(response); }
        }
        
        /// <summary>
        /// The function checks the file extension and sets the content type.
        /// </summary>
        /// <param name="fileExtension">Extension of the read file</param>
        /// <returns>Content type relative to extension of the file</returns>
        private string GetContentType(string fileExtension)
        {
            var contentType = string.Empty;

            switch (fileExtension)
            {
                //images
                case ".gif": contentType = MediaTypeNames.Image.Gif; break;
                case ".jpg":
                case ".jpeg": contentType = MediaTypeNames.Image.Jpeg; break;
                case ".tiff": contentType = MediaTypeNames.Image.Tiff; break;
                case ".png": contentType = "image/png"; break;
                // application
                case ".pdf": contentType = MediaTypeNames.Application.Pdf; break;
                case ".zip": contentType = MediaTypeNames.Application.Zip; break;
                // text
                case ".htm":
                case ".html": contentType = MediaTypeNames.Text.Html; break;
                case ".txt": contentType = MediaTypeNames.Text.Plain; break;
                case ".xml": contentType = MediaTypeNames.Text.Xml; break;
                case ".css": contentType = "text/css"; break;
                case ".js": contentType = "text/javascript"; break;
                // 
                default: contentType = MediaTypeNames.Application.Octet; break;
            }

            return contentType;
        }
        
        /// <summary>
        /// The function sets the response to the "404 Not Found" error.
        /// </summary>
        /// <param name="response">Listener response</param>
        private void FileNotFound(HttpListenerResponse response)
        {
            ServerError(response, (int)HttpStatusCode.NotFound);
        }
        
        /// <summary>
        /// The function sets the response to the "503 Internal Server Error" error.
        /// </summary>
        /// <param name="response">Listener response</param>
        private void InternalServerError(HttpListenerResponse response)
        {
            ServerError(response, (int)HttpStatusCode.InternalServerError);
        }
        
        /// <summary>
        /// The function sets the response to the error and write it to the output stream
        /// </summary>
        /// <param name="response">Listener response</param>
        /// <param name="statusCode">Status code of the error</param>
        private void ServerError(HttpListenerResponse response, int statusCode)
        {
            var bufferString = string.Empty;
            var statusDescription = string.Empty;

            switch (statusCode)
            {
                case 404:
                    statusDescription = " Not Found";
                    bufferString = @"<html><head><title>Error</title></head><body><div align=""center""><h1>404 Not Found</h1><br><a href=""/"">back</a></div></body></html>";
                    break;
                case 500:
                    statusDescription = " Internal Server Error";
                    bufferString = @"<html><head><title>Error</title></head><body><div align=""center""><h1>500 Internal Server Error</h1><br><a href=""/"">back</a></div></body></html>";
                    break;
                default:
                    bufferString = @"<html><head><title>Error</title></head><body><div align=""center""><h1>Server Error</h1><br><a href=""/"">back</a></div></body></html>";
                    break;
            }

            response.StatusCode = statusCode;
            response.StatusDescription = response.StatusCode + statusDescription;
            WriteResponse(response, bufferString);
        }
    }
}
