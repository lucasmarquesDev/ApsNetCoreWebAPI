using System;

namespace SmartSchool.API.v1.Dtos
{
    public class ProfessorDTO
    {
        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int TempoDeEmpresa { get; set; }
        public bool Ativo { get; set; }
    }
}