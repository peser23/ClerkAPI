using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using EFModel = Clerk.Data.Model.Models;

namespace Clerk.Business.Service.Mapper.Mapping
{
    public class CommitteeAssignmentToCommitteeAssignmentResponse : ITypeConverter<EFModel.CommitteeAssignment, Entity.CommitteeAssignmentResponse>
    {
        public Entity.CommitteeAssignmentResponse Convert(EFModel.CommitteeAssignment source, Entity.CommitteeAssignmentResponse destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            if (destination == null)
            {
                destination = new Entity.CommitteeAssignmentResponse();

            }

            destination.Code = source.Committee.Code;
            //destination.Member = string.Concat(source.Member.LastName, " ", source.Member.FirstName);
            destination.Rank = source.Rank.ToString();
            //destination.TimezoneGuid = source.Time;
            //destination.ParentEntityGuid = ;
            //destination.ChildEntityLable = ;
            return destination;
        }
    }
}
