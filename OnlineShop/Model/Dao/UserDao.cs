using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        public OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }


        //hiện tất cả các bản ghi trong database bằng cách thông thường 
        //public List<User> ListAll()
        //{
        //    var lis = db.Users.ToList();
        //    return lis;
        //}



        public IEnumerable<User> LiskPage(String SeachString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(SeachString))
            {
                model = db.Users.Where(x => x.UserName.Contains(SeachString) || x.Name.Contains(SeachString)); //Contains là tìm kiếm chuổi gần đúng khi SeachStrong được nhập vào. where là tìm kiếm theo những gì.
            }
            return model.OrderByDescending(x => x.CreateBy).ToPagedList(page, pageSize);
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity); //nếu nhâpj vào 1 cái entity thì nó add lun vài
            db.SaveChanges(); // lưu lại
            return entity.ID; // nó sẻ return ra cái ID tự sinh
        }

        public long InssertToFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if(user == null)
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
               return entity.ID;
            }
        }
        public bool Upadate(User entity)
        {
            try
            {
                var re = db.Users.Find(entity.ID); // lấy ra 1 cái bảng ghi
                re.Name = entity.Name; //liệt kê những cái mình cần update tại đây
                re.Address = entity.Address;
                re.ModifieBy = entity.ModifieBy;
                re.Email = entity.Email;
                re.CreateDate = DateTime.Now;
                re.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var de = db.Users.Find(id);
                db.Users.Remove(de);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }




        public User GetById(string userName) // Sử dung cho Login
        {

            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public User ViewDale(int id) // sử dụng cho câu lện update
        {
            return db.Users.Find(id);
        }
        public int Login(string username, string password)
        {
            var relsut = db.Users.SingleOrDefault(x => x.UserName == username);
            if (relsut == null)
            {
                return 0;
            }
            else
            {
                if (relsut.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (relsut.Password == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }

        public bool CheckUsername (string username)
        {
            return db.Users.Count(x => x.UserName == username) > 0;
        }  
        public bool CheckEmail (string Email)
        {
            return db.Users.Count(x => x.Email == Email) > 0;
        }



    }
}




