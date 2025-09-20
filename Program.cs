namespace SimuladorSO
{
    public class Program
    {
        static void Main(string[] args)
        {
            Simulador sim = new Simulador();
            sim.GerarProcessos(3);
            sim.MostrarProcessos();

            // Solicitar E/S de uma thread aleatória
            var primeiraThread = sim.Processos[0].Threads[0];
            sim.SolicitarES(primeiraThread, "Disco");

            Console.WriteLine("\nFim da simulação");
        }
    }
}
