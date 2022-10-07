using EasyTicket.Connection.DAO;
using EasyTicket.Connection.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyTicket.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var categoryDAO = new CategoryDAO();
            var category = categoryDAO.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var categoryDAO = new CategoryDAO();
                long id = categoryDAO.Insert(category);
                if (id > 0)
                {
                    SetAlert("Insert Category Successful", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Insert Category Failed");
                }
            }
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            var category = new CategoryDAO().ViewDetail(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var categoryDAO = new CategoryDAO();
                var result = categoryDAO.Update(category);
                if (result)
                {
                    SetAlert("Edit Category Successful", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Edit Category Failed");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}