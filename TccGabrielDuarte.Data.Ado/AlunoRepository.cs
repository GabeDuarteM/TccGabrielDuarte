using System;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Ado
{
    public class AlunoRepository : EntityBaseRepository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(TccContextADO context) : base(context) { }
    }
}
