using System;

namespace SimuladorSO
{
    public class GerenciadorMemoria
    {
        public int MemoriaTotal { get; private set; }
        public int MemoriaDisponivel { get; private set; }

        public GerenciadorMemoria(int memoriaTotal)
        {
            MemoriaTotal = memoriaTotal;
            MemoriaDisponivel = memoriaTotal;
        }

        public bool Alocar(Processo p)
        {
            if (p.MemoriaNecessaria <= MemoriaDisponivel)
            {
                MemoriaDisponivel -= p.MemoriaNecessaria;
                Console.WriteLine($"Memória alocada para processo (Memória usada: {p.MemoriaNecessaria})");
                return true;
            }
            else
            {
                Console.WriteLine($"Memória insuficiente para processo (Necessário: {p.MemoriaNecessaria}, Disponível: {MemoriaDisponivel})");
                return false;
            }
        }

        public void Liberar(Processo p)
        {
            MemoriaDisponivel += p.MemoriaNecessaria;
            Console.WriteLine($"Memória liberada do processo (Memória liberada: {p.MemoriaNecessaria})");
        }

        public void MostrarStatus()
        {
            Console.WriteLine($"[Memória] Total: {MemoriaTotal}, Disponível: {MemoriaDisponivel}");
        }
    }

}
