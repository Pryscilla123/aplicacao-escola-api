using AplicacaoEscola.Models;
using AplicacaoEscola.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AplicacaoEscola.Controllers
{
    [ApiController]
    [Route("materias")]
    public class MateriaController : Controller
    {
        private readonly IMateriaRepository _materiaRepository;

        public MateriaController(IMateriaRepository materiaRepository) 
        { 
            _materiaRepository = materiaRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Materia), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Materia> materias = await _materiaRepository.GetMeteriasAsync();

            if(materias.Any())
            {
                return Ok(materias);
            }

            return NotFound();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Materia), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            Materia materia = await _materiaRepository.GetMateriaByIdAsync(id);

            if(materia == null) return NotFound();

            return Ok(materia);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Materia), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Materia materia)
        {
            int result = await _materiaRepository.SaveMateriaAsync(materia);

            if (result > 0) return CreatedAtAction(nameof(Get), materia);

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Materia), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] Materia materia)
        {
            int result = await _materiaRepository.UpdateMateriaAsync(id, materia);

            if(result > 0) return NoContent();

            return BadRequest();
        }

        [HttpPatch("{id:int}/cadastra/{professorId:int}/professor")]
        [ProducesResponseType(typeof(Materia), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(int id, int professorId)
        {
            int result = await _materiaRepository.UpdateMateriaProfessorAsync(id, professorId);

            if(result > 0) return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(Materia), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            int result = await _materiaRepository.DeleteMateriaAsync(id);

            if(result > 0) return NoContent();

            return BadRequest();
        }
    }
}
