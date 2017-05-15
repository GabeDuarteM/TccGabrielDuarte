using System;
using System.Collections.Generic;

namespace TccGabrielDuarte.Model
{
    public class Curso : IEntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public ICollection<AlunoCurso> AlunoCursos { get; set; }
        public ICollection<CursoDisciplina> CursoDisciplinas { get; set; }
    }
}