# 📊 Análise do Código Original - Dp.SAFTXMLAddition

## Visão Geral

O código original `Dp.SAFTXMLAddition` é uma aplicação Windows Forms integrada ao sistema **PULSE DOMINOS** da Domino's Pizza, desenvolvida para gerar ficheiros SAFT-PT versão 1.03_01.

## Estrutura do Código Original

### Arquivos e Classes (36 ficheiros .cs)

#### 1. Classes Geradas do XSD SAFT-PT 1.03
Estas classes foram geradas automaticamente a partir do schema XSD oficial:

- **AuditFile.cs** - Classe raiz do ficheiro SAFT
- **Header.cs** - Cabeçalho do ficheiro
- **AuditFileMasterFiles.cs** - Dados mestre (clientes, produtos)
- **Customer.cs** - Cliente com serialização XML
- **Product.cs** - Produto com serialização XML
- **SourceDocuments.cs** - Documentos fonte (faturas)
- **SourceDocumentsSalesInvoices.cs** - Faturas de venda
- **SourceDocumentsSalesInvoicesInvoice.cs** - Fatura individual
- **SourceDocumentsSalesInvoicesInvoiceLine.cs** - Linha de fatura
- **Tax.cs**, **TaxTable.cs**, **TaxTableEntry.cs** - Impostos
- **CompanyAddress.cs**, **AddressStructure.cs**, **AddressStructurePT.cs** - Endereços
- E outras classes auxiliares...

**Namespace XML**: `urn:OECD:StandardAuditFile-Tax:PT_1.03_01`

#### 2. Classes de Negócio

##### SAFTXMLDetails.cs (987 linhas - Classe Crítica)
Responsável por extrair dados e popular as classes SAFT.

**Métodos principais:**
```csharp
public AuditFile GetSaftXmlDetails(DateTime startDate, DateTime endDate)
private bool GetHeader(DateTime startDate, DateTime endDate)
private void GetCustomers()
private void GetProducts()
private void GetInvoices(DateTime startDate, DateTime endDate)
private void GetTaxTable()
private void MapTaxTable()
private string GetATCUD(string VerificationCode, string invoiceNumber)
```

**Stored Procedures utilizadas:**
- `spGetCustomersForSAFTXML` - Extrai clientes com `@LocationCode` e `@InvoiceNumbers`
- `spGetProductsForSAFTXML` - Extrai produtos
- `spGetInvoicesForSAFTXML` - Extrai faturas
- E outras SPs específicas do PULSE DOMINOS

**Características:**
- Usa `SqlDataReader` para ler dados
- Valida dados antes de adicionar ao SAFT
- Mantém coleções de números de fatura e códigos de produto
- Integra com `SystemSettings.PulseContext` para localização e utilizador
- Regista logs de auditoria

##### SAFTXMLObject.cs
Responsável por serializar o `AuditFile` para XML.

```csharp
public void CreateSaftXmlFile(DateTime startDate, DateTime endDate, string destinationFolder)
private void WriteSaftXmlFile(AuditFile saftXmlAuditFile, string destinationFolder, string fileName)
```

**Características:**
- Usa `XmlSerializer` para serialização
- Encoding: **Windows-1252** (não UTF-8!)
- Nome do ficheiro: `{LocationCode}_{StartDate}_{EndDate}_SAFT.xml`
- Corrige a versão do namespace após serialização (hack)

##### SAFTAdditionForm.cs
Formulário principal da aplicação.

**Controles:**
- `DateTimePicker` para Start Date
- `DateTimePicker` para End Date
- `TextBox` para Fiscal Year (read-only)
- `TextBox` para Destination Folder
- `Button` Browse, Generate, Cancel

**Validações:**
- Data início não pode ser maior que data atual
- Data fim não pode ser maior que data atual
- Data fim tem que ser >= data início
- Datas têm que estar dentro do mesmo ano fiscal
- Pasta de destino obrigatória

**Características:**
- Usa multi-threading para carregar header em background
- Todas as mensagens vêm de `PulseContext.Language.GetText()`
- Validação robusta de datas e ano fiscal

##### SAFTAdditionStartup.cs
Ponto de entrada da aplicação.

```csharp
public static void Main()
public static bool Login()
private static void Startup()
```

