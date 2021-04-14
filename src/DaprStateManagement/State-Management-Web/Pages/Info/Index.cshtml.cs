using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace State_Management_Web.Pages.Info
{
    public class IndexPageModel : PageModel
    {
        private readonly ILogger<IndexPageModel> logger;

        public IndexPageModel(ILogger<IndexPageModel> logger)
        {
            this.logger = logger;
        }

        public void OnGet()
        {
            logger.LogInformation("Loaded info page");
        }
    }
}