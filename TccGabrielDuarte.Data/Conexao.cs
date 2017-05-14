using System;
using System.Collections.Generic;
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
                default:
                    break;
            }
        }

        public void LimparBase()
        {
            Conn.LimparBase();
        }

        public int RealizarOperacao(Enums.OPCOES opcao, int? qtAlunos)
        {
            switch (opcao)
            {
                case Enums.OPCOES.Alunos:
                    return Conn.GetListaAlunos();
                case Enums.OPCOES.Cursos:
                    return Conn.GetListaCursos();
                case Enums.OPCOES.Disciplinas:
                    return Conn.GetListaDisciplinas();
                case Enums.OPCOES.Turmas:
                    return Conn.GetListaTurmas();
                case Enums.OPCOES.PopularTabelas:
                    Conn.Seed((int)qtAlunos);
                    return (int)qtAlunos;
                default:
                    return -1;
            }
        }
    }
}
