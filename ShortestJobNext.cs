using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorSO
{
    public class ShortestProcessNext : Escalonador
    {
        private Simulador Sim;

        public ShortestProcessNext(Simulador sim)
        {
            Sim = sim;
            Algoritmo = "Shortest Process Next";
        }

        public override void Executar(List<Processo> processos)
        {
            Console.WriteLine($"\nExecutando escalonador: {Algoritmo}");

            var prontos = processos.ToList();
            int tempoAtual = 0;

            while (prontos.Any(p => p.Estado != "Finalizado"))
            {
                var disponiveis = prontos
                    .Where(p => p.TempoChegada <= tempoAtual && p.Estado != "Finalizado")
                    .OrderBy(p => p.TempoProcessamento - p.PC)
                    .ToList();

                if (!disponiveis.Any())
                {
                    tempoAtual++;
                    continue;
                }

                var processo = disponiveis.First();
                processo.Estado = "Executando";

                while (processo.PC < processo.TempoProcessamento)
                {
                    processo.PC++;
                    tempoAtual++;
                }

                processo.Estado = "Finalizado";
                Console.WriteLine($"Processo (Chegada={processo.TempoChegada}) concluído!");
                Sim.IncrementarTrocasDeContexto();
            }

            Console.WriteLine("\nSPN concluído!");
        }
    }
}
