using System;
using System.Collections.Generic;
using System.Linq;

namespace TccGabrielDuarte.Model
{
    public class DataGenerator
    {
        private static readonly Random rnd = new Random();

        public static List<Curso> Cursos()
        {
            return new List<Curso>
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
        }

        public static List<Disciplina> Disciplinas(List<Curso> cursos)
        {
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

            return disciplinas;
        }

        public static List<Aluno> Alunos(int qtAlunos, List<Curso> cursos)
        {
            var alunos = new List<Aluno>();
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

                    alunos.Add(aluno);
                }
            }

            return alunos;
        }

        public static List<HistoricoEscolar> HistoricosEscolares(List<Curso> cursos, List<Aluno> alunos, List<Turma> turmas)
        {
            var historicos = new List<HistoricoEscolar>();

            foreach (var aluno in alunos)
            {
                var historico = new HistoricoEscolar
                {
                    Aluno = aluno,
                    Media = rnd.Next(0, 10),
                    Turma = turmas.First(x => x.Disciplinas.First().CursoDisciplinas.First().Curso == aluno.Curso)
                };

                historicos.Add(historico);
            }

            return historicos;
        }

        public static List<Turma> Turmas(List<Curso> cursos, List<Disciplina> disciplinas)
        {
            var turmas = new List<Turma>();

            foreach (var curso in cursos)
            {
                var cursoDisciplinas = curso.CursoDisciplinas.Where(x => x.CursoId == curso.Id).ToList();
                var disciplinasDoCurso = disciplinas.Where(x => cursoDisciplinas.Select(y => y.DisciplinaId).Contains(x.Id)).ToList();

                var turma = new Turma
                {
                    Ano = 2017,
                    Disciplinas = disciplinasDoCurso,
                    Professor = $"Professor {curso.Nome}",
                    Semestre = 1
                };
                turmas.Add(turma);
            }

            return turmas;
        }
    }
}
