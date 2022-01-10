using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repos;
        public readonly IMapper _mapper;

        public AlunoController(IRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repos.GetAllAlunos(true);
            var alunosDTO = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);

            return Ok(alunosDTO);
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDTO());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repos.GetAlunoById(id);

            if (aluno == null) return BadRequest("O aluno não foi encontrado");

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            return Ok(alunoDTO);
        }

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDTO model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repos.add(aluno);

            if (_repos.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDTO>(aluno));

            return BadRequest("Aluno não cadastrado");
        }

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

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDTO model)
        {
            var aluno = _repos.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repos.update(aluno);

            if (_repos.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDTO>(aluno));

            return BadRequest("Aluno não atualizado");
        }

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