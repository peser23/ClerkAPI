using Clerk.Common.Helper;
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
        public virtual void SetModified<K>(K entity) where K : class
        {
            _uow.DbContext.Entry(entity).State = EntityState.Modified;
        }
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
        public T GetByUniqueId(Expression<Func<T, bool>> predicate)
        {
            var obj = Entities.AsNoTracking().FirstOrDefault(predicate);
            return obj;
        }
        public Entity.ActionStatus Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var selfTran = false;
            if (!_uow.InTransaction)
            {
                _uow.BeginTransaction();
                selfTran = true;
            }

            var _actionStatus = new Entity.ActionStatus();
            try
            {
                // entity.RefId = entity.RefId == Guid.Empty ? Guid.NewGuid() : entity.RefId;
                Entities.Add(entity);
                _actionStatus = ApplyChanges();

                if (!_actionStatus.Success) throw new Exception(_actionStatus.Message);
                // _actionStatus.Result = entity.RecordId;
                _actionStatus.Data = entity;
            }
            catch (Exception ex)
            {
                _actionStatus.Success = false;
                _actionStatus.Message = ex.Message;
            }
            finally
            {
                if (selfTran)
                {
                    if (_actionStatus.Success)
                    {
                        var _tactionStatus = _uow.EndTransaction();
                        if (!_tactionStatus.Success) _actionStatus = _tactionStatus;
                    }
                    else
                    {
                        _uow.RollBack();
                    }
                }
            }

            return _actionStatus;
        }
        public Entity.ActionStatus InsertRange(List<T> entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var selfTran = false;
            if (!_uow.InTransaction)
            {
                _uow.BeginTransaction();
                selfTran = true;
            }

            var _actionStatus = new Entity.ActionStatus();
            try
            {
                // entity.RefId = entity.RefId == Guid.Empty ? Guid.NewGuid() : entity.RefId;
                Entities.AddRange(entity);
                _actionStatus = ApplyChanges();

                if (!_actionStatus.Success) throw new Exception(_actionStatus.Message);
                // _actionStatus.Result = entity.RecordId;
                _actionStatus.Data = entity;
            }
            catch (Exception ex)
            {
                _actionStatus.Success = false;
                _actionStatus.Message = ex.Message;
            }
            finally
            {
                if (selfTran)
                {
                    if (_actionStatus.Success)
                    {
                        var _tactionStatus = _uow.EndTransaction();
                        if (!_tactionStatus.Success) _actionStatus = _tactionStatus;
                    }
                    else
                    {
                        _uow.RollBack();
                    }
                }
            }

            return _actionStatus;
        }
        public virtual Entity.ActionStatus Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var selfTran = false;
            if (!_uow.InTransaction)
            {
                _uow.BeginTransaction();
                selfTran = true;
            }

            var _actionStatus = new Entity.ActionStatus();
            try
            {
                SetModified(entity);
                _actionStatus = ApplyChanges();

                if (!_actionStatus.Success) throw new Exception(_actionStatus.Message);
                //_actionStatus.Result = entity.RecordId;
                _actionStatus.Data = entity;
            }
            catch (Exception ex)
            {
                _actionStatus.Success = false;
                _actionStatus.Message = ex.Message;
            }
            finally
            {
                if (selfTran)
                {
                    if (_actionStatus.Success)
                    {
                        var _tactionStatus = _uow.EndTransaction();
                        if (!_tactionStatus.Success) _actionStatus = _tactionStatus;
                    }
                    else
                    {
                        _uow.RollBack();
                    }
                }
            }

            return _actionStatus;
        }
        public Entity.ActionStatus Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            var selfTran = false;
            if (!_uow.InTransaction)
            {
                _uow.BeginTransaction();
                selfTran = true;
            }

            var _actionStatus = new Entity.ActionStatus();
            try
            {
                Entities.Remove(entity);
                _actionStatus = ApplyChanges();

                if (!_actionStatus.Success) throw new Exception(_actionStatus.Message);
            }
            catch (Exception ex)
            {
                _actionStatus.Success = false;
                _actionStatus.Message = ex.Message;
            }
            finally
            {
                if (selfTran)
                {
                    if (_actionStatus.Success)
                    {
                        _actionStatus = _uow.EndTransaction();
                    }
                    else
                    {
                        _uow.RollBack();
                    }
                }
            }

            return _actionStatus;
        }
        public Entity.ActionStatus RemoveRange(Expression<Func<T, bool>> predicate)
        {
            var _actionStatus = new Entity.ActionStatus(false, string.Empty, string.Empty, null);
            var entityList = Entities.Where(predicate);
            if (entityList != null && entityList.Count() > 0)
            {
                _actionStatus.Success = true;
                return _actionStatus;
            }

            var selfTran = false;
            if (!_uow.InTransaction)
            {
                _uow.BeginTransaction();
                selfTran = true;
            }

            try
            {
                _entities.RemoveRange(entityList);
                _actionStatus = ApplyChanges();

                if (!_actionStatus.Success) throw new Exception(_actionStatus.Message);
            }
            catch (Exception ex)
            {
                _actionStatus.Success = false;
                _actionStatus.Message = ex.Message;
            }
            finally
            {
                if (selfTran)
                {
                    if (_actionStatus.Success)
                    {
                        _actionStatus = _uow.EndTransaction();
                    }
                    else
                    {
                        _uow.RollBack();
                    }
                }
            }

            return _actionStatus;
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