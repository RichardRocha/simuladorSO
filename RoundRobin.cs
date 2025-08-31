namespace SimuladorSO
{
    public class RoundRobin : Escalonador
    {
        public int Quantum { get; private set; }

        public RoundRobin(int quantum)
        {
            Algoritmo = "Round Robin";
            Quantum = quantum;
        }

    }
}
