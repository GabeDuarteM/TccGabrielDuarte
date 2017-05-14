using System.Collections.Generic;

namespace TccGabrielDuarte.Model
{
    public class Turma : IEntityBase
    {
        public int Id { get; set; }
        public string Professor { get; set; }
        public Curso Curso { get; set; }
        public int CursoId { get; set; }
        public ICollection<Disciplina> Disciplinas { get; set; }
    }
}
