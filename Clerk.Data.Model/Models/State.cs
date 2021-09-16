using System;
using System.Collections.Generic;

#nullable disable

namespace Clerk.Data.Model.Models
{
    public partial class State
    {
        public State()
        {
            Members = new HashSet<Member>();
        }

        public int StateId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
