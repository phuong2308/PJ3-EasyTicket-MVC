using BotDetect.Web.Mvc;
using EasyTicket.Common;
using EasyTicket.Connection.DAO;
using EasyTicket.Connection.EF;
using EasyTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyTicket.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var userDAO = new UserDAO();
                if (userDAO.CheckUserName(registerModel.Username))
                {
                    ModelState.AddModelError("", "Username available!");
                }
                else if (userDAO.CheckEmail(registerModel.Email))
                {
                    ModelState.AddModelError("", "Email available!");
                }
                else
                {
                    var user = new User();
                    user.UserName = registerModel.Username;
                    user.Name = registerModel.Name;
                    user.Password = Encryptor.MD5Hash(registerModel.Password);
                    user.Email = registerModel.Email;
                    var result = userDAO.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Register successful";
                        registerModel = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Register Failed");
                    }
                }
            }
            return View(registerModel);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var userDAO = new UserDAO();
                var result = userDAO.Login(loginModel.Username, Encryptor.MD5Hash(loginModel.Password));
                if (result == 1)
                {
                    var user = userDAO.GetByID(loginModel.Username);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.GroupID = user.GroupID;
                    var listCredential = userDAO.GetListCredential(loginModel.Username);
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredential);
                    if (userSession.GroupID == CommonConstants.ADMIN_GROUP)
                    {
                        return Redirect("/Admin/Home");
                    }
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Account does not exist");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Password is not correct");
                }
                else
                {
                    ModelState.AddModelError("", "Login Failed");
                }
            }
            return View(loginModel);
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }
    }
}