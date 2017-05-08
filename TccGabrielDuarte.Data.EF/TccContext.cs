using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.EF
{
    public class TccContext : DbContext
    {
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<Disciplina> Disciplina { get; set; }
        public DbSet<HistoricoEscolar> HistoricoEscolar { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<CursoDisciplina> CursoDisciplina { get; set; }

        public bool UseSqlServer { get; set; }

        public TccContext()
        {
            UseSqlServer = true;
        }

        public TccContext(Enums.BANCOS banco)
        {
            UseSqlServer = banco == Enums.BANCOS.SQLServer ? true : false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (UseSqlServer)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Escola;Trusted_Connection=True;");
            }
            else
            {
                optionsBuilder.UseSqlite(@"Data Source=Escola.db");
            }
        }

        public void Seed(int qtAlunos)
        {
            var rnd = new Random();

            var cursos = DataGenerator.Cursos();

            Curso.AddRange(cursos);
            SaveChanges();

            var disciplinas = DataGenerator.Disciplinas(cursos);

            Disciplina.AddRange(disciplinas);
            SaveChanges();

            var turmas = DataGenerator.Turmas(cursos, disciplinas);

            Turma.AddRange(turmas);
            SaveChanges();

            var alunos = DataGenerator.Alunos(qtAlunos, cursos);
            Aluno.AddRange(alunos);

            var historicos = DataGenerator.HistoricosEscolares(cursos, alunos, turmas);
            HistoricoEscolar.AddRange(historicos);

            SaveChanges();
        }
    }
}
