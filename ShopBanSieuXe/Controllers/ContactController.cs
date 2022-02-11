using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Dao;
using MyClass.Models;

namespace ShopBanSieuXe.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        ContactDao contactDao = new ContactDao();
        public ActionResult Index()
        {
            //var list = contactDao.getActiveContact();
            return View();
        }
        [HttpPost]
        public JsonResult Send(string name, string email, string phone, string title, string detail)
        {
            var contact = new Contact();
            contact.FullName = name;
            contact.Email = email;
            contact.Phone = phone;
            contact.Title = title;
            contact.Detail = detail;
            contact.Updated_at = DateTime.Now;

            var id = new ContactDao().InsertContact(contact);
            if (id > 1)
                return Json(new
                {
                    status = true
                });
            else
                return Json(new
                {
                    status = false
                });
        }
    }
}