# 🗄️ Referência: Tabelas e Colunas do PULSE DOMINOS

Este documento documenta as tabelas e colunas reais usadas no código original **Dp.SAFTXMLAddition** para facilitar a adaptação das Stored Procedures.

## 📊 Campos Utilizados nas Stored Procedures Originais

### spGetInvoicesForSAFTXML

**Stored Procedure Original:**
```sql
spGetInvoicesForSAFTXML
    @LocationCode
    @StartDate  
    @EndDate
```

**Campos retornados (extraídos do código original):**

| Campo SQL | Uso no SAFT | Observações |
|-----------|-------------|-------------|
| `InvoiceNumber` | InvoiceNo | Número da fatura |
| `VerificationCode` | ATCUD (parte) | Para cálculo ATCUD (desde 2023) |
| `ActualOrderDate` | InvoiceDate, SystemEntryDate | Data da fatura |
| `OrdPayUpdateUserCode` | SourceID | Utilizador que atualizou |
| `Added_By` | SourceID (fallback) | Utilizador que criou |
| `HashValue` | Hash | Hash do documento |
| `ManualInvoiceReferenceNumber` | HashControl (parte) | Referência manual |
| `IsManual` | SourceBilling | M=Manual, P=Programa |
| `OrderDate` | TaxPointDate | Data do pedido |
| `Customer_Code` | CustomerID | Código do cliente |
| `Order_Number` | Description (linha) | Número do pedido |
| `Line_Number` | LineNumber | Número da linha |
| `ProductCode` | ProductCode | Código do produto |
| `ProdTextProductDesc` | ProductDescription | Descrição do produto |
| `Quantity` | Quantity | Quantidade |
| `OrdLineFinalPrice` | Valor da linha | Preço final |
| `OrdLineTaxAmt` | Valor do IVA | Montante de imposto |
| `TaxRate` | TaxPercentage | Taxa de IVA (%) |
| `IsOfficial` | Tipo de linha | Crédito ou débito |
| `DiscountAmount` | Desconto | Desconto aplicado |
| `LineDiscountAmount` | SettlementAmount | Desconto da linha |
| `OrdPayAmt` | Pagamento | Valor pago |
| `OrderPayTypeCode` | PaymentMechanism | Tipo de pagamento |
| `OrdPayIsLater` | Validação | Pagamento posterior |
| `CreditReferenceNumber` | Reference | Referência de crédito |

### spGetCustomersForSAFTXML

**Stored Procedure Original:**
```sql
spGetCustomersForSAFTXML
    @LocationCode
    @InvoiceNumbers (lista de números de fatura, separados por |)
```

**Campos retornados (extraídos do código original):**

| Campo SQL | Uso no SAFT | Observações |
|-----------|-------------|-------------|
| `Customer_Code` | CustomerID | Código do cliente |
| `AccountID` | AccountID | ID da conta (configurável) |
| `TaxID` | CustomerTaxID | NIF do cliente |
| `Name` | CompanyName | Nome da empresa |
| `Customer_Name` | CompanyName (fallback) | Nome do cliente |
| `StreetNumber` | BuildingNumber | Número da rua |
| `StreetName` | StreetName | Nome da rua |
| `OrderStreetNumber` | BuildingNumber (fallback) | Número rua pedido |
| `OrderStreetName` | StreetName (fallback) | Nome rua pedido |
| `SelfBillingIndicator` | SelfBillingIndicator | Auto-faturação (configurável) |
| `Country` | Country | País |

**Código Especial:**
- Se `Customer_Code` = "0" ou vazio → Cliente genérico
- Se `TaxID` vazio → Cliente genérico
- Cliente genérico usa valores configuráveis

### spGetProductsForSAFTXML

**Stored Procedure Original:**
```sql
spGetProductsForSAFTXML
    @LocationCode
    @ProductCodes (lista de códigos de produto, separados por |)
```

**Campos utilizados:**

| Campo SQL | Uso no SAFT | Observações |
|-----------|-------------|-------------|
| `ProductCode` | ProductCode | Código do produto |
| `ProdTextProductDesc` | ProductDescription | Descrição |
| `ProductType` | ProductType | P=Produto, S=Serviço |
| `ProductGroup` | ProductGroup | Grupo de produto |

