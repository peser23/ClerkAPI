using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Entity = Clerk.Business.Entity;

namespace Clerk.Data.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}