using System.ComponentModel.DataAnnotations;

namespace ProvaPratica.Models
{
    public class Frete
    {
        public int Id { get; set; }

        [Required]
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [Required]
        public double Distancia { get; set; }

        public double ValorFrete { get; set; }
        public double TaxaFrete { get; set; }
        public double ValorTotal { get; set; }
        public StatusFrete Status { get; set; }
    }

   }
