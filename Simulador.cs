using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SimuladorSO
{
    public class Simulador
    {
        private List<Processo> processos;
        private GerenciadorMemoria memoria;

        public Simulador()
        {
            processos = new List<Processo>();
            memoria = new GerenciadorMemoria(100); // 100 de RAM p/ teste
        }

        public void GerarProcessos(int quantidade)
        {
            Random rnd = new Random();
            for (int i = 0; i < quantidade; i++)
            {
                int chegada = rnd.Next(0, 10);
                int execucao = rnd.Next(1, 10);
                int memoriaNecessaria = rnd.Next(1, 30);

                var p = new Processo(chegada, execucao, memoriaNecessaria);
                processos.Add(p);
            }
        }

        public void MostrarProcessos()
        {
            Console.WriteLine("\n--- Lista de Processos ---");
            foreach (var p in processos)
            {
                p.ExibirInfo();
            }
        }

        public void Executar(Escalonador escalonador)
        {
            Console.WriteLine($"\n>>> Simulação com {escalonador.Algoritmo} <<<");
            escalonador.Executar(processos);
        }
    }
}
