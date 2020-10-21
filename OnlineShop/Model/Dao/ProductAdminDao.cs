using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductAdminDao
    {
        public OnlineShopDbContext db = null;
        public ProductAdminDao()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<Product> ListPage(string SeachString, int page, int pagesize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(SeachString))
            {
                model = model.Where(x => x.Name.Contains(SeachString));

            }
            return model.OrderByDescending(x => x.CreateBy).ToPagedList(page, pagesize);
        }
        /// <summary>
        /// thêm sảm phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public long Insert(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID;
        }
        /// <summary>
        /// update sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        public bool Update(Product product)
        {
            try {
                var up = db.Products.Find(product.ID);
                up.Name = product.Name;
                up.Images = product.Images;
                up.CreateDate = DateTime.Now;
                up.Price = product.Price;
                up.Status = product.Status;
                up.Decriptions = product.Decriptions;
                db.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// lấy id ra thử sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetUpdate(long id)
        {
            return db.Products.Find(id);
        }

        /// <summary>
        /// xóa sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var de = db.Products.Find(id);
                db.Products.Remove(de);
                db.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

       
    }
}
