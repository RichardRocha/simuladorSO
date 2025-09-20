namespace SimuladorSO
{
    public class ShortestProcessNext : Escalonador
    {
        public ShortestProcessNext()
        {
            Algoritmo = "Shortest Process Next";
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
                        t.Executar();
                        t.PC = p.TempoProcessamento;
                        t.Finalizar();
                    }
                }
            }

            Console.WriteLine("\nSPN concluído!");
        }
    }
}
