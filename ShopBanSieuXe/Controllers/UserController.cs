using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Dao;
using MyClass.Models;
using System.Security.Cryptography;
using System.Text;
using ShopBanSieuXe.Models;

namespace ShopBanSieuXe.Controllers
{
    public class UserController : Controller
    {
        // GET: User        
        UserDao userDao = new UserDao();
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.StrError = "";
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var userDao = new UserDao();
                if (userDao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
                }
                else if (userDao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại !");
                }
                else
                {
                    var user = new User();
                    user.FullName = model.FullName;
                    user.UserName = model.UserName;
                    user.PassWord = GetMD5(model.PassWord);
                    user.Email = model.Email;
                    user.Gender = 1;
                    user.Phone = model.Phone;
                    user.Img = "jpg";
                    user.Access = 1;
                    user.Created_at = DateTime.Now;
                    user.Status = 1;
                    var result = userDao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công.";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(FormCollection filed)
        {
            String user = filed["username"];
            String pass = GetMD5(filed["password"]);
            String error = "";
            //xu ly
            User user_row = userDao.getRow(user);
            if (user_row != null)
            {
                //dang nhap
                if (user_row.PassWord.Equals(pass))
                {
                    Session["UserAdmin"] = user_row.UserName;
                    Session["UserId"] = user_row.Id.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    error = "Mật khẩu không chính xác";
                }
            }
            else
            {
                error = "Tên đăng nhập không chính xác";
            }
            ViewBag.StrError = "<div class='text-danger'>" + error + "</div>";
            //ViewBag.StrError = pass;
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }
        public static string GetMD5(string str)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {

                sbHash.Append(String.Format("{0:x2}", b));

            }

            return sbHash.ToString();

        }
    }
}