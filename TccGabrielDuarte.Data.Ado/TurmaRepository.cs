using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Ado
{
    public class TurmaRepository : EntityBaseRepository<Turma>, ITurmaRepository
    {
        public TurmaRepository(TccContextADO context) : base(context) { }
    }
}
