using Clerk.Business.Service.Interface;
using Clerk.Data.Repository.Implementation;
using Clerk.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EFModel = Clerk.Data.Model.Models;
using Clerk.Business.Entity;

namespace Clerk.Business.Service
{
    public class CommitteeService : ICommitteeService
    {

        private readonly ICommitteeRepository _committeeRepository;

        public CommitteeService(ICommitteeRepository committeeRepository)
        {
            _committeeRepository = committeeRepository;
        }

        public List<Entity.Committee> Get()
        {
            try
            {
                return _committeeRepository.GetAll().Select(p => Mapper.Configuration.Mapper.Map<Entity.Committee>(p)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SearchResult<List<CommitteeResponse>> GetList(SearchRequest request)
        {
            try
            {
                return _committeeRepository.List(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}