using EasyTicket.Common;
using EasyTicket.Connection.DAO;
using EasyTicket.Connection.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyTicket.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var userDAO = new UserDAO();
            var user = userDAO.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(user);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var userDAO = new UserDAO();
                var encryptorMD5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptorMD5Pas;
                long id = userDAO.Insert(user);
                if (id > 0)
                {
                    SetAlert("Insert User Successful", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Insert User Failed");
                }
            }
            return View("Index");
        }

        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(long id)
        {           
            var user = new UserDAO().ViewDetail(id);
            SetViewBag(user.GroupID);
            return View(user);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var userDAO = new UserDAO();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptorMD5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptorMD5Pas;
                }
                var result = userDAO.Update(user);
                if (result)
                {
                    SetAlert("Edit User Successful", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Edit User Failed");
                }
            }
            SetViewBag();
            return View("Index");
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(long id)
        {
            new UserDAO().Delete(id);
            return RedirectToAction("Index");
        }

        public void SetViewBag(string selectedID = null)
        {
            var userGroupDAO = new UserGroupDAO();
            ViewBag.GroupID = new SelectList(userGroupDAO.ListAll(), "ID", "Name", selectedID);
        }
    }
}