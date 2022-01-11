using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
        void add<T>(T entity) where T : class;
        void update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        //ALUNOS
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);
        Aluno[] GetAllAlunos(bool includeProfessor = false);
        Aluno[] GetAllAlunosByDisciplinaId(int id, bool includeProfessor = false);
        Aluno GetAlunoById(int id, bool includeProfessor = false);
        //PROFESSORES
        Professor[] GetAllProfessors(bool includeAlunos = false);
        Professor[] GetAllProfessorByDisciplinaId(int id, bool includeAlunos = false);
        Professor GetProfessorById(int id, bool includeAlunos = false);
    }
}