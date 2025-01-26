using AplicacaoEscola.Models;

namespace AplicacaoEscola.Repository
{
    public interface IAlunoMateriaRepository
    {
        Task<IEnumerable<AlunoMateria>> GetAlunosMaterias();
        Task<IEnumerable<AlunoMateria>> GetAlunoMaterias(int id);
        Task<int> SaveAlunoMateriaAsync(int alunoId, int materiaId);
        Task<int> DeleteAlunoMateriaAsync(int alunoId, int materiaId);
    }
}
