using System;
using System.Collections.Generic;

namespace Clerk.Business.Entity
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Suffix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string BioguideId { get; set; }
        public string Party { get; set; }
        public string Caucus { get; set; }
        public string TownName { get; set; }
        public string District { get; set; }
        public string Phone { get; set; }
        public DateTime? ElectedDate { get; set; }
        public DateTime? SwornDate { get; set; }
        public int? StateId { get; set; }
        public int? OfficeId { get; set; }

        public virtual Office Office { get; set; }
        public virtual State State { get; set; }
        public virtual List<CommitteeAssignment> CommitteeAssignments { get; set; }
    }
}
