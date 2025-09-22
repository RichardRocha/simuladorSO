using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorSO
{
    public class ShortestJobFirst : Escalonador
    {
        private Simulador Sim;

        public ShortestJobFirst(Simulador sim)
        {
            Sim = sim;
            Algoritmo = "Shortest Job First";
        }

        public override void Executar(List<Processo> processos)
        {
            Console.WriteLine($"\nExecutando escalonador: {Algoritmo}");

            var processosOrdenados = processos.OrderBy(p => p.TempoProcessamento).ToList();

            foreach (var processo in processosOrdenados)
            {
                processo.Estado = "Executando";
                while (processo.PC < processo.TempoProcessamento)
                {
                    processo.PC++;
                }
                processo.Estado = "Finalizado";
                Console.WriteLine($"Processo (Chegada={processo.TempoChegada}) concluído!");
                Sim.IncrementarTrocasDeContexto();
            }

            Console.WriteLine("\nSJF concluído!");
        }
    }
}
