using System.Collections.Generic;
using Entity = Clerk.Business.Entity;
using EFModel = Clerk.Data.Model.Models;

namespace Clerk.Data.Repository.Interface
{
    public interface ICommitteeRepository : IGenericRepository<EFModel.Committee>
    {
        EFModel.Committee Get(string name);
        Entity.SearchResult<List<Entity.CommitteeResponse>> List(Entity.SearchRequest request);
    }
}