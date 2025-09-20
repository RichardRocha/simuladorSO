namespace SimuladorSO
{
    public class ArquivoSimulado
    {
        public string Nome { get; private set; }
        public int Tamanho { get; set; } // em unidades fictícias
        public string Conteudo { get; set; } = string.Empty;

        public ArquivoSimulado(string nome, int tamanho)
        {
            Nome = nome;
            Tamanho = tamanho;
        }

        public void ExibirInfo()
        {
            Console.WriteLine($"Arquivo: {Nome}, Tamanho: {Tamanho}, Conteúdo: {Conteudo}");
        }
    }
}
