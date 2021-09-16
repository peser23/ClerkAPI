using System;
using System.Collections.Generic;

#nullable disable

namespace Clerk.Data.Model.Models
{
    public partial class CommitteeAssignment
    {
        public int AssignmentId { get; set; }
        public int MemberId { get; set; }
        public int CommitteeId { get; set; }
        public int? Rank { get; set; }

        public virtual Committee Committee { get; set; }
        public virtual Member Member { get; set; }
    }
}
