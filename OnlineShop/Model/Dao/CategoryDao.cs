using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class CategoryDao
    {
        public OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        //public List<Category> ListAll()
        //{
        //    var ls = db.Categories.ToList();
        //    return ls;
        //}

        public IEnumerable<Category> ListPage(string SeachString, int page , int pagesize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(SeachString))
            {
                model = db.Categories.Where(x => x.Name.Contains(SeachString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }

        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Category entity)
        {
            try
            {
                var up = db.Categories.Find(entity.ID);
                up.Name = entity.Name;
                up.Metatile = entity.Metatile;
                up.ModifieDate = DateTime.Now;
                up.ParenID = entity.ParenID;
                up.DisplayOrder = entity.DisplayOrder;
                up.SeoTitle = entity.SeoTitle;
                up.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Deletecategory(int id) {
            try
            {
                var de = db.Categories.Find(id);
                db.Categories.Remove(de);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




        public Category ViewUpdate(int id)
        {
            return db.Categories.Find(id);
        }

        public List<Category> AllList()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }

       
    }
}
