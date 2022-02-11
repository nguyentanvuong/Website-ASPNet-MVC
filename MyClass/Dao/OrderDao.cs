using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.Dao
{
    public class OrderDao
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();

        public List<Order> getList(String page = "Index")
        {
            if (page == "Index")
            {
                var list = db.Orders
                .Where(m => m.Status != 0)
                .OrderBy(m => m.Updated_at)
                .ToList();
                return list;
            }
            else
            {
                var list = db.Orders
                .Where(m => m.Status == 0)
                .OrderBy(m => m.Updated_at)
                .ToList();
                return list;
            }

        }

        //3. trả về một mẫu tin object
        public Order getRow(int? id)
        {
            var row = db.Orders.Find(id);
            return row;
        }
        public Order getRow()
        {
            var row = db.Orders.Where(m => m.Status == 1).FirstOrDefault();
            return row;
        }

        public int Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.Id;
        }

        //5.sửa
        public int Update(Order entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //6. xóa
        public int Delete(Order entity)
        {
            db.Orders.Remove(entity);
            return db.SaveChanges();
        }
    }  
}
