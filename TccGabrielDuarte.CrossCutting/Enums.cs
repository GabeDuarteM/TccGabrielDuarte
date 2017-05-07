using System;

namespace TccGabrielDuarte.CrossCutting
{
    public static class Enums
    {
        public static string[] LIST_OPCOES { get => Enum.GetNames(typeof(OPCOES)); } 
        public static string[] LIST_PROVIDERS { get => Enum.GetNames(typeof(PROVIDERS)); }
        public static string[] LIST_BANCOS { get => Enum.GetNames(typeof(BANCOS)); }

        public enum OPCOES
        {
            PopularTabelas,
            Alunos,
            Cursos,
            Disciplinas,
            Historicos,
            Turmas
        }

        public enum PROVIDERS
        {
            Dapper,
            EF
        }

        public enum BANCOS
        {
            SQLite,
            SQLServer
        }

        public static string[] ToList(this Type e) {
            return Enum.GetNames(e);
        }
    }
}
