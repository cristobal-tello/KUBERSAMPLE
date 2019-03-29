using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Clients.Interfaces;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class MemberController : Controller
    {
        private readonly ITeamClient teamClient;
        private readonly IMemberClient memberClient;
        public MemberController(ITeamClient teamClient, IMemberClient memberClient)
        {
            this.teamClient = teamClient;
            this.memberClient = memberClient;
        }

        // GET: Member
        public async Task<IActionResult> Index()
        {
            IEnumerable<Member> members = await memberClient.GetMembersAsync();
            /* IEnumerable<Team> teams = await teamClient.GetTeamsAsync();
             var memberViewModelsList = from item in members
                                        select new MemberViewModel()
                                        {
                                            MemberId = item.MemberId,
                                            Name = item.Name,
                                            TeamId = item.TeamId,
                                            TeamName = teams.FirstOrDefault(t => t.TeamId == item.TeamId)?.Name ?? string.Empty
                                         };*/

            var memberViewModelsList = from item in members
                                       select new MemberViewModel()
                                       {
                                           MemberId = item.MemberId,
                                           Name = item.Name,
                                           TeamId = item.TeamId,
                                           TeamName = teamClient.GetTeamAsync(item.TeamId).Result?.Name ?? string.Empty
                                       };



            return View(memberViewModelsList);
        }

        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            return await this.Details(id);
        }

        // GET: Member/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await memberClient.GetMemberAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            var team = await teamClient.GetTeamAsync(member.TeamId);

            return View(new MemberViewModel() { MemberId = member.MemberId, Name = member.Name, TeamId = member.TeamId, TeamName=team.Name });
        }

        // GET: Member/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,Name")] MemberViewModel memberViewModel)
        {
            if (ModelState.IsValid)
            {
                await memberClient.CreateMemberAsync(new Member()
                {
                    MemberId = Guid.NewGuid(),
                    Name = memberViewModel.Name,
                    TeamId = memberViewModel.TeamId
                });

                return RedirectToAction(nameof(Index));
            }
            return View(memberViewModel);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MemberId,Name,TeamId,TeamName")] MemberViewModel memberViewModel)
        {
            if (id != memberViewModel.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await memberClient.UpdateMemberAsync(new Member()
                {
                    MemberId = memberViewModel.MemberId,
                    Name = memberViewModel.Name,
                    TeamId = memberViewModel.TeamId
                });

                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        // GET: Member/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await memberClient.DeleteMemberAsync(id);


            return RedirectToAction(nameof(Index));
        }
    }
}
