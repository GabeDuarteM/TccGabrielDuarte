using System.Collections.Generic;

namespace TccGabrielDuarte.Model
{
    public class Disciplina : IEntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CodDisciplina { get; set; }
        public int Creditos { get; set; }
        public virtual ICollection<CursoDisciplina> CursoDisciplinas { get; set; }
    }
}