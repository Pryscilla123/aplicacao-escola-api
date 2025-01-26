using System.ComponentModel.DataAnnotations;

namespace AplicacaoEscola.Models
{
    public class AlunoMateria
    {
        public string NomeAluno { get; set; }
        public string NomeMateria { get; set; }
        public string NomeProfessor {  get; set; }
        public int Periodo { get; set; }
        public int Ano {  get; set; }
    }
}
