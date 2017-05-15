using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.EF
{
    public class TccContext : DbContext
    {
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<Disciplina> Disciplina { get; set; }
        public DbSet<Curso> Curso { get; set; }

        public bool UseSqlServer { get; set; }

        public TccContext()
        {
            UseSqlServer = true;
            Database.EnsureCreated();
        }

        public TccContext(Enums.BANCOS banco)
        {
            UseSqlServer = banco == Enums.BANCOS.SQLServer ? true : false;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoCurso>()
                .HasKey(x => new { x.AlunoId, x.CursoId });
            modelBuilder.Entity<CursoDisciplina>()
                .HasKey(x => new { x.CursoId, x.DisciplinaId });

            modelBuilder.Entity<AlunoCurso>()
                .HasOne(x => x.Aluno)
                .WithMany(x => x.AlunoCursos);

            modelBuilder.Entity<CursoDisciplina>()
                .HasOne(x => x.Curso)
                .WithMany(x => x.CursoDisciplinas);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (UseSqlServer)
            {
                optionsBuilder.UseSqlServer(Constantes.CONN_SQLSERVER);
            }
            else
            {
                optionsBuilder.UseSqlite(Constantes.CONN_SQLITE);
            }
        }

        public void Seed(int qtAlunos)
        {
            var rnd = new Random();

            var cursos = DataGenerator.Cursos();
            var turmas = DataGenerator.Turmas(cursos);
            var disciplinas = DataGenerator.Disciplinas(cursos, turmas);
            var alunos = DataGenerator.Alunos(qtAlunos, cursos);

            Curso.AddRange(cursos);
            Disciplina.AddRange(disciplinas);
            Aluno.AddRange(alunos);

            SaveChanges();
        }
    }
}
