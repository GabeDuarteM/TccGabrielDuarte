using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Dapper
{
    public class TccContextDapper
    {
        private readonly string connString;

        public IDbConnection Conn { get => new SqlConnection(connString); }

        public TccContextDapper(Enums.BANCOS banco)
        {
            switch (banco)
            {
                case Enums.BANCOS.SQLite:
                    connString = "Data Source=Escola.db";
                    break;
                case Enums.BANCOS.SQLServer:
                    connString = "Server=(localdb)\\mssqllocaldb;Database=Escola;Trusted_Connection=True;";
                    break;
            }
        }

        public void Seed(int qtAlunos)
        {
            var curso = DataGenerator.Cursos();

            Conn.Insert(curso);

            var disciplinas = DataGenerator.Disciplinas(curso);
            Conn.Insert(disciplinas);

            var turmas = DataGenerator.Turmas(curso, disciplinas);
            Conn.Insert(turmas);

            var alunos = DataGenerator.Alunos(qtAlunos, curso);
            Conn.Insert(alunos);

            var historicos = DataGenerator.HistoricosEscolares(curso, alunos, turmas);
            Conn.Insert(historicos);
        }
    }
}
