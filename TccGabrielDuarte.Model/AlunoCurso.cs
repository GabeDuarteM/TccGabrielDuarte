using System;
using System.Collections.Generic;
using System.Text;

namespace TccGabrielDuarte.Model
{
    public class AlunoCurso
    {
        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
    }
}
