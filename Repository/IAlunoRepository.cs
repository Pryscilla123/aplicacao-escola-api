using AplicacaoEscola.Models;

namespace AplicacaoEscola.Repository
{
    public interface IAlunoRepository
    {
        Task<List<Aluno>> GetAlunosAsync();
        Task<Aluno> GetAlunoByIdAsync(int id);
        Task<int> SaveAsync(Aluno aluno);
        /*Task<int> UpdateAsync(int id, Aluno aluno);
        Task<int> DeleteAsync(int id);*/
    }
}
