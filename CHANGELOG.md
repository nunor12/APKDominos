# 📝 Changelog - SAFTExtractor

Todas as alterações notáveis deste projeto serão documentadas neste ficheiro.

O formato é baseado em [Keep a Changelog](https://keepachangelog.com/pt/1.0.0/),
e este projeto adere ao [Semantic Versioning](https://semver.org/).

## [1.0.0] - 2026-02-11

### 🎉 Versão Inicial

Primeira versão funcional completa da aplicação SAFTExtractor para extração de dados SAFT do sistema PULSE DOMINOS.

### ✨ Adicionado

#### Funcionalidades Core
- Aplicação Windows Forms completa em C# .NET 8.0
- Interface gráfica intuitiva para configuração e geração de SAFT
- Conexão a SQL Server (PULSE DOMINOS)
- Suporte para Autenticação Windows e SQL Server
- Teste de conexão à base de dados
- Seleção de ano fiscal
- Geração de ficheiros SAFT-PT XML versão 1.04_01
- Extração de dados via Stored Procedures

#### Classes e Componentes
- **SAFTAdditionForm** - Formulário principal da aplicação
- **SAFTAdditionStartup** - Lógica de arranque e autenticação
- **SAFTXMLDetails** - Classe crítica para extração de dados das SPs
- **SAFTXMLObject** - Serialização XML conforme schema SAFT-PT
- **SAFTDate** - Utilitários para processamento de datas e anos fiscais
- **DatabaseConfig** - Configuração de conexão à base de dados

#### Modelos de Dados
- **Customer** - Cliente com todos os campos SAFT + extensíveis
- **Product** - Produto/Artigo com campos SAFT + extensíveis
- **Invoice** - Fatura/Documento com linhas e totais
- **InvoiceLine** - Linha de fatura com impostos
- **SAFTHeader** - Cabeçalho do ficheiro SAFT
- **CompanyAddress** - Endereço da empresa
- **SAFTPTSourceBilling** - Enum de tipos de documento

#### Estrutura XML Gerada
- Header completo (informações da empresa)
- MasterFiles (Clientes e Produtos)
- SourceDocuments > SalesInvoices (Faturas)
- Conformidade com schema SAFT-PT 1.04_01

#### Documentação Completa
- **README.md** (300+ linhas)
  - Descrição detalhada do projeto
  - Arquitetura e estrutura de pastas
  - Como usar a aplicação
  - Guia completo: como adicionar novos campos
  - Estrutura do ficheiro SAFT
  - Segurança e validação
  
- **STORED_PROCEDURES.md** (300+ linhas)
  - 5 Stored Procedures necessárias com código SQL completo
  - spGetCustomersForSAFTXML
  - spGetProductsForSAFTXML
  - spGetInvoicesForSAFTXML
  - spGetInvoiceLinesForSAFTXML
  - spGetSAFTHeader
  - Scripts de teste
  - Checklist de campos obrigatórios
  
- **QUICK_START.md** (250+ linhas)
  - Guia de configuração rápida em 5 passos
  - Compilação da aplicação
  - Configuração da base de dados
  - Primeiro ficheiro SAFT
  - Validação com AT
  - Resolução de problemas
  - Checklist final
  
- **CONFIGURATION_EXAMPLES.md** (200+ linhas)
  - Exemplos de configuração de conexão
  - Autenticação Windows e SQL Server
  - Configurações para Azure SQL
  - Boas práticas de segurança
  - Permissões SQL necessárias
  - Nomenclatura de ficheiros
  
- **FAQ.md** (350+ linhas)
  - 50+ perguntas e respostas
  - Categorias: Geral, Instalação, BD, Geração, Segurança, Erros, etc.
  - Troubleshooting detalhado
  - Dicas de performance

#### Qualidade de Código
- Código limpo e bem estruturado
- Separação clara de responsabilidades
- Comentários XML em todas as classes públicas
- Tratamento de exceções adequado
- Uso de nullable reference types (.NET 8)
- Arquitetura extensível para novos campos

#### Ferramentas e Configuração
- .gitignore completo para projetos C#
- Estrutura de solução Visual Studio
- Configuração de projeto .NET 8.0
- Suporte para compilação cross-platform (EnableWindowsTargeting)

### 📊 Estatísticas

- **13** ficheiros de código C# (1285 linhas)
- **5** ficheiros de documentação (1156 linhas)
- **17** componentes principais
- **5** stored procedures de exemplo
- **4** modelos de dados principais

### 🎯 Casos de Uso Suportados

1. ✅ Extração de dados SAFT de ano fiscal completo
2. ✅ Conexão segura à base de dados PULSE DOMINOS
3. ✅ Geração de ficheiro XML conforme schema AT
4. ✅ Adição fácil de novos campos (documentado)
5. ✅ Validação de configuração antes da geração
6. ✅ Tratamento de erros com mensagens claras

### 🔒 Segurança

- Suporte para Autenticação Windows (recomendado)
- Sem armazenamento de passwords
- Validação de conexão antes de operações
- Permissões mínimas necessárias (apenas leitura)
- Documentação de boas práticas de segurança

### 📦 Dependências

- .NET 8.0 (net8.0-windows)
- Windows Forms
- System.Data.SqlClient 4.8.6

### 🚀 Como Usar

1. Clone o repositório
2. Abra no Visual Studio 2022
3. Compile o projeto
4. Crie as Stored Procedures na BD (ver STORED_PROCEDURES.md)
5. Execute a aplicação
6. Configure e teste a conexão
7. Gere o ficheiro SAFT
8. Valide com o validador da AT

### 📝 Notas

- Esta é a primeira versão funcional completa
- Pronta para uso em produção após configuração das SPs
- Documentação completa incluída
- Código-fonte totalmente comentado
- Arquitetura preparada para evolução

### 🙏 Agradecimentos

Desenvolvido para facilitar a extração de dados SAFT do sistema PULSE DOMINOS, em conformidade com os requisitos da Autoridade Tributária portuguesa.

---

## [Unreleased] - Funcionalidades Futuras

### Planeado para próximas versões

- [ ] Ficheiro de configuração JSON persistente
- [ ] Suporte a múltiplas empresas
- [ ] Geração de períodos personalizados (não apenas ano fiscal completo)
- [ ] Validação automática integrada com API da AT
- [ ] Exportação para diferentes versões SAFT (1.03, 1.04)
- [ ] Interface de linha de comando (CLI)
- [ ] Logs detalhados de operação
- [ ] Histórico de ficheiros gerados
- [ ] Preview do XML antes de guardar
- [ ] Estatísticas de dados extraídos
- [ ] Suporte a outras bases de dados (MySQL, PostgreSQL)
- [ ] Testes unitários e de integração
- [ ] Publicação como executável autónomo

---

**Para reportar bugs ou sugerir funcionalidades, abra uma issue no GitHub.**

**Versão atual: 1.0.0**
