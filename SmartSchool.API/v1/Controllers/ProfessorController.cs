using System.Collections;
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
    /// Versão 1 do controllador Professor
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repos;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repos"></param>
        /// <param name="mapper"></param>
        public ProfessorController(IRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repos.GetAllProfessors(true);
            var professoresDTO = _mapper.Map<IEnumerable<ProfessorDTO>>(professores);

            return Ok(professoresDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDTO());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repos.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            var professorDTO = _mapper.Map<ProfessorDTO>(professor);

            return Ok(professorDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDTO model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repos.add(professor);

            if (_repos.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDTO>(professor));

            return BadRequest("Professor não cadastrado");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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