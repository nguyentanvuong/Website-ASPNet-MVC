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
using System.IO;

namespace ShopBanSieuXe.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();

        ProductDao productDao = new ProductDao();
        CategoryDao categoryDao = new CategoryDao();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(productDao.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(productDao.getList("Trash"));
        }
        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDao.getList("Index"), "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = MyString.Str_slug(product.Name);
                product.TypeId = 1;
                product.TopHot = DateTime.Now;
                product.TopNew = DateTime.Now;
                product.Featured = DateTime.Now;
                product.Created_at = DateTime.Now;
                product.Created_by = int.Parse(Session["UserId"].ToString());
                product.Updated_at = DateTime.Now;
                product.Updated_by = int.Parse(Session["UserId"].ToString());
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0) 
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".gif", ".jegp", ".jfif" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Content/img/product/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //Upload file
                        fileImg.SaveAs(pathImg);
                        //Lưu hình vào csdl
                        product.Img = imgName;
                    }
                }
                productDao.Insert(product);              
                TempData["XMessage"] = new MyMessage("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDao.getList("Index"), "Id", "Name");
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(categoryDao.getList("Index"), "Id", "Name");
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = MyString.Str_slug(product.Name);
                product.TypeId = 1;
                product.TopHot = DateTime.Now;
                product.TopNew = DateTime.Now;
                product.Featured = DateTime.Now;
                product.Updated_at = DateTime.Now;
                product.Updated_by = int.Parse(Session["UserId"].ToString());
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".gif", ".jegp", ".jfif" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Content/img/product/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);

                        if(product.Img != null)
                        {
                            string pathImgDetele = Path.Combine(Server.MapPath(pathDir), product.Img);
                            System.IO.File.Delete(pathImgDetele);
                        }
                        //Upload file
                        fileImg.SaveAs(pathImg);
                        //Lưu hình vào csdl
                        product.Img = imgName;
                    }
                }
                productDao.Update(product);
                TempData["XMessage"] = new MyMessage("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDao.getList("Index"), "Id", "Name");
            return View(product);
        }      

        // POST: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");
            }
            Product product = productDao.getRow(id);
            if (product == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            productDao.Delete(product);
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
            Product product = productDao.getRow(id);
            if (product == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.Updated_at = DateTime.Now;
            product.Updated_by = int.Parse(Session["UserId"].ToString());
            productDao.Update(product);
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
            Product product = productDao.getRow(id);
            if (product == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            product.Status = 0;
            product.Updated_at = DateTime.Now;
            product.Updated_by = int.Parse(Session["UserId"].ToString());
            productDao.Update(product);
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
            Product product = productDao.getRow(id);
            if (product == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            product.Status = 2;
            product.Updated_at = DateTime.Now;
            product.Updated_by = int.Parse(Session["UserId"].ToString());
            productDao.Update(product);
            TempData["XMessage"] = new MyMessage("Khôi phục thành công", "success");
            return RedirectToAction("Index");
        }
    }
}
