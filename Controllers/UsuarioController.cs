using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProvaPratica.Context;
using ProvaPratica.Models;


namespace ProvaPratica.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly FreteContext _context;

        public SignInManager<IdentityUser> _signInManager { get; }

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
                else
                {
                    ModelState.AddModelError(string.Empty,
                        "Senha ou e-mail invalido. Tente novamente");
                    return View(login);
                }
            }
            else
            {
                return View(login);
            }
        }  

 [HttpGet]
    public IActionResult Criar()
    {
        return View();
    }

    
    [HttpPost]
    [Route("Usuario/Criar")]
    public async Task<IActionResult> Criar(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            var usuarioBanco = await _userManager.FindByIdAsync(id);
            if (usuarioBanco == null)
            {
                MostrarMensagem("Usuário não encontrado.", true);
                return RedirectToAction("Index", "Home");
            }
            var usuarioModel = new Usuario
            {
                Id = usuarioBanco.Id,
                Nome = usuarioBanco.UserName,
                Email = usuarioBanco.Email,
            };
            return View(usuarioModel);
        }
        return View(new Usuario());
    }

    
    [HttpPost]
    
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
                var usuarioBanco = await _userManager.FindByEmailAsync(usuarioModel.Id);
                if (usuarioModel.Email != usuarioBanco.Email && _userManager.Users.Any(u => u.NormalizedEmail == usuarioModel.Email.ToUpper().Trim()))
                {
                    ModelState.AddModelError("Email", "Já existe um usuário com este e-mail.");
                    return View(usuarioModel);
                }
                MapearCadastroUsuario(usuarioModel, usuarioBanco);

                var resultado = await _userManager.UpdateAsync(usuarioBanco);
                if (resultado.Succeeded)
                {
                    this.MostrarMensagem("Usuário alterado com sucesso.");
                    return RedirectToAction("Index");
                }
                else
                {
                    this.MostrarMensagem("Não foi possível alterar o usuário.", true);
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(usuarioModel);
                }
            }
            else
            {
                var usuarioBanco = await _userManager.FindByEmailAsync(usuarioModel.Email);
                if (usuarioBanco != null)
                {
                    ModelState.AddModelError("Email", "Já existe um usuário cadastrado com este e-mail.");
                    return View(usuarioBanco);
                }
                usuarioBanco = new IdentityUser();
                MapearCadastroUsuario(usuarioModel, usuarioBanco);

                var resultado = await _userManager.CreateAsync(usuarioBanco, usuarioModel.Senha);
                if (resultado.Succeeded)
                {
                    this.MostrarMensagem("Usuário cadastrado com sucesso. Use suas credenciais para entrar no sistema.");
                    return RedirectToAction("Login");
                }
                else
                {
                    this.MostrarMensagem("Erro ao cadastrar usuário.", true);
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(usuarioModel);
                }
            }
        }
        else
        {
            return View(usuarioModel);
        }
    }

        private void MapearCadastroUsuario(Usuario usuarioModel, IdentityUser usuarioBanco)
        {
            throw new NotImplementedException();
        }

        private bool UsuarioExistente(string id)
        {
            throw new NotImplementedException();
        }

        private void MostrarMensagem(string Mostrar)
        {
            throw new NotImplementedException();
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

        private void MostrarMensagem(string mensagem, bool isError)
        {
            
            TempData["Mensagem"] = mensagem;
            TempData["IsError"] = isError;
        }
    }
}