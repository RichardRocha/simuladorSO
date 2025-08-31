using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SimuladorSO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Simulador sim = new Simulador();

            sim.GerarProcessos(5);
            sim.MostrarProcessos();

            Escalonador rr = new RoundRobin(2);
            sim.Executar(rr);

            Escalonador sjf = new ShortestJobFirst();
            sim.Executar(sjf);

            Escalonador spn = new ShortestProcessNext();
            sim.Executar(spn);

            Console.WriteLine("\nFim da simulação");
        }
    }
}
