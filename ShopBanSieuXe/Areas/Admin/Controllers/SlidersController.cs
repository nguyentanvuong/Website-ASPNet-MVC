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
    public class SlidersController : Controller
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();

        SliderDao sliderDao = new SliderDao();

        // GET: Admin/Sliders
        public ActionResult Index()
        {
            return View(sliderDao.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(sliderDao.getList("Trash"));
        }
        // GET: Admin/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Admin/Sliders/Create
        public ActionResult Create()
        {
            ViewBag.ListOrder = new SelectList(sliderDao.getList("Index"), "Orders", "Name",0);
            return View();           
        }

        // POST: Admin/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Slider slider)
        {
            if (ModelState.IsValid)
            {
                slider.Link = MyString.Str_slug(slider.Name);
                slider.Created_at = DateTime.Now;
                slider.Created_by = int.Parse(Session["UserId"].ToString());
                slider.Updated_at = DateTime.Now;
                slider.Updated_by = int.Parse(Session["UserId"].ToString());
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".gif", ".jegp", ".jfif" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = slider.Name + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Content/img/slider/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //Upload file
                        fileImg.SaveAs(pathImg);
                        //Lưu hình vào csdl
                        slider.Img = imgName;
                    }
                }
                sliderDao.Insert(slider);
                TempData["XMessage"] = new MyMessage("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(sliderDao.getList("Index"), "Orders", "Name",0);
            return View(slider);
        }

        // GET: Admin/Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListOrder = new SelectList(sliderDao.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // POST: Admin/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                slider.Link = MyString.Str_slug(slider.Name);
                slider.Updated_at = DateTime.Now;
                slider.Updated_by = int.Parse(Session["UserId"].ToString());
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".gif", ".jegp", ".jfif" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = slider.Name + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Content/img/slider/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        if (slider.Img != null)
                        {
                            string pathImgDetele = Path.Combine(Server.MapPath(pathDir), slider.Img);
                            System.IO.File.Delete(pathImgDetele);
                        }
                        //Upload file
                        fileImg.SaveAs(pathImg);
                        //Lưu hình vào csdl
                        slider.Img = imgName;
                    }
                }
                sliderDao.Update(slider);
                TempData["XMessage"] = new MyMessage("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(sliderDao.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // GET: Admin/Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");
            }
            Slider slider = sliderDao.getRow(id);
            if (slider == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            sliderDao.Delete(slider);
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
            Slider slider = sliderDao.getRow(id);
            if (slider == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            slider.Status = (slider.Status == 1) ? 2 : 1;
            slider.Updated_at = DateTime.Now;
            slider.Updated_by = int.Parse(Session["UserId"].ToString());
            sliderDao.Update(slider);
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
            Slider slider = sliderDao.getRow(id);
            if (slider == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            slider.Status = 0;
            slider.Updated_at = DateTime.Now;
            slider.Updated_by = int.Parse(Session["UserId"].ToString());
            sliderDao.Update(slider);
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
            Slider slider = sliderDao.getRow(id);
            if (slider == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            slider.Status = 2;
            slider.Updated_at = DateTime.Now;
            slider.Updated_by = int.Parse(Session["UserId"].ToString());
            sliderDao.Update(slider);
            TempData["XMessage"] = new MyMessage("Khôi phục thành công", "success");
            return RedirectToAction("Index");
        }
    }
}
