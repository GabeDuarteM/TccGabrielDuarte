using System;
using System.Collections.Generic;
using System.Text;

namespace TccGabrielDuarte.Model
{
    public class Curso : IEntityBase
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Sigla { get; set; }
        public virtual ICollection<Aluno> Alunos { get; set; }
        public virtual ICollection<CursoDisciplina> CursoDisciplinas { get; set; }
    }
}
