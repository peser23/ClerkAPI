using Clerk.Data.Repository.Interface;
using Clerk.Framework.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFModel = Clerk.Data.Model.Models;
using Entity = Clerk.Business.Entity;

namespace Clerk.Data.Repository.Implementation
{
    public class CommitteeRepository : GenericRepository<EFModel.Committee>, ICommitteeRepository
    {
        public CommitteeRepository(IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, configuration)
        {
            _uow = unitOfWork;
        }
        public EFModel.Committee Get(string code)
        {
            var result = new EFModel.Committee();
            try
            {
                _uow.DbContext.Committees.Where(u => u.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Entity.SearchResult<List<Entity.CommitteeResponse>> List(Entity.SearchRequest request)
        {
            Entity.SearchResult<List<Entity.CommitteeResponse>> result = new Entity.SearchResult<List<Entity.CommitteeResponse>>();
            try
            {
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
                    var commandDef = sqlDataAccess.CreateCommand("[Committee_List]", CommandType.StoredProcedure, null);
                    System.Data.Common.DbDataReader dbDataReader = sqlDataAccess.ExecuteReader(commandDef, parameters.ToArray());
                    result.Items = DataUtils.DataReaderToList<Entity.CommitteeResponse>(dbDataReader, null);
                    result.Count = int.Parse(parameters.Where(p => p.ParameterName.Equals("count"))?.FirstOrDefault().Value.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}