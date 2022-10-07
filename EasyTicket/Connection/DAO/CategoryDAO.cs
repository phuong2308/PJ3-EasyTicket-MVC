using EasyTicket.Connection.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace EasyTicket.Connection.DAO
{
    public class CategoryDAO
    {
        EasyTicketDbContext db = null;

        public CategoryDAO()
        {
            db = new EasyTicketDbContext();
        }

        public Category ViewDetail(long id)
        {
            return db.Category.Find(id);
        }

        public long Insert(Category category)
        {
            db.Category.Add(category);
            db.SaveChanges();
            return category.ID;
        }

        public bool Update(Category category)
        {
            try
            {
                var categories = db.Category.Find(category.ID);
                categories.Name = category.Name;
                categories.MetaTitle = category.MetaTitle;
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
                var categories = db.Category.Find(id);
                db.Category.Remove(categories);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> category = db.Category;
            if (!string.IsNullOrEmpty(searchString))
            {
                category = category.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            return category.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public List<Category> ListAll()
        {
            return db.Category.ToList();
        }
    }
}