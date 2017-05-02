using System;
using System.Collections.Generic;
using System.Text;

namespace TccGabrielDuarte.Model
{
    public class CursoDisciplina : IEntityBase
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public virtual Curso Curso { get; set; }
        public int DisciplinaId { get; set; }
        public virtual Disciplina Disciplina { get; set; }
    }
}
