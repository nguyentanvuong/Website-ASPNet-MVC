using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
using System.Data;
using System.Data.Entity;

namespace MyClass.Dao
{
    public class MenuDao
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();
        public List<Menu> getList()
        {
            var list = db.Menus.Where(m => m.Status == 1).ToList();
            return list;
        }
        public List<Menu> getList(String page = "Index")
        {
            if (page == "Index")
            {
                var list = db.Menus
                .Where(m => m.Status != 0)
                .OrderBy(m => m.Created_at)
                .ToList();
                return list;
            }
            else
            {
                var list = db.Menus
                .Where(m => m.Status == 0)
                .OrderBy(m => m.Created_at)
                .ToList();
                return list;
            }

        }
        //3. trả về một mẫu tin object
        public Menu getRow(int? id)
        {
            var row = db.Menus.Find(id);
            return row;
        }
        public Menu getRow(String slug)
        {
            var row = db.Menus.Where(m => m.Status == 1).FirstOrDefault();
            return row;
        }
        //4.Thêm 
        public int Insert(Menu entity)
        {
            db.Menus.Add(entity);
            return db.SaveChanges();
        }
        //5.sửa
        public int Update(Menu entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //6. xóa
        public int Delete(Menu entity)
        {
            db.Menus.Remove(entity);
            return db.SaveChanges();
        }
    }
}
