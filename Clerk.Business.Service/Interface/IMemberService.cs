using Clerk.Business.Entity;
using System;
using System.Collections.Generic;

namespace Clerk.Business.Service.Interface
{
    public interface IMemberService
    {
        List<Entity.MemberByResponse> GetRepresentativesByState(string state);
        List<Entity.MemberByResponse> GetRepresentativesByParty(string party);  
        SearchResult<List<MemberResponse>> GetList(SearchRequest searchRequest);
    }
}