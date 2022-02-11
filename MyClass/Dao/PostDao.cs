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
    public class PostDao
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();
        public List<Post> getList()
        {
            var list = db.Posts.Where(m => m.Status == 1).ToList();
            return list;
        }
        public List<Post> ListFooter()
        {
            var list_footer = db.Posts.Where(m => m.Status == 1 && m.Type == "post").ToList();
            return list_footer;
        }
        public List<Post> getList(int? topid, int notid)
        {
            var list = db.Posts.Where(m => m.TopId == topid && m.Status == 1 && m.Id != notid)
                .OrderByDescending(m => m.Created_at)
                .Take(4)
                .ToList();
            return list;
        }
        public Post getRow(string slug)
        {
            var row = db.Posts.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
            return row;
        }
    }
}
