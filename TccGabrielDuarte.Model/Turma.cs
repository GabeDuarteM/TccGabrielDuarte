using System.Collections.Generic;

namespace TccGabrielDuarte.Model
{
    public class Turma : IEntityBase
    {
        public int Id { get; set; }
        public virtual ICollection<Disciplina> Disciplinas { get; set; }
        public int Semestre { get; set; }
        public int Ano { get; set; }
        public string Professor { get; set; }
    }
}
