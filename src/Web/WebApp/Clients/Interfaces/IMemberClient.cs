using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Clients.Interfaces
{
    public interface IMemberClient
    {
        Task<IEnumerable<Member>> GetMembersAsync();
        Task<Member> GetMemberAsync(Guid memberId);
        Task CreateMemberAsync(Member team);
        Task UpdateMemberAsync(Member team);
        Task DeleteMemberAsync(Guid memberId);
    }
}
