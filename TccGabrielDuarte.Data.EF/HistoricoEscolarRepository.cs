using System;
using System.Collections.Generic;
using System.Text;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.EF
{
    public class HistoricoEscolarRepository : EntityBaseRepository<HistoricoEscolar>
    {
        public HistoricoEscolarRepository(TccContext context) : base(context) { }
    }
}
