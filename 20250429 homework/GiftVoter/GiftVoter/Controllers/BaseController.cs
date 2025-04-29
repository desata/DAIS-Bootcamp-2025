using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftVoter.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
