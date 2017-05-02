using System.Collections.Generic;

namespace TccGabrielDuarte.Model
{
    public class Aluno : IEntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int TipoAluno { get; set; }
        public virtual Curso Curso { get; set; }
    }
}
