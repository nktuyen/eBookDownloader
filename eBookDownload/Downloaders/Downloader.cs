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
        public int ID { get; set; }
        public string Location { get; set; }
        public bool Success { get; set; }
        public DownloadEventArg(string title, string url, int id = -1, string location="")
        {
            Title = title;
            URL = url;
            ID = id;
            Location = location;
            Success = false;
        }
    }
    public abstract class Downloader
    {
        protected string _name = string.Empty;
        protected string _home = string.Empty;
        protected string _keyword = string.Empty;
        protected string _query = string.Empty;
        protected Downloader _instance = null;
        protected WebRequest _request = null;

        public delegate int FileFoundEventHandler(object sender, DownloadEventArg e);
        public event FileFoundEventHandler FileFound;
        public delegate bool QueryCancelEventHandler();
        public event QueryCancelEventHandler QueryCancel;
        public delegate void FileDownloadedEventHandler(object sender, DownloadEventArg e);
        public event FileDownloadedEventHandler FileDownloaded;
        public delegate void AddKeywordEventHandler(string keyword);
        public event AddKeywordEventHandler AddKeyword;

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

        public abstract Dictionary<string,string> Search(string keyword = "", bool bDownloadDirectly = false);
        protected abstract Dictionary<string,string> SearchBooksInPage(int page, bool bDownloadDirectly = false);
        protected abstract KeyValuePair<string,string> SearchBook(string link, bool bDownloadDirectly = false);
        protected virtual int OnFileFound(KeyValuePair<string,string> file)
        {
            if (FileFound != null)
                return FileFound(this, new DownloadEventArg(file.Value, file.Key));
            else
                return -1;
        }

        protected virtual void OnFileDownloaded(int id, KeyValuePair<string, string> file, string location)
        {
            if (FileDownloaded != null)
            {
                DownloadEventArg e = new DownloadEventArg(file.Value, file.Key, id, location);
                e.Success = (location != string.Empty);
                FileDownloaded(this, e);
            }
        }

        protected void OnKeywordAdded(string keyword)
        {
            AddKeyword?.Invoke(keyword);
        }

        private string ReplaceSpecialCharacters(string input)
        {
            string str1 = input.Replace('\\','_');
            string str2 = str1.Replace('*', '_');
            string str3 = str2.Replace('/', '_');
            string str4 = str3.Replace('<', '_');
            string str5 = str4.Replace('>', '_');
            string str6 = str5.Replace('?', '_');
            string str7 = str6.Replace(':', '_');
            string str8 = str7.Replace(';', '_');
            return str8;
        }

        public string DownloadFile(string url, string name, int id)
        {
            if (IsCancel)
                return string.Empty;

            if (url == null || url == string.Empty)
                return string.Empty;

            WebClient webClient = new WebClient();
            string strExt = string.Empty;
            int extPos = url.LastIndexOf(".");
            if (extPos > 0)
                strExt = url.Substring(extPos);


            string strPath = WorkingDirectory + "\\" + _keyword + "\\";
            string strName = name + strExt;

            System.IO.DirectoryInfo directory = System.IO.Directory.CreateDirectory(strPath);
            if (!directory.Exists)
                directory.Create();

            try
            {
                if (IsCancel)
                    return string.Empty;
                string location = strPath + ReplaceSpecialCharacters(strName);
                webClient.DownloadFile(url, location);
                DownloadEventArg e = new DownloadEventArg(name, url, id, location);
                e.Success = true;
                FileDownloaded(this, e);
            }
            catch (Exception ex)
            {
                if (System.IO.File.Exists(strPath + strName))
                    System.IO.File.Delete(strPath + strName);

                DownloadEventArg e = new DownloadEventArg(name, url, id, string.Empty);
                e.Success = false;
                FileDownloaded(this, e);
                return string.Empty;
            }
            return strPath + strName;
        }
    }
}
