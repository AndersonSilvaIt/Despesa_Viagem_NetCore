using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DespViagem.UI.Controllers
{
    [Authorize]
    public class DespesaController : Controller
    {
        public IActionResult Inicial()
        {
            return View();
        }
    }
}