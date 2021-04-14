using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using State_Management_Models;
using State_Management_Web.Interfaces;

namespace State_Management_Web.Pages.Info
{
    public class IndexPageModel : PageModel
    {
        private readonly ILogger<IndexPageModel> logger;
        private readonly IStateApi stateApi;

        public IndexPageModel(ILogger<IndexPageModel> logger, IStateApi stateApi)
        {
            this.logger = logger;
            this.stateApi = stateApi;
        }

        public async Task OnGet(string email)
        {
            logger.LogInformation("Loaded info page");
            if (!string.IsNullOrEmpty(email))
            {
                logger.LogInformation($"Getting person by email {email}");
                RetrievedPerson = await stateApi.GetPersonAsync(email);
                logger.LogInformation($"Person retrieved {RetrievedPerson.FullName}");
                Message = "";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            logger.LogInformation("Adding person to the API");
            try
            {
                await stateApi.SavePersonAsync(CurrentPerson);
                Message = $"Person {CurrentPerson.FullName} has been added!";
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                Message = "There has been an error! Check logs";
            }

            return RedirectToPage("/Info/Index", new {email = CurrentPerson.Email});
        }

        [TempData] public string Message { get; set; }
        [BindProperty] public Person CurrentPerson { get; set; } = new();
        [BindProperty] public Person RetrievedPerson { get; set; } = new();
        [BindProperty(SupportsGet = true)] public string Email { get; set; }
    }
}