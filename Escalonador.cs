using System;
using System.Collections.Generic;

namespace SimuladorSO
{
    public abstract class Escalonador
    {
        public string Algoritmo { get; set; } = string.Empty;

        public virtual void Executar(List<Processo> processos)
        {
            Console.WriteLine($"Executando escalonador: {Algoritmo}");
            foreach (var p in processos)
            {
                p.ExibirInfo();
            }
        }
    }
}
