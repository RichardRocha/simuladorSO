using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorSO
{
    public class Frame
    {
        public int Index { get; private set; }
        public bool Free { get; set; } = true;
        public int? OwnerProcessId { get; set; } = null;

        public Frame(int idx)
        {
            Index = idx;
        }
    }

    public class GerenciadorMemoria
    {
        public int MemoriaTotal { get; private set; }
        public int PageSize { get; private set; }
        public int NumFrames { get; private set; }
        public List<Frame> Frames { get; private set; } = new List<Frame>();

        public int TotalAlocacoes { get; private set; } = 0;
        public int FalhasAlocacao { get; private set; } = 0;

        public GerenciadorMemoria(int memoriaTotal, int pageSize)
        {
            if (pageSize <= 0) throw new ArgumentException("pageSize deve ser > 0");
            MemoriaTotal = memoriaTotal;
            PageSize = pageSize;

            NumFrames = Math.Max(1, MemoriaTotal / PageSize);
            for (int i = 0; i < NumFrames; i++)
                Frames.Add(new Frame(i));
        }

        public int PagesNeeded(Processo p)
        {
            return (p.MemoriaNecessaria + PageSize - 1) / PageSize;
        }

        public bool Alocar(Processo p)
        {
            int need = PagesNeeded(p);

            var freeFrames = Frames.Where(f => f.Free).ToList();
            if (freeFrames.Count < need)
            {
                FalhasAlocacao++;
                return false;
            }

            var alocados = freeFrames.Take(need).ToList();
            foreach (var f in alocados)
            {
                f.Free = false;
                f.OwnerProcessId = p.Id;
                p.PageTable.Add(f.Index);
            }

            TotalAlocacoes++;
            return true;
        }

        public void Liberar(Processo p)
        {
            foreach (var f in Frames.Where(ff => ff.OwnerProcessId == p.Id))
            {
                f.Free = true;
                f.OwnerProcessId = null;
            }
            p.PageTable.Clear();
        }

        public void MostrarStatus()
        {
            Console.WriteLine($"[Memória] Total: {MemoriaTotal}, PageSize: {PageSize}, Frames: {NumFrames}, Alocações: {TotalAlocacoes}, Falhas: {FalhasAlocacao}");
            Console.WriteLine($"Frames livres: {Frames.Count(f => f.Free)} / {NumFrames}");
        }
    }
}
