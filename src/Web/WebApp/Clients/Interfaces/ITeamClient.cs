using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Clients.Interfaces
{
    public interface ITeamClient
    {
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<Team> GetTeamAsync(Guid teamId);
        Task CreateTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(Guid teamId);
    }
}
