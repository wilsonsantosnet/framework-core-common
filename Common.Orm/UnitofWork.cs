using Common.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Common.Orm
{
    public class UnitOfWork<T> : IUnitOfWork
    {

        private DbContext _ctx;
        private bool _disposed;

        public UnitOfWork(T _ctx)
        {
            this._ctx = _ctx as DbContext;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void Commit()
        {
            this._ctx.SaveChanges();
        }

        public void CommitAsync()
        {
            this._ctx.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {

            if (!_disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }

        }

    }
}
