using System;
using System.Collections.Generic;

#nullable disable

namespace Clerk.Data.Model.Models
{
    public partial class CommitteeRatio
    {
        public int CommitteeRatioId { get; set; }
        public int CommitteeId { get; set; }
        public string Majority { get; set; }

        public virtual Committee Committee { get; set; }
    }
}
