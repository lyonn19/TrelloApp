using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloApp.Models
{
    public class NonPublic
    {
    }

    public class Member
    {
        public string id { get; set; }
        public string username { get; set; }
        public bool confirmed { get; set; }
        public string memberType { get; set; }
        public bool activityBlocked { get; set; }
        public string avatarHash { get; set; }
        public string avatarUrl { get; set; }
        public string fullName { get; set; }
        public string idMemberReferrer { get; set; }
        public string initials { get; set; }
        public NonPublic nonPublic { get; set; }
        public bool nonPublicAvailable { get; set; }
    }

    public class Membership
    {
        public string id { get; set; }
        public string idMember { get; set; }
        public string memberType { get; set; }
        public bool unconfirmed { get; set; }
        public bool deactivated { get; set; }
    }

    public class BoardMember
    {
        public string id { get; set; }
        public List<Member> members { get; set; }
        public List<Membership> memberships { get; set; }
    }
}
