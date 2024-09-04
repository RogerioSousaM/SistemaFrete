
using Microsoft.AspNetCore.Mvc;
using ProvaPratica.Context;
using ProvaPratica.Models;
using System.Linq;
using System.Threading.Tasks;

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
           
            var usuarioExistente = _context.Users.FirstOrDefault(u => u.Email == usuario.Email);
            if (usuarioExistente != null)
            {
                ModelState.AddModelError("Email", "Este e-mail já está em uso.");
            }

            if (ModelState.IsValid)
            {
                _context.Users.Add(usuario);
                _context.SaveChanges();
                
                TempData["SuccessMessage"] = "Usuário criado com sucesso!";
                return RedirectToAction("Login", "Usuario");
            }

            return View(usuario);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Users
                    .FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);

                if (usuario != null)
                {
                    TempData["SuccessMessage"] = "Login realizado com sucesso!";
                    return RedirectToAction("Index", "Frete");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                }
            }

            return View(login);
        }
    }
}
