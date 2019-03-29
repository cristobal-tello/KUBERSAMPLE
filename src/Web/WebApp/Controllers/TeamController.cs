using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.Clients.Interfaces;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamClient teamClient;
        public TeamController(ITeamClient teamClient)
        {
            this.teamClient = teamClient;
        }

        
        // GET: Team
        public async Task<IActionResult> Index()
        {
            return View(await teamClient.GetTeamsAsync());
        }

        // GET: Team/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            return await this.Details(id);
        }
        
        // GET: Team/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await teamClient.GetTeamAsync(id);
                
            if (team == null)
            {
                return NotFound();
            }

            return View(new TeamViewModel() { TeamId = team.TeamId, Name=team.Name });
        }
        
        // GET: Team/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,Name")] TeamViewModel teamViewModel)
        {
            if (ModelState.IsValid)
            {
                await teamClient.CreateTeamAsync(new Team()
                {
                    TeamId = Guid.NewGuid(),
                    Name = teamViewModel.Name
                });

                return RedirectToAction(nameof(Index));
            }
            return View(teamViewModel);
        }
        
        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TeamId,Name")] TeamViewModel teamViewModel)
        {
            if (id != teamViewModel.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await teamClient.UpdateTeamAsync(new Team()
                {
                    TeamId = teamViewModel.TeamId,
                    Name = teamViewModel.Name
                });

                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        // GET: Team/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await teamClient.DeleteTeamAsync(id);


            return RedirectToAction(nameof(Index));
        }
    }
}
