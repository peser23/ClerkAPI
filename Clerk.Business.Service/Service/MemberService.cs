using Clerk.Business.Service.Interface;
using Clerk.Common;
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
        public List<Entity.Member> Get()
        {
            try
            {
                return _memberRepository.GetAll().Select(p => Mapper.Configuration.Mapper.Map<Entity.Member>(p)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
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

        public Entity.Member Get(int id)
        {
            try
            {
                return _memberRepository.FindBy(r => r.MemberId == id).Select(p => Mapper.Configuration.Mapper.Map<Entity.Member>(p)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Entity.ActionStatus Manage(Entity.Member request)
        {
            Entity.ActionStatus actionStatus = new Entity.ActionStatus(true);
            try
            {
                var dbUser = Mapper.Configuration.Mapper.Map<Entity.Member, EFModel.Member>(request);
                if (request.MemberId <= 0)
                {
                    actionStatus = _memberRepository.Insert(dbUser);
                }
                else
                {
                    var olddbMember = _memberRepository.FindBy(x => x.MemberId.Equals(request.MemberId)).FirstOrDefault();
                    if (olddbMember == null)
                    {
                        throw new NotFoundCustomException($"{CommonException.Name.NoRecordsFound} : Member");
                    }

                    actionStatus = _memberRepository.Update(dbUser);
                    if (!actionStatus.Success)
                    {
                        actionStatus.Success = false;
                        actionStatus.Message = "Somthing went wrong!";
                    }
                }
            }
            catch (Exception ex)
            {
                actionStatus.Success = false;
                actionStatus.Message = ex.Message;
            }
            return actionStatus;
        }

        public Entity.ActionStatus Delete(int id)
        {
            Entity.ActionStatus actionStatus = new Entity.ActionStatus(true);
            try
            {
                var dbUser = _memberRepository.FindBy(x => x.MemberId.Equals(id)).FirstOrDefault();
                if (dbUser == null)
                {
                    throw new NotFoundCustomException($"{CommonException.Name.NoRecordsFound} : Member");
                }

                //dbUser.IsDeleted = true;
                actionStatus = _memberRepository.Update(dbUser);
                actionStatus.Data = Mapper.Configuration.Mapper.Map<EFModel.Member, Entity.Member>(actionStatus.Data);

            }
            catch (Exception ex)
            {
                actionStatus.Success = false;
                actionStatus.Message = ex.Message;
            }
            return actionStatus;
        }

    }
}