using System;

namespace WebApp.ViewModels
{
    public class MemberViewModel
    {
        public Guid MemberId { set; get; }
        public string Name { set; get; }
        public Guid TeamId {get; set;}
        public String TeamName { get; set; }
    }
}
