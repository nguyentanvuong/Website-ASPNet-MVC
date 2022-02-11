using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanSieuXe.Models;
using MyClass.Dao;
using MyClass.Models;
using System.Web.Script.Serialization;

namespace ShopBanSieuXe.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private const string CartSession = "CartSession";
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddToCart(int productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(m => m.Product.Id == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.Id == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart
                    var item = new CartItem();
                    item.Product = (MyClass.Models.Product)product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //gián vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart
                var item = new CartItem();
                item.Product = (MyClass.Models.Product)product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gián vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        //Cập nhật giỏ hàng
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(m => m.Product.Id == item.Product.Id);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult DeleteAll(string cartModel)
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(m => m.Product.Id == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string name, string adderss, string phone , string email)
        {
            var order = new Order();
            order.code = "hdcg334";
            order.UserId = 1;
            order.CreatedDate = DateTime.Now;
            order.Deliveryaddress = adderss;
            order.Deliveryemail = email;
            order.Deliveryname = name;
            order.Deliveryphone = phone;
            order.Updated_at = DateTime.Now;
            order.Updated_by = 1;
            order.Status = 1;
            try
            {
                var id = new OrderDao().Insert(order);
                var sessionCart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailDao();
                foreach (var item in sessionCart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderId = id ;
                    orderDetail.ProductId = item.Product.Id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Amount = item.Product.Price * item.Quantity;
                    detailDao.Insert(orderDetail);
                }
            } catch (Exception)
            {
                return Redirect("/loi-than-toan");
            }
            return Redirect("/hoan-thanh");
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}