using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class ContentDao
    {
        public OnlineShopDbContext db = null;
       public ContentDao()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<Content> ListAll(string SeachString, int page, int pagesize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(SeachString))
            {
                model = db.Contents.Where(x => x.Name.Contains(SeachString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }

        public long Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Content content)
        {
            try
            {
                var up = db.Contents.Find(content.ID);
                up.Name = content.Name;
                up.Images = content.Images;
                up.CategoryID = content.CategoryID;
                up.Detal = content.Detal;
                up.Status = content.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Content ViewUpdate(int id)
        {
            return db.Contents.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var de = db.Contents.Find(id);
                db.Contents.Remove(de);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
