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
        public Aluno[] GetAllAlunos(bool includeProfessor = false);
        public Aluno[] GetAllAlunosByDisciplinaId(int id, bool includeProfessor = false);
        public Aluno GetAlunoById(int id, bool includeProfessor = false);
        //PROFESSORES
        public Professor[] GetAllProfessors(bool includeAlunos = false);
        public Professor[] GetAllProfessorByDisciplinaId(int id, bool includeAlunos = false);
        public Professor GetProfessorById(int id, bool includeAlunos = false);
    }
}