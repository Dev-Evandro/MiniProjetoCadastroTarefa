# 📌 Mini Projeto - Cadastro de Tarefas

## 📖 Sobre o Projeto

Este projeto tem como objetivo o controle de tarefas e suas etapas, permitindo organizar atividades de forma simples e estruturada.

Atualmente o sistema já permite:

* ✅ Cadastrar tarefas
* ✅ Editar tarefas
* ✅ Inativar (excluir logicamente) tarefas
* ✅ Consultar tarefas (ativas ou inativas)
* ✅ Cadastrar etapas dentro de uma tarefa
* ✅ Editar e inativar etapas

---

## 🧩 Funcionalidades atuais

### 📝 Tarefas

* Cadastro de tarefas com:

  * Título
  * Descrição
  * Datas (início, prevista, fim)
  * Status
  * Desenvolvedor
* Listagem com filtro por título
* Filtro por ativo/inativo
* Edição e inativação (exclusão lógica)

---

### 🔹 Etapas

* Cada tarefa pode conter várias etapas
* Cadastro de etapas com:

  * Título
  * Descrição
  * Datas
  * Ordem
* Cálculo automático de **tempo restante**
* Inativação de etapas

---

## 🚀 Próximas evoluções

O projeto ainda está em desenvolvimento. Algumas melhorias planejadas:

* 📊 Dashboard com:

  * Tempo médio por tarefa
  * Quantidade de tarefas concluídas
  * Acompanhamento de produtividade
* 🧑‍💻 Cadastro de Desenvolvedores (atualmente fixo em classe)
* 📌 Cadastro de Status (atualmente fixo em classe)
* 🔄 Melhorias na arquitetura com MVVM completo

---

## ⚙️ Configuração do Sistema

O sistema busca automaticamente a configuração do banco de dados no seguinte diretório:

```
C:\PDVFinanceiro\Config
```

### 📄 Arquivo de configuração

O arquivo deve conter a seguinte estrutura:

```xml
<configuracao>
    <nomeInstancia>dev-evandro\pdvnet</nomeInstancia>
    <nomeUsuario>sa</nomeUsuario>
    <nomeSenha>senha</nomeSenha>
    <nomeBanco>EvandroEdgariano</nomeBanco>
</configuracao>
```

---

## 🗄️ Banco de Dados

### 🔧 SGBD utilizado

* SQL Server

### 📊 Estrutura atual

#### Tabela: TAREFA

Responsável por armazenar as tarefas principais do sistema.

#### Tabela: ETAPA

Responsável por armazenar as etapas vinculadas a uma tarefa.

---

### 🔮 Estrutura futura

As seguintes entidades ainda serão persistidas no banco:

* Desenvolvedor
* Status
* (Possível evolução de Etapas com mais detalhes)

Atualmente essas informações estão sendo controladas via classes no sistema.

---

## 💾 Backup do Banco

Você pode baixar o backup do banco de dados no link abaixo:

👉 https://1drv.ms/u/c/4434e7202289c105/IQBVf7kymw5-TI9lygiYeCaCAeQq4Y_3J-Q0Nw0QL7DfGvg?e=wk9ktV

---

## 📚 Outros Projetos

Este repositório também contém outros projetos desenvolvidos durante estudos, principalmente relacionados a:

* LINQ
* Manipulação de dados
* Exercícios práticos

---

## 🧠 Observações

* O projeto está em evolução contínua
* Parte da arquitetura já está migrando para o padrão **MVVM**
* O objetivo também é evolução profissional e adaptação a práticas de mercado modernas

---

## 👨‍💻 Autor

Projeto desenvolvido por Evandro, com foco em evolução técnica e transição de tecnologias legadas para padrões atuais.
