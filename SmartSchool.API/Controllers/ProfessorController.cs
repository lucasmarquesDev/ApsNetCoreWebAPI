using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repos;

        public ProfessorController(IRepository repos)
        {
            _repos = repos;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repos.GetAllProfessors(true));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professores = _repos.GetProfessorById(id);
            if (professores == null) return BadRequest("Professor não encontrado");

            return Ok(professores);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repos.add(professor);

            if (_repos.SaveChanges()) return Ok(professor);

            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor _professor)
        {
            var professor = _repos.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            _repos.update(_professor);
            if (_repos.SaveChanges()) return Ok();

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor _professor)
        {
            var professor = _repos.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            _repos.update(_professor);
            if (_repos.SaveChanges()) return Ok();

            return BadRequest("Professor não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repos.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            _repos.Delete(professor);
            if (_repos.SaveChanges()) return Ok();

            return BadRequest("Professor não deletado");
        }
    }
}