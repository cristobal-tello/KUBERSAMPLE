using API.Member.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Member.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MemberDbContext memberDbContext;

        public MemberController(MemberDbContext memberDbContext)
        {
            this.memberDbContext = memberDbContext;
        }

        // GET: api/Member
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Member>>> GetMember()
        {
            return await this.memberDbContext.Member.ToListAsync();
        }

        // GET: api/Member/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Member>> GetMember(Guid id)
        {
            var member = await memberDbContext.Member.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Member/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(Guid id, Models.Member member)
        {
            if (id != member.MemberId)
            {
                return BadRequest();
            }

            memberDbContext.Entry(member).State = EntityState.Modified;

            try
            {
                await memberDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/Member
        [HttpPost]
        public async Task<ActionResult<Models.Member>> PostMember(Models.Member member)
        {
            memberDbContext.Member.Add(member);
            await memberDbContext.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.MemberId }, member);
        }

        // DELETE: api/Member/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Member>> DeleteMember(Guid id)
        {
            var member = await memberDbContext.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            memberDbContext.Member.Remove(member);
            await memberDbContext.SaveChangesAsync();

            return member;
        }

        private bool MemberExists(Guid id)
        {
            return memberDbContext.Member.Any(e => e.MemberId == id);
        }
    }
}
