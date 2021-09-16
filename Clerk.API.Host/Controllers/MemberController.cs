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
