using AplicacaoEscola.Models;
using AplicacaoEscola.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AplicacaoEscola.Controllers
{
    [ApiController]
    [Route("professores")]
    public class ProfessorController : Controller
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorController(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Professor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Professor> professores = await _professorRepository.GetProfessoresAsync();

            if (professores.Any())
            {
                return Ok(professores);
            }

            return NotFound();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Professor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            Professor professor = await _professorRepository.GetProfessorAsync(id);

            if (professor == null)
            {
                return NotFound();
            }

            return Ok(professor);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Professor), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Professor professor)
        {
            var result = await _professorRepository.SaveProfessorAsync(professor);

            if(result > 0)
            {
                return CreatedAtAction(nameof(Get), professor);
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Professor), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] Professor professor)
        {
            var result = await _professorRepository.UpdateProfessorAsync(id, professor);

            if (result > 0) return NoContent();

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(Professor), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _professorRepository.DeleteProfessorAsync(id);

            if (result > 0) return NoContent();

            return NotFound();
        }
    }
}
