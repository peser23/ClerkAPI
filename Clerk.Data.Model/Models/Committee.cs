using System;
using System.Collections.Generic;

#nullable disable

namespace Clerk.Data.Model.Models
{
    public partial class Committee
    {
        public Committee()
        {
            CommitteeAssignments = new HashSet<CommitteeAssignment>();
            CommitteeRatios = new HashSet<CommitteeRatio>();
            InverseParentCommittee = new HashSet<Committee>();
        }

        public int CommitteeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Room { get; set; }
        public string HeaderText { get; set; }
        public string ZipCode { get; set; }
        public string ZipSuffix { get; set; }
        public string Phone { get; set; }
        public string BuildingCode { get; set; }
        public int? ParentCommitteeId { get; set; }

        public virtual Committee ParentCommittee { get; set; }
        public virtual ICollection<CommitteeAssignment> CommitteeAssignments { get; set; }
        public virtual ICollection<CommitteeRatio> CommitteeRatios { get; set; }
        public virtual ICollection<Committee> InverseParentCommittee { get; set; }
    }
}
