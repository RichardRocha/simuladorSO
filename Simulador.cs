using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SimuladorSO
{
    public class Simulador
    {
        private List<Processo> processos;
        private GerenciadorMemoria memoria;
        private List<DispositivoES> dispositivos;
        public DiretorioSimulado Raiz { get; private set; }
        public int TrocasDeContexto { get; set; } = 0;


        public Simulador()
        {
            processos = new List<Processo>();
            memoria = new GerenciadorMemoria(100); // 100 de RAM p/ teste

            dispositivos = new List<DispositivoES>
            {
                new DispositivoES("Disco"),
                new DispositivoES("Impressora")
            };
            Raiz = new DiretorioSimulado("root");
        }

        public List<Processo> Processos
        {
            get { return processos; }
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
            foreach (var p in processos)
            {
                p.ExibirInfo();
            }
        }

        public void Executar(Escalonador escalonador)
        {
            Console.WriteLine($"\n>>> Simulação com {escalonador.Algoritmo} <<<");

            // Tentar alocar memória antes de executar
            foreach (var p in processos)
            {
                bool alocou = memoria.Alocar(p);
                if (!alocou)
                {
                    p.Estado = "Bloqueado";
                    Console.WriteLine($"Processo {p.TempoChegada} bloqueado por falta de memória");
                }
            }

            // Executar escalonador
            escalonador.Executar(processos);

            // Liberar memória usando método separado
            LiberarMemoriaDeTodosProcessos();

            // Mostrar estado final
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
            }
        }
        public void CriarArquivo(string nome, int tamanho, DiretorioSimulado dir = null)
        {
            if (dir == null) dir = Raiz;
            dir.AdicionarArquivo(new ArquivoSimulado(nome, tamanho));
        }

        public void ListarDiretorio(DiretorioSimulado dir = null)
        {
            if (dir == null) dir = Raiz;
            dir.ListarArquivos();
        }

        public void LogEvento(string evento)
        {
            Console.WriteLine($"[Clock {DateTime.Now:T}] {evento}");
        }


    }
}
