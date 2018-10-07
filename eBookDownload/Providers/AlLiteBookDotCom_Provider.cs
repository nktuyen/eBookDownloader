using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using System.Windows.Forms;

namespace eBookDownloader
{
    public class AlLiteBookDotCom_Provider : Provider
    {
        private static AlLiteBookDotCom_Provider _inst = null;

        private AlLiteBookDotCom_Provider() : base("http://www.allitebooks.com", "http://www.allitebooks.com")
        {

        }

        public static AlLiteBookDotCom_Provider GetInstance()
        {
            if (null == _inst)
            {
                _inst = new AlLiteBookDotCom_Provider();
            }

            return _inst;
        }

        public override Dictionary<string, string> Search(string keyword = "")
        {
            _keyword = (WebUtility.UrlEncode(keyword));
            _query = "?s=" + _keyword;
            HttpWebRequest httpReq = WebRequest.Create(Home + _query) as HttpWebRequest;
            Dictionary<string, string> files = new Dictionary<string, string>();
            if (httpReq != null)
            {
                if (IsCancel)
                    return files;
                WebResponse webResponse = httpReq.GetResponse();
                Stream html = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(html);
                string htmlString = reader.ReadToEnd();

                string strScriptOpen = "<div class=\"pagination clearfix\">";
                string strScripClose = "</div>";
                int first = htmlString.IndexOf(strScriptOpen, 0);
                int last = 0;
                bool bFoundCloseTag = false;
                string code = string.Empty;
                int ff = 0, ll = 0, pgs = 1;
                while (first < htmlString.Length)
                {
                    if (IsCancel)
                        return files;

                    bFoundCloseTag = true;
                    last = htmlString.IndexOf(strScripClose, first + strScriptOpen.Length + 1);
                    if ((last > htmlString.Length) || (-1 == last))
                    {
                        last = htmlString.Length;
                        bFoundCloseTag = false;
                    }

                    code = htmlString.Substring(first + strScriptOpen.Length, last - first - (bFoundCloseTag ? strScripClose.Length : 0));
                    ff = code.IndexOf("<span class=\"pages\">");
                    if (ff > 0)
                    {
                        ff += "<span class=\"pages\">".Length;
                        ll = code.IndexOf("</span>", ff);
                        if (ll > ff)
                        {
                            string str = code.Substring(ff + 1, ll - ff - 1);
                            string subStr = str;

                            ff = str.LastIndexOf("/");
                            subStr = str.Substring(ff + 1);
                            str = subStr.Trim();
                            ll = str.IndexOf(" ", ff + 1);
                            if (ll < ff)
                                ll = ff;
                            subStr = str.Substring(0, ll - ff + 1);
                            if (int.TryParse(subStr, out pgs))
                                break;
                        }
                    }
                    first = htmlString.IndexOf(strScriptOpen, last + (bFoundCloseTag ? strScripClose.Length : 0) + 1);
                }

                if (pgs > 0)
                {
                    OnSearchStarted(keyword);
                    for (int i = 1; i <= pgs; i++)
                    {
                        if (IsCancel)
                            return files;
                        var res = SearchInPage(i);
                        foreach (KeyValuePair<string, string> books in res)
                        {
                            if (IsCancel)
                                return files;
                            files.Add(books.Key, books.Value);
                        }
                    }
                }
            }

            return files;
        }

