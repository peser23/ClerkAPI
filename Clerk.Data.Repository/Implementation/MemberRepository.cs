using Clerk.Data.Repository.Interface;
using Clerk.Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFModel = Clerk.Data.Model.Models;
using Entity = Clerk.Business.Entity;

namespace Clerk.Data.Repository.Implementation
{
    public class MemberRepository : GenericRepository<EFModel.Member>, IMemberRepository
    {
        public MemberRepository(IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, configuration)
        {
            _uow = unitOfWork;
        }

        public EFModel.Member Get(string name)
        {
            var result = new EFModel.Member();
            try
            {
                result = _uow.DbContext.Members.Where(u => u.FirstName.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public List<EFModel.Member> GetRepresentativesByState(string state)
        {
            List<EFModel.Member> result = new List<EFModel.Member>();
            result = _uow.DbContext.Members.Include("State").Include("CommitteeAssignments")
                    .Include("Office").Include("CommitteeAssignments.Committee")
                    .Include("CommitteeAssignments.Member")
                    .Where(u => u.State.Name.Equals(state) || u.State.Code.Equals(state)).ToList();
            return result;
        }

        public List<EFModel.Member> GetRepresentativesByParty(string party)
        {
            List<EFModel.Member> result = new List<EFModel.Member>();
            result = _uow.DbContext.Members.Include("State").Include("CommitteeAssignments")
                    .Include("Office").Include("CommitteeAssignments.Committee")
                    .Include("CommitteeAssignments.Member")
                .Where(u => u.Party.Equals(party)).ToList();
            return result;
        }

        public Entity.SearchResult<List<Entity.MemberResponse>> List(Entity.SearchRequest request)
        {
            Entity.SearchResult<List<Entity.MemberResponse>> result = new Entity.SearchResult<List<Entity.MemberResponse>>();
            using (var sqlDataAccess = new SqlDataAccess(ConnectionString))
            {
                List<System.Data.Common.DbParameter> parameters = sqlDataAccess.CreateParams();
                parameters.Add(sqlDataAccess.CreateParameter("search", request.SearchText, DbType.String, ParameterDirection.Input));
                parameters.Add(sqlDataAccess.CreateParameter("pagesize", request.PageSize, DbType.Int32, ParameterDirection.Input));
                parameters.Add(sqlDataAccess.CreateParameter("pagenumber", request.PageNumber, DbType.Int32, ParameterDirection.Input));
                parameters.Add(sqlDataAccess.CreateParameter("orderby", request.OrderBy, DbType.String, ParameterDirection.Input));
                parameters.Add(sqlDataAccess.CreateParameter("count", DbType.Int32, ParameterDirection.Output, 16));
                parameters.Add(sqlDataAccess.CreateParameter("output", DbType.Int16, ParameterDirection.Output, 16));
                parameters.Add(sqlDataAccess.CreateParameter("fieldname", DbType.String, ParameterDirection.Output, 100));
                var commandDef = sqlDataAccess.CreateCommand("[Member_List]", CommandType.StoredProcedure, null);
                System.Data.Common.DbDataReader dbDataReader = sqlDataAccess.ExecuteReader(commandDef, parameters.ToArray());
                result.Items = DataUtils.DataReaderToList<Entity.MemberResponse>(dbDataReader, null);
                result.Count = int.Parse(parameters.Where(p => p.ParameterName.Equals("count"))?.FirstOrDefault().Value.ToString());
            }
            return result;
        }
    }
}