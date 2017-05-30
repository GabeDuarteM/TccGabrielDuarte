using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Data.EF;

namespace TccGabrielDuarte.Data
{
    public class Conexao
    {
        public ITipoConexao Conn { get; set; }
        public Conexao(Enums.BANCOS banco, Enums.PROVIDERS provider)
        {
            switch (provider)
            {
                case Enums.PROVIDERS.Dapper:
                    Conn = new ConnDapper(banco);
                    break;
                case Enums.PROVIDERS.EF:
                    Conn = new ConnEF(banco);
                    break;
                case Enums.PROVIDERS.ADO:
                    Conn = new ConnAdo(banco);
                    break;
                default:
                    break;
            }
        }

        public void LimparBase()
        {
            Conn.LimparBase();
        }

        public int RealizarOperacao(Enums.OPCOES opcao, int? qtAlunos, int id)
        {
            switch (opcao)
            {
                case Enums.OPCOES.GetAll:
                    return Conn.GetListaAlunos().Count;
                case Enums.OPCOES.GetById:
                    return Conn.GetAlunoById(id);
                case Enums.OPCOES.PopularTabelas:
                    Conn.Seed((int)qtAlunos);
                    return (int)qtAlunos;
                default:
                    return -1;
            }
        }

        public int GetLastId()
        {
            return Conn.GetListaAlunos().Last().Id;
        }
    }
}
