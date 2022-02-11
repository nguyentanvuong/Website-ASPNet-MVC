using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using MyClass.Models;

namespace MyClass.Dao
{
    public class CategoryDao
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();
        public List<Category> getList()
        {
            var list = db.Categories.Where(m => m.Status == 1).ToList();
            return list;
        }
        public List<Category> getList(int parentid)
        {
            var List = db.Categories.Where(m => m.ParentId == parentid && m.Status == 1)
                .OrderBy(m => m.Orders)
                .ToList();
            return List;
        }
        public List<int> getListId(int parentid)
        {
            List<int> listcatid = new List<int>();
            List<Category> listcategory1 = this.getList(parentid);
            if (listcategory1.Count > 0)
            {
                foreach (var cat1 in listcategory1)
                {
                    listcatid.Add(cat1.Id);
                    List<Category> listcategory2 = this.getList(cat1.Id);
                    if (listcategory2.Count > 0)
                    {
                        foreach (var cat2 in listcategory1)
                        {
                            listcatid.Add(cat1.Id);
                            List<Category> listcategory3 = this.getList(cat2.Id);
                            if (listcategory3.Count > 0)
                            {
                                foreach (var cat3 in listcategory3)
                                {
                                    listcatid.Add(cat3.Id);

                                }
                            }
                        }
                    }
                }
            }
            listcatid.Add(parentid);
            return listcatid;
        }
        public List<Category> getList(String page = "Index")
        {
            if (page == "Index")
            {
                var list = db.Categories
                .Where(m => m.Status != 0)
                .OrderBy(m => m.Created_at)
                .ToList();
                return list;
            }
            else
            {
                var list = db.Categories
                .Where(m => m.Status == 0)
                .OrderBy(m => m.Created_at)
                .ToList();
                return list;
            }

        }
        //3. trả về một mẫu tin object
        public Category getRow(int? id)
        {
            var row = db.Categories.Find(id);
            return row;
        }
        public Category getRow(String slug)
        {
            var row = db.Categories.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
            return row;
        }
        //4.Thêm 
        public int Insert(Category entity)
        {
            db.Categories.Add(entity);
            return db.SaveChanges();
        }
        //5.sửa
        public int Update(Category entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //6. xóa
        public int Delete(Category entity)
        {
            db.Categories.Remove(entity);
            return db.SaveChanges();
        }
    }
}
