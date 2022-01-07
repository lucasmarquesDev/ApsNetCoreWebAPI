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
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _context.Professors;
            return Ok(professores);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var professores = _context.Professors.FirstOrDefault(x => x.Id == id);
            if(professores == null) return BadRequest("Professor não encontrado");

            return Ok(professores);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome)
        {
            var professores = _context.Professors.FirstOrDefault(x => x.Nome.Contains(nome));
            if (professores == null) return BadRequest("Professor não encontrado");

            return Ok(professores);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Professors.Add(professor);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor _professor)
        {
            var professor = _context.Professors.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (professor == null) return BadRequest("Professor não encontrado");   

            _context.Professors.Update(_professor);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor _professor)
        {
            var professor = _context.Professors.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (professor == null) return BadRequest("Professor não encontrado");

            _context.Professors.Update(_professor);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professors.FirstOrDefault(x => x.Id == id);
            if (professor == null) return BadRequest("Aluno não encontrado");

            _context.Professors.Remove(professor);
            _context.SaveChanges();

            return Ok();
        }
    }
}