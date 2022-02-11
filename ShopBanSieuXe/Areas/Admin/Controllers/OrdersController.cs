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
    public class OrdersController : Controller
    {
        OrderDao orderDao = new OrderDao();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            return View(orderDao.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(orderDao.getList("Trash"));
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDao.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Order order)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDao.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Order order)
        {
            if (ModelState.IsValid)
            {
                order.Updated_at = DateTime.Now;
                order.Updated_by = int.Parse(Session["UserId"].ToString());
                TempData["XMessage"] = new MyMessage("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["XMessage"] = new MyMessage("Không có Id", "danger");
                return RedirectToAction("Index");
            }
            Order order = orderDao.getRow(id);
            if (order == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            orderDao.Delete(order);
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
            Order order = orderDao.getRow(id);
            if (order == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            order.Status = (order.Status == 1) ? 2 : 1;
            order.Updated_at = DateTime.Now;
            order.Updated_by = int.Parse(Session["UserId"].ToString());
            orderDao.Update(order);
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
            Order order = orderDao.getRow(id);
            if (order == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            order.Status = 0;
            order.Updated_at = DateTime.Now;
            order.Updated_by = int.Parse(Session["UserId"].ToString());
            orderDao.Update(order);
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
            Order order = orderDao.getRow(id);
            if (order == null)
            {
                TempData["XMessage"] = new MyMessage("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            order.Status = 2;
            order.Updated_at = DateTime.Now;
            order.Updated_by = int.Parse(Session["UserId"].ToString());
            orderDao.Update(order);
            TempData["XMessage"] = new MyMessage("Khôi phục thành công", "success");
            return RedirectToAction("Index");
        }
    }
}
