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
            Algoritmo = "Shortest Process Next (SPN)";
        }

        public override void Executar(List<Processo> processos)
        {
            Console.WriteLine($"\nExecutando escalonador: {Algoritmo}");

            var prontos = processos
                .Where(p => p.Estado != "Bloqueado")
                .ToList();

            while (prontos.Any(p => p.Estado != "Finalizado"))
            {
                var disponiveis = prontos
                    .Where(p => p.TempoChegada <= Sim.Clock && p.Estado != "Finalizado")
                    .OrderBy(p => p.TempoProcessamento - p.PC)
                    .ToList();

                if (!disponiveis.Any())
                {
                    Sim.AvancarTempoOcioso(1);
                    continue;
                }

                var processo = disponiveis.First();

                if (processo.TempoResposta == -1)
                {
                    processo.TempoResposta = Sim.Clock - processo.TempoChegada;
                    if (processo.TempoResposta < 0)
                        processo.TempoResposta = 0;
                }

                processo.Estado = "Executando";
                Sim.LogEvento($"SPN: processo {processo.Id} começou execução (PC={processo.PC})");

                while (processo.PC < processo.TempoProcessamento)
                {
                    processo.PC++;
                    Sim.AvancarTempo(1);
                }

                processo.Estado = "Finalizado";
                processo.TempoRetorno = Sim.Clock - processo.TempoChegada;
                if (processo.TempoRetorno < 0)
                    processo.TempoRetorno = 0;

                Sim.LogEvento($"SPN: processo {processo.Id} concluído (PC={processo.PC})");
                Sim.IncrementarTrocasDeContexto();
            }

            Console.WriteLine("\nSPN concluído!");
        }
    }
}