        protected override Dictionary<string, string> SearchInPage(int page)
        {
            Dictionary<string, string> files = new Dictionary<string, string>();
            HttpWebRequest httpReq = WebRequest.Create(Home + "/page/" + page + "/" + _query) as HttpWebRequest;
            if (httpReq != null)
            {
                if (IsCancel)
                    return files;

                WebResponse webResponse = httpReq.GetResponse();
                Stream html = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(html);
                string htmlString = reader.ReadToEnd();

                string strDIVOpen = "<h2";
                string strDIVClose = "</h2>";
                int first = htmlString.IndexOf(strDIVOpen, 0);
                int last = 0;
                string strhRef = string.Empty;
                string strAlt = string.Empty;
                bool bFoundCloseTag = false;
                string code = string.Empty;
                int ff = 0, ll = 0;
                KeyValuePair<string, string> bookInfo = new KeyValuePair<string, string>();
                while (first < htmlString.Length)
                {
                    if (IsCancel)
                        return files;

                    bFoundCloseTag = true;
                    last = htmlString.IndexOf(strDIVClose, first + strDIVOpen.Length + 1);
                    if ((last > htmlString.Length) || (-1 == last))
                    {
                        last = htmlString.Length;
                        bFoundCloseTag = false;
                    }

                    code = htmlString.Substring(first + strDIVOpen.Length, last - first - (bFoundCloseTag ? strDIVClose.Length : 0));
                    ff = code.IndexOf("class=\"entry-title\"");
                    if (ff > 0)
                    {
                        ff = code.IndexOf("<a href=\"", ff);
                        if ((ff > 0) && (ff < code.Length))
                        {
                            ff += "<a href=\"".Length;
                            ll = code.IndexOf("\"", ff);
                            if ((ll > code.Length) || (-1 == ll))
                            {
                                ll = code.Length;
                            }
                            strhRef = code.Substring(ff, ll - ff);
                            if (strhRef.Trim().Length > 0)
                            {
                                if (IsCancel)
                                    return files;

                                bookInfo = SearchLink(strhRef);
                                try
                                {
                                    if (bookInfo.Value.Length > 0)
                                    {
                                        files.Add(bookInfo.Key, bookInfo.Value);
                                    }
                                }
                                catch (Exception ex) {; }
                            }
                        }
                    }
                    first = htmlString.IndexOf(strDIVOpen, last + (bFoundCloseTag ? strDIVClose.Length : 0) + 1);
                    if (first < 0)
                        break;
                }
            }

            return files;
        }

        protected override KeyValuePair<string, string> SearchLink(string link)
        {
            string strHome = link.Substring(0, Home.Length);
            string strURL = Home + link;
            if (string.Compare(strHome, Home, true) == 0)
                strURL = link;
            
            HttpWebRequest httpReq = WebRequest.Create(strURL) as HttpWebRequest;
            KeyValuePair<string, string> file = new KeyValuePair<string, string>();
            if (httpReq != null)
            {
                if (IsCancel)
                    return file;

                WebResponse webResponse = httpReq.GetResponse();
                Stream html = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(html);
                string htmlString = reader.ReadToEnd();                

                string strDIVOpen = "<h1 class=\"single-title\">";
                string strDIVClose = "</h1>";
                int first = htmlString.IndexOf(strDIVOpen, 0);
                int last = htmlString.IndexOf(strDIVClose, first+strDIVOpen.Length);
                string strLink = string.Empty;
                string strTemp = string.Empty;
                string strTitle = string.Empty;

                strURL = string.Empty;
                
                if(first > 0)
                {
                    if (last < first)
                        last = htmlString.Length;

                    strTitle = htmlString.Substring(first+strDIVOpen.Length, last - first- strDIVOpen.Length).Trim();
                }

                strDIVOpen = "<span class=\"download-links\">";
                strDIVClose = "</span>";

                first = htmlString.IndexOf(strDIVOpen,first);
                if (first > 0)
                {
                    last = htmlString.IndexOf(strDIVClose, first);
                    if (last > first)
                    {
                        strDIVOpen = "<a href=\"";
                        strDIVClose = "\"";
                        first = htmlString.IndexOf(strDIVOpen, first);
                        if (first > 0)
                        {
                            last = htmlString.IndexOf(strDIVClose, first+strDIVOpen.Length);
                            if (last > first)
                            {
                                strURL = htmlString.Substring(first + strDIVOpen.Length, last - first - strDIVOpen.Length);
                                if (strURL.Length > 0)
                                {
                                    first = strURL.LastIndexOf("/");
                                    last = strURL.LastIndexOf(".");
                                    if (last < first)
                                        last = strURL.Length;

                                    string strID = strURL.Substring(first, last - first + 1);

                                    if (strTitle == string.Empty)
                                    {
                                        strTitle = strID;
                                    }

                                    file = new KeyValuePair<string, string>(strURL, WebUtility.HtmlDecode(strTitle));
                                    BookEventArg e = new BookEventArg(strID, file.Value, file.Key);
                                    OnBookFound(e);
                                    return file;
                                }
                            }
                        }
                    }
                }
            }
            return new KeyValuePair<string, string>();
        }
    }
}
