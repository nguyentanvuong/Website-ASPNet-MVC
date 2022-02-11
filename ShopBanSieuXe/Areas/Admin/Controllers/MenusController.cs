using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.Dao;
using ShopBanSieuXe.Library;  

namespace ShopBanSieuXe.Areas.Admin.Controllers
{
    public class MenusController : Controller
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();
        MenuDao menuDao = new MenuDao();
        LinhkDao linhkDao = new LinhkDao();
        // GET: Admin/Menus
        public ActionResult Index()
        {
            return View(menuDao.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(menuDao.getList("Trash"));
        }

        // GET: Admin/Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Admin/Menus/Create
        public ActionResult Create()
        {
            ViewBag.ListType = new SelectList(menuDao.getList("Index"), "Type", "Type");
            ViewBag.ListOrder = new SelectList(menuDao.getList("Index"), "Orders", "Name");
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.Link = MyString.Str_slug(menu.Name);
                menu.Created_at = DateTime.Now;
                menu.Created_by = int.Parse(Session["UserId"].ToString());
                menu.Updated_at = DateTime.Now;
                menu.Updated_by = int.Parse(Session["UserId"].ToString());
                TempData["XMessage"] = new MyMessage("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.ListType = new SelectList(menuDao.getList("Index"), "Type", "Type");
            ViewBag.ListOrder = new SelectList(menuDao.getList("Index"), "Orders", "Name",0);
            return View(menu);
        }

        // GET: Admin/Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: Admin/Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");
            }
            Menu menu = menuDao.getRow(id);
            if (menu == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            menuDao.Delete(menu);
            TempData["XMessage"] = new MyMessage("Xóa thành công", "success");
            return RedirectToAction("Trash");
        }
        // Trạng Thái
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");
            }
            Menu menu = menuDao.getRow(id);
            if (menu == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            menu.Status = (menu.Status == 1) ? 2 : 1;
            menu.Updated_at = DateTime.Now;
            menu.Updated_by = int.Parse(Session["UserId"].ToString());
            menuDao.Update(menu);
            TempData["XMessage"] = new MyMessage("Thay đổi trạng thái thành công", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");
            }
            Menu menu = menuDao.getRow(id);
            if (menu == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            menu.Status = 0;
            menu.Updated_at = DateTime.Now;
            menu.Updated_by = int.Parse(Session["UserId"].ToString());
            menuDao.Update(menu);
            TempData["XMessage"] = new MyMessage("Xóa thành công", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");
            }
            Menu menu = menuDao.getRow(id);
            if (menu == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            menu.Status = 2;
            menu.Updated_at = DateTime.Now;
            menu.Updated_by = int.Parse(Session["UserId"].ToString());
            menuDao.Update(menu);
            TempData["XMessage"] = new MyMessage("Khôi phục thành công", "success");
            return RedirectToAction("Index");
        }
    }
}
