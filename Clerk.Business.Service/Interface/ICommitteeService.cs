using System;
using System.Collections.Generic;

namespace Clerk.Business.Service.Interface
{
    public interface ICommitteeService
    {
        List<Entity.Committee> Get();
        Entity.SearchResult<List<Entity.CommitteeResponse>> GetList(Entity.SearchRequest request);
    }
}