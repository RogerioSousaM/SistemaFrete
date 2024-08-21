using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProvaPratica.Models;

namespace ProvaPratica.Models
{
    public class Login
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatorio.")]
        public string Usuario {get; set;}

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatorio.")]
        public string Senha { get;   set;}
        
        [Required]
        [Display(Name = "Lembrar de mim")]
        public bool Lembrar { get; set;}

    }
}