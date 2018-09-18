using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookDownloader
{ 
    public class Downloaders
    {
        private static Dictionary<string, BookStore> _downloaders = null;
        private static Dictionary<string, BookStore>.Enumerator _enumer;
        private static Downloaders _instance = null;

        public static Downloaders Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new Downloaders();
                return _instance;
            }
        }

        private Downloaders()
        {
            if (null == _downloaders)
            {
                _downloaders = new Dictionary<string, BookStore>();
                BookStore downloader = SachLapTrinh_Dot_Com_Downloader.GetInstance();
                _downloaders.Add(downloader.Name, downloader);
            }
        }

        public bool First()
        {
            if (_downloaders.Count > 0)
            {
                _enumer = _downloaders.GetEnumerator();
                return true;
            }

            return false;
        }

        public BookStore Next()
        {
            if(_downloaders.Count>0)
            {
                if(_enumer.MoveNext())
                {
                    return _enumer.Current.Value;
                }
            }

            return null;
        }

        public BookStore Find(string name)
        {
            if((_downloaders.Count>0) && (_downloaders.ContainsKey(name)))
            {
                return _downloaders[name];
            }

            return null;
        }

        internal void Register(string name, BookStore downloader)
        {
            if ((null == name) || (string.Empty == name))
            {
                throw new Exception("Name cannot be empty.");
            }

            if (_downloaders.ContainsKey(name))
            {
                throw new Exception("Downloader with name '"+name+"' already exist.");
            }

            _downloaders.Add(name, downloader);
        }
    }
}
