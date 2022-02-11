using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
using PagedList.Mvc;
using PagedList;
using System.Data.Entity;

namespace MyClass.Dao
{
    public class ProductDao
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();
       
        public List<Product> ListFeaturedProduct()
        {
            var list_featuredProduct = db.Products.Where(m => m.Status == 1)
                .OrderByDescending(m => m.Featured)
                .Take(8)
                .ToList();
            return list_featuredProduct;
        }
        public List<Product> ListNewProduct()
        {
            var list_NewProduct = db.Products.Where(m => m.Status == 1)
                .OrderByDescending(m => m.TopNew)
                .Take(3)
                .ToList();
            return list_NewProduct;
        }
        public List<Product> ListHotProduct()
        {
            var list_NewProduct = db.Products.Where(m => m.Status == 1)
                .OrderByDescending(m => m.TopHot)
                .Take(3)
                .ToList();
            return list_NewProduct;
        }
        public List<Product> ListSellingProduct()
        {
            var list_SellingProduct = db.Products.Where(m => m.Status == 1)
                .OrderBy(m => m.Number)
                .Take(3)
                .ToList();
            return list_SellingProduct;
        }
        public List<string> ListName(string keyword)
        {
            return db.Products.Where(m => m.Name.Contains(keyword)).Select(m => m.Name).ToList();
        }
        //lấy sản phẩm theo loại
        public List<Product> getList(int? catid)
        {
            var list = db.Products
                .Where(m => m.CatId == catid && m.Status == 1)
                .Take(4)
                .ToList();
            return list;
        }
        // chi tiết sản phẩm
        public List<Product> getList(List<int> listcatid, int notid, bool check = true)
        {
            var list = db.Products
                .Where(m => m.Status == 1 && m.Id != notid && listcatid.Contains(m.CatId))
                .OrderByDescending(m => m.Created_at)
                .Take(4)
                .ToList();
            return list;
        }
        public List<Product> getList(String page = "Index")
        {
            if (page == "Index")
            {                
                var list = db.Products.Where(m => m.Status != 0).OrderBy(m => m.Created_at).ToList();
                return list;
            }
            else
            {
                var list = db.Products.Where(m => m.Status == 0).OrderBy(m => m.Created_at).ToList();
                return list;
            }
        }
        public PagedList.IPagedList<Product> getList(List<int> listcatid, int PageSize, int PageNumber)
        {
            var list = db.Products
                .Where(m => m.Status == 1 && listcatid.Contains(m.CatId))
                .OrderByDescending(m => m.Created_at).ToPagedList(PageNumber, PageSize);
            return list;
        }
        public PagedList.IPagedList<Product> Search(string keyword, int PageSize, int PageNumber)
        {
            var list = db.Products
                .Where(m => m.Status == 1 && m.Name.Contains(keyword))
                .OrderByDescending(m => m.Created_at).ToPagedList(PageNumber, PageSize);
            return list;
        }

        public object ViewDetail(int productId)
        {
            return db.Products.Find(productId);
        }
        public Product getRow(int? id)
        {
            var Row = db.Products.Find(id);
            return Row;
        }
        //3. trả về một mẫu tin object
        public Product getRow(String slug)
        {
            var Row = db.Products.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
            return Row;
        }
        //4.Thêm 
        public int Insert(Product entity)
        {
            db.Products.Add(entity);
            return db.SaveChanges();
        }
        //5.sửa
        public int Update(Product entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //6. xóa
        public int Delete(Product entity)
        {
            db.Products.Remove(entity);
            return db.SaveChanges();
        }
    }
}
