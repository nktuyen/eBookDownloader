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
    public class SachLapTrinhDotCom_Provider : Provider
    {
        private static SachLapTrinhDotCom_Provider _inst = null;

        private SachLapTrinhDotCom_Provider(): base("http://www.sachlaptrinh.com", "http://www.sachlaptrinh.com")
        {
            
        }

        public static SachLapTrinhDotCom_Provider GetInstance()
        {
            if (null == _inst)
            {
                _inst = new SachLapTrinhDotCom_Provider();
            }

            return _inst;
        }

        public override Dictionary<string,string> Search(string keyword = "")
        {
            _keyword = WebUtility.UrlEncode(WebUtility.UrlEncode(keyword));
            _query = "/searchbooks?keyword=" + _keyword;
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

                string strScriptOpen = "<script>";
                string strScripClose = "</script>";
                int first = htmlString.IndexOf(strScriptOpen, 0);
                int last = 0;
                bool bFoundCloseTag = false;
                string code = string.Empty;
                int ff = 0, ll = 0, pgs = 0;
                while (first < htmlString.Length)
                {
                    if (IsCancel)
                        return files;

                    bFoundCloseTag = true;
                    last = htmlString.IndexOf(strScripClose, first + strScriptOpen.Length + 1);
                    if ((last > htmlString.Length) || (-1 == last) )
                    {
                        last = htmlString.Length;
                        bFoundCloseTag = false;
                    }

                    code = htmlString.Substring(first + strScriptOpen.Length, last - first - (bFoundCloseTag ? strScripClose.Length : 0));
                    ff = code.IndexOf("totalPages:");
                    if (ff > 0)
                    {
                        ff += "totalPages:".Length;
                        ll = code.IndexOf(",", ff);
                        if(ll > ff)
                        {
                            string str = code.Substring(ff + 1, ll - ff - 1);
                            if (int.TryParse(str.Trim(), out pgs))
                                break;
                        }
                    }
                    first = htmlString.IndexOf(strScriptOpen, last + (bFoundCloseTag?strScripClose.Length:0) + 1);
                }

                if (pgs > 0)
                {
                    OnSearchStarted(keyword);
                    for (int i = 1; i <= pgs; i++)
                    {
                        if (IsCancel)
                            return files;
                        var res = SearchInPage(i);
                        foreach(KeyValuePair<string,string> books in res)
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

        protected override Dictionary<string,string> SearchInPage(int page)
        {
            Dictionary<string, string> files = new Dictionary<string, string>();
            HttpWebRequest httpReq = WebRequest.Create(Home + _query + "&pageIndex=" + page) as HttpWebRequest;
            if (httpReq != null)
            {
                if (IsCancel)
                    return files;
                WebResponse webResponse = httpReq.GetResponse();
                Stream html = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(html);
                string htmlString = reader.ReadToEnd();

                string strDIVOpen = "<div";
                string strDIVClose = "</div>";
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
                    ff = code.IndexOf("class=\"book-image\"");
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
                                catch(Exception ex) {; }
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

        protected override KeyValuePair<string,string> SearchLink(string link)
        {
            HttpWebRequest httpReq = WebRequest.Create(Home +link) as HttpWebRequest;
            KeyValuePair<string, string> file = new KeyValuePair<string, string>();
            if (httpReq != null)
            {
                if (IsCancel)
                    return file;

                WebResponse webResponse = httpReq.GetResponse();
                Stream html = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(html);
                string htmlString = reader.ReadToEnd();


                string strDIVOpen = "<div";
                string strDIVClose = "</div>";
                string signal= "<a target=\"_blank\" class=\"btn btn-block btn-sm btn-primary\" href=\"";
                int first = htmlString.IndexOf(strDIVOpen, 0);
                int last = 0;
                string strLink = string.Empty;
                bool bFoundCloseTag = false;
                string strhRef = string.Empty;
                string strTitle = string.Empty;
                int ff = 0, ll = 0;
                int ff2 = 0, ll2 = 0;
                string bookPath = string.Empty;
                while (first>0 && first < htmlString.Length)
                {
                    if (IsCancel)
                        return file;

                    bFoundCloseTag = true;
                    last = htmlString.IndexOf(strDIVClose, first + strDIVOpen.Length + 1);
                    if ((last > htmlString.Length) || (-1 == last))
                    {
                        last = htmlString.Length;
                        bFoundCloseTag = false;
                    }

                    strhRef = htmlString.Substring(first + strDIVOpen.Length, last - first - (bFoundCloseTag ? strDIVClose.Length : 0));

                    ff = strhRef.IndexOf("class=\"book-image\"");
                    if (ff > 0)
                    {
                        ff = strhRef.IndexOf("<img alt=\"", ff);
                        if ((ff > 0) && (ff < strhRef.Length))
                        {
                            ff += "<img alt=\"".Length;
                            ll = strhRef.IndexOf("\"", ff);
                            if ((ll > strhRef.Length) || (-1 == ll))
                            {
                                ll = strhRef.Length;
                            }
                            strTitle = strhRef.Substring(ff, ll - ff).Trim();
                        }
                    }

                    ff2 = strhRef.IndexOf(signal);
                    if (ff2 > 0)
                    {
                        ff2 += signal.Length;
                        ll2 = strhRef.IndexOf("\"", ff2);
                        if (-1!=ll2)
                        {
                            strLink = strhRef.Substring(ff2, ll2 - ff2);
                            if (strLink.Length > 0)
                            {
                                string strID = string.Empty;
                                int nPos = strLink.LastIndexOf("/");
                                int nStart = 0;
                                int nEnd = strLink.Length;
                                if (nPos > 0)
                                    nStart = nPos + 1;

                                nPos = strLink.LastIndexOf(".");
                                if (nPos <= nStart)
                                    nPos = strLink.Length;
                                nEnd = nPos - 1;

                                strID = strLink.Substring(nStart, nEnd - nStart + 1);
                                file = new KeyValuePair<string, string>(strLink, WebUtility.HtmlDecode(WebUtility.HtmlDecode(strTitle)));
                                BookEventArg e = new BookEventArg(strID, file.Value, file.Key);
                                OnBookFound(e);
                                return file;
                            }
                        }
                    }
                    first = htmlString.IndexOf(strDIVOpen, last + (bFoundCloseTag ? strDIVClose.Length : 0) + 1);
                    if (first < 0)
                        break;
                }
            }
            return new KeyValuePair<string, string>();
        }
    }
}
