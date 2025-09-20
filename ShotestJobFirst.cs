namespace SimuladorSO
{
    public class ShortestJobFirst : Escalonador
    {
        public ShortestJobFirst()
        {
            Algoritmo = "Shortest Job First";
        }

        public override void Executar(List<Processo> processos)
        {
            Console.WriteLine($"\nExecutando escalonador: {Algoritmo}");

            // Criar lista de todas as threads prontas
            var threads = new List<ThreadSimulada>();
            foreach (var p in processos)
            {
                threads.AddRange(p.Threads);
            }

            // Ordenar threads pelo tempo de processamento do processo pai (menor primeiro)
            threads = threads.OrderBy(t => t.ProcessoPai.TempoProcessamento).ToList();

            foreach (var thread in threads)
            {
                if (thread.Estado != "Finalizada")
                {
                    thread.Executar();
                    // Executa até terminar
                    thread.PC = thread.ProcessoPai.TempoProcessamento;
                    thread.Finalizar();
                }
            }

            Console.WriteLine("\nSJF concluído!");
        }
    }
}
