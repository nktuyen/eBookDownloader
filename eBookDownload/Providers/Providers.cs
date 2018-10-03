using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookDownloader
{ 
    public class Providers
    {
        private static Dictionary<string, Provider> _providers = null;
        private static Dictionary<string, Provider>.Enumerator _enumer;
        private static Providers _instance = null;

        public static Providers Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new Providers();
                return _instance;
            }
        }

        private Providers()
        {
            if (null == _providers)
            {
                _providers = new Dictionary<string, Provider>();
                Provider downloader = SachLapTrinhDotCom_Provider.GetInstance();
                _providers.Add(downloader.Name, downloader);
            }
        }

        public bool First()
        {
            if (_providers.Count > 0)
            {
                _enumer = _providers.GetEnumerator();
                return true;
            }

            return false;
        }

        public Provider Next()
        {
            if(_providers.Count>0)
            {
                if(_enumer.MoveNext())
                {
                    return _enumer.Current.Value;
                }
            }

            return null;
        }

        public Provider Find(string name)
        {
            if((_providers.Count>0) && (_providers.ContainsKey(name)))
            {
                return _providers[name];
            }

            return null;
        }

        internal void Register(string name, Provider provider)
        {
            if ((null == name) || (string.Empty == name))
            {
                throw new Exception("Name cannot be empty.");
            }

            if (_providers.ContainsKey(name))
            {
                throw new Exception("Provider '"+name+"' already exist.");
            }

            _providers.Add(name, provider);
        }
    }
}
