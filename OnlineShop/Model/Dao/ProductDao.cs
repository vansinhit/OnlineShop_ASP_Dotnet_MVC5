using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        public OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }
        /// <summary>
        /// List To sản phẩm new
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Product> ListProductNew(int id)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(id).ToList();
        }
        /// <summary>
        /// list to sản phẩm hot
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Product> ListProductHot(int id)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now)
                        .OrderByDescending(x => x.CreateDate).Take(id).ToList();
        }
        /// <summary>
        /// lấy ra tất cac cấ product
        /// </summary>
        /// <returns></returns>

        public List<Product> ListAll()
        {
            return db.Products.ToList();
        }
        /// <summary>
        /// list to chi tiết cản phẩn productDetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductDetail(long id)
        {
            return db.Products.Find(id);
        }
        /// <summary>
        /// list by productCategory
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public List<Product> ListByCategoryId(int ID)
        {
            return db.Products.Where(x => x.CategoryID == ID).ToList();
        }
    }
}
