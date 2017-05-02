using System;
using System.Collections.Generic;
using System.Text;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Data.EF;

namespace TccGabrielDuarte.Data
{
    public class Conexao
    {
        public Conexao(Enums.BANCOS banco, Enums.PROVIDERS provider)
        {
            ITipoConexao conexao = null;

            switch (provider)
            {
                case Enums.PROVIDERS.Dapper:
                    switch (banco)
                    {
                        case Enums.BANCOS.SQLite:
                            conexao = new TccContext(false);
                            break;
                        case Enums.BANCOS.SQLServer:
                            conexao = new TccContext(true);
                            break;
                    }
                    break;
                case Enums.PROVIDERS.EF:
                    switch (banco)
                    {
                        case Enums.BANCOS.SQLite:
                            conexao = new TccContext(false);
                            break;
                        case Enums.BANCOS.SQLServer:
                            conexao = new TccContext(true);
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
