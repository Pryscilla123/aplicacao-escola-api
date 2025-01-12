using AplicacaoEscola.Models;
using AplicacaoEscola.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AplicacaoEscola.Controllers
{
    [ApiController]
    [Route("aluno")]
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

            if(alunos.Count > 0)
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
            var registrado = await _alunoRepository.SaveAsync(aluno);

            if(registrado > 0)
            {
                return CreatedAtAction(nameof(Get), aluno);
            }
            return BadRequest();
        }

    }
}
