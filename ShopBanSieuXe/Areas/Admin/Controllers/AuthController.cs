using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MyClass.Dao;
using MyClass.Models;

namespace ShopBanSieuXe.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        // GET: Admin/Auth
        UserDao userDao = new UserDao();
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.StrError = "";
            return View();
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
                    return RedirectToAction("Index", "Dashboard");
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
            return Redirect("~/admin/login");
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