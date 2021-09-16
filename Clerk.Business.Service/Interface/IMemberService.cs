using Clerk.Business.Entity;
using System;
using System.Collections.Generic;

namespace Clerk.Business.Service.Interface
{
    public interface IMemberService
    {
        List<Entity.Member> Get();
        List<Entity.MemberByResponse> GetRepresentativesByState(string state);
        List<Entity.MemberByResponse> GetRepresentativesByParty(string party);
        Entity.Member Get(int id);
        Entity.ActionStatus Manage(Entity.Member request);
        //Entity.SearchResult<List<Entity.Member>> List(Entity.SearchRequest request);
        Entity.ActionStatus Delete(int id);
        SearchResult<List<MemberResponse>> GetList(SearchRequest searchRequest);
    }
}