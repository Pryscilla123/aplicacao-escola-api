using AplicacaoEscola.Models;

namespace AplicacaoEscola.Repository
{
    public interface IMateriaRepository
    {
        Task<IEnumerable<Materia>> GetMeteriasAsync();
        Task<Materia> GetMateriaByIdAsync(int id);
        Task<int> SaveMateriaAsync(Materia materia);
        Task<int> UpdateMateriaAsync(int id, Materia materia);
        Task<int> UpdateMateriaProfessorAsync(int materiaId, int professorId);
        Task<int> DeleteMateriaAsync(int id);
    }
}
