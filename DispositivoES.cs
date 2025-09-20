namespace SimuladorSO
{
    public class DispositivoES
    {
        public string Nome { get; private set; }

        public DispositivoES(string nome)
        {
            Nome = nome;
        }

        public void Solicitar(ThreadSimulada thread)
        {
            Console.WriteLine($"Thread {thread.Id} do processo {thread.ProcessoPai.TempoChegada} solicitou E/S no dispositivo {Nome}");
            thread.Estado = "Bloqueada";

            // Simulação imediata: após a "E/S", thread volta a pronto
            thread.Estado = "Pronto";
            Console.WriteLine($"Thread {thread.Id} do processo {thread.ProcessoPai.TempoChegada} concluiu E/S no dispositivo {Nome}");
        }
    }
}
