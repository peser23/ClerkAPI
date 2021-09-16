using System.Collections.Generic;
using Entity = Clerk.Business.Entity;
using EFModel = Clerk.Data.Model.Models;

namespace Clerk.Data.Repository.Interface
{
    public interface IMemberRepository : IGenericRepository<EFModel.Member>
    {
        EFModel.Member Get(string name);
        Entity.SearchResult<List<Entity.MemberResponse>> List(Entity.SearchRequest request);
        List<EFModel.Member> GetRepresentativesByState(string state);
        List<EFModel.Member> GetRepresentativesByParty(string party);
    }
}