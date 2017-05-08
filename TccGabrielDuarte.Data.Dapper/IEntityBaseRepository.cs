using System;
using System.Collections.Generic;
using System.Text;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Dapper
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase
    {
        ICollection<T> GetAll();
    }
}
