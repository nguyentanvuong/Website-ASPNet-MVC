using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MyClass.Dao;
using MyClass.Models;

namespace ShopBanSieuXe.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        LinhkDao linhkDao = new LinhkDao();
        CategoryDao categoryDao = new CategoryDao();
        ProductDao productDao = new ProductDao();
        PostDao postDao = new PostDao();
        TopicDao topicDao = new TopicDao();

        public ActionResult Index(String slug = "", int? page = 1)
        {
            if (slug == "")
            {
                return this.Home();
            }
            else
            {
                Link row_link = linhkDao.getRow(slug);
                if (row_link != null)
                {
                    string type = row_link.Type;
                    switch (type)
                    {
                        case "category": { return this.ProductCategory(slug, page); }
                        case "topic": { return this.PostTopic(slug); }
                        case "page": { return this.PostPage(slug); }
                            //case "them-gio-hang": { return this.Index(); }
                    }
                }
                else
                {
                    if (productDao.getRow(slug) != null)
                    {
                        return this.ProductDetail(slug);
                    }
                    if (postDao.getRow(slug) != null)
                    {
                        return this.PostDetail(slug);
                    }
                    return this.Error404(slug);
                }
            }
            return this.Error404(slug);
        }
        public ActionResult Home()
        {
            var list = categoryDao.getList();
            return View("Home", list);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                Status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductCategory(String slug, int? page)
        {
            int PageSize = 3;
            int PageNumber = (page ?? 1);
            var row_cat = categoryDao.getRow(slug);
            int catid = row_cat.Id;
            List<int> listcatid = categoryDao.getListId(catid);
            var list = productDao.getList(listcatid, PageSize, PageNumber);
            ViewBag.Slug = slug;
            ViewBag.Title = row_cat.Name;
            return View("ProductCategory", list);
        }
        public ActionResult Search(string keyword, String slug, int? page)
        {
            int PageSize = 3;
            int PageNumber = (page ?? 1);
            var list = productDao.Search(keyword, PageSize, PageNumber);
            ViewBag.Slug = slug;
            return View("Search", list);
        }
        public ActionResult PostTopic(String slug)
        {
            var row_topic = topicDao.getRow(slug);
            ViewBag.Title = row_topic.Name;
            int topid = row_topic.Id;
            var list_PostTopic = postDao.getList();
            return View("PostTopic", list_PostTopic);
        }
        public ActionResult PostPage(String slug)
        {
            var row = postDao.getRow(slug);
            int? topid = row.TopId;
            var listother = postDao.getList(topid, row.Id);
            ViewBag.ListOther = listother;
            return View("PostPage", row);
        }
        public ActionResult ProductDetail(String slug)
        {
            var row = productDao.getRow(slug);
            int catid = row.CatId;//thuoc cung loai
            List<int> listcatid = categoryDao.getListId(catid);
            var listother = productDao.getList(listcatid, row.Id, true);
            ViewBag.ListOther = listother;
            return View("ProductDetail", row);
        }
        public ActionResult PostDetail(String slug)
        {
            var row = postDao.getRow(slug);
            int? topid = row.TopId;
            var listother = postDao.getList(topid, row.Id);
            ViewBag.ListOther = listother;
            return View("PostDetail", row);
        }
        public ActionResult Error404(String slug)
        {
            return View("Error404");
        }
    }
}