using EasyTicket.Connection.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using EasyTicket.Common;

namespace EasyTicket.Connection.DAO
{
    public class UserDAO
    {
        EasyTicketDbContext db = null;

        public UserDAO()
        {
            db = new EasyTicketDbContext();
        }

        public long Insert(User user)
        {
            db.User.Add(user);
            db.SaveChanges();
            return user.ID;
        }

        public bool Update(User user)
        {
            try
            {
                var users = db.User.Find(user.ID);
                if (!string.IsNullOrEmpty(user.Password))
                {
                    users.Password = user.Password;
                }
                users.Name = user.Name;
                users.Email = user.Email;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var users = db.User.Find(id);
                db.User.Remove(users);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User GetByID(string userName)
        {
            return db.User.SingleOrDefault(x => x.UserName == userName);
        }

        public User ViewDetail(long id)
        {
            return db.User.Find(id);
        }

        public IEnumerable<User> ListAllPaging(string searchString, int page = 1, int pageSize = 10)
        {
            IQueryable<User> user = db.User;
            if (!string.IsNullOrEmpty(searchString))
            {
                user = user.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return user.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public List<string> GetListCredential(string userName)
        {
            var user = db.User.Single(x => x.UserName == userName);
            var data = (from a in db.Credential
                        join b in db.UserGroup on a.UserGroupID equals b.ID
                        join c in db.Role on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();
        }

        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.User.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == CommonConstants.ADMIN_GROUP || result.GroupID == CommonConstants.MOD_GROUP)
                    {
                        if (result.Password == passWord)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -2;
                    }
                }
                else
                {
                    if (result.Password == passWord)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public bool CheckUserName(string userName)
        {
            return db.User.Count(x => x.UserName == userName) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.User.Count(x => x.Email == email) > 0;
        }
    }
}