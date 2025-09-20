namespace SimuladorSO
{
    public class ShortestJobFirst : Escalonador
    {
        private Simulador sim; // referência do simulador

        public ShortestJobFirst(Simulador simulador)
        {
            Algoritmo = "Shortest Job First";
            sim = simulador;
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
                    sim.LogEvento($"Thread {thread.Id} do processo {thread.ProcessoPai.TempoChegada} começou execução");

                    thread.Executar();
                    thread.PC = thread.ProcessoPai.TempoProcessamento;
                    thread.Finalizar();

                    sim.LogEvento($"Thread {thread.Id} finalizou execução do processo {thread.ProcessoPai.TempoChegada}");

                    sim.IncrementarTrocasDeContexto(); // atualiza métrica
                }
            }

            Console.WriteLine("\nSJF concluído!");
        }
    }
}