## 🏗️ Estrutura das Tabelas (deduzido do código)

### Tabela: Orders/Invoices

Campos relacionados com pedidos/faturas:
- `InvoiceNumber`
- `VerificationCode` (para ATCUD)
- `ActualOrderDate`
- `OrderDate`
- `Customer_Code`
- `HashValue`
- `ManualInvoiceReferenceNumber`
- `IsManual` (bit)
- `Added_By`
- `OrdPayUpdateUserCode`

### Tabela: OrderLines/InvoiceLines

Campos relacionados com linhas:
- `Line_Number`
- `Order_Number`
- `ProductCode`
- `ProdTextProductDesc`
- `Quantity`
- `OrdLineFinalPrice`
- `OrdLineTaxAmt`
- `TaxRate`
- `IsOfficial`
- `DiscountAmount`
- `LineDiscountAmount`

### Tabela: OrderPayments

Campos relacionados com pagamentos:
- `OrdPayAmt`
- `OrderPayTypeCode`
- `OrdPayIsLater`

### Tabela: Customers

Campos relacionados com clientes:
- `Customer_Code`
- `Customer_Name`
- `TaxID`
- `Name`
- `StreetNumber`
- `StreetName`
- `OrderStreetNumber` (endereço de entrega)
- `OrderStreetName` (endereço de entrega)

## 🔄 Mapeamento de Códigos

### Tipo de Pagamento (OrderPayTypeCode)

Do código original:
- "1" → Cash (Dinheiro)
- "2" → Check (Cheque)
- "3" → Credit Card (Cartão de Crédito)
- "4" → Bank Transfer (Transferência)
- "5" → Restaurant Account (Conta Restaurante)
- "6" → Voucher (Vale)
- "7" → Other (Outro)
- "8" → Electronic Payment (Pagamento Eletrónico)

### TaxCode (código de IVA)

Sistema de mapeamento por taxa:
- Taxa na TaxTable → TaxCode correspondente
- Exemplo: 23% → "NOR", 13% → "INT", 6% → "RED", 0% → "ISE"

## 💡 Observações Importantes

### 1. LocationCode
O código original sempre filtra por `@LocationCode`, que identifica a loja/localização no sistema PULSE DOMINOS.

### 2. InvoiceNumbers e ProductCodes
As SPs de clientes e produtos recebem listas de números/códigos separados por `|` (pipe) para filtrar apenas os dados necessários.

### 3. Cliente Genérico
Quando um cliente não tem NIF ou tem Customer_Code = "0", usa-se cliente genérico com valores padrão configuráveis.

### 4. ATCUD
O campo `VerificationCode` é usado para calcular o ATCUD. O código original tem um método `GetATCUD()` que faz este cálculo.

### 5. Hash
O campo `HashValue` deve conter o hash do documento calculado pelo sistema PULSE.

### 6. Campos de Isenção (NOVOS)

Para adicionar suporte a produtos isentos (como DONATPTONV), adicione nas suas SPs:

```sql
CASE 
    WHEN OL.ProductCode = 'DONATPTONV' THEN 'Nao sujeito; nao tributado'
    ELSE NULL
END AS TaxExemptionReason,

CASE 
    WHEN OL.ProductCode = 'DONATPTONV' THEN 'M99'
    ELSE NULL
END AS TaxExemptionCode
```

## 🔍 Como Adaptar as SPs

1. **Identifique suas tabelas reais** no SQL Server Management Studio
2. **Mapeie os campos** usando esta referência
3. **Mantenha os alias** (AS CustomerID, AS ProductCode, etc.) conforme documentado
4. **Adicione lógica CASE** para campos de isenção
5. **Teste cada SP** individualmente antes de usar na aplicação

## 📚 Documentos Relacionados

- `STORED_PROCEDURES.md` - Exemplos completos de SPs
- `ORIGINAL_CODE_ANALYSIS.md` - Análise do código original
- `README.md` - Documentação geral

---

**Esta referência foi extraída do código original Dp.SAFTXMLAddition para facilitar a adaptação das Stored Procedures ao ambiente PULSE DOMINOS real.**
