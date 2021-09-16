namespace Clerk.Business.Entity
{

    public class CommitteeAssignment
    {
        public int AssignmentId { get; set; }
        public int MemberId { get; set; }
        public int CommitteeId { get; set; }
        public int? Rank { get; set; }
    }
}
