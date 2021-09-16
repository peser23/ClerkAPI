using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using EFModel = Clerk.Data.Model.Models;

namespace Clerk.Business.Service.Mapper.Mapping
{
    public class MemberToMemberByResponseMapping : ITypeConverter<EFModel.Member, Entity.MemberByResponse>
    {
        public Entity.MemberByResponse Convert(EFModel.Member source, Entity.MemberByResponse destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null;
            }

            if (destination == null)
            {
                destination = new Entity.MemberByResponse();

            }

            destination.NameList = string.Concat(source.LastName, ", ", source.FirstName);
            destination.SortName = string.Concat(source.LastName, ", ", source.FirstName).ToUpper();
            destination.OfficialName = string.Concat(source.LastName, " ", source.FirstName);
            destination.FormalName = string.Concat(source.Suffix, " ", source.LastName);

            destination.Suffix = source.Suffix;
            destination.FirstName = source.FirstName;
            destination.MiddleName = source.MiddleName;
            destination.LastName = source.LastName;
            destination.BioguideId = source.BioguideId;
            destination.Party = source.Party;
            destination.Caucus = source.Caucus;
            destination.TownName = source.TownName;
            destination.District = source.District;
            destination.Phone = source.Phone;

            destination.SwornDate = source.SwornDate;
            destination.ElectedDate = source.ElectedDate;

            destination.OfficeBuilding = source.Office.Building;
            destination.OfficeRoom = source.Office.Room;
            destination.OfficeZipCode = source.Office.ZipCode;
            destination.OfficeZipSuffix = source.Office.ZipSuffix;

            destination.State = new Entity.StateResponse() { Code = source.State.Code, Name = source.State.Name };
            destination.CommitteeAssignments = source.CommitteeAssignments.Select(p => Mapper.Configuration.Mapper.Map<Entity.CommitteeAssignmentResponse>(p)).ToList();

            //destination.TimezoneGuid = source.Time;
            //destination.ParentEntityGuid = ;
            //destination.ChildEntityLable = ;
            return destination;
        }
    }
}