**Características:**
- Login obrigatório com autenticação PULSE
- Verifica autorização do utilizador (`SaftApplicationKey = 60010`)
- Testa conexão SQL no arranque
- Tratamento global de exceções
- Integração com `PulseStartUp`, `PulseContext`, `PulseUiContext`

##### SAFTXMLHeader.cs e SAFTXMLHeaderObject.cs
Responsáveis por obter dados do header da empresa.

```csharp
public SAFTDate GetFiscalYear(DateTime startDate)
public SAFTHeader GetSAFTXMLHeaderDetails()
```

##### SAFTDate.cs
Classe simples para representar ano fiscal.

```csharp
public string FiscalYear { get; set; }
public DateTime MaxEndDate { get; set; }
public DateTime MinEndDate { get; set; }
```

## Integração com PULSE DOMINOS

### Dependências Externas
```csharp
using Dominos.Core;
using Dominos.LogForPulse;
using Dominos.Pulse;
using Dominos.PulseUI;
```

### SystemSettings.PulseContext
Usado extensivamente em todo o código:
- `SystemSettings.PulseContext.LocationCode` - Código da localização/loja
- `SystemSettings.PulseContext.User` - Utilizador autenticado
- `SystemSettings.PulseContext.Language.GetText(id)` - Textos internacionalizados

### PulseConnection
```csharp
SqlConnection sqlConnection = PulseConnection.CreateSQLConnection()
```

## Comparação: Original vs. Implementação Atual

| Aspecto | Original (Dp.SAFTXMLAddition) | Atual (SAFTExtractor) |
|---------|-------------------------------|----------------------|
| **Target** | PULSE DOMINOS específico | SQL Server genérico |
| **Schema SAFT** | PT_1.03_01 | PT_1.04_01 |
| **Serialização** | XmlSerializer (classes XSD) | XmlWriter manual |
| **Encoding** | Windows-1252 | UTF-8 |
| **Autenticação** | Login PULSE obrigatório | Sem autenticação |
| **UI - Datas** | DateTimePicker com validação completa | NumericUpDown (apenas ano) |
| **Validação Fiscal** | Completa (min/max dates por ano) | Básica (apenas ano) |
| **Mensagens** | Internacionalizadas (IDs) | Texto direto |
| **Conexão BD** | PulseConnection | ConnectionString configurável |
| **SPs Parameters** | LocationCode, InvoiceNumbers | StartDate, EndDate apenas |
| **Logging** | Logger.Error + Audit Log BD | Console.WriteLine apenas |
| **Threading** | Sim (carregamento header) | Não |
| **Nome Ficheiro** | LocationCode_Date_SAFT.xml | SAFT_Year.xml |

## Pontos Fortes do Original

1. **Validação Robusta**: Validações extensivas de datas e ano fiscal
2. **Classes Tipadas**: Uso de classes geradas do XSD (type-safe)
3. **Integração Completa**: Profundamente integrado com PULSE DOMINOS
4. **Segurança**: Login obrigatório e verificação de autorização
5. **Auditoria**: Regista todas as gerações de SAFT na BD
6. **UI Profissional**: DatePickers, validações em tempo real
7. **Internacionalização**: Todas as mensagens vêm de ficheiro de idiomas
8. **Threading**: UI responsiva durante operações longas

## Pontos Fortes da Implementação Atual

1. **Genérica**: Funciona com qualquer SQL Server, não apenas PULSE
2. **Schema Atualizado**: Usa SAFT-PT 1.04_01 (mais recente)
3. **Documentação**: Extensa documentação (1,900+ linhas)
4. **Independente**: Sem dependências externas (Dominos.*)
5. **UTF-8**: Encoding moderno e universal
6. **Flexível**: Fácil adaptar para diferentes estruturas de BD
7. **Extensível**: Comentários e guias para adicionar campos

## Recomendações de Melhorias

### 🔴 Críticas (Alta Prioridade)

1. **Adicionar DatePickers ao Formulário**
   - Substituir `NumericUpDown` por `DateTimePicker` para start/end dates
   - Manter campo de ano fiscal (read-only, calculado automaticamente)
   - Implementar validações de data como no original

