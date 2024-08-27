using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProvaPratica.Context;
using ProvaPratica.Models;
using System.Threading.Tasks;

namespace ProvaPratica.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly FreteContext _context;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioController(FreteContext context, SignInManager<Usuario> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
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
            var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.Email == usuario.Email);
            if (usuarioExistente != null)
            {
                ModelState.AddModelError("Email", "Este e-mail já está em uso.");
            }
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
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
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, login.Lembrar, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Frete");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                    return View(login);
                }
            }

            return View(login);
        }
    }
}
