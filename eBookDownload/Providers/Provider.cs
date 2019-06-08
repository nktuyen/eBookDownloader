using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml.XPath;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;

namespace eBookDownloader
{
    public sealed class BookEventArg : EventArgs
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string URL{ get; set; }
        public string Path { get; set; }
        public int Progress { get; set; }
        public int Index { get; set; }
        public bool Download { get; set; }
        public bool Overwriten { get; set; }
        public int Status { get; set; }
        public bool Group { get; set; }
        public string Category { get; set; }
        public BookEventArg(string id, string title, string url)
        {
            ID = id;
            Title = title;
            URL = url;
            Path = string.Empty;
            Progress = 0;
            Index = -1;
            Status = 0;
            Download = false;
            Overwriten = false;
            Category = string.Empty;
            Group = false;
        }
    }
    public abstract class Provider
    {
        protected Dictionary<string, WebClient> _downloaders = new Dictionary<string, WebClient>();

        protected string _name = string.Empty;
        protected string _home = string.Empty;
        protected string _encodedKeyword = string.Empty;
        protected string _keyword = string.Empty;
        protected string _query = string.Empty;
        protected WebRequest _request = null;

        public delegate void BookFoundEventHandler(object sender, BookEventArg e);
        public event BookFoundEventHandler BookFound;
        public delegate void BookDownloadingEventHandler(object sender, BookEventArg e);
        public event BookDownloadingEventHandler BookDownloading;
        public delegate void BookDownloadCompletedEventHandler(object sender, BookEventArg e);
        public event BookDownloadCompletedEventHandler BookDownloadCompleted;
        public delegate bool QueryCancelEventHandler();
        public delegate void BookDownloadingFileExistEventHandler(object sender, BookEventArg e);
        public event BookDownloadingFileExistEventHandler BookDownloadingFileExist;
        public event QueryCancelEventHandler QueryCancel;
        public delegate void SearchStartedEventHandler(string keyword);
        public event SearchStartedEventHandler SearchStarted;

        public string Name
        {
            get { return _name; }
        }

        public string Home {
            get { return _home; }
        }

        protected bool IsCancel
        {
            get
            {
                if ((this.QueryCancel != null) && (this.QueryCancel()))
                    return true;
                else
                    return false;
            }
        }


        public string WorkingDirectory { get; set; }

        public Provider(string name, string home="")
        {
            if ((null==name)||(string.Empty == name))
            {
                throw new Exception("Name cannot be empty.");
            }
            _name = name;
            _home = home;
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int lastIdx = exePath.LastIndexOf("\\");
            if (lastIdx > 0)
                WorkingDirectory = exePath.Substring(0, lastIdx);
            else
                WorkingDirectory = System.Environment.CurrentDirectory;
        }

        public void Cleanup()
        {
            foreach(WebClient webClient in _downloaders.Values)
            {
                if (webClient.IsBusy)
                    webClient.CancelAsync();

                webClient.Dispose();
            }
            _downloaders.Clear();
        }

        public abstract Dictionary<string,string> Search(string keyword = "");
        protected abstract Dictionary<string,string> SearchInPage(int page);
        protected abstract KeyValuePair<string,string> SearchLink(string title);
        protected void OnSearchStarted(string keyword)
        {
            SearchStarted?.Invoke(keyword);
        }
        protected void OnBookFound(BookEventArg e)
        {
            e.Category = _keyword;
            BookFound?.Invoke(this, e);
            if (e.Download)
                DownloadBook(e);
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BookEventArg file = e.UserState as BookEventArg;
            file.Progress = e.ProgressPercentage;
            BookDownloading?.Invoke(this, file);
        }

        private void OnDownloadFinished(object sender, AsyncCompletedEventArgs e)
        {
            BookEventArg file = e.UserState as BookEventArg;

            if (e.Cancelled)
            {
                file.Status = -1;
            }
            else if(e.Error!=null)  //Failed
            {
                file.Status = e.Error.HResult;
            }
            else //Success
            {
                file.Status = 0;
            }

            BookDownloadCompleted?.Invoke(this, file);
            _downloaders.Remove(file.URL);
        }

        private bool EnsureDirectoryExist(string filePath)
        {
            int pos = filePath.IndexOf("\\");
            string strPath = string.Empty;
            bool res = true;

            while (pos > 0 && pos < filePath.Length)
            {
                if (pos > 0)
                    strPath = filePath.Substring(0, pos);
                else
                    strPath = filePath;

                if (!System.IO.Directory.Exists(strPath))
                {
                    try
                    {
                        DirectoryInfo di = System.IO.Directory.CreateDirectory(strPath);
                        if (di != null && di.Exists)
                            res = true;
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        res = false;
                    }
                }

                pos = filePath.IndexOf("\\", pos+1);
            }

            return res;
        }

        protected void DownloadBook(BookEventArg e)
        {
            if (IsCancel)
            {
                e.Status = -1;
                e.Progress = 100;
                BookDownloadCompleted?.Invoke(this, e);
                return;
            }

            WebClient webClient = null;
            if (_downloaders.ContainsKey(e.URL))
            {
                webClient = _downloaders[e.URL];
            }
            else
            {
                webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFinished);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(OnDownloadProgressChanged);
                _downloaders.Add(e.URL, webClient);
            }

            if (IsCancel)
            {
                e.Status = -1;
                e.Progress = 100;
                BookDownloadCompleted?.Invoke(this, e);
                return;
            }

            if (webClient != null)
            {
                if (webClient.IsBusy)
                    webClient.CancelAsync();

                if(File.Exists(e.Path))
                {
                    System.IO.FileInfo fi = new FileInfo(e.Path);
                    if (fi.Length > 0)
                    {
                        BookDownloadingFileExist?.Invoke(this, e);
                        if (!e.Overwriten)
                        {
                            e.Status = 2;
                            e.Progress = 100;
                            BookDownloadCompleted?.Invoke(this, e);
                            return;
                        }
                    }
                }

                if (IsCancel)
                {
                    e.Status = -1;
                    e.Progress = 100;
                    BookDownloadCompleted?.Invoke(this, e);
                    return;
                }

                try
                {
                    if (EnsureDirectoryExist(e.Path))
                    {
                        webClient.DownloadFile(new Uri(e.URL), e.Path);
                        e.Status = 0;
                    }
                    else
                    {
                        e.Status = -1;
                    }
                    e.Progress = 100;
                    BookDownloadCompleted?.Invoke(this, e);
                    return;
                }
                catch(Exception ex)
                {
                    e.Status = 2;
                    e.Progress = 100;
                    BookDownloadCompleted?.Invoke(this, e);
                    return;
                }
            }
        }
    }
}
