using System.ComponentModel.DataAnnotations;

namespace ProvaPratica.Models
{
    public class Login
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string Senha { get; set; }

        [Required]
        [Display(Name = "Lembrar de mim")]
        public bool Lembrar { get; set; }
    }
}