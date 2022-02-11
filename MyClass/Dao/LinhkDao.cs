using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.Dao
{
    public class LinhkDao
    {
         private OnlineShopDBContext db = new OnlineShopDBContext();
        public Link getRow(String slug)
        {
            var row = db.Links.Where(m => m.Slug == slug).FirstOrDefault();
            return row;
        }
        public Link getRow(int? id)
        {
            return db.Links.Find(id);
        }
        public Link getRow(int tableid, String type)
        {
            return db.Links.Where(m => m.TableId == tableid && m.Type == type).FirstOrDefault();
        }
        public int Insert(Link entity)
        {
            db.Links.Add(entity);
            return db.SaveChanges();
        }
        //5.sửa
        public int Update(Link entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //6. xóa
        public int Delete(Link entity)
        {
            db.Links.Remove(entity);
            return db.SaveChanges();
        }
    }
}
