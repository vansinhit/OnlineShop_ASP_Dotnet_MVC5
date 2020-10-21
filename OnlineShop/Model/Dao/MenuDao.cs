using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        public OnlineShopDbContext db = null;
        public MenuDao()
        {
            db = new OnlineShopDbContext();

        }
        public List<Menu> ListGrop(int id)
        {
            return db.Menus.Where(x=>x.TypeID==id).ToList();
        }
    }
}
