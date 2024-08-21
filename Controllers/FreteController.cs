

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProvaPratica.Context;
using ProvaPratica.Models;

namespace ProvaPratica.Controllers
{
    public class FreteController : Controller
    {
        private readonly FreteContext _context;

        public FreteController(FreteContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var fretes = _context.Fretes.Include(f => f.Veiculo).Include(f => f.Usuario).ToList();
            return View(fretes);
        }
        

        public IActionResult Criar()
        {
            ViewBag.Veiculos = new SelectList(_context.Veiculos, "Id", "TipoVeiculo");
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "Id", "Nome");
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Frete frete)
        {
            if (ModelState.IsValid)
            {
                var veiculo = _context.Veiculos.Find(frete.VeiculoId);
                frete.ValorFrete = CalcularFrete(frete.Distancia, veiculo.Peso);
                frete.TaxaFrete = CalcularTaxa(frete.Distancia);
                frete.ValorTotal = frete.ValorFrete - frete.TaxaFrete;

                _context.Fretes.Add(frete);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(frete);
        }

        private double CalcularFrete(double distancia, int peso)
        {
            return distancia * peso;
        }

        private double CalcularTaxa(double distancia)
        {
            if (distancia <= 100)
                return distancia * 0.20;
            else if (distancia <= 200)
                return distancia * 0.15;
            else if (distancia <= 500)
                return distancia * 0.10;
            else
                return distancia * 0.075;
        }
    }
}
