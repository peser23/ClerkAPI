using Microsoft.EntityFrameworkCore;
using System;
using Entity = Clerk.Business.Entity;
using EF = Clerk.Data.Model.Models;

namespace Clerk.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        EF.ClerkDataContext DbContext { get; }
        bool InTransaction { get; }
        void BeginTransaction();
        //Entity.ActionStatus SaveAndContinue();
        Entity.ActionStatus EndTransaction();
        //void RollBack();
    }
}