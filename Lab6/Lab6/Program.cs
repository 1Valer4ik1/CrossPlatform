using System;
using System.Collections.Generic;

namespace GlobalCommunity.Models
{
    public class LocalCommunity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime? DateEnded { get; set; }

        public ICollection<Member> Members { get; set; }
    }

    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime? DateLeft { get; set; }

        public int LocalCommunityId { get; set; }
        public LocalCommunity LocalCommunity { get; set; }

        public ICollection<MemberInterest> MemberInterests { get; set; }
    }

    public class InterestGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<MemberInterest> MemberInterests { get; set; }
    }

    public class MemberInterest
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int InterestGroupId { get; set; }
        public InterestGroup InterestGroup { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class Publication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Frequency { get; set; }
        public string Description { get; set; }
        public DateTime PlannedPublicationDate { get; set; }
    }

    public class PublicationSubscription
    {
        public int Id { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public DateTime? UnsubscriptionDate { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
    }
}