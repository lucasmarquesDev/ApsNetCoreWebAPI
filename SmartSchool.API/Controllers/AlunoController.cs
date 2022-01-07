using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repos;

        public AlunoController( IRepository repos)
        {
            _repos = repos;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repos.GetAllAlunos(true));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repos.GetAlunoById(id);

            if (aluno == null) return BadRequest("O aluno não foi encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repos.add(aluno);

            if(_repos.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repos.GetAlunoById(id);
            if (alu == null) return BadRequest("Aluno não encontrado");

            _repos.update(aluno);

            if(_repos.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repos.GetAlunoById(id);
            if (alu == null) return BadRequest("Aluno não encontrado");

            _repos.update(aluno);

            if(_repos.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repos.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repos.Delete(aluno);

            if(_repos.SaveChanges()) return Ok("Aluno deletado");

            return BadRequest("Aluno não deletado");
        }
    }
}