## Grupo:
### Nome: Richard Nicholas Rocha | RA: 113760
### Nome: Arlyson Silva Nascimento | RA: 113627
### Nome: Kauan Melo | RA: 113471

# Simulador de Sistema Operacional

**SimuladorSO** é uma aplicação desenvolvida em C# que visa a simulação detalhada de escalonamento de processos e gerenciamento de memória em sistemas operacionais. Este projeto oferece uma plataforma para estudo, experimentação e análise de algoritmos clássicos de escalonamento, proporcionando uma visão prática de como os sistemas operacionais gerenciam processos e recursos de memória.

## Funcionalidades Principais

- **Escalonamento de Processos**
  - Implementa algoritmos como **Round Robin (RR)** e **Shortest Job Next (SJN)**
  - Estrutura modular que permite a adição de novos algoritmos de escalonamento

- **Gerenciamento de Memória**
  - Alocação e liberação eficiente de memória para processos simulados
  - Controle rigoroso dos estados dos processos (ativos, bloqueados, finalizados)

- **Simulação Interativa**
  - Execução passo a passo, permitindo observação detalhada do comportamento do escalonador e da gestão de memória
  - Registro de eventos relevantes, como troca de contexto e finalização de processos

## Arquitetura do Sistema

O projeto é estruturado em classes orientadas a objetos, garantindo modularidade, legibilidade e extensibilidade:

- `Processo`: Representa um processo do sistema, com atributos como ID, nome, tempo de execução, prioridade e memória requerida.
- `Escalonador`: Classe abstrata responsável por definir a interface e comportamento básico de qualquer algoritmo de escalonamento.
- `RoundRobin` e `ShortestJobNext`: Implementações concretas de algoritmos de escalonamento.
- `GerenciadorMemoria`: Gerencia a alocação e liberação de memória dos processos, controlando o estado da memória disponível.
- `Simulador`: Classe central que integra escalonamento e memória, coordenando a execução da simulação de forma coesa.

## Execução do Projeto

1. Certifique-se de possuir o [.NET SDK](https://dotnet.microsoft.com/en-us/download) instalado.
2. Clone o repositório:  
   ```bash
   git clone https://github.com/RichardRocha/simuladorSO.git
3. Acesse o diretório do projeto:
   ```bash
   cd simuladorSO
5. Compile e execute a aplicação:
   ```bash
   dotnet build
   dotnet run
## UML do programa
![Diagrama UML do SimuladorSO](img/uml.png)
