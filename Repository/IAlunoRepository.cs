using AplicacaoEscola.Models;

namespace AplicacaoEscola.Repository
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> GetAlunosAsync();
        Task<Aluno> GetAlunoByIdAsync(int id);
        Task<int> SaveAlunoAsync(Aluno aluno);
        Task<int> UpdateAlunoAsync(int id, Aluno aluno);
        Task<int> DeleteAlunoAsync(int id);
    }
}
