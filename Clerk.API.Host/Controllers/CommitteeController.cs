using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clerk.Business.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Entity = Clerk.Business.Entity;

namespace Clerk.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommitteeController : BaseController
    {

        private readonly ICommitteeService _committeeService;
        private readonly ILogger<CommitteeController> _logger;

        public CommitteeController(ICommitteeService committeeService, ILogger<CommitteeController> logger)
        {
            _committeeService = committeeService;
            _logger = logger;
        }

        /// <summary>
        /// Get Data on all Committees
        /// </summary>
        /// <param name="searchText">filter by committee name</param>
        /// <param name="pageNo">Number gt 0</param>
        /// <param name="pageSize">Number gt 0</param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public Entity.BaseResponse<Entity.SearchResult<List<Entity.CommitteeResponse>>> GetAllCommittees(string searchText = "", int? pageNo = -1, int? pageSize = -1, string orderBy = "")
        {
            Entity.BaseResponse<Entity.SearchResult<List<Entity.CommitteeResponse>>> response = new Entity.BaseResponse<Entity.SearchResult<List<Entity.CommitteeResponse>>>();

            var searchRequest = new Entity.SearchRequest()
            {
                SearchText = searchText,
                PageNumber = pageNo.Value,
                OrderBy = orderBy,
                PageSize = pageSize.Value
            };

            try
            {
                response.Data = _committeeService.GetList(searchRequest);
                response.IsSuccess = true;
                response.Message = "";
            }
            catch (Exception ex)
            {
                base.LogException(ex);
                return new Entity.BaseResponse<Entity.SearchResult<List<Entity.CommitteeResponse>>>(false, ex.Message);
            }

            return response;
        }
    }
}
