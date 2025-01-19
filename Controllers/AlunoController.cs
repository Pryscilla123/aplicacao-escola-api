using AplicacaoEscola.Models;
using AplicacaoEscola.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AplicacaoEscola.Controllers
{
    [ApiController]
    [Route("alunos")]
    public class AlunoController : Controller
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoController(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Aluno), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var alunos = await _alunoRepository.GetAlunosAsync();

            if(alunos.Any())
            {
                return Ok(alunos);
            }

            return NotFound();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Aluno), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            Aluno aluno = await _alunoRepository.GetAlunoByIdAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Aluno), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Aluno aluno)
        {
            var registrado = await _alunoRepository.SaveAlunoAsync(aluno);

            if(registrado > 0)
            {
                return CreatedAtAction(nameof(Get), aluno);
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Aluno), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] Aluno aluno)
        {
            //Perguntar porque
            //Perguntar qual a melhor forma de fazer um put
            //if(!await _alunoExists(id)) return NotFound();

            var result = await _alunoRepository.UpdateAlunoAsync(id, aluno);

            if(result > 0) return NoContent();

            return NotFound();

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(Aluno), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _alunoRepository.DeleteAlunoAsync(id);

            if(result > 0) return NoContent();

            return NotFound();
        }
    }
}
