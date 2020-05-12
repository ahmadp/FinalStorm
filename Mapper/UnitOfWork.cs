using Model;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class UnitOfWork
    {
        private FinalStromContext context = new FinalStromContext();
        private Repository<Users> usersRepository;

        private bool disposed = false;

        public void Save()
        {
            //try
            //{
            context.SaveChanges();
            //}
            //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            //{

            //}
            //context.SaveChanges();
        }

        public Repository<Users> UsersRepository
        {
            get
            {

                if (this.usersRepository == null)
                {
                    this.usersRepository = new Repository<Users>(context);
                }
                return usersRepository;
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
