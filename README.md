# 📝 TestNote: Gerenciamento de Testes e Checklists

O TestNote é um aplicativo móvel (Android, iOS) e desktop (Windows, macOS) para gerenciamento de testes de qualidade ou checklists operacionais. Ele permite que gerentes criem testes detalhados com listas de verificação (checklists), e que funcionários os executem e reportem o progresso, salvando os resultados em um banco de dados local.

\<p align="center"\>
\<img width="500" height="300" alt="Exemplo da interface de Execução de Teste do TestNote" src="[Placeholder para Screenshot da Execução de Teste]" /\>
\</p\>

\<p align="center"\>
\<a href="\#about"\>📌 Sobre\</a\> •
\<a href="\#features"\>✨ Funcionalidades\</a\> •
\<a href="\#started"\>🚀 Como Executar\</a\> •
\<a href="\#architecture"\>📐 Arquitetura\</a\> •
\<a href="\#screenshots"\>📷 Screenshots\</a\>
\</p\>

-----

\<h2 id="about"\>📌 Sobre o Projeto\</h2\>

TestNote é uma aplicação **Cross-Platform** desenvolvida utilizando **.NET MAUI** e a arquitetura **MVVM (Model-View-ViewModel)**. O objetivo principal é fornecer uma ferramenta robusta para o gerenciamento do ciclo de vida de testes operacionais ou de qualidade, desde a **criação** pelo gerente até a **execução** e **monitoramento** pelo funcionário.

### Tecnologias Principais

  * **Linguagem:** C\#
  * **Framework:** .NET MAUI (.NET 8)
  * **Padrão de Projeto:** MVVM (usando CommunityToolkit.Mvvm)
  * **Banco de Dados:** SQLite (com sqlite-net-pcl)
  * **Persistência de Dados:** SQLite e Serialização de dados (`|` delimited) para checklists.

-----

\<h2 id="features"\>✨ Funcionalidades\</h2\>

O sistema é dividido em três perfis de acesso, cada um com responsabilidades e funcionalidades específicas:

### 1\. 👑 Administrador (Super Admin)

  * **Gerenciamento de Empresas:** CRUD completo de empresas.
  * **Gestão de Usuários:** Criação e gerenciamento de Gerentes e Funcionários.

### 2\. 👨‍💼 Gerente

  * **Criação de Testes:** Criação de novos testes, definindo Título, Descrição, Seção (`Section`) e o checklist de itens (`TestItems`).
  * **Visualização de Testes:** Acesso a todos os testes da sua empresa.
  * **Rastreamento:** Visualização do Status (`Pendente`, `Em Andamento`, `Concluído`) e quem executou (`TestedBy`).

### 3\. 👷 Funcionário

  * **Lista de Testes:** Visualiza os testes pendentes ou em andamento atribuídos à sua empresa.
  * **Execução de Teste:**
      * Marcação individual de itens do checklist.
      * Atualização de progresso em tempo real.
      * Adição de **Observações** (`ExecutionNotes`).
      * **Lógica de Status Inteligente:**
          * Ao marcar o primeiro item, o status muda para `Em Andamento`.
          * Ao desmarcar todos os itens, o status volta para `Pendente`.
          * Ao marcar todos os itens, o status muda para `Concluído`.
  * **Rastreabilidade:** Ao salvar, registra o nome do funcionário (`TestedBy`) e a data/hora da última execução.

-----

\<h2 id="started"\>🚀 Como Executar o Projeto\</h2\>

### Pré-requisitos

  * **Visual Studio 2022** (com a workload de .NET MAUI)
  * **SDK do .NET 8** ou superior

### Passo a Passo

1.  Clone o repositório:

    ```bash
    git clone https://github.com/RafaelHDSV/TestNote.git
    ```

2.  Entre na pasta do projeto:

    ```bash
    cd TestNote/TestNote
    ```

3.  Abra a solução (`TestNote.sln`) no Visual Studio 2022.

4.  **Rode a aplicação**

      * Selecione o alvo desejado (Windows Machine, Android Emulator, etc.).
      * Pressione F5 ou clique em "Start".

5.  **Acesso Inicial (Usuário Padrão)**

O banco de dados SQLite será criado automaticamente e populado com o usuário administrador padrão:

\<details\>
\<summary\>Usuário administrador para login\</summary\>

```
  Email = "admin@sistema.com",
  Senha = "123",
```

\</details\>

> Uma vez logado como administrador, você pode criar empresas, gerentes e funcionários para testar os diferentes perfis de acesso e o ciclo de vida dos testes.

-----

\<h2 id="architecture"\>📐 Arquitetura do Projeto\</h2\>

O projeto segue estritamente o padrão MVVM, o que facilita a separação de responsabilidades e a testabilidade do código.

  * **Views:** Camada de UI (páginas XAML) que contém o visual do aplicativo (Ex: `Login.xaml`, `TestExecutionPage.xaml`).
  * **ViewModels:** Camada de lógica de UI, responsável por expor dados do Model para a View e manipular comandos (Ex: `TestExecutionViewModel.cs`). Utiliza o **CommunityToolkit.Mvvm** para propriedades observáveis e comandos.
  * **Models:** Camada de dados e entidades do banco de dados (Ex: `Test.cs`, `User.cs`, `Employee.cs`).
  * **Services:** Camada de acesso a dados (persisistência) e lógica de negócio (Ex: `DatabaseService.cs`, `UserSession.cs`).

<!-- end list -->

```
TestNote/
├── Models/              # Entidades do SQLite (Test, User, Company, etc.)
├── Services/            # Lógica de Banco de Dados e Sessão (DatabaseService)
├── ViewModels/          # Lógica de UI (MVVM)
│   ├── TestExecutionViewModel.cs
│   └── LoginViewModel.cs
└── Views/               # Páginas XAML da Interface
    ├── Login.xaml
    ├── TestExecutionPage.xaml
    └── ... (outras páginas)
```

-----

\<h2 id="screenshots"\>📷 Screenshots\</h2\>

[Insira aqui as screenshots do seu projeto TestNote, como a tela de login, a lista de testes, e a tela de execução do checklist.]

\<img width="1086" height="816" alt="image" src="[https://www.youtube.com/watch?v=CdQpToy5hZQ](https://www.youtube.com/watch?v=CdQpToy5hZQ)" /\>
\<img width="1086" height="816" alt="image" src="[https://medium.com/android-dev-br/utilizando-testes-de-snapshot-2fa770903ed7](https://medium.com/android-dev-br/utilizando-testes-de-snapshot-2fa770903ed7)" /\>
\<img width="1086" height="816" alt="image" src="[https://www.researchgate.net/figure/Figura-5-Tela-de-Execucao-de-Plano-de-Teste-A-tela-de-execucao-de-plano-de-teste-contera\_fig5\_268163041](https://www.researchgate.net/figure/Figura-5-Tela-de-Execucao-de-Plano-de-Teste-A-tela-de-execucao-de-plano-de-teste-contera_fig5_268163041)" /\>

\<p align="center"\> Desenvolvido com ❤️ por [Seu nome de usuário(s) do GitHub]\</p\>
