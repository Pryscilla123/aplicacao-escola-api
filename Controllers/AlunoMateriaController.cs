using AplicacaoEscola.Models;
using AplicacaoEscola.Repository;
using AplicacaoEscola.Services;
using Microsoft.AspNetCore.Mvc;

namespace AplicacaoEscola.Controllers
{
    [ApiController]
    public class AlunoMateriaController : Controller
    {
        private readonly IAlunoMateriaService _alunoMateriaService;

        public AlunoMateriaController(IAlunoMateriaService alunoMateriaService)
        {
            _alunoMateriaService = alunoMateriaService;
        }

        [HttpGet("alunos/materias")]
        [ProducesResponseType(typeof(AlunoMateria), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<AlunoMateria> alunosMaterias = await _alunoMateriaService.MostrarAlunosMaterias();

            if (alunosMaterias.Any()) return Ok(alunosMaterias);

            return BadRequest();
        }

        [HttpGet("aluno/{id:int}/materias")]
        [ProducesResponseType(typeof(AlunoMateria), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            IEnumerable<AlunoMateria> alunoMaterias = await _alunoMateriaService.MostrarMateriasAluno(id);

            if (alunoMaterias.Any()) return Ok(alunoMaterias);

            return NotFound();
        }

        [HttpPost("matricular/aluno/{idAluno:int}/materia/{idMateria:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(int idAluno, int idMateria)
        {
            var result = await _alunoMateriaService.CadastrarAlunoMateria(idAluno, idMateria);

            if (result > 0) return CreatedAtAction(nameof(Get), idAluno);

            return BadRequest();
        }

        [HttpDelete("remover/aluno/{idAluno:int}/materia/{idMateria:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int idAluno, int idMateria)
        {
            var result = await _alunoMateriaService.RemoverAlunoMateria(idAluno, idMateria);

            if(result > 0) return NoContent();

            return NotFound();
        }
    }
}
