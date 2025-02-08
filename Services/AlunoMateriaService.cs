
using AplicacaoEscola.Models;
using AplicacaoEscola.Repository;

namespace AplicacaoEscola.Services
{
    public class AlunoMateriaService : IAlunoMateriaService
    {
        private readonly IAlunoMateriaRepository _alunoMateriaRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMateriaRepository _materiaRepository;

        public AlunoMateriaService(
            IAlunoMateriaRepository alunoMateriaRepository, 
            IAlunoRepository alunoRepository, 
            IMateriaRepository materiaRepository
            )
        {
            _alunoMateriaRepository = alunoMateriaRepository;
            _alunoRepository = alunoRepository;
            _materiaRepository = materiaRepository;
        }

        public async Task<int> CadastrarAlunoMateria(int alunoId, int materiaId)
        {
            try
            {
                Aluno aluno = await _alunoRepository.GetAlunoByIdAsync(alunoId);
                Materia materia = await _materiaRepository.GetMateriaByIdAsync(materiaId);

                if (aluno.Periodo < materia.Periodo) throw new InvalidOperationException("Aluno não está no mesmo período da matéria.");

                int alunosMatriculados = await _alunoMateriaRepository.GetCountAlunosByMateria(materiaId);

                if (materia.NumeroVagasTotal == alunosMatriculados) throw new InvalidOperationException("Número de vagas para matéria já preenchidos.");

                var result = await _alunoMateriaRepository.SaveAlunoMateriaAsync(alunoId, materiaId);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<AlunoMateria>> MostrarAlunosMaterias()
        {
            try
            {
                IEnumerable<AlunoMateria> alunosMaterias = await _alunoMateriaRepository.GetAlunosMaterias();

                return alunosMaterias;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<AlunoMateria>> MostrarMateriasAluno(int alunoId)
        {
            try
            {
                IEnumerable<AlunoMateria> alunoMaterias = await _alunoMateriaRepository.GetAlunoMaterias(alunoId);

                return alunoMaterias;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<int> RemoverAlunoMateria(int alunoId, int materiaId)
        {
            try
            {
                var result = await _alunoMateriaRepository.DeleteAlunoMateriaAsync(alunoId, materiaId);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
