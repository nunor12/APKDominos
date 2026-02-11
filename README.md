# APKDominos - Extração de Dados SAFT do PULSE DOMINOS

Aplicação C# WinForms para extração de dados SAFT (Standard Audit File for Tax) do sistema PULSE DOMINOS.

## 📋 Descrição

Esta aplicação permite extrair dados do sistema PULSE DOMINOS e gerar ficheiros SAFT-PT conformes com o schema da Autoridade Tributária (AT) portuguesa. O ficheiro gerado pode ser validado e submetido à AT.

## 🚀 Funcionalidades

- **Conexão à Base de Dados**: Suporte para autenticação Windows ou SQL Server
- **Extração de Dados**: Extrai clientes, produtos e documentos (faturas) do PULSE DOMINOS
- **Geração de SAFT**: Cria ficheiros XML no formato SAFT-PT 1.04_01
- **Configuração Flexível**: Permite selecionar ano fiscal para extração
- **Campos Extensíveis**: Arquitetura preparada para adicionar novos campos facilmente

## 🏗️ Arquitetura do Projeto

### Estrutura de Pastas

```
SAFTExtractor/
├── Enums/
│   └── SAFTPTSourceBilling.cs      # Tipos de documentos
├── Forms/
│   ├── SAFTAdditionForm.cs         # Formulário principal
│   └── SAFTAdditionForm.Designer.cs
├── Models/
│   ├── Customer.cs                  # Modelo de cliente
│   ├── Product.cs                   # Modelo de produto
│   ├── Invoice.cs                   # Modelo de fatura/documento
│   └── SAFTHeader.cs                # Cabeçalho SAFT
├── Services/
│   ├── SAFTXMLDetails.cs           # **Classe crítica** - Extração de dados
│   ├── SAFTXMLObject.cs            # Serialização XML
│   └── SAFTAdditionStartup.cs      # Arranque e autenticação
└── Utils/
    ├── DatabaseConfig.cs            # Configuração de BD
    └── SAFTDate.cs                  # Utilitários de data
```

### Classes Principais

#### 1. **SAFTXMLDetails** (Crítica)
Classe que extrai dados das Stored Procedures e monta a estrutura SAFT.

#### 2. **SAFTXMLObject**
Recebe dados da `SAFTXMLDetails` e faz a serialização para XML conforme schema SAFT-PT.

#### 3. **SAFTAdditionForm**
Interface gráfica para configuração e geração do ficheiro SAFT.

## 📝 Como Usar

### 1. Requisitos

- .NET 8.0 ou superior
- Windows Forms
- SQL Server (PULSE DOMINOS)
- Visual Studio 2022 ou superior (recomendado)

### 2. Configuração

1. Abra o projeto no Visual Studio
2. Compile a solução
3. Execute a aplicação

### 3. Geração de Ficheiro SAFT

1. **Configure a Conexão**:
   - Servidor: Nome do servidor SQL
   - Base de Dados: Nome da BD PULSE DOMINOS
   - Utilizador/Password (ou marque "Autenticação Windows")
   - Clique em "Testar Conexão"

2. **Gerar SAFT**:
   - Selecione o Ano Fiscal
   - Clique em "Gerar Ficheiro SAFT"
   - Escolha o local para salvar o ficheiro XML

## 🔧 Como Adicionar Novos Campos

### Passo 1: Atualizar Stored Procedures

Adicione os novos campos às suas Stored Procedures:

```sql
-- Exemplo: spGetCustomersForSAFTXML
ALTER PROCEDURE spGetCustomersForSAFTXML
AS
BEGIN
    SELECT 
        CustomerID,
        AccountID,
        CustomerTaxID,
        CompanyName,
        -- ... campos existentes ...
        NIFRepresentante  -- NOVO CAMPO
    FROM Customers
END
```

### Passo 2: Atualizar Models

Adicione a propriedade na classe correspondente:

