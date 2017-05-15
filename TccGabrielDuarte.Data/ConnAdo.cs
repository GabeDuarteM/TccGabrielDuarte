﻿using System;
using System.Data.SqlClient;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Data.Ado;

namespace TccGabrielDuarte.Data
{
    public class ConnAdo : ITipoConexao
    {
        public Enums.BANCOS Banco { get; set; }

        public TccContextADO Conn { get => new TccContextADO(Banco); }

        public ConnAdo(Enums.BANCOS banco)
        {
            Banco = banco;
        }

        public int GetListaAlunos()
        {
            throw new NotImplementedException();
        }

        public int GetListaCursos()
        {
            throw new NotImplementedException();
        }

        public int GetListaDisciplinas()
        {
            throw new NotImplementedException();
        }

        public int GetListaHistoricos()
        {
            throw new NotImplementedException();
        }

        public int GetListaTurmas()
        {
            throw new NotImplementedException();
        }

        public void LimparBase()
        {
            using (var conn = Conn.Conn)
            {
                conn.Open();

                switch (Banco)
                {
                    case Enums.BANCOS.SQLite:
                        break;
                    case Enums.BANCOS.SQLServer:
                        using (var cmd = new SqlCommand())
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = (SqlConnection)conn;

                            cmd.CommandText = "DELETE FROM AlunoCurso";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Aluno";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM CursoDisciplina";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Disciplina";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Turma";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Curso";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DBCC CHECKIDENT ('Aluno',RESEED, 0)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DBCC CHECKIDENT ('Disciplina',RESEED, 0)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DBCC CHECKIDENT ('Turma',RESEED, 0)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DBCC CHECKIDENT ('Curso',RESEED, 0)";
                            cmd.ExecuteNonQuery();
                        }
                        break;
                }
            }
        }

        public void Seed(int qtAlunos)
        {
            Conn.Seed(qtAlunos);
        }
    }
}