using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TccGabrielDuarte.CrossCutting
{
    public class Constantes
    {
        public const string CONN_SQLSERVER = @"Server=tcp:tccgabrielduarte.database.windows.net,1433;Initial Catalog=TccGabrielDuarte;Persist Security Info=False;User ID=gabrielduartem;Password=123pass321$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public const string CONN_SQLITE = "Data Source=Sqlite.db";
    }
}
