using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Team.Models;

namespace API.Team.Data
{
    public class TeamDbContext : DbContext
    {
        public TeamDbContext (DbContextOptions<TeamDbContext> options)
            : base(options)
        {
        }

        public DbSet<API.Team.Models.Team> Team { get; set; }
    }
}
