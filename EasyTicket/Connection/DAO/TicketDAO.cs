using EasyTicket.Connection.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTicket.Connection.ViewModel;
using PagedList;

namespace EasyTicket.Connection.DAO
{
    public class TicketDAO
    {
        EasyTicketDbContext db = null;

        public TicketDAO()
        {
            db = new EasyTicketDbContext();
        }

        public long Insert(Ticket ticket)
        {
            db.Ticket.Add(ticket);
            db.SaveChanges();
            return ticket.ID;
        }

        public bool Update(Ticket ticket)
        {
            try
            {
                var ticketEdit = db.Ticket.Find(ticket.ID);
                ticketEdit.Name = ticket.Name;
                ticketEdit.MetaTitle = ticket.MetaTitle;
                ticketEdit.Description = ticket.Description;
                ticketEdit.Price = ticket.Price;
                ticketEdit.Quantity = ticket.Quantity;
                ticketEdit.CategoryID = ticket.CategoryID;
                ticketEdit.WatchDate = ticket.WatchDate;
                ticketEdit.NextViewingDate = ticket.NextViewingDate;
                ticketEdit.Artist = ticket.Artist;
                ticketEdit.ShowOnHome = ticket.ShowOnHome;
                ticketEdit.ImageUrl = ticket.ImageUrl;
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
                var ticket = db.Ticket.Find(id);
                db.Ticket.Remove(ticket);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Ticket> ListTrendTicket(int top)
        {
            return db.Ticket.OrderByDescending(x => x.WatchDate).Take(top).ToList();
        }

        public List<Ticket> ListTopHotTicket(int top)
        {
            return db.Ticket.Where(x => x.ShowOnHome == true).OrderByDescending(x => x.WatchDate).Take(top).ToList();
        }

        public List<Ticket> ListFeatureTicket(long id,int top)
        {
            var ticket = db.Ticket.Find(id);
            return db.Ticket.Where(x => x.ID != id && x.CategoryID == ticket.CategoryID).Take(top).ToList();
        }

        public List<Ticket> ListMovie(int top)
        {
            return db.Ticket.Where(x => x.CategoryID == 1).OrderByDescending(x => x.WatchDate).Take(top).ToList();
        }

        public List<Ticket> ListMusic(int top)
        {
            return db.Ticket.Where(x => x.CategoryID == 2).OrderByDescending(x => x.WatchDate).Take(top).ToList();
        }

        public List<Ticket> ListDrama(int top)
        {
            return db.Ticket.Where(x => x.CategoryID == 3).OrderByDescending(x => x.WatchDate).Take(top).ToList();
        }

        public List<Ticket> ListSport(int top)
        {
            return db.Ticket.Where(x => x.CategoryID == 4).OrderByDescending(x => x.WatchDate).Take(top).ToList();
        }

        public List<TicketViewModel> ListByCategoryID(long id, ref int totalRecord, int page = 1, int pageSize = 9)
        {
            totalRecord = db.Ticket.Where(x => x.CategoryID == id).Count();
            var ticket = from a in db.Ticket
                         join b in db.Category on a.CategoryID equals b.ID
                         where a.CategoryID == id
                         select new TicketViewModel()
                         {
                             ID = a.ID,
                             Images = a.ImageUrl,
                             Name = a.Name,
                             Price = a.Price,
                             MetaTitle = a.MetaTitle,
                             CategoryName = b.Name,
                             WatchDate = a.WatchDate,
                             Quantity = a.Quantity
                         };
            ticket.OrderByDescending(x => x.WatchDate).Skip((page - 1) * pageSize).Take(pageSize);
            return ticket.ToList();
        }

        public Ticket ViewDetail(long id)
        {
            return db.Ticket.Find(id);
        }

        public List<string> ListName(string keyword)
        {
            return db.Ticket.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        public List<TicketViewModel> Search(string keyword, ref int totalRecord, int page = 1, int pageSize = 9)
        {
            totalRecord = db.Ticket.Where(x => x.Name == keyword).Count();
            var ticket = (from a in db.Ticket
                          join b in db.Category on a.CategoryID equals b.ID
                          where a.Name.Contains(keyword)
                          select new
                          {
                              ID = a.ID,
                              Images = a.ImageUrl,
                              Name = a.Name,
                              Price = a.Price,
                              MetaTitle = a.MetaTitle,
                              CategoryName = b.Name,
                              WatchDate = a.WatchDate,
                              Quantity = a.Quantity
                          }).AsEnumerable().Select(x => new TicketViewModel()
                          {
                              ID = x.ID,
                              Images = x.Images,
                              Name = x.Name,
                              Price = x.Price,
                              MetaTitle = x.MetaTitle,
                              CategoryName = x.CategoryName,
                              WatchDate = x.WatchDate,
                              Quantity = x.Quantity
                          });
            ticket.OrderByDescending(x => x.WatchDate).Skip((page - 1) * pageSize).Take(pageSize);
            return ticket.ToList();
        }

        public IEnumerable<Ticket> ListAllPaging(string searchString, int page = 1, int pageSize = 10)
        {
            IQueryable<Ticket> ticket = db.Ticket;
            if (!string.IsNullOrEmpty(searchString))
            {
                ticket = ticket.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return ticket.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
    }
}