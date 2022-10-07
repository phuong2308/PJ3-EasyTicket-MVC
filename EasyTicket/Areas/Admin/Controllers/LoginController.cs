using EasyTicket.Areas.Admin.Model;
using EasyTicket.Common;
using EasyTicket.Connection.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyTicket.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var userDAO = new UserDAO();
                var result = userDAO.Login(loginModel.UserName, Encryptor.MD5Hash(loginModel.Password), true);
                if (result == 1)
                {
                    var user = userDAO.GetByID(loginModel.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.GroupID = user.GroupID;
                    var listCredential = userDAO.GetListCredential(loginModel.UserName);
                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredential);
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Account does not exist");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Password is not correct");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Your account does not have permission to login");
                }
                else
                {
                    ModelState.AddModelError("", "Login Failed");
                }
            }
            return View("Index");
        }
    }
}