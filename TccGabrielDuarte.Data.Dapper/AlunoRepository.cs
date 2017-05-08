using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Dapper
{
    public class AlunoRepository : EntityBaseRepository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(TccContextDapper context) : base(context) { }

    }
}
