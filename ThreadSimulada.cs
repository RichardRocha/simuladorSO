public class ThreadSimulada
{
    public int Id { get; private set; }
    public Processo ProcessoPai { get; private set; }
    public string Estado { get; set; } = "Pronto"; // Estado da thread
    public int PC { get; set; } = 0; // Contador de programa da thread
    public int QuantumExecutado { get; set; } = 0; // para RR

    public Stack<int> Pilha { get; private set; } = new Stack<int>(); // Pilha lógica simulada

    private static int contador = 0;

    public ThreadSimulada(Processo processoPai)
    {
        Id = ++contador;
        ProcessoPai = processoPai;
    }

    public void Executar()
    {
        Estado = "Executando";
        Console.WriteLine($"Thread {Id} do processo {ProcessoPai.TempoChegada} executando");
    }

    public void Bloquear()
    {
        Estado = "Bloqueada";
        Console.WriteLine($"Thread {Id} bloqueada");
    }

    public void Finalizar()
    {
        Estado = "Finalizada";
        Console.WriteLine($"Thread {Id} finalizada");
    }
}
