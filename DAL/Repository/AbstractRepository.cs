using Model;
using System;

namespace DAL.Repository
{
   public class AbstractRepository : IDisposable
    {
        private bool disposed;

        protected ModelSalesContainer Context { get; }

        protected AbstractRepository()
        {
            Context = new ModelSalesContainer();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Context.Dispose();
            }

            disposed = true;
        }

        ~AbstractRepository()


        {
            Dispose(false);
        }
    }
}
