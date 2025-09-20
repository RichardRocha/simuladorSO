using System;
using System.Collections.Generic;

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
    }
}
