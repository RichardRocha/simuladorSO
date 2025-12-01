using System;

namespace SimuladorSO
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Configuração inicial do simulador:");

            Console.Write("Tamanho total da memória (padrão 100): ");
            string? entradaMem = Console.ReadLine();
            int memoriaTotal = 100;
            if (!string.IsNullOrWhiteSpace(entradaMem) &&
                !int.TryParse(entradaMem, out memoriaTotal))
            {
                Console.WriteLine("Valor inválido. Usando 100.");
                memoriaTotal = 100;
            }

            Console.Write("Tamanho da página (padrão 10): ");
            string? entradaPag = Console.ReadLine();
            int pageSize = 10;
            if (!string.IsNullOrWhiteSpace(entradaPag) &&
                !int.TryParse(entradaPag, out pageSize))
            {
                Console.WriteLine("Valor inválido. Usando 10.");
                pageSize = 10;
            }

            Simulador sim = new Simulador(memoriaTotal, pageSize);
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

                string? opcao = Console.ReadLine();

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
                        Console.WriteLine("4 - First-Come, First-Served (FCFS)");
                        Console.Write("Opção: ");
                        string? escolha = Console.ReadLine();

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
                        else if (escolha == "4")
                        {
                            escalonador = new FCFS(sim);
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
                            Console.WriteLine(
                                $"Processo {p.Id} | Chegada={p.TempoChegada}, Execução={p.TempoProcessamento}, " +
                                $"Retorno={p.TempoRetorno}, Espera={p.TempoEspera}, Resposta={p.TempoResposta}, Prioridade={p.Prioridade}"
                            );
                        }

                        Console.WriteLine($"\nTrocas de contexto: {sim.TrocasDeContexto}");
                        Console.WriteLine($"Clock total: {sim.Clock}");
                        Console.WriteLine($"Tempo de CPU ociosa: {sim.TempoOciosoCPU}");
                        Console.WriteLine($"Utilização da CPU: {sim.CpuUtilizacao:F2}%");
                        Console.WriteLine($"Throughput: {sim.Throughput:F4} processos/unidade de tempo");

                        Console.WriteLine("\n--- Métricas de Memória ---");
                        Console.WriteLine(
                            $"Total memória: {sim.Memoria.MemoriaTotal}, PageSize: {sim.Memoria.PageSize}, Frames: {sim.Memoria.NumFrames}"
                        );
                        Console.WriteLine(
                            $"Alocações: {sim.Memoria.TotalAlocacoes}, Falhas de alocação (simulando faltas de página): {sim.Memoria.FalhasAlocacao}"
                        );
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
