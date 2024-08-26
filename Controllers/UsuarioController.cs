using Microsoft.AspNetCore.Mvc;
using ProvaPratica.Context;
using ProvaPratica.Models;
using Microsoft.AspNetCore.Authentication;

namespace ProvaPratica.Controllers
{
    public class UsuarioController : Controller
    {
       
        private readonly FreteContext _context;

        public UsuarioController(FreteContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }
    
        
        [HttpPost]
        public IActionResult Criar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }
            public IActionResult Login()
            {
                return View();
            }
        }
}
     