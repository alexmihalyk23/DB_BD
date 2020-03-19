using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_App.Entity
{
    class Store_Singleton
    {
        private static Store_Entities store_Entity = null;
        private static readonly object Lock = new Object();

        public static Store_Entities init()
        {
            if (store_Entity == null)
            {
                lock (Lock)
                {
                    if (store_Entity == null)
                    {
                        store_Entity = new Store_Entities();
                    }
                }
            }
            return store_Entity;
        }
    }
}
