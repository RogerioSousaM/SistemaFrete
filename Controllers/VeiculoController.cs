using Microsoft.AspNetCore.Mvc;
using ProvaPratica.Context;
using ProvaPratica.Models;
using System.Linq;

namespace ProvaPratica.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly FreteContext _context;

        public VeiculoController(FreteContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Veiculos.ToList());
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                if (_context.Veiculos.Any(v => v.TipoVeiculo == veiculo.TipoVeiculo))
                {
                    ModelState.AddModelError("", "Este tipo de veículo já está cadastrado.");
                    return View(veiculo);
                }

                _context.Veiculos.Add(veiculo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(veiculo);
        }

        public IActionResult Editar(int id)
        {
            var veiculo = _context.Veiculos.Find(id);
            if (veiculo == null)
                return NotFound();
            return View(veiculo);
        }

        [HttpPost]
        public IActionResult Editar(Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Veiculos.Update(veiculo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(veiculo);
        }

        public IActionResult Deletar(int id)
        {
            var veiculo = _context.Veiculos.Find(id);
            if (veiculo == null)
                return NotFound();

            _context.Veiculos.Remove(veiculo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
