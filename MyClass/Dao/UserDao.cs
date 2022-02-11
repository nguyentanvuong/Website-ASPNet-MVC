using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Dao
{
    public class UserDao
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            return entity.Id;
        }
        public bool CheckUserName(string userName)
        {
            return db.Users.Count(m => m.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Users.Count(m => m.Email == email) > 0;
        }
        public User getRow(String username)
        {
            var Row = db.Users
                .Where(m => m.Access == 1 && m.Status == 1 && (m.UserName == username || m.Email == username))
                .FirstOrDefault();
            return Row;
        }
    }
}
