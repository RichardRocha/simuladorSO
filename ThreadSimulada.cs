using System;

namespace SimuladorSO
{
    public class ThreadSimulada
    {
        public int Id { get; private set; }
        public int PC { get; set; }
        public string Estado { get; set; } = "Pronto";
        public Processo ProcessoPai { get; private set; }

        private static int _contador = 0;

        public ThreadSimulada(Processo pai)
        {
            Id = ++_contador;
            ProcessoPai = pai;
        }

        public void Executar()
        {
            Estado = "Executando";
            Console.WriteLine($"Thread {Id} do processo {ProcessoPai.Id} executando");
        }

        public void Finalizar()
        {
            Estado = "Finalizada";
            Console.WriteLine($"Thread {Id} finalizada");
        }
    }
}
