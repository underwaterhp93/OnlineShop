﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.EF;

namespace Model.Dao
{
    public class UserDao
    {
        private OnlineShopDbContext db = null;

        public UserDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!String.IsNullOrEmpty(user.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
          
        }
        public IEnumerable<User> ListAllPaging(int page, int pageSize)
        {
            return db.Users.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public User GetbyId(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);//&& x.Password == passWord);
            if (result == null)
            {
                return 0;//"Tài khoản không tồn tại";
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;//"Tài khoản bị khóa";
                }
                else
                {
                    if (result.Password == passWord)
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
    }
}
