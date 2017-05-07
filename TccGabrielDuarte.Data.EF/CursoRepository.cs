using System;
using System.Collections.Generic;
using System.Text;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.EF
{
    public class CursoRepository : EntityBaseRepository<Curso>
    {
        public CursoRepository(TccContext context) : base(context) { }
    }
}
