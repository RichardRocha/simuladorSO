using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorSO
{
    public class DiretorioSimulado
    {
        public string Nome { get; private set; }
        public List<ArquivoSimulado> Arquivos { get; private set; }
        public List<DiretorioSimulado> SubDiretorios { get; private set; }

        public DiretorioSimulado(string nome)
        {
            Nome = nome;
            Arquivos = new List<ArquivoSimulado>();
            SubDiretorios = new List<DiretorioSimulado>();
        }

        public void AdicionarArquivo(ArquivoSimulado arquivo)
        {
            Arquivos.Add(arquivo);
        }

        public void ListarArquivos()
        {
            Console.WriteLine($"\nConteúdo do diretório {Nome}:");
            foreach (var a in Arquivos)
            {
                a.ExibirInfo();
            }
            foreach (var d in SubDiretorios)
            {
                Console.WriteLine($"[DIR] {d.Nome}");
            }
        }

        // Busca recursiva por arquivo pelo nome
        public ArquivoSimulado? BuscarArquivo(string nome)
        {
            var arq = Arquivos.FirstOrDefault(a => a.Nome == nome);
            if (arq != null) return arq;

            foreach (var d in SubDiretorios)
            {
                var achou = d.BuscarArquivo(nome);
                if (achou != null) return achou;
            }

            return null;
        }

        // Remove arquivo pelo nome (no diretório atual ou subdiretórios)
        public bool RemoverArquivo(string nome)
        {
            var arq = Arquivos.FirstOrDefault(a => a.Nome == nome);
            if (arq != null)
            {
                Arquivos.Remove(arq);
                return true;
            }

            foreach (var d in SubDiretorios)
            {
                if (d.RemoverArquivo(nome))
                    return true;
            }

            return false;
        }
    }
}
