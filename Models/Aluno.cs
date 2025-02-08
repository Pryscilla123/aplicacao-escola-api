using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AplicacaoEscola.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Idade { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Periodo { get; set; }
    }
}
