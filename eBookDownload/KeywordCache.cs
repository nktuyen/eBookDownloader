using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookDownloader
{
    public class KeywordCache
    {
        private Dictionary<string, string> _keywords = new Dictionary<string, string>();


        public bool Exist(string keyword)
        {
            if ((_keywords == null) || (_keywords.Count <= 0))
                return false;

            return _keywords.ContainsKey(MD5(keyword));
        }

        private string MD5(string input)
        {

            return string.Empty;
        }
    }
}