```csharp
// Models/Customer.cs
public class Customer
{
    // ... campos existentes ...
    
    // Campos extensíveis
    public string? NIFRepresentante { get; set; }  // NOVO CAMPO
}
```

### Passo 3: Atualizar SAFTXMLDetails

Mapeie o campo na leitura dos dados:

```csharp
// Services/SAFTXMLDetails.cs - método GetCustomers()
var customer = new Customer
{
    // ... campos existentes ...
    
    // NOVO CAMPO:
    NIFRepresentante = reader["NIFRepresentante"] != DBNull.Value ? 
        reader["NIFRepresentante"].ToString() : null
};
```

### Passo 4: Atualizar SAFTXMLObject (se necessário)

Se o novo campo deve aparecer no XML, adicione-o na serialização:

```csharp
// Services/SAFTXMLObject.cs - método WriteMasterFiles()
if (!string.IsNullOrEmpty(customer.NIFRepresentante))
    writer.WriteElementString("NIFRepresentante", customer.NIFRepresentante);
```

## 🗄️ Stored Procedures Necessárias

A aplicação espera as seguintes Stored Procedures na base de dados PULSE DOMINOS:

### 1. spGetCustomersForSAFTXML
Retorna clientes com todos os campos necessários para o SAFT.

### 2. spGetProductsForSAFTXML
Retorna produtos/artigos com todos os campos necessários.

### 3. spGetInvoicesForSAFTXML
Retorna faturas/documentos dentro do período fiscal especificado.

**Parâmetros:**
- `@StartDate` (DateTime): Data início
- `@EndDate` (DateTime): Data fim

### 4. spGetInvoiceLinesForSAFTXML
Retorna linhas de uma fatura específica.

**Parâmetros:**
- `@InvoiceNo` (string): Número da fatura

### 5. spGetSAFTHeader
Retorna informações da empresa e configurações SAFT.

**Parâmetros:**
- `@FiscalYear` (int): Ano fiscal

## ⚙️ Estrutura do Ficheiro SAFT

O ficheiro SAFT gerado segue a estrutura:

```xml
<?xml version="1.0" encoding="utf-8"?>
<AuditFile xmlns="urn:OECD:StandardAuditFile-Tax:PT_1.04_01">
  <Header>
    <!-- Informações da empresa -->
  </Header>
  <MasterFiles>
    <Customer>...</Customer>
    <Product>...</Product>
  </MasterFiles>
  <SourceDocuments>
    <SalesInvoices>
      <Invoice>...</Invoice>
    </SalesInvoices>
  </SourceDocuments>
</AuditFile>
```

## 🔐 Segurança

- ⚠️ **Nunca guarde passwords em texto plano**
- Use `IntegratedSecurity` quando possível
- Valide sempre os dados antes de gerar o ficheiro
- Proteja o acesso à base de dados com permissões adequadas

## 🧪 Validação

Após gerar o ficheiro SAFT:

1. Valide o ficheiro com o validador oficial da AT
2. Verifique se todos os campos obrigatórios estão presentes
3. Confirme que os totais estão corretos
4. Teste com um período pequeno primeiro

## 📚 Documentação Adicional

- [Schema SAFT-PT](https://info.portaldasfinancas.gov.pt/pt/apoio_contribuinte/Faturacao_e_software/Comunicacao_Dados_Faturacao/Documents/SAF_T_PT.pdf)
- [Portaria SAFT-PT](https://info.portaldasfinancas.gov.pt)

## 🤝 Contribuir

Para contribuir com melhorias:

1. Faça fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/NovaFuncionalidade`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`)
4. Push para a branch (`git push origin feature/NovaFuncionalidade`)
5. Abra um Pull Request

## 📄 Licença

Este projeto é fornecido "como está", sem garantias de qualquer tipo.

## ✨ Versão

**v1.0.0** - Versão inicial com funcionalidades base de extração SAFT

---

**Desenvolvido para extração de dados SAFT do sistema PULSE DOMINOS**
