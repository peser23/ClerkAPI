using System.Collections.Generic;

namespace Clerk.Business.Entity
{
    public class Committee
    {
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

        public virtual List<CommitteeRatio> CommitteeRatios { get; set; }
        public virtual List<Committee> SubCommittee { get; set; }
    }
}
