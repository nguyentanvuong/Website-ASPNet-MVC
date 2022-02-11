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
    public class SliderDao
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();
        public List<Slider> getList()
        {
            var list = db.Sliders.Where(m => m.Status == 1 && m.Position == "slidershow").ToList();
            return list;
        }
        public List<Slider> ListBanner()
        {
            var list_banner = db.Sliders.Where(m => m.Status == 1 && m.Position == "Banner")
                .Take(2).ToList();
            return list_banner;
        }
        public List<Slider> getList(String page = "Index")
        {
            if (page == "Index")
            {
                var list = db.Sliders
                .Where(m => m.Status != 0)
                .OrderBy(m => m.Created_at)
                .ToList();
                return list;
            }
            else
            {
                var list = db.Sliders
                .Where(m => m.Status == 0)
                .OrderBy(m => m.Created_at)
                .ToList();
                return list;
            }
        }
        //3. trả về một mẫu tin object
        public Slider getRow(int? id)
        {
            var row = db.Sliders.Find(id);
            return row;
        }
        public Slider getRow(String slug)
        {
            var row = db.Sliders.Where(m => m.Status == 1).FirstOrDefault();
            return row;
        }
        //4.Thêm 
        public int Insert(Slider entity)
        {
            db.Sliders.Add(entity);
            return db.SaveChanges();
        }
        //5.sửa
        public int Update(Slider entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //6. xóa
        public int Delete(Slider entity)
        {
            db.Sliders.Remove(entity);
            return db.SaveChanges();
        }
    }
}
