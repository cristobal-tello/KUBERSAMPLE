using System;

namespace API.Member.Models
{
    public class Member
    {
        public Guid MemberId { set; get; }
        public string Name { set; get; }
        public Guid TeamId {get; set;}
    }
}
