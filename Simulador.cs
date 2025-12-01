using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorSO
{
    public class Simulador
    {
        private List<Processo> processos;
        private GerenciadorMemoria memoria;
        private List<DispositivoES> dispositivos;

        public DiretorioSimulado Raiz { get; private set; }

        public int TrocasDeContexto { get; private set; } = 0;
        public int Clock { get; private set; } = 0;

        // tempo que a CPU ficou ociosa (sem processo rodando)
        public int TempoOciosoCPU { get; private set; } = 0;

        // Métricas globais derivadas
        public double Throughput { get; private set; } = 0.0;
        public double CpuUtilizacao
        {
            get
            {
                if (Clock == 0) return 0.0;
                int ocupado = Clock - TempoOciosoCPU;
                return (double)ocupado / Clock * 100.0;
            }
        }

        public GerenciadorMemoria Memoria => memoria;

        public Simulador() : this(100, 10) { }

        public Simulador(int memoriaTotal, int pageSize)
        {
            processos = new List<Processo>();
            memoria = new GerenciadorMemoria(memoriaTotal, pageSize);

            dispositivos = new List<DispositivoES>
            {
                new DispositivoES("Disco"),
                new DispositivoES("Impressora")
            };

            Raiz = new DiretorioSimulado("root");
        }

        public List<Processo> Processos => processos;

        public void GerarProcessos(int quantidade)
        {
            Random rnd = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                int chegada = rnd.Next(0, 10);
                int execucao = rnd.Next(1, 10);
                int memoriaNecessaria = rnd.Next(1, 30);
                int prioridade = rnd.Next(1, 6);

                var p = new Processo(chegada, execucao, memoriaNecessaria)
                {
                    Prioridade = prioridade
                };

                int numThreads = rnd.Next(1, 4);
                for (int t = 0; t < numThreads; t++)
                {
                    p.CriarThread();
                }

                processos.Add(p);
            }
        }

        public void MostrarProcessos()
        {
            Console.WriteLine("\n--- Lista de Processos ---");
            if (processos.Count == 0)
            {
                Console.WriteLine("Nenhum processo criado ainda.");
                return;
            }

            foreach (var p in processos)
            {
                p.ExibirInfo();
            }
        }

        public void Executar(Escalonador escalonador)
        {
            ResetarMetricas();

            Console.WriteLine($"\n>>> Simulação com {escalonador.Algoritmo} <<<");

            foreach (var p in processos)
            {
                if (p.Estado == "Finalizado")
                    continue;

                bool alocou = memoria.Alocar(p);
                if (!alocou)
                {
                    p.Estado = "Bloqueado";
                    LogEvento($"Processo {p.Id} bloqueado por falta de memória (necessário {p.MemoriaNecessaria})");
                }
                else
                {
                    LogEvento($"Memória alocada para processo {p.Id} | Frames: {string.Join(", ", p.PageTable)}");
                }
            }

            escalonador.Executar(processos);

            foreach (var p in processos)
            {
                if (p.Estado == "Finalizado")
                {
                    p.TempoEspera = p.TempoRetorno - p.TempoProcessamento;
                    if (p.TempoEspera < 0)
                        p.TempoEspera = 0;
                }
            }

            int finalizados = processos.Count(p => p.Estado == "Finalizado");
            Throughput = Clock == 0 ? 0.0 : (double)finalizados / Clock;

            LiberarMemoriaDeTodosProcessos();

            Console.WriteLine("\n--- Estado final dos processos ---");
            foreach (var p in processos)
            {
                p.ExibirInfo();
            }
            memoria.MostrarStatus();
        }

        public void LiberarMemoriaDeTodosProcessos()
        {
            foreach (var p in processos)
            {
                memoria.Liberar(p);
            }
        }

        public void SolicitarES(ThreadSimulada thread, string nomeDispositivo)
        {
            var disp = dispositivos.Find(d => d.Nome == nomeDispositivo);
            if (disp != null)
            {
                disp.Solicitar(thread);
                LogEvento($"Thread {thread.Id} do processo {thread.ProcessoPai.Id} solicitou E/S em {disp.Nome}");
            }
            else
            {
                Console.WriteLine($"Dispositivo {nomeDispositivo} não encontrado.");
            }
        }

        // SISTEMA DE ARQUIVOS

        public void CriarArquivo(string nome, int tamanho, DiretorioSimulado? dir = null)
        {
            if (dir == null) dir = Raiz;

            dir.AdicionarArquivo(new ArquivoSimulado(nome, tamanho));
            LogEvento($"Arquivo '{nome}' criado no diretório '{dir.Nome}'");
        }

        public void ListarDiretorio(DiretorioSimulado? dir = null)
        {
            if (dir == null) dir = Raiz;
            dir.ListarArquivos();
        }

        public ArquivoSimulado? AbrirArquivo(Processo processo, string nome)
        {
            var arq = Raiz.BuscarArquivo(nome);
            if (arq == null)
            {
                Console.WriteLine($"Arquivo '{nome}' não encontrado.");
                return null;
            }

            if (!processo.ArquivosAbertos.Contains(arq))
            {
                processo.ArquivosAbertos.Add(arq);
                LogEvento($"Processo {processo.Id} abriu arquivo '{nome}'");
            }

            return arq;
        }

        public void FecharArquivo(Processo processo, string nome)
        {
            var arq = processo.ArquivosAbertos.FirstOrDefault(a => a.Nome == nome);
            if (arq != null)
            {
                processo.ArquivosAbertos.Remove(arq);
                LogEvento($"Processo {processo.Id} fechou arquivo '{nome}'");
            }
            else
            {
                Console.WriteLine($"Processo {processo.Id} não tinha o arquivo '{nome}' aberto.");
            }
        }

        public void LerArquivo(Processo processo, string nome)
        {
            var arq = processo.ArquivosAbertos.FirstOrDefault(a => a.Nome == nome);
            if (arq == null)
            {
                Console.WriteLine($"Processo {processo.Id} não tem o arquivo '{nome}' aberto para leitura.");
                return;
            }

            Console.WriteLine($"[Leitura] P{processo.Id} lendo '{nome}': \"{arq.Conteudo}\"");
        }

        public void EscreverArquivo(Processo processo, string nome, string texto)
        {
            var arq = processo.ArquivosAbertos.FirstOrDefault(a => a.Nome == nome);
            if (arq == null)
            {
                Console.WriteLine($"Processo {processo.Id} não tem o arquivo '{nome}' aberto para escrita.");
                return;
            }

            arq.Conteudo += texto;
            arq.Tamanho = arq.Conteudo.Length;
            LogEvento($"Processo {processo.Id} escreveu no arquivo '{nome}'");
        }

        public void ApagarArquivo(string nome)
        {
            bool removido = Raiz.RemoverArquivo(nome);
            if (removido)
            {
                LogEvento($"Arquivo '{nome}' apagado do sistema de arquivos");

                foreach (var p in processos)
                {
                    p.ArquivosAbertos.RemoveAll(a => a.Nome == nome);
                }
            }
            else
            {
                Console.WriteLine($"Arquivo '{nome}' não encontrado para apagar.");
            }
        }

        // MÉTRICAS E CLOCK

        public void LogEvento(string evento)
        {
            Console.WriteLine($"[t={Clock}] {evento}");
        }

        public void IncrementarTrocasDeContexto()
        {
            TrocasDeContexto++;
        }

        public void AvancarTempo(int delta = 1)
        {
            Clock += delta;
        }

        public void AvancarTempoOcioso(int delta = 1)
        {
            Clock += delta;
            TempoOciosoCPU += delta;
        }

        public void ResetarMetricas()
        {
            Clock = 0;
            TrocasDeContexto = 0;
            TempoOciosoCPU = 0;
            Throughput = 0.0;

            foreach (var p in processos)
            {
                p.PC = 0;
                p.Estado = "Pronto";
                p.TempoRetorno = 0;
                p.TempoEspera = 0;
                p.TempoResposta = -1;
            }
        }
    }
}
