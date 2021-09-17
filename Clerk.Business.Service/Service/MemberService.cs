using Clerk.Business.Service.Interface;
using Clerk.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EFModel = Clerk.Data.Model.Models;

namespace Clerk.Business.Service
{
    public class MemberService : IMemberService
    {

        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }       

        public Entity.SearchResult<List<Entity.MemberResponse>> GetList(Entity.SearchRequest request)
        {
            try
            {
                return _memberRepository.List(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Entity.MemberByResponse> GetRepresentativesByState(string state)
        {
            try
            {
                return _memberRepository.GetRepresentativesByState(state).Select(p => Mapper.Configuration.Mapper.Map<Entity.MemberByResponse>(p)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Entity.MemberByResponse> GetRepresentativesByParty(string party)
        {
            try
            {
                return _memberRepository.GetRepresentativesByParty(party).Select(p => Mapper.Configuration.Mapper.Map<Entity.MemberByResponse>(p)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}