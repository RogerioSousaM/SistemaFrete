using System.ComponentModel.DataAnnotations;
using ProvaPratica.Models;


namespace ProvaPratica.Models
{  
    public class Usuario
    {
        public string Id { get; set; }
        
        [Display(Name = "Nome de Usuário")]
        [Required(ErrorMessage = "O nome é obrigatorio")]
        public string Nome { get; set; }

        
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]  
        [Required(ErrorMessage = "O e-mail é obrigatorio")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(16, ErrorMessage = "O tamanho máximo do campo {0} é de {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O tamanho minímo do campo {0} é de {1} caracteres.")]
        public string Senha { get; set; }

        [Display(Name ="Confirmação de senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MaxLength(6, ErrorMessage = "O tamanho mínimo do campo {0} é de {1} caracteres.")]
        [Compare(nameof(Senha), ErrorMessage = "A conirmação da senha não confere")]
        public string ConfirmarSenha { get; set; }

        
    }


} 