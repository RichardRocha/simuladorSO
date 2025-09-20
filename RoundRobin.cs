namespace SimuladorSO
{
    public class RoundRobin : Escalonador
    {
        public int Quantum { get; private set; }
        private Simulador sim; // referência do simulador

        public RoundRobin(int quantum, Simulador simulador)
        {
            Algoritmo = "Round Robin";
            Quantum = quantum;
            sim = simulador;
        }

        public override void Executar(List<Processo> processos)
        {
            Console.WriteLine($"Executando escalonador: {Algoritmo} com quantum = {Quantum}\n");

            // Fila de threads prontas
            Queue<ThreadSimulada> filaProntos = new Queue<ThreadSimulada>();

            // Colocar todas as threads na fila
            foreach (var p in processos)
            {
                foreach (var t in p.Threads)
                {
                    filaProntos.Enqueue(t);
                }
            }

            while (filaProntos.Count > 0)
            {
                var thread = filaProntos.Dequeue();
                if (thread.Estado != "Finalizada")
                {
                    // Log início execução
                    sim.LogEvento($"Thread {thread.Id} do processo {thread.ProcessoPai.TempoChegada} começou execução");

                    thread.Executar();

                    // Simular execução de Quantum unidades
                    thread.PC += Quantum;
                    thread.QuantumExecutado += Quantum;

                    // Atualiza métricas básicas
                    sim.TrocasDeContexto++;

                    // Simular finalização se PC >= TempoProcessamento do processo
                    if (thread.PC >= thread.ProcessoPai.TempoProcessamento)
                    {
                        thread.Finalizar();
                        sim.LogEvento($"Thread {thread.Id} finalizou execução do processo {thread.ProcessoPai.TempoChegada}");
                    }
                    else
                    {
                        thread.Estado = "Pronto"; // retorna para fila se não terminou
                        filaProntos.Enqueue(thread);
                        sim.LogEvento($"Thread {thread.Id} retorna para fila (pronto)");
                    }
                }
            }

            Console.WriteLine("\nRound Robin concluído!");
        }
    }
}
