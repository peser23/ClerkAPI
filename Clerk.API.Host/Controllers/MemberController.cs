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
    public class MemberController : BaseController
    {

        private readonly IMemberService _memberService;
        private readonly ILogger<MemberController> _logger;

        public MemberController(IMemberService memberService, ILogger<MemberController> logger)
        {
            _memberService = memberService;
            _logger = logger;
        }

        /// <summary>
        /// Get Members by State
        /// </summary>
        /// <param name="state">Example: VA, MD, AK</param>
        /// <returns>returns Member details and committee assignments</returns>
        [HttpGet]
        [Route("GetByState")]
        public Entity.BaseResponse<List<Entity.MemberByResponse>> GetRepresentativesByState(string state)
        {
            try
            {
                Entity.BaseResponse<List<Entity.MemberByResponse>> response = new Entity.BaseResponse<List<Entity.MemberByResponse>>();
                response.Data = _memberService.GetRepresentativesByState(state);
                response.IsSuccess = true;
                response.Message = "";
                return response;
            }
            catch (Exception ex)
            {
                base.LogException(ex);
                return new Entity.BaseResponse<List<Entity.MemberByResponse>>(false, ex.Message);
            }
        }

        /// <summary>
        /// Get Members by Party
        /// </summary>
        /// <param name="party">Example: D or R</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByParty")]
        public Entity.BaseResponse<List<Entity.MemberByResponse>> GetRepresentativesByParty(string party)
        {
            try
            {
                Entity.BaseResponse<List<Entity.MemberByResponse>> response = new Entity.BaseResponse<List<Entity.MemberByResponse>>();
                response.Data = _memberService.GetRepresentativesByParty(party);
                response.IsSuccess = true;
                response.Message = "";
                return response;
            }
            catch (Exception ex)
            {
                base.LogException(ex);
                return new Entity.BaseResponse<List<Entity.MemberByResponse>>(false, ex.Message);
            }
        }

        /// <summary>
        /// Get All Members
        /// </summary>
        /// <param name="searchText">filter by Member Name</param>
        /// <param name="pageNo">Number gt 0</param>
        /// <param name="pageSize">Number gt 0</param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public Entity.BaseResponse<Entity.SearchResult<List<Entity.MemberResponse>>> GetAllMembers(string searchText = "", int? pageNo = -1, int? pageSize = -1, string orderBy = "")
        {
            var searchRequest = new Entity.SearchRequest()
            {
                SearchText = searchText,
                PageNumber = pageNo.Value,
                OrderBy = orderBy,
                PageSize = pageSize.Value
            };

            try
            {
                Entity.BaseResponse<Entity.SearchResult<List<Entity.MemberResponse>>> response = new Entity.BaseResponse<Entity.SearchResult<List<Entity.MemberResponse>>>();
                response.Data = _memberService.GetList(searchRequest);
                response.IsSuccess = true;
                response.Message = "";
                return response;
            }
            catch (Exception ex)
            {
                base.LogException(ex);
                return new Entity.BaseResponse<Entity.SearchResult<List<Entity.MemberResponse>>>(false, ex.Message);
            }
        }
    }
}
