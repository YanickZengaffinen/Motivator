using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Motivator.Pages.Todos
{
    [Authorize]
    public class OverviewModel : PageModel
    {

    }
}