using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FeedBackDao
    {
        public OnlineShopDbContext db = null;
        public FeedBackDao()
        {
            db = new OnlineShopDbContext();
        }
        public long  Insert(FeedBack fe)
        {
            db.FeedBacks.Add(fe);
            db.SaveChanges();
            return fe.ID;
        }

    }
}
