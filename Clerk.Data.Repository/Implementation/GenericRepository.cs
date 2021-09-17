using Clerk.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Entity = Clerk.Business.Entity;

namespace Clerk.Data.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        public GenericRepository(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");

            if (unitOfWork == null)
                throw new ArgumentNullException("UnitOfWork cannot be null.");

            _uow = unitOfWork;
        }
        private DbSet<T> Entities
        {
            get
            {
                if (_entities == null) _entities = _uow.DbContext.Set<T>();
                return _entities;
            }
        }
        public string ConnectionString { get; }       
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var list = Entities.AsNoTracking().Where(predicate);
            return list;
        }
        public IQueryable<T> GetAll()
        {
            IQueryable<T> list = Entities;
            return list;
        }      

        #region Private Methods
        private Entity.ActionStatus ApplyChanges()
        {
            var result = new Entity.ActionStatus();
            try
            {
                result = _uow.SaveAndContinue();
                if (!result.Success) throw new Exception(result.Message);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                result.Message = ex.Message;
            }
            catch (DbUpdateException ese)
            {
                result.Message = ese.Message;
            }


            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
        #endregion

        #region Variable Declaration
        protected IUnitOfWork _uow;
        private DbSet<T> _entities;
        #endregion


    }
}