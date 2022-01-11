using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.v1.Dtos;
using SmartSchool.API.Models;

namespace SmartSchool.API.v1.Controllers
{
    /// <summary>
    /// Versão 1 do controllador Alunos
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repos;
        public readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repos"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }

        /// <summary>
        /// Método Responsavel Por Retornar Todos Os Alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repos.GetAllAlunos(true);
            var alunosDTO = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);

            return Ok(alunosDTO);
        }

        /// <summary>
        /// Método Responsavel Por Retornar Um Unico Aluno Por Meio Do Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repos.GetAlunoById(id);

            if (aluno == null) return BadRequest("O aluno não foi encontrado");

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            return Ok(alunoDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDTO model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repos.add(aluno);

            if (_repos.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDTO>(aluno));

            return BadRequest("Aluno não cadastrado");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDTO model)
        {
            var aluno = _repos.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repos.update(aluno);

            if (_repos.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDTO>(aluno));

            return BadRequest("Aluno não atualizado");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repos.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repos.Delete(aluno);

            if (_repos.SaveChanges()) return Ok("Aluno deletado");

            return BadRequest("Aluno não deletado");
        }
    }
}