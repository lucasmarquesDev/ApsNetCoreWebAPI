using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;
using SmartSchool.WebAPI.Data;

namespace SmartSchool.API.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;

        public Repository(SmartContext context)
        {
            _context = context;
        }

        public void add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(x => x.AlunosDisciplinas)
                             .ThenInclude(y => y.Disciplina)
                             .ThenInclude(z => z.Professor);
            }

            query = query.AsNoTracking().OrderBy(x => x.Id);

            if (!string.IsNullOrEmpty(pageParams.Nome))
            {
                query = query.Where(x => x.Nome.ToUpper().Contains(pageParams.Nome.ToUpper()) ||
                                         x.Sobrenome.ToUpper().Contains(pageParams.Nome.ToUpper()));
            }

            if (pageParams.Matricula > 0)
                query = query.Where(x => x.Matricula == pageParams.Matricula);

            if (pageParams.Ativo != null)
                query = query.Where(x => x.Ativo == (pageParams.Ativo != 0));

            //return await query.ToListAsync();

            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(x => x.AlunosDisciplinas)
                             .ThenInclude(y => y.Disciplina)
                             .ThenInclude(z => z.Professor);
            }

            query = query.AsNoTracking().OrderBy(x => x.Id);

            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int id, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(x => x.AlunosDisciplinas)
                             .ThenInclude(y => y.Disciplina)
                             .ThenInclude(z => z.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(x => x.Id)
                         .Where(s => s.AlunosDisciplinas.Any(a => a.DisciplinaId == id));

            return query.ToArray();
        }

        public Aluno GetAlunoById(int id, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(x => x.AlunosDisciplinas)
                             .ThenInclude(y => y.Disciplina)
                             .ThenInclude(z => z.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(x => x.Id)
                         .Where(s => s.Id == id);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessors(bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query.Include(x => x.Disciplinas)
                             .ThenInclude(y => y.AlunosDisciplinas)
                             .ThenInclude(z => z.Aluno);
            }

            return query.ToArray();
        }

        public Professor[] GetAllProfessorByDisciplinaId(int id, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query.Include(x => x.Disciplinas)
                             .ThenInclude(y => y.AlunosDisciplinas)
                             .ThenInclude(z => z.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(x => x.Id)
                         .Where(y => y.Disciplinas.Any(dis => dis.Id == id));

            // query = query.AsNoTracking()
            // .OrderBy(x => x.Id)
            // .Where(y => y.Disciplinas.Any(dis => dis.AlunosDisciplinas.Any(y => y.DisciplinaId == id)));

            return query.ToArray();
        }

        public Professor GetProfessorById(int id, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query.Include(x => x.Disciplinas)
                             .ThenInclude(y => y.AlunosDisciplinas)
                             .ThenInclude(z => z.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(x => x.Id)
                         .Where(y => y.Id == id);

            return query.FirstOrDefault();
        }
    }
}