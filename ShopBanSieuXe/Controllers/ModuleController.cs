using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Dao;
using ShopBanSieuXe.Models;

namespace ShopBanSieuXe.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        private const string CartSession = "CartSession";

        MenuDao menuDao = new MyClass.Dao.MenuDao();
        CategoryDao categoryDao = new CategoryDao();
        SliderDao sliderDao = new SliderDao();
        ProductDao productDao = new ProductDao();
        PostDao postDao = new PostDao();
        public sealed class StackOverflowException : SystemException { }
        // GET: Module
        public ActionResult MainMenu()
        {
            var list_MainMenu = menuDao.getList();
            return View("MainMenu", list_MainMenu);
        }
        public ActionResult ListCategory()
        {
            var List_Category = categoryDao.getList();
            return View("ListCategory", List_Category);
        }
        public ActionResult SlideShow()
        {
            var list_SlideShow = sliderDao.getList();
            return View("SlideShow", list_SlideShow);
        }
        public ActionResult FeaturedProduct()
        {
            var list_FeaturedProduct = productDao.ListFeaturedProduct();
            return View("FeaturedProduct", list_FeaturedProduct);
        }
        public ActionResult Banner()
        {
            var list_banner = sliderDao.ListBanner();
            return View("Banner", list_banner);
        }
        public ActionResult ProductHot()
        {
            var list_ProductHot = productDao.ListHotProduct();
            return View("ProductHot", list_ProductHot);
        }
        public ActionResult NewProduct()
        {
            var list_NewProduct = productDao.ListNewProduct();
            return View("NewProduct", list_NewProduct);
        }
        public ActionResult SellingProduct()
        {
            var list_SellingProduct = productDao.ListSellingProduct();
            return View("SellingProduct", list_SellingProduct);
        }
        public ActionResult Blog()
        {
            return View("Blog");
        }
        public ActionResult Footer()
        {
            var list_footer = postDao.ListFooter();
            return View("Footer", list_footer);
        }
        public ActionResult HeaderCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View("HeaderCart", list);
        }
    }
}