# 💊 SneezePharma — Console Application

> **SneezePharma** é um sistema de gerenciamento de farmácia de manipulação desenvolvido em **C# (.NET Console App)**.  
> O projeto foi criado para simular o controle de cadastros, estoque e manipulação de medicamentos, aplicando conceitos de **programação estruturada, orientação a objetos e regras de negócio**.

---

## 📈 Status do Projeto

| Versão | Início | Conclusão | Contexto |
|:-------:|:-------:|:-----------:|:----------:|
| **v1.1** | 21/10/2025 | 27/10/2025 | Avaliação em Equipe — C# Básico |

---

## 🗂️ Sumário

1. [🎯 Objetivos do Projeto](#-objetivos-do-projeto)  
2. [🧩 Módulos do Sistema](#-módulos-do-sistema)  
3. [⚙️ Requisitos e Regras de Negócio](#️-requisitos-e-regras-de-negócio)  
4. [🚀 Como Executar o Projeto](#-como-executar-o-projeto)  
5. [👥 Equipe de Desenvolvimento](#-equipe-de-desenvolvimento)

---

## 🎯 Objetivos do Projeto

### 🎯 Objetivo Geral  
Desenvolver um sistema que gerencie, de forma integrada, os processos de uma **farmácia de manipulação**, incluindo o controle de clientes, médicos, insumos, receitas e medicamentos manipulados.

### 🎯 Objetivos Específicos
- 📋 Implementar cadastros de **Clientes, Fornecedores, Médicos** e **Princípios Ativos**.  
- 🧾 Controlar a **compra** dos princípios ativos.  
- ⚗️ Gerenciar a **manipulação de medicamentos** com base em receitas médicas.  
- ✅ Garantir **validações, consistência de dados** e aplicação das **regras de negócio** em todas as operações.

---

## 🧩 Módulos do Sistema

O sistema é dividido em **três módulos principais**, que interagem entre si para formar um fluxo de trabalho completo.

### 1. 👥 Cadastros Básicos  
Gerencia as entidades fundamentais do sistema:

- **Clientes:** controle de pacientes e validação de idade mínima (18+).  
- **Fornecedores:** cadastro de laboratórios e distribuidores de insumos.  
- **Médicos:** registro de médicos com CRM para emissão de receitas válidas.  
- **Princípios Ativos:** cadastro e controle de matérias-primas usadas nas manipulações.

---

### 2. 🛒 Compra   
Administra o fluxo de aquisição e armazenamento dos insumos.

- **Pedidos de Compra:** lançamentos vinculados aos fornecedores.  
- **Entrada de Estoque:** atualização automática do estoque após recebimento.  
- **Limites de Itens:** o sistema impõe limites configurados para evitar inconsistências.

---

### 3. ⚗️ Manipulação de Medicamentos  
Responsável pela produção e rastreabilidade dos medicamentos manipulados.

- **Receitas Médicas:** registro vinculado a um Cliente e um Médico.  
- **Ordem de Manipulação:** baixa automática de insumos conforme a fórmula.  
- **Medicamento Final:** geração do produto pronto, com rastreabilidade de origem e datas.

---

## ⚙️ Requisitos e Regras de Negócio

### 🔧 Requisitos Técnicos

- **Interface:** 100% via **Console (CLI)**, com menus intuitivos e validações em todas as entradas.  
- **Validação de Dados:** impede valores nulos, formatos incorretos e entradas inválidas.  
- **Integridade do Sistema:** impede ações que violem o fluxo de negócio (ex: manipular sem estoque disponível).  

---

### 📜 Regras de Negócio Principais

#### 🧱 Exclusão Lógica  
Nenhum registro é excluído fisicamente. Todas as entidades possuem o campo:
```csharp
bool Ativo
```
Indicando se o registro está ativo ou inativo.

#### 📦 Controle de Limites  
- Limites de quantidade por pedido de compra e manipulação.  

#### ⏱️ Controle de Datas  
Cada entidade possui:
```csharp
DateTime DataCriacao
DateTime DataAlteracao
```
Gerenciadas automaticamente nas operações de cadastro e atualização.

#### 👤 Validação de Idade  
Clientes devem ter **18 anos ou mais** para serem cadastrados como responsáveis.

---

## 🚀 Como Executar o Projeto

Este é um **Console Application em .NET**.  
Para executar o projeto localmente:

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/krysgh/SneezePharma.git
   ```

2. **Abra a solução no Visual Studio ou VS Code:**
   ```
   SneezePharma.sln
   ```

3. **Compile o projeto (Build):**
   - No Visual Studio: pressione `Ctrl + Shift + B`  
   - Ou via terminal:
     ```bash
     dotnet build
     ```

4. **Execute a aplicação:**
   ```bash
   dotnet run
   ```

---


## 🧾 Licença

Este projeto foi desenvolvido para fins **educacionais** e não possui licença comercial.  
Sinta-se livre para estudar, melhorar e adaptar o código.

---

## 🧠 Tecnologias Utilizadas

| Tecnologia | Descrição |
|-------------|------------|
| 💻 **C# (.NET 8.0)** | Linguagem e framework utilizados |
| 🧩 **POO** | Paradigma aplicado ao design do sistema |
| 🧮 **Console App** | Interface via linha de comando |
| 🧱 **Validações e Regras de Negócio** | Implementadas em todas as camadas |
