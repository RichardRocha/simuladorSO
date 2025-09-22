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

            var fila = new Queue<Processo>(processos);

            while (fila.Any())
            {
                var processo = fila.Dequeue();

                if (processo.Estado == "Finalizado")
                    continue;

                processo.Estado = "Executando";

                int tempoExecutado = 0;
                while (tempoExecutado < Quantum && processo.PC < processo.TempoProcessamento)
                {
                    processo.PC++;
                    tempoExecutado++;
                }

                if (processo.PC >= processo.TempoProcessamento)
                {
                    processo.Estado = "Finalizado";
                    Console.WriteLine($"Processo (Chegada={processo.TempoChegada}) concluído!");
                }
                else
                {
                    processo.Estado = "Pronto";
                    fila.Enqueue(processo);
                }

                Sim.IncrementarTrocasDeContexto();
            }

            Console.WriteLine("\nRound Robin concluído!");
        }
    }
}
