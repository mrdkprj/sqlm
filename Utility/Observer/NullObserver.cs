using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class NullObserver : IObserver
    {
        static NullObserver _instance = null;
        private NullObserver() { }
        public static NullObserver Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NullObserver();
                return _instance;
            }
        }

        public void Update(object sender)
        {
        }

        public void Complete(object sender)
        {
        }

        public void Error(object sender, Exception e)
        {
        }
    }
}
