using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Dapper
{
    public class TurmaRepository : EntityBaseRepository<Turma>, ITurmaRepository
    {
        public TurmaRepository(TccContextDapper context) : base(context) { }
    }
}
