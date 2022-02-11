using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.Dao
{
    public class OrderDetailDao
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();

        public List<OrderDetail> getList(String page = "Index")
        {
            if (page == "Index")
            {
                var list = db.OrderDetails
                .ToList();
                return list;
            }
            else
            {
                var list = db.OrderDetails           
                .ToList();
                return list;
            }

        }

        //3. trả về một mẫu tin object
        public OrderDetail getRow(int? id)
        {
            var row = db.OrderDetails.Find(id);
            return row;
        }
        public OrderDetail getRow()
        {
            var row = db.OrderDetails.FirstOrDefault();
            return row;
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //5.sửa
        public int Update(OrderDetail entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //6. xóa
        public int Delete(OrderDetail entity)
        {
            db.OrderDetails.Remove(entity);
            return db.SaveChanges();
        }
    }
}
