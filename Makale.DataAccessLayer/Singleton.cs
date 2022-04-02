using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale.DataAccessLayer
{
    public static class Singleton
    {
        private static DatabaseContext db;
        private static object _lock = new object();
        public static DatabaseContext CreateContext()
        {
            lock(_lock)
            {
              if (db == null)
                db = new DatabaseContext();
            }
            
            return db;
        }
    }
}
