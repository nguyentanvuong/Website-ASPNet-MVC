using System.Data.Entity.Validation;

namespace MyClass.Dao
{
    internal class FormattedDbEntityValidationException
    {
        private DbEntityValidationException e;

        public FormattedDbEntityValidationException(DbEntityValidationException e)
        {
            this.e = e;
        }
    }
}