
using System.ComponentModel.DataAnnotations;

namespace ProvaPratica.Models
{
    public class Veiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O tipo do veículo é obrigatório")]
        public string TipoVeiculo { get; set; }

        [Required(ErrorMessage = "O peso do veículo é obrigatório")]
        public int Peso { get; set; }
        
    }

}