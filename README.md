MiniProjetoCadastroTarefa
📌 Sobre o Projeto

O MiniProjetoCadastroTarefa é um sistema desktop desenvolvido em C# com WPF, com foco no gerenciamento de tarefas e etapas.

O projeto surgiu como estudo e evolução técnica, aplicando conceitos modernos de desenvolvimento como:

Arquitetura em camadas
MVVM
Separação de responsabilidades
Organização por projetos
Boas práticas de estruturação
🚀 Funcionalidades Atuais
📝 Cadastro de Tarefas

O sistema permite:

✅ Cadastrar tarefas
✅ Editar tarefas
✅ Inativar tarefas (exclusão lógica)
✅ Consultar tarefas ativas e inativas
✅ Filtrar tarefas por título
✅ Controle de status
✅ Controle de desenvolvedor responsável
✅ Controle de datas:
Início
Previsão
Finalização
🔹 Cadastro de Etapas

Cada tarefa pode possuir várias etapas vinculadas.

Funcionalidades:

✅ Cadastro de etapas
✅ Edição de etapas
✅ Inativação lógica
✅ Controle de datas
✅ Cálculo automático de tempo restante
🏗️ Arquitetura do Projeto

O sistema foi reorganizado utilizando arquitetura em camadas, visando:

Melhor organização
Separação de responsabilidades
Facilidade de manutenção
Reaproveitamento de código
Evolução futura do sistema
📂 Estrutura da Solution
Projeto	Responsabilidade
UI	Interface gráfica WPF (Views, ViewModels, Commands)
Core	Models, enums e contratos da aplicação
Application	Regras de negócio e Services
Infrastructure	Acesso a dados e configurações
🔄 Fluxo da Aplicação
UI → Application → Infrastructure
        ↓
      Core
🎨 Interface

O sistema utiliza:

WPF
MVVM parcial
Estilização customizada
Separação visual por telas
Componentização de comandos
⚙️ Configuração do Sistema

O sistema carrega automaticamente o arquivo de configuração localizado em:

PDVFinanceiro\Config\config.xml

O caminho é baseado automaticamente no diretório da aplicação utilizando:

AppDomain.CurrentDomain.BaseDirectory
📄 Estrutura do arquivo config.xml
<configuracao>
    <nomeInstancia>SERVIDOR\SQL</nomeInstancia>
    <nomeUsuario>sa</nomeUsuario>
    <nomeSenha>senha</nomeSenha>
    <nomeBanco>MiniProjeto</nomeBanco>
</configuracao>
🗄️ Banco de Dados
🔧 SGBD Utilizado
SQL Server
📊 Estrutura Atual
TAREFA

Responsável pelo armazenamento das tarefas principais.

ETAPA

Responsável pelo armazenamento das etapas vinculadas às tarefas.

🔮 Evoluções Futuras

Planejamentos futuros do sistema:

📊 Dashboard administrativo
📈 Indicadores financeiros e produtividade
👨‍💻 Cadastro de desenvolvedores
📌 Cadastro de status
🔐 Controle de usuários e permissões
📋 Melhorias completas em MVVM
💉 Injeção de dependência (Dependency Injection)
🧪 Testes unitários
💾 Backup do Banco

Backup disponível em:

👉 https://1drv.ms/u/c/4434e7202289c105/IQBVf7kymw5-TI9lygiYeCaCAeQq4Y_3J-Q0Nw0QL7DfGvg?e=wk9ktV

📚 Objetivo do Projeto

Este projeto tem como foco:

Evolução técnica
Aprendizado de arquitetura moderna
Migração de conceitos legados para padrões atuais
Aplicação prática de WPF + MVVM
👨‍💻 Autor

Desenvolvido por Evandro Edgariano
Analista de Sistemas / Desenvolvedor

# 🏗️ Arquitetura da Solução

<p align="center">
 <img src="docs/images/camadasProjeto.png" width="700"/>
</p>

O sistema foi dividido em camadas visando melhor manutenção,
organização e reaproveitamento de código.
