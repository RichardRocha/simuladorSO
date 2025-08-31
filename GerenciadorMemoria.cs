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

        public void MostrarStatus()
        {
            Console.WriteLine($"[Memória] Total: {MemoriaTotal}, Disponível: {MemoriaDisponivel}");
        }
    }
}
