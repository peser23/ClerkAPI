using System;
using System.Collections.Generic;

#nullable disable

namespace Clerk.Data.Model.Models
{
    public partial class Office
    {
        public Office()
        {
            Members = new HashSet<Member>();
        }

        public int OfficeId { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
        public string ZipCode { get; set; }
        public string ZipSuffix { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
