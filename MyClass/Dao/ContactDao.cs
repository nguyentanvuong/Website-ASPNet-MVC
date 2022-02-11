using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MyClass.Models;

namespace MyClass.Dao
{
    public class ContactDao
    {
        private OnlineShopDBContext db = new OnlineShopDBContext();
        //public Contact getActiveContact()
        //{
        //    var list = db.Contacts.Single(m => m.Status == 1);
        //    return list;
        //}

        public int InsertContact(Contact ct)
        {
            db.Contacts.Add(ct);
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
            return ct.Id;
        }
        
    }
}
