## Grupo:
### Nome: Richard Nicholas Rocha | RA: 113760
### Nome: Arlyson Silva Nascimento | RA: 113627
### Nome: Kauan Melo | RA: 113471

# Simulador de Sistema Operacional

**SimuladorSO** é uma aplicação desenvolvida em C# que visa a simulação detalhada de escalonamento de processos, threads, gerenciamento de memória, E/S e sistema de arquivos em sistemas operacionais.  
Este projeto oferece uma plataforma para estudo, experimentação e análise de algoritmos clássicos de escalonamento, proporcionando uma visão prática de como os sistemas operacionais gerenciam processos, recursos de memória e dispositivos.

---

# Funcionalidades Principais

## ✔️ Menu interativo
- Interface de linha de comando com opções claras
- Permite gerar processos, exibir processos, executar escalonamento e ver métricas
- Validação de entradas do usuário para evitar erros

---

## ✔️ Escalonamento de Processos e Threads
- Round Robin (RR) com quantum configurável  
- Shortest Job First (SJF)  
- Shortest Process Next (SPN)  
- First-Come First-Served (FCFS)  
- Contagem real de trocas de contexto  
- Logs detalhados da execução  
- Threads com estados: **Pronto**, **Executando**, **Bloqueado**, **Finalizado**

---

## ✔️ Processos (PCB)
Cada processo possui:

- PID incremental
- Tempo de chegada
- Tempo de execução
- Memória necessária
- Prioridade
- Registradores simulados
- Contador de programa (PC)
- Estado atual
- Threads internas
- Tabela de arquivos abertos (simplificada)

Estados possíveis:
- **Pronto**
- **Executando**
- **Bloqueado**
- **Finalizado**

---

## ✔️ Threads Simuladas
- Identificador próprio  
- Referência ao processo pai  
- Contador de programa (PC)  
- Estado  
- Execução simulada por quantum  
- Mudança automática para Finalizada quando a execução termina  

---

## ✔️ Gerenciamento de Memória
- Paginação simples com:
  - Memória total configurável
  - Tamanho de página configurável
  - Frames (molduras) livres e ocupados
- Page table por processo
- Política de alocação: **First Fit**
- Processos são bloqueados se não houver memória suficiente
- Liberação completa da memória ao final da execução
- Estatísticas:
  - Total de alocações
  - Falhas de alocação (interpretação como falta de página)

---

## ✔️ Entrada e Saída (E/S)
Dispositivos simulados:
- Disco
- Impressora

Funcionalidades:
- E/S bloqueante sem busy waiting
- Fila interna de solicitações
- Mudança automática de estado da thread (Bloqueada → Pronto)
- Logs detalhados de início e fim de operação

---

## ✔️ Sistema de Arquivos Simulado
- Diretório raiz `"root"`
- Suporte a:
  - Criação de arquivos
  - Listagem de arquivos
- Estrutura hierárquica com diretórios e arquivos
- Metadados:
  - Nome
  - Tamanho

---

## ✔️ Métricas e Logs
Ao final da simulação, o sistema exibe:

- Tempo de retorno
- Tempo de espera
- Tempo de resposta
- Prioridade
- Trocas de contexto
- Clock total
- CPU ociosa
- Utilização da CPU (%)
- Throughput
- Estatísticas de memória:
  - Frames livres
  - Total de alocações
  - Falhas de alocação
- Log completo com timestamps lógicos (t)

---

# Arquitetura do Sistema

O projeto utiliza programação orientada a objetos, com as seguintes classes principais:

- `Processo`
- `ThreadSimulada`
- `Escalonador` (abstrata)
- `RoundRobin`, `ShortestJobFirst`, `ShortestProcessNext`, `FCFS`
- `GerenciadorMemoria`, `Frame`
- `DispositivoES`
- `ArquivoSimulado`, `DiretorioSimulado`
- `Simulador`
- `Program` (menu interativo)

Cada módulo é desacoplado e segue boas práticas de encapsulamento.

---

# Como Executar

### Pré-requisitos
- .NET 6 ou superior  
Baixe em: https://dotnet.microsoft.com/en-us/download

### Passos:

```bash
git clone https://github.com/RichardRocha/simuladorSO.git
cd simuladorSO
dotnet build
dotnet run
```
```bash
#  UML do Programa
![Diagrama UML do SimuladorSO](img/uml.png)
``` 
