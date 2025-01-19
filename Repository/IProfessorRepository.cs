using AplicacaoEscola.Models;

namespace AplicacaoEscola.Repository
{
    public interface IProfessorRepository
    {
        Task<IEnumerable<Professor>> GetProfessoresAsync();

        Task<Professor> GetProfessorAsync(int id);

        Task<int> SaveProfessorAsync(Professor professor);

        Task<int> UpdateProfessorAsync(int id, Professor professor);

        Task<int> DeleteProfessorAsync(int id);
    }
}
