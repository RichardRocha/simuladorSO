namespace SimuladorSO
{
    public class RoundRobin : Escalonador
    {
        public int Quantum { get; private set; }

        public RoundRobin(int quantum)
        {
            Algoritmo = "Round Robin";
            Quantum = quantum;
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
                    thread.Executar();

                    // Simular execução de Quantum unidades
                    thread.PC += Quantum;

                    // Simular finalização se PC >= TempoProcessamento do processo
                    if (thread.PC >= thread.ProcessoPai.TempoProcessamento)
                    {
                        thread.Finalizar();
                    }
                    else
                    {
                        thread.Estado = "Pronto"; // retorna para fila se não terminou
                        filaProntos.Enqueue(thread);
                    }
                }
            }

            Console.WriteLine("\nRound Robin concluído!");
        }
    }
}
