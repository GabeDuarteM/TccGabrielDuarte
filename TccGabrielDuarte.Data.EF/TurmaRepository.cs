using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.EF
{
    public class TurmaRepository : EntityBaseRepository<Turma>
    {
        public TurmaRepository(TccContext context) : base(context) { }
    }
}
