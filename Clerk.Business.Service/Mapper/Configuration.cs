using AutoMapper;
using Clerk.Business.Service.Mapper.Mapping;
using EFModel = Clerk.Data.Model.Models;

namespace Clerk.Business.Service.Mapper
{
    public class Configuration
    {
        public static IMapper Mapper { get; private set; }

        public static void Initialize()
        {
            var config = new MapperConfiguration(mc =>
            {
                mc.CreateMap<EFModel.Member, Entity.Member>().ReverseMap();
                mc.CreateMap<EFModel.Committee, Entity.Committee>().ReverseMap();

                mc.CreateMap<EFModel.Office, Entity.Office>().ReverseMap();
                mc.CreateMap<EFModel.State, Entity.State>().ReverseMap();
                mc.CreateMap<EFModel.CommitteeAssignment, Entity.CommitteeAssignment>().ReverseMap();

                mc.CreateMap<EFModel.Member, Entity.MemberByResponse>().ConvertUsing(new MemberToMemberByResponseMapping());
                mc.CreateMap<EFModel.CommitteeAssignment, Entity.CommitteeAssignmentResponse>().ConvertUsing(new CommitteeAssignmentToCommitteeAssignmentResponse());
            });

            Mapper = config.CreateMapper();
        }

    }


}