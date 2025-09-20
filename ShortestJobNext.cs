namespace SimuladorSO
{
    public class ShortestProcessNext : Escalonador
    {
        private Simulador sim; // referência do simulador

        public ShortestProcessNext(Simulador simulador)
        {
            Algoritmo = "Shortest Process Next";
            sim = simulador;
        }

        public override void Executar(List<Processo> processos)
        {
            Console.WriteLine($"\nExecutando escalonador: {Algoritmo}");

            // Ordena processos pelo tempo de processamento total (menor primeiro)
            var processosOrdenados = processos.OrderBy(p => p.TempoProcessamento).ToList();

            foreach (var p in processosOrdenados)
            {
                foreach (var t in p.Threads)
                {
                    if (t.Estado != "Finalizada")
                    {
                        // Log início execução
                        sim.LogEvento($"Thread {t.Id} do processo {p.TempoChegada} começou execução");

                        t.Executar();
                        t.PC = p.TempoProcessamento;
                        t.Finalizar();

                        // Log finalização
                        sim.LogEvento($"Thread {t.Id} finalizou execução do processo {p.TempoChegada}");

                        // Incrementa métricas
                        sim.IncrementarTrocasDeContexto();
                    }
                }
            }

            Console.WriteLine("\nSPN concluído!");
        }
    }
}
