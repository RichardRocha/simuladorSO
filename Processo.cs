
public class Processo
{
    public int TempoChegada { get; set; }
    public int TempoProcessamento { get; set; }
    public int MemoriaNecessaria { get; set; }
    public int TempoRetorno { get; set; } = 0;
    public int TempoEspera { get; set; } = 0;

    // Estado como string
    public string Estado { get; set; } = "Pronto";

    // Registradores simulados
    public int[] Registradores { get; private set; } = new int[8];
    // Contador de programa
    public int PC { get; set; } = 0;
    // Lista de arquivos abertos
    public List<string> ArquivosAbertos { get; private set; } = new List<string>();
    public List<ThreadSimulada> Threads { get; private set; } = new List<ThreadSimulada>();
    public Processo(int chegada, int processamento, int memoria)
    {
        TempoChegada = chegada;
        TempoProcessamento = processamento;
        MemoriaNecessaria = memoria;
    }

    public void CriarThread()
    {
        var thread = new ThreadSimulada(this);
        Threads.Add(thread);
    }
    public void ExibirInfo()
    {
        Console.WriteLine($"[Processo] Chegada: {TempoChegada}, Execução: {TempoProcessamento}, Memória: {MemoriaNecessaria}, Estado: {Estado}, PC: {PC}");
    }

    // Método para executar o processo
    public void Executar()
    {
        Estado = "Executando";
        Console.WriteLine($"Processo iniciado: PC={PC}");
    }

    // Método para bloquear o processo (ex: esperando E/S)
    public void Bloquear()
    {
        Estado = "Bloqueado";
        Console.WriteLine("Processo bloqueado");
    }

    // Método para finalizar o processo
    public void Finalizar()
    {
        Estado = "Finalizado";
        Console.WriteLine("Processo finalizado");
    }
}
