using System.ComponentModel.DataAnnotations;

namespace AplicacaoEscola.Models
{
    public class Materia
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        public int? ProfessorId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Periodo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int NumeroVagasTotal { get; set; }
    }
}
