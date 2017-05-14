using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TccGabrielDuarte.Data.EF;

namespace TccGabrielDuarte.Data.EF.Migrations
{
    [DbContext(typeof(TccContext))]
    partial class TccContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TccGabrielDuarte.Model.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.Property<int>("Semestre");

                    b.HasKey("Id");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("TccGabrielDuarte.Model.AlunoCurso", b =>
                {
                    b.Property<int>("AlunoId");

                    b.Property<int>("CursoId");

                    b.HasKey("AlunoId", "CursoId");

                    b.HasIndex("CursoId");

                    b.ToTable("AlunoCurso");
                });

            modelBuilder.Entity("TccGabrielDuarte.Model.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.Property<string>("Sigla");

                    b.HasKey("Id");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("TccGabrielDuarte.Model.CursoDisciplina", b =>
                {
                    b.Property<int>("CursoId");

                    b.Property<int>("DisciplinaId");

                    b.HasKey("CursoId", "DisciplinaId");

                    b.HasIndex("DisciplinaId");

                    b.ToTable("CursoDisciplina");
                });

            modelBuilder.Entity("TccGabrielDuarte.Model.Disciplina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CodDisciplina");

                    b.Property<int>("Creditos");

                    b.Property<string>("Nome");

                    b.Property<int>("TurmaId");

                    b.HasKey("Id");

                    b.HasIndex("TurmaId");

                    b.ToTable("Disciplina");
                });

            modelBuilder.Entity("TccGabrielDuarte.Model.Turma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Professor");

                    b.HasKey("Id");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("TccGabrielDuarte.Model.AlunoCurso", b =>
                {
                    b.HasOne("TccGabrielDuarte.Model.Aluno", "Aluno")
                        .WithMany("AlunoCursos")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TccGabrielDuarte.Model.Curso", "Curso")
                        .WithMany("AlunoCursos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TccGabrielDuarte.Model.CursoDisciplina", b =>
                {
                    b.HasOne("TccGabrielDuarte.Model.Curso", "Curso")
                        .WithMany("CursoDisciplinas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TccGabrielDuarte.Model.Disciplina", "Disciplina")
                        .WithMany("CursoDisciplinas")
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TccGabrielDuarte.Model.Disciplina", b =>
                {
                    b.HasOne("TccGabrielDuarte.Model.Turma", "Turma")
                        .WithMany("Disciplinas")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
