using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorSO
{
    public class FCFS : Escalonador
    {
        private Simulador Sim;

        public FCFS(Simulador sim)
        {
            Sim = sim;
            Algoritmo = "First-Come, First-Served (FCFS)";
        }

        public override void Executar(List<Processo> processos)
        {
            Console.WriteLine($"\nExecutando escalonador: {Algoritmo}");

            var ordem = processos
                .Where(p => p.Estado != "Bloqueado")
                .OrderBy(p => p.TempoChegada)
                .ToList();

            foreach (var processo in ordem)
            {
                if (processo.Estado == "Finalizado")
                    continue;

                if (Sim.Clock < processo.TempoChegada)
                {
                    int delta = processo.TempoChegada - Sim.Clock;
                    Sim.AvancarTempoOcioso(delta);
                }

                if (processo.TempoResposta == -1)
                {
                    processo.TempoResposta = Sim.Clock - processo.TempoChegada;
                    if (processo.TempoResposta < 0)
                        processo.TempoResposta = 0;
                }

                processo.Estado = "Executando";
                Sim.LogEvento($"FCFS: processo {processo.Id} começou execução (PC={processo.PC})");

                while (processo.PC < processo.TempoProcessamento)
                {
                    processo.PC++;
                    Sim.AvancarTempo(1);
                }

                processo.Estado = "Finalizado";
                processo.TempoRetorno = Sim.Clock - processo.TempoChegada;
                if (processo.TempoRetorno < 0)
                    processo.TempoRetorno = 0;

                Sim.LogEvento($"FCFS: processo {processo.Id} concluído (PC={processo.PC})");
                Sim.IncrementarTrocasDeContexto();
            }

            Console.WriteLine("\nFCFS concluído!");
        }
    }
}
