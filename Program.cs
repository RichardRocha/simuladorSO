using System;

namespace SimuladorSO
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Entrada de parâmetros simples
            Console.WriteLine("Escolha o algoritmo de escalonamento: 1=RoundRobin, 2=SJF, 3=SPN");
            int escolha = int.Parse(Console.ReadLine());

            int quantum = 2; // padrão
            if (escolha == 1)
            {
                Console.WriteLine("Digite o valor do Quantum:");
                quantum = int.Parse(Console.ReadLine());
            }

            Simulador sim = new Simulador();
            sim.GerarProcessos(3);
            sim.MostrarProcessos();

            Escalonador escalonador;
            switch (escolha)
            {
                case 1:
                    escalonador = new RoundRobin(quantum, sim);
                    break;
                case 2:
                    escalonador = new ShortestJobFirst(sim);
                    break;
                case 3:
                    escalonador = new ShortestProcessNext(sim);
                    break;
                default:
                    Console.WriteLine("Escolha inválida! Usando Round Robin padrão.");
                    escalonador = new RoundRobin(quantum, sim);
                    break;
            }

            sim.Executar(escalonador);

            Console.WriteLine("\n--- Métricas ---");
            foreach (var p in sim.Processos)
            {
                Console.WriteLine($"Processo {p.TempoChegada}: Retorno={p.TempoRetorno}, Espera={p.TempoEspera}");
            }
            Console.WriteLine($"Trocas de contexto: {sim.TrocasDeContexto}");
        }
    }
}
