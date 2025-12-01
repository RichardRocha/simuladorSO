using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorSO
{
    public class RoundRobin : Escalonador
    {
        private int Quantum;
        private Simulador Sim;

        public RoundRobin(int quantum, Simulador sim)
        {
            Quantum = quantum;
            Sim = sim;
            Algoritmo = $"Round Robin com quantum = {quantum}";
        }

        public override void Executar(List<Processo> processos)
        {
            Console.WriteLine($"\nExecutando escalonador: {Algoritmo}");

            var fila = new Queue<Processo>(processos.Where(p => p.Estado != "Bloqueado"));

            while (fila.Any())
            {
                var processo = fila.Dequeue();

                if (processo.Estado == "Finalizado")
                    continue;

                // primeira vez que pega CPU → tempo de resposta
                if (processo.TempoResposta == -1)
                {
                    processo.TempoResposta = Sim.Clock - processo.TempoChegada;
                    if (processo.TempoResposta < 0)
                        processo.TempoResposta = 0;
                }

                processo.Estado = "Executando";
                Sim.LogEvento($"RR: processo {processo.Id} começou fatia (PC={processo.PC})");

                int tempoExecutado = 0;

                while (tempoExecutado < Quantum && processo.PC < processo.TempoProcessamento)
                {
                    processo.PC++;
                    tempoExecutado++;
                    Sim.AvancarTempo(1);
                }

                if (processo.PC >= processo.TempoProcessamento)
                {
                    processo.Estado = "Finalizado";
                    processo.TempoRetorno = Sim.Clock - processo.TempoChegada;
                    if (processo.TempoRetorno < 0)
                        processo.TempoRetorno = 0;

                    Sim.LogEvento($"RR: processo {processo.Id} concluído (PC={processo.PC})");
                }
                else
                {
                    processo.Estado = "Pronto";
                    fila.Enqueue(processo);
                    Sim.LogEvento($"RR: processo {processo.Id} voltou para fila (PC={processo.PC})");
                }

                Sim.IncrementarTrocasDeContexto();
            }

            Console.WriteLine("\nRound Robin concluído!");
        }
    }
}
