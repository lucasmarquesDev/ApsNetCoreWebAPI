using System.Collections;
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
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repos;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repos.GetAllProfessors(true);
            var professoresDTO = _mapper.Map<IEnumerable<ProfessorDTO>>(professores);

            return Ok(professoresDTO);
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDTO());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repos.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            var professorDTO = _mapper.Map<ProfessorDTO>(professor);

            return Ok(professorDTO);
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDTO model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repos.add(professor);

            if (_repos.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDTO>(professor));

            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDTO model)
        {
            var professor = _repos.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, professor);

            _repos.update(professor);
            if (_repos.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDTO>(professor));

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDTO model)
        {
            var professor = _repos.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, professor);

            _repos.update(professor);
            if (_repos.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDTO>(professor));

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