2. **Melhorar Validação de Datas**
   - Adicionar `SAFTDate` com `MinEndDate` e `MaxEndDate`
   - Validar que datas estão no mesmo ano fiscal
   - Prevenir datas futuras

3. **Encoding Configurável**
   - Adicionar opção para escolher UTF-8 ou Windows-1252
   - Windows-1252 pode ser necessário para compatibilidade com AT

### 🟡 Importantes (Média Prioridade)

4. **Melhorar SAFTXMLDetails**
   - Estudar lógica de validação de clientes genéricos
   - Implementar lógica de ATCUD (obrigatório desde 2023)
   - Adicionar validações extras antes de serializar

5. **Adicionar Logging Estruturado**
   - Implementar logging para arquivo
   - Logs de debug, info, warning, error
   - Útil para troubleshooting

6. **Nome de Ficheiro Melhorado**
   - Incluir datas no nome: `SAFT_20240101_20241231.xml`
   - Opcionalmente incluir código da empresa

### 🟢 Desejáveis (Baixa Prioridade)

7. **Threading para UI Responsiva**
   - Carregamento de header em background
   - Progress bar durante geração

8. **Validação com Schema XSD**
   - Validar XML gerado contra o schema oficial
   - Alertar sobre erros antes de gravar

9. **Classes Geradas do XSD**
   - Considerar usar classes geradas para type-safety
   - Mas manter flexibilidade atual

## Código Útil para Adaptar

### 1. Validação de Datas (do SAFTAdditionForm.cs)

```csharp
// Validar data fim
bool flag3 = this.endDatePicker.Value.Year > this.MaxEndDate.Year 
    || this.endDatePicker.Value < this.MinEndDate 
    || this.endDatePicker.Value.Date > this.MaxEndDate.Date;
if (flag3)
{
    MessageBox.Show("Data não está no ano fiscal correto");
    this.endDatePicker.Value = this.startDatePicker.Value;
}
```

### 2. Cálculo de Ano Fiscal (do SAFTXMLHeader.cs)

```csharp
public SAFTDate GetFiscalYear(DateTime startDate)
{
    SAFTDate fiscalYear = new SAFTDate();
    fiscalYear.FiscalYear = startDate.Year.ToString();
    fiscalYear.MinEndDate = new DateTime(startDate.Year, 1, 1);
    fiscalYear.MaxEndDate = new DateTime(startDate.Year, 12, 31);
    return fiscalYear;
}
```

### 3. Nome do Ficheiro (do SAFTXMLObject.cs)

```csharp
string fileName = string.Concat(new string[]
{
    SystemSettings.PulseContext.LocationCode, "_",
    string.Format("{0:dd}", startDate),
    string.Format("{0:MM}", startDate),
    string.Format("{0:yyyy}", startDate), "_",
    string.Format("{0:dd}", endDate),
    string.Format("{0:MM}", endDate),
    string.Format("{0:yyyy}", endDate), "_",
    "SAFT", ".xml"
});
```

### 4. Encoding Windows-1252 (do SAFTXMLObject.cs)

```csharp
XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
xmlWriterSettings.Indent = true;
xmlWriterSettings.NewLineOnAttributes = true;
xmlWriterSettings.Encoding = Encoding.GetEncoding(1252);
```

## Conclusão

O código original é muito robusto mas específico para PULSE DOMINOS. A implementação atual é mais genérica e flexível. 

**Melhor Abordagem:** Combinar o melhor dos dois mundos:
- Manter a flexibilidade e documentação da implementação atual
- Adicionar as validações robustas e UI profissional do original
- Oferecer encoding configurável
- Manter a independência de sistemas externos

## Ficheiros de Referência

- `Dp.SAFTXMLAddition/SAFTXMLDetails.cs` - Lógica de extração (987 linhas)
- `Dp.SAFTXMLAddition/SAFTAdditionForm.cs` - UI e validações
- `Dp.SAFTXMLAddition/SAFTXMLObject.cs` - Serialização
- `Dp.SAFTXMLAddition/AuditFile.cs` - Modelo SAFT 1.03

---

**Data da Análise:** 2026-02-11  
**Versão Original:** SAFT-PT 1.03_01  
**Versão Atual:** SAFT-PT 1.04_01
