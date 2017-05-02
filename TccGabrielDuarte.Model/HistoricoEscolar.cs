using System;
using System.Collections.Generic;
using System.Text;

namespace TccGabrielDuarte.Model
{
    public class HistoricoEscolar : IEntityBase
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }
        public int TurmaId { get; set; }
        public virtual Turma Turma { get; set; }
        public int Media { get; set; }
    }
}
