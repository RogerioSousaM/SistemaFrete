using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProvaPratica.Context;
using ProvaPratica.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaPratica.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly FreteContext _context;

        public UsuarioController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, FreteContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] Login login)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(login.Usuario, login.Senha, login.Lembrar, false);
                if (resultado.Succeeded)
                {
                    login.ReturnURL = login.ReturnURL ?? "~/";
                    return LocalRedirect(login.ReturnURL);
                }
                ModelState.AddModelError(string.Empty, "Senha ou e-mail inválido. Tente novamente.");
            }
            return View(login);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("Usuario/Criar")]
        public async Task<IActionResult> Criar([FromForm] Usuario usuarioModel)
        {
            if (!string.IsNullOrEmpty(usuarioModel.Id))
            {
                ModelState.Remove("Senha");
                ModelState.Remove("ConfirmarSenha");
            }

            if (ModelState.IsValid)
            {
                if (UsuarioExistente(usuarioModel.Id))
                {
                    var usuarioBanco = await _userManager.FindByEmailAsync(usuarioModel.Email);
                    if (usuarioBanco != null && usuarioModel.Email != usuarioBanco.Email)
                    {
                        ModelState.AddModelError("Email", "Já existe um usuário cadastrado com este e-mail.");
                        return View(usuarioModel);
                    }
                    MapearCadastroUsuario(usuarioModel, usuarioBanco);
                    var resultado = await _userManager.UpdateAsync(usuarioBanco);
                    if (resultado.Succeeded)
                    {
                        MostrarMensagem("Usuário alterado com sucesso.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        MostrarMensagem("Não foi possível alterar o usuário.", true);
                        foreach (var error in resultado.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    var usuarioBanco = await _userManager.FindByEmailAsync(usuarioModel.Email);
                    if (usuarioBanco != null)
                    {
                        ModelState.AddModelError("Email", "Já existe um usuário cadastrado com este e-mail.");
                        return View(usuarioModel);
                    }
                    usuarioBanco = new IdentityUser();
                    MapearCadastroUsuario(usuarioModel, usuarioBanco);
                    var resultado = await _userManager.CreateAsync(usuarioBanco, usuarioModel.Senha);
                    if (resultado.Succeeded)
                    {
                        MostrarMensagem("Usuário cadastrado com sucesso. Use suas credenciais para entrar no sistema.");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        MostrarMensagem("Erro ao cadastrar usuário.", true);
                        foreach (var error in resultado.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(usuarioModel);
        }

        private void MapearCadastroUsuario(Usuario usuarioModel, IdentityUser usuarioBanco)
        {
            usuarioBanco.UserName = usuarioModel.Nome;
            usuarioBanco.Email = usuarioModel.Email;
        }

        private bool UsuarioExistente(string id)
        {
            return _userManager.Users.Any(u => u.Id == id);
        }

        private void MostrarMensagem(string mensagem, bool isError = false)
        {
            TempData["Mensagem"] = mensagem;
            TempData["IsError"] = isError;
        }

        public async Task<IActionResult> Editar(string id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public async Task<IActionResult> Deletar(string id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
