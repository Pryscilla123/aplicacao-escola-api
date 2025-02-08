using AplicacaoEscola.Models;

namespace AplicacaoEscola.Services
{
    public interface IAlunoMateriaService
    {
        Task<int> CadastrarAlunoMateria(int alunoId, int materiaId);
        Task<int> RemoverAlunoMateria(int alunoId, int materiaId);

        Task<IEnumerable<AlunoMateria>> MostrarAlunosMaterias();
        Task<IEnumerable<AlunoMateria>> MostrarMateriasAluno(int alunoId);
    }
}
