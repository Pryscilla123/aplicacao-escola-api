using AplicacaoEscola.Models;
using AplicacaoEscola.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AplicacaoEscola.Controllers
{
    [ApiController]
    public class AlunoMateriaController : Controller
    {
        private readonly IAlunoMateriaRepository _alunoMateriaRepository;

        public AlunoMateriaController(IAlunoMateriaRepository alunoMateriaRepository)
        {
            _alunoMateriaRepository = alunoMateriaRepository;
        }

        [HttpGet("alunos/materias")]
        [ProducesResponseType(typeof(AlunoMateria), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<AlunoMateria> alunosMaterias = await _alunoMateriaRepository.GetAlunosMaterias();

            if (alunosMaterias.Any()) return Ok(alunosMaterias);

            return BadRequest();
        }

        [HttpGet("aluno/{id:int}/materias")]
        [ProducesResponseType(typeof(AlunoMateria), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            IEnumerable<AlunoMateria> alunoMaterias = await _alunoMateriaRepository.GetAlunoMaterias(id);

            if (alunoMaterias.Any()) return Ok(alunoMaterias);

            return NotFound();
        }

        [HttpPost("matricular/aluno/{idAluno:int}/materia/{idMateria:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(int idAluno, int idMateria)
        {
            var result = await _alunoMateriaRepository.SaveAlunoMateriaAsync(idAluno, idMateria);

            if (result > 0) return CreatedAtAction(nameof(Get), idAluno);

            return BadRequest();
        }

        [HttpDelete("remover/aluno/{idAluno:int}/materia/{idMateria:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int idAluno, int idMateria)
        {
            var result = await _alunoMateriaRepository.DeleteAlunoMateriaAsync(idAluno, idMateria);

            if(result > 0) return NoContent();

            return NotFound();
        }
    }
}
