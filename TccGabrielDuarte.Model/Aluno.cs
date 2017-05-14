using System.Collections.Generic;

namespace TccGabrielDuarte.Model
{
    public class Aluno : IEntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Semestre { get; set; }
        public ICollection<AlunoCurso> AlunoCursos { get; set; }
    }
}
