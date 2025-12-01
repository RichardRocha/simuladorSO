using System.Collections.Generic;

namespace SimuladorSO
{
    public class Processo
    {
        private static int _contadorId = 0;
        public int Id { get; private set; }
        public int TempoChegada { get; set; }
        public int TempoProcessamento { get; set; }
        public int MemoriaNecessaria { get; set; }
        public string Estado { get; set; } = "Pronto";

        // prioridade simples (1 = mais alta)
        public int Prioridade { get; set; } = 3;

        // registradores simulados e PC
        public int[] Registradores { get; private set; } = new int[8];
        public int PC { get; set; } = 0;

        // threads
        public List<ThreadSimulada> Threads { get; private set; } = new List<ThreadSimulada>();

        // Page table: lista de frames (índices) alocados para este processo
        public List<int> PageTable { get; private set; } = new List<int>();

        // Tabela de arquivos abertos pelo processo
        public List<ArquivoSimulado> ArquivosAbertos { get; private set; } = new List<ArquivoSimulado>();

        // Métricas básicas
        public int TempoRetorno { get; set; } = 0;
        public int TempoEspera { get; set; } = 0;
        public int TempoResposta { get; set; } = -1; // primeiro acesso à CPU

        public Processo(int chegada, int processamento, int memoria)
        {
            Id = ++_contadorId;
            TempoChegada = chegada;
            TempoProcessamento = processamento;
            MemoriaNecessaria = memoria;
        }

        public void CriarThread()
        {
            var t = new ThreadSimulada(this);
            Threads.Add(t);
        }

        public void ExibirInfo()
        {
            System.Console.WriteLine(
                $"[Processo] Id:{Id} Chegada:{TempoChegada}, Execução:{TempoProcessamento}, " +
                $"Memória:{MemoriaNecessaria}, Prioridade:{Prioridade}, Estado:{Estado}, PC:{PC}"
            );
            if (PageTable.Count > 0)
            {
                System.Console.Write("    Pages -> frames: ");
                System.Console.WriteLine(string.Join(", ", PageTable));
            }
            foreach (var t in Threads)
            {
                System.Console.WriteLine($"    [Thread] Id:{t.Id}, Estado:{t.Estado}, PC:{t.PC}");
            }

            if (ArquivosAbertos.Count > 0)
            {
                System.Console.WriteLine("    Arquivos abertos:");
                foreach (var a in ArquivosAbertos)
                {
                    System.Console.WriteLine($"        - {a.Nome}");
                }
            }
        }
    }
}
