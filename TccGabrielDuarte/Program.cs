using System;
using System.Diagnostics;
using System.Linq;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Data;

namespace TccGabrielDuarte
{
    public class Program
    {
        public static Enums.PROVIDERS PROVIDER { get; set; }
        public static Enums.BANCOS DB { get; set; }
        public static void Main(string[] args)
        {
            SetarEncoding();

            EscolherProvider();

            EscolherDB();

            EscolherOpcoes();

            Console.WriteLine("Pressione algo para finalizar...");
            
            Console.ReadKey();

            Environment.Exit(0);
        }

        private static void EscolherOpcoes()
        {
            var opcoes = Enum.GetValues(typeof(Enums.OPCOES)).Cast<int>();

            ConsoleKeyInfo opt;
            
            do
            {
                opt = ExibirOpcoes();
            }
            while (opt.Key != ConsoleKey.S && !opcoes.Contains(int.Parse(opt.KeyChar.ToString())));

            if (opt.Key == ConsoleKey.S)
            {
                return;
            }

            var opcao = (Enums.OPCOES)int.Parse(opt.KeyChar.ToString());

            IniciarOperacao(opcao);

            EscolherOpcoes();
        }

        private static void IniciarOperacao(Enums.OPCOES opcao)
        {
            int? qtAlunos = null;

            if (opcao == Enums.OPCOES.PopularTabelas)
            {
                Console.WriteLine("Com quantos alunos você deseja fazer o teste?");
                var strQtAlunos = Console.ReadLine();

                Console.Write(Environment.NewLine);

                if (int.TryParse(strQtAlunos.Trim(), out int intQtAluno))
                {
                    qtAlunos = intQtAluno;
                }
                else
                {
                    Console.WriteLine("Quantidade informada não é um número válido.");
                    return;
                }
            }

            var conn = new Conexao(DB, PROVIDER);

            if (opcao == Enums.OPCOES.PopularTabelas)
            {
                Console.WriteLine("Limpando a base antes de inserir registros...");
                Console.Write(Environment.NewLine);
                conn.LimparBase();
            }

            Console.WriteLine("Iniciando operação: " + opcao.ToString());
            Console.Write(Environment.NewLine);

            var timer = Stopwatch.StartNew();

            var qtRegistros = conn.RealizarOperacao(opcao, qtAlunos);

            timer.Stop();

            Console.WriteLine($"Operação levou {timer.ElapsedMilliseconds}ms e retornou {qtRegistros} registros.");
            Console.Write(Environment.NewLine);
        }

        private static ConsoleKeyInfo ExibirOpcoes()
        {
            Console.WriteLine("Escolha uma operação para ser realizada:");

            Console.WriteLine($"S: SAIR");

            for (int i = 0; i < Enums.LIST_OPCOES.Length; i++)
            {
                Console.WriteLine($"{i}: {Enums.LIST_OPCOES[i]}");
            }


            var opt = Console.ReadKey();
            Console.WriteLine(Environment.NewLine);
            return opt;
        }

        private static void EscolherDB()
        {
            var valoresAceitos = Enum.GetValues(typeof(Enums.BANCOS)).Cast<int>();

            ConsoleKeyInfo opt;
            do
            {
                opt = ExibirOpcoesBanco();
            }
            while (!valoresAceitos.Contains(int.Parse(opt.KeyChar.ToString())));

            DB = (Enums.BANCOS)int.Parse(opt.KeyChar.ToString());

            Console.WriteLine("Trabalhando com o banco " + DB.ToString());
            Console.Write(Environment.NewLine);
        }

        private static ConsoleKeyInfo ExibirOpcoesBanco()
        {
            Console.WriteLine("Escolha o banco para ser usado:");

            for (int i = 0; i < Enums.LIST_BANCOS.Length; i++)
            {
                Console.WriteLine($"{i}: {Enums.LIST_BANCOS[i]}");
            }

            var opt = Console.ReadKey();
            Console.WriteLine(Environment.NewLine);
            return opt;
        }

        private static void EscolherProvider()
        {
            var valoresAceitos = Enum.GetValues(typeof(Enums.PROVIDERS)).Cast<int>();

            ConsoleKeyInfo opt;
            do
            {
                opt = ExibirOpcoesProvider();
            }
            while (!valoresAceitos.Contains(int.Parse(opt.KeyChar.ToString())));

            PROVIDER = (Enums.PROVIDERS)int.Parse(opt.KeyChar.ToString());

            Console.WriteLine("Trabalhando com o provider " + PROVIDER.ToString());
            Console.Write(Environment.NewLine);
        }

        private static ConsoleKeyInfo ExibirOpcoesProvider()
        {
            Console.WriteLine("Escolha o provider para ser usado:");

            for (int i = 0; i < Enums.LIST_PROVIDERS.Length; i++)
            {
                Console.WriteLine($"{i}: {Enums.LIST_PROVIDERS[i]}");
            }

            var opt = Console.ReadKey();
            Console.WriteLine(Environment.NewLine);
            return opt;
        }

        private static void SetarEncoding()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
        }
    }
}