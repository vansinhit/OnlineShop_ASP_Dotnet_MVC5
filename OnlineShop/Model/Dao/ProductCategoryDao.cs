using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        public OnlineShopDbContext db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<ProductCategory> listAll()
        {
            return db.ProductCategories.Where(x=>x.Status == true).ToList();
        }
        public ProductCategory ViewProductCategoryDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
