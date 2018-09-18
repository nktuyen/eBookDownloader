using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml.XPath;
using System.Windows.Forms;
using System.IO;

namespace eBookDownloader
{
    public sealed class BookFoundEventArg : EventArgs
    {
        public string Title { get; set; }
        public string URL{ get; set; }
        public BookFoundEventArg(string title, string url)
        {
            Title = title;
            URL = url;
        }
    }
    public abstract class BookStore
    {
        protected string _name = string.Empty;
        protected string _home = string.Empty;
        protected string _keyword = string.Empty;
        protected string _query = string.Empty;
        protected BookStore _instance = null;
        protected WebRequest _request = null;

        public delegate int BookFoundEventHandler(object sender, BookFoundEventArg e);
        public event BookFoundEventHandler BookFound;
        public delegate bool QueryCancelEventHandler();
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

        public BookStore Instance
        {
            get { return _instance; }
        }

        public BookStore(string name, string home="")
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

        public abstract Dictionary<string,string> Search(string keyword = "");
        protected abstract Dictionary<string,string> SearchInPage(int page);
        protected abstract KeyValuePair<string,string> SearchLink(string title);
        protected virtual void OnBookFound(KeyValuePair<string,string> file)
        {
            BookFound?.Invoke(this, new BookFoundEventArg(file.Value, file.Key));
        }

        protected void OnSearchStarted(string keyword)
        {
            SearchStarted?.Invoke(keyword);
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
    }
}
