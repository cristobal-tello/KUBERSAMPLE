using API.Team.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Team.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamDbContext teamDbContext;

        public TeamController(TeamDbContext teamDbContext)
        {
            this.teamDbContext = teamDbContext;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Team>>> GetTeams()
        {
            return await this.teamDbContext.Team.ToListAsync();
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Team>> GetTeam(Guid id)
        {
            var team = await this.teamDbContext.Team.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(Guid id, Models.Team team)
        {
            if (id != team.TeamId)
            {
                return BadRequest();
            }

            this.teamDbContext.Entry(team).State = EntityState.Modified;

            try
            {
                await this.teamDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Team
        [HttpPost]
        public async Task<ActionResult<Models.Team>> PostTeam(Models.Team team)
        {
            this.teamDbContext.Team.Add(team);
            await this.teamDbContext.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.TeamId }, team);
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Team>> DeleteTeam(Guid id)
        {
            var team = await this.teamDbContext.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            this.teamDbContext.Team.Remove(team);
            await this.teamDbContext.SaveChangesAsync();

            return team;
        }

        private bool TeamExists(Guid id)
        {
            return this.teamDbContext.Team.Any(e => e.TeamId == id);
        }
    }
}
