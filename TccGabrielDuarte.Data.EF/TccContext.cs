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
                optionsBuilder.UseSqlite(@"Data Source=.\Escola.db");
            }
        }

        public void Seed(int qtAlunos)
        {
            var rnd = new Random();

            var cursos = new List<Curso>
            {
                new Curso
                {
                    Nome = "Sistemas de Informação",
                    Sigla = "SI"
                },
                new Curso
                {
                    Nome = "Administração",
                    Sigla = "ADM"
                },
                new Curso
                {
                    Nome = "Direito",
                    Sigla = "DIR"
                },
                new Curso
                {
                    Nome = "Gastronomia",
                    Sigla = "GST"
                },
                new Curso
                {
                    Nome = "Psicologia",
                    Sigla = "PSI"
                },
            };

            Curso.AddRange(cursos);
            SaveChanges();


            var disciplinas = new List<Disciplina>();

            foreach (var curso in cursos)
            {
                for (int i = 0; i < 11; i++)
                {
                    var disciplina = new Disciplina
                    {
                        Nome = $"{curso.Nome}: {i + 1}",
                        CodDisciplina = $"{curso.Sigla}{ (i + 1).ToString().PadLeft(3, '0')}",
                        Creditos = rnd.Next(1, 8),
                        CursoDisciplinas = new List<CursoDisciplina>
                        {
                            new CursoDisciplina
                            {
                                Curso = curso
                            }
                        }
                    };

                    disciplinas.Add(disciplina);
                }
            }

            Disciplina.AddRange(disciplinas);
            SaveChanges();

            var turmas = new List<Turma>();

            foreach (var curso in cursos.ToList())
            {
                var cursoDisciplinas = CursoDisciplina.Where(x => x.CursoId == curso.Id).ToList();
                var disciplinasDoCurso = Disciplina.Where(x => cursoDisciplinas.Select(y => y.DisciplinaId).Contains(x.Id)).ToList();

                var turma = new Turma
                {
                    Ano = 2017,
                    Disciplinas = disciplinasDoCurso,
                    Professor = $"Professor {curso.Nome}",
                    Semestre = 1
                };
                turmas.Add(turma);
            }

            Turma.AddRange(turmas);
            SaveChanges();

            var alunos = new List<Aluno>();
            var historicos = new List<HistoricoEscolar>();
            var alunosPorCurso = qtAlunos / 5;

            foreach (var curso in cursos)
            {
                for (int i = 0; i < alunosPorCurso; i++)
                {
                    var aluno = new Aluno
                    {
                        Curso = curso,
                        Nome = $"Aluno {i} {curso.Sigla}",
                        TipoAluno = rnd.Next(1, 8)
                    };

                    var historico = new HistoricoEscolar
                    {
                        Aluno = aluno,
                        Media = rnd.Next(0, 10),
                        Turma = turmas.First(x => x.Disciplinas.First().CursoDisciplinas.First().Curso == curso)
                    };

                    alunos.Add(aluno);
                    historicos.Add(historico);
                }
            }

            Aluno.AddRange(alunos);
            HistoricoEscolar.AddRange(historicos);
            SaveChanges();
        }
    }
}
