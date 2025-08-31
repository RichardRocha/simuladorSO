using System;

namespace SimuladorSO
{
    public class Processo
    {
        public int TempoChegada { get; set; }
        public int TempoProcessamento { get; set; }
        public int MemoriaNecessaria { get; set; }

        public Processo(int chegada, int processamento, int memoria)
        {
            TempoChegada = chegada;
            TempoProcessamento = processamento;
            MemoriaNecessaria = memoria;
        }

        public void ExibirInfo()
        {
            Console.WriteLine($"[Processo] Chegada: {TempoChegada}, Execução: {TempoProcessamento}, Memória: {MemoriaNecessaria}");
        }
    }
}
