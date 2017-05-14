using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TccGabrielDuarte.CrossCutting
{
    public class Constantes
    {
        public const string CONN_SQLSERVER = @"Server=(localdb)\mssqllocaldb;Database=SqlServer;Trusted_Connection=True;";
        public const string CONN_SQLITE = "Data Source=Sqlite.db";
    }
}
