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
    public class CategoryController : Controller
    {
        CategoryDao categoryDao = new CategoryDao();
        LinhkDao linhkDao = new LinhkDao();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(categoryDao.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(categoryDao.getList("Trash"));
        }
        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDao.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDao.getList("Index"), "Id", "Name",0);
            ViewBag.ListOrder = new SelectList(categoryDao.getList("Index"), "Orders", "Name");
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
            
                category.Slug = MyString.Str_slug(category.Name);
                category.Orders = (category.Orders == null) ? 0 : (category.Orders + 1);
                category.Created_at = DateTime.Now;
                category.Created_by = int.Parse(Session["UserId"].ToString());
                category.Updated_at = DateTime.Now;
                category.Updated_by = int.Parse(Session["UserId"].ToString());
                if(categoryDao.Insert(category) == 1) 
                {
                    Link link = new Link();
                    link.Name = category.Name;
                    link.Slug = category.Slug;
                    link.TableId = category.Id;
                    link.Type = "category";
                    linhkDao.Insert(link);
                }
                TempData["XMessage"] = new MyMessage("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDao.getList("Index"), "Id", "Name",0);
            ViewBag.ListOrder = new SelectList(categoryDao.getList("Index"), "Orders", "Name");
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDao.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(categoryDao.getList("Index"), "Id", "Name",0);
            ViewBag.ListOrder = new SelectList(categoryDao.getList("Index"), "Orders", "Name");
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = MyString.Str_slug(category.Name);
                category.Orders = (category.Orders == null) ? 0 : (category.Orders + 1);
                category.Updated_at = DateTime.Now;
                category.Updated_by = int.Parse(Session["UserId"].ToString());
                if(categoryDao.Update(category) == 1) 
                {
                    Link link = linhkDao.getRow(category.Id, "category");
                    link.Slug = category.Slug;
                    linhkDao.Update(link);
                }
                TempData["XMessage"] = new MyMessage("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDao.getList("Index"), "Id", "Name",0);
            ViewBag.ListOrder = new SelectList(categoryDao.getList("Index"), "Orders", "Name");
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");
            }
            Category category = categoryDao.getRow(id);
            if (category == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            categoryDao.Delete(category);
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
            Category category = categoryDao.getRow(id);
            if (category == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            category.Status = (category.Status == 1) ? 2 : 1;
            category.Updated_at = DateTime.Now;
            category.Updated_by = int.Parse(Session["UserId"].ToString());
            categoryDao.Update(category);
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
            Category category = categoryDao.getRow(id);
            if (category == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            category.Status = 0;
            category.Updated_at = DateTime.Now;
            category.Updated_by = int.Parse(Session["UserId"].ToString());
            categoryDao.Update(category);
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
            Category category = categoryDao.getRow(id);
            if (category == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            category.Status = 2;
            category.Updated_at = DateTime.Now;
            category.Updated_by = int.Parse(Session["UserId"].ToString());
            categoryDao.Update(category);
            TempData["XMessage"] = new MyMessage("Khôi phục thành công", "success");
            return RedirectToAction("Index");
        }
    }
}
