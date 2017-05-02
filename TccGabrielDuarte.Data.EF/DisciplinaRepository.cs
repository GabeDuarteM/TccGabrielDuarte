using System;
using System.Collections.Generic;
using System.Text;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.EF
{
    public class DisciplinaRepository : EntityBaseRepository<Disciplina>
    {
        public DisciplinaRepository(TccContext context) : base(context) { }
    }
}
