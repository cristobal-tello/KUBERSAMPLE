using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IConfiguration configuration;

        public ConfigurationController(IConfiguration configuration)
        {
            this.configuration = configuration;   
        }

        
        // GET: Team
        public async Task<IActionResult> Index()
        {
            var teamClientUrl = this.configuration.GetSection("team:url").Value;
            var memberClientUrl = this.configuration.GetSection("member:url").Value;

            var configViewModel = new ConfigurationViewModel();

            configViewModel.Configuration.Add(new KeyValuePair<string, string>("team:url", teamClientUrl));
            configViewModel.Configuration.Add(new KeyValuePair<string, string>("member:url", teamClientUrl));

            return View(configViewModel);
        }
    }
}
