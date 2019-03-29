using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Member.Models;

namespace API.Member.Data
{
    public class MemberDbContext : DbContext
    {
        public MemberDbContext (DbContextOptions<MemberDbContext> options)
            : base(options)
        {
        }

        public DbSet<API.Member.Models.Member> Member { get; set; }
    }
}
