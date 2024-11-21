using ClearVolt.Domain.Models;
using ClearVolt.DTO.Pessoa;
using ClearVolt.DTO.Role;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClearVolt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaInterface _pessoaInterface;

        public PessoaController(IPessoaInterface pessoaInterface)
        {
            _pessoaInterface = pessoaInterface;
        }

        [HttpGet("ListarPessoa")]
        public async Task<ActionResult<RespostaModel<List<PessoaModel>>>> ListarPessoa()
        {
            var pessoa = await _pessoaInterface.ListarPessoa();
            return Ok(pessoa);
        }

        [HttpGet("ListarPessoaPorId/{id_pessoa}")]
        public async Task<ActionResult<RespostaModel<List<PessoaModel>>>> ListarPessoaPorId(int id_pessoa)
        {
            var pessoa = await _pessoaInterface.ListarPessoaPorId(id_pessoa);
            return Ok(pessoa);
        }

        [HttpPost("CriarPessoa")]
        public async Task<ActionResult<RespostaModel<PessoaModel>>> CriarPessoa(PessoaCreateDto pessoaCreateDto)
        {
            var pessoa = await _pessoaInterface.CriarPessoa(pessoaCreateDto);
            return Ok(pessoa);
        }

        [HttpPut("EditarPessoa/{id_pessoa}")]
        public async Task<ActionResult<RespostaModel<PessoaModel>>> EditarPessoa(int id_pessoa, [FromBody] PessoaEditDto pessoaEditDto)
        {
            if (id_pessoa != pessoaEditDto.id_pessoa)
            {
                return BadRequest("Id na URL e no corpo são diferentes");
            }

            var pessoa = await _pessoaInterface.EditarPessoa(pessoaEditDto);

            if (pessoa.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("DeletarPessoa/{id_pessoa}")]
        public async Task<ActionResult<RespostaModel<PessoaModel>>> DeletarPessoa(int id_pessoa)
        {
            var pessoa = await _pessoaInterface.DeletarPessoa(id_pessoa);

            if (pessoa.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
