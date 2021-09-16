using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using EF = Clerk.Data.Model.Models;
using Entity = Clerk.Business.Entity;

namespace Clerk.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("DBContext cannot be null.");

            DbContext = (EF.ClerkDataContext) dbContext;
        }

        private bool _disposed;
        private IDbContextTransaction _transaction { get; set; }

        public bool InTransaction { get; private set; }
        public EF.ClerkDataContext DbContext { get; }
        public virtual void BeginTransaction()
        {
            try
            {
                InTransaction = true;
                _transaction = DbContext.Database.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual Entity.ActionStatus EndTransaction()
        {
            var status = new Entity.ActionStatus();
            try
            {
                if (_disposed) throw new ObjectDisposedException(GetType().FullName);
                DbContext.SaveChanges();
                _transaction.Commit();
                InTransaction = false;
                status.Success = true;
            }
            //catch (DbEntityValidationException dbEx)
            //{
            //    _logger.Error(Constants.ACTION_EXCEPTION + ":UnitofWork.EndTransaction", dbEx);
            //    status.Message = dbEx.Message;
            //    status.Success = false;
            //    _inTransaction = false;
            //}
            catch (Exception ex)
            {
                status.Message = ex.Message;
                status.Success = false;
            }
            return status;
        }
        public virtual void RollBack()
        {
            try
            {
                _transaction.Rollback();
                _transaction.Dispose();
                InTransaction = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual Entity.ActionStatus SaveAndContinue()
        {
            var status = new Entity.ActionStatus();
            try
            {
                DbContext.SaveChanges();
                status.Success = true;                
            }
            //catch (DbEntityValidationException dbEx)
            //{
            //    _logger.Error(Constants.ACTION_EXCEPTION + ":UnitofWork.SaveAndContinue", dbEx);
            //    var errorMessages = dbEx.EntityValidationErrors
            //        .SelectMany(x => x.ValidationErrors)
            //        .Select(x => x.ErrorMessage);

            //    status.Message = string.Join("; ", errorMessages);
            //    status.Success = false;
            //}
            catch (Exception ex)
            {
                status.Message = ex.Message;
                status.Success = false;
            }

            return status;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && DbContext != null && InTransaction) _transaction.Dispose();
            if (disposing && DbContext != null) DbContext.Dispose();

            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}