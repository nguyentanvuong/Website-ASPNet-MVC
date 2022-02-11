using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
namespace MyClass.Dao
{
    public class TopicDao
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();
        public List<Topic> getList(string slug)
        {
            var list = db.Topics.Where(m => m.Slug == slug && m.Status == 1).ToList();
            return list;
        }
        public Topic getRow(String slug)
        {
            var Row = db.Topics.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
            return Row;
        }
    }
}
