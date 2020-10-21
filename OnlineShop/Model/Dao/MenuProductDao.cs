using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public  class MenuProductDao
    {
        public OnlineShopDbContext db = null;
        public MenuProductDao()
        {
            db = new OnlineShopDbContext();

        }
        public IEnumerable<Product> ListName(string SeachString, int page, int pagesize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(SeachString))
            {
                model =  db.Products.Where(x => x.Name.Contains(SeachString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pagesize);
        }
    }
}
