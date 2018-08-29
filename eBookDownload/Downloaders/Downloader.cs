using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml.XPath;
using System.Windows.Forms;
using System.IO;

namespace eBookDownload
{
    public sealed class DownloadEventArg : EventArgs
    {
        public string Title { get; set; }
        public string URL{ get; set; }
        public DownloadEventArg(string title, string url)
        {
            Title = title;
            URL = url;
        }
    }
    public abstract class Downloader
    {
        protected string _name = string.Empty;
        protected string _home = string.Empty;
        protected string _query = string.Empty;
        protected Downloader _instance = null;
        protected WebRequest _request = null;

        public delegate void DownloadEventHandler(object sender, DownloadEventArg e);
        public event DownloadEventHandler FileFound;

        public string Name
        {
            get { return _name; }
        }

        public string Home {
            get { return _home; }
        }

        public string WorkingDirectory { get; set; }

        public Downloader Instance
        {
            get { return _instance; }
        }

        public Downloader(string name, string home="")
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

        public abstract Dictionary<string,string> Download(string keyword = "");
        protected abstract Dictionary<string,string> DownloadBooksInPage(int page);
        protected abstract KeyValuePair<string,string> DownloadBook(string link);
        protected virtual void OnFileFound(KeyValuePair<string,string> file)
        {
            FileFound?.Invoke(this, new DownloadEventArg(file.Value, file.Key));
        }
        protected string DownloadFile(string url, string contentType = "")
        {
            if (url == null || url == string.Empty)
                return string.Empty;

            WebClient webClient = new WebClient();
            string filePath = WorkingDirectory + url.Substring(url.LastIndexOf("/"));
            try
            {
                webClient.DownloadFile(url, filePath);
            }
            catch(Exception ex)
            {
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                filePath = string.Empty;
            }
            return filePath;
        }
    }
}
