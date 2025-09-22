using System;

namespace SimuladorSO
{
    public class Program
    {
        static void Main(string[] args)
        {
            Simulador sim = new Simulador();
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("\n===== Simulador de Sistema Operacional =====");
                Console.WriteLine("1 - Gerar processos aleatórios");
                Console.WriteLine("2 - Mostrar processos atuais");
                Console.WriteLine("3 - Executar escalonamento");
                Console.WriteLine("4 - Ver métricas finais");
                Console.WriteLine("5 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Write("Quantos processos deseja criar? ");
                        if (int.TryParse(Console.ReadLine(), out int qtd))
                        {
                            sim.GerarProcessos(qtd);
                            Console.WriteLine($"{qtd} processos foram gerados com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine("Entrada inválida, tente novamente.");
                        }
                        break;

                    case "2":
                        sim.MostrarProcessos();
                        break;

                    case "3":
                        Console.WriteLine("\nEscolha o algoritmo de escalonamento:");
                        Console.WriteLine("1 - Round Robin (RR)");
                        Console.WriteLine("2 - Shortest Job First (SJF)");
                        Console.WriteLine("3 - Shortest Process Next (SPN)");
                        Console.Write("Opção: ");
                        string escolha = Console.ReadLine();

                        Escalonador escalonador;
                        int quantum = 2;

                        if (escolha == "1")
                        {
                            Console.Write("Digite o quantum (número inteiro): ");
                            if (!int.TryParse(Console.ReadLine(), out quantum))
                            {
                                Console.WriteLine("Valor inválido. Usando quantum padrão (2).");
                                quantum = 2;
                            }
                            escalonador = new RoundRobin(quantum, sim);
                        }
                        else if (escolha == "2")
                        {
                            escalonador = new ShortestJobFirst(sim);
                        }
                        else if (escolha == "3")
                        {
                            escalonador = new ShortestProcessNext(sim);
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida. Usando Round Robin com quantum padrão.");
                            escalonador = new RoundRobin(quantum, sim);
                        }

                        sim.Executar(escalonador);
                        break;

                    case "4":
                        Console.WriteLine("\n===== Métricas =====");
                        foreach (var p in sim.Processos)
                        {
                            Console.WriteLine($"Processo {p.TempoChegada}: Retorno={p.TempoRetorno}, Espera={p.TempoEspera}");
                        }
                        Console.WriteLine($"Trocas de contexto: {sim.TrocasDeContexto}");
                        break;

                    case "5":
                        sair = true;
                        Console.WriteLine("Encerrando simulador...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida, tente novamente.");
                        break;
                }
            }
        }
    }
}
