using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Dapper
{
    public class HistoricoEscolarRepository : EntityBaseRepository<HistoricoEscolar>, IHistoricoEscolarRepository
    {
        public HistoricoEscolarRepository(TccContextDapper context) : base(context) { }
    }
}
