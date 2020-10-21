using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AboutDao
    {
        public OnlineShopDbContext db = null;
        public AboutDao()
        {
            db = new OnlineShopDbContext();

        }

        public IEnumerable<About> Listpage(string SeachString, int page , int pagesize)
        {
            IQueryable<About> model = db.Abouts;
            if (!string.IsNullOrEmpty(SeachString))
            {
                model = db.Abouts.Where(x => x.Name.Contains(SeachString));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pagesize);
        }
        /// <summary>
        /// insert
        /// </summary>
        /// <param name="about"></param>
        /// <returns></returns>
        public long Insert(About about)
        {
            db.Abouts.Add(about);
            db.SaveChanges();
            return about.ID;
        }
        public bool Update (About about)
        {
            try
            {
                var up = db.Abouts.Find(about.ID);
                up.Name = about.Name;
                up.MetaTitle = about.MetaTitle;
                up.Status = about.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// lấy id nhập vào qua phần update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public About Getupdate(long id)
        {
            return db.Abouts.Find(id);
        }

        public bool Delete(long id)
        {
            try
            {
               var De =  db.Abouts.Find(id);
                db.Abouts.Remove(De);
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
