# Stored Procedures para SAFT Extractor

Este documento contém exemplos de Stored Procedures que devem ser criadas na base de dados PULSE DOMINOS para que a aplicação SAFTExtractor funcione corretamente.

⚠️ **IMPORTANTE**: Estes são exemplos genéricos. Você deve adaptar os nomes das tabelas e campos conforme a estrutura real da sua base de dados PULSE DOMINOS.

## 1. spGetCustomersForSAFTXML

Retorna todos os clientes com os campos necessários para o SAFT.

```sql
CREATE PROCEDURE spGetCustomersForSAFTXML
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        -- Campos obrigatórios
        c.CustomerCode AS CustomerID,
        c.CustomerCode AS AccountID,
        c.TaxID AS CustomerTaxID,
        c.CompanyName AS CompanyName,
        
        -- Campos opcionais
        c.ContactName AS Contact,
        c.Phone AS Telephone,
        c.Fax AS Fax,
        c.Email AS Email,
        c.Website AS Website,
        
        -- Auto-faturação
        ISNULL(c.SelfBilling, 0) AS SelfBillingIndicator,
        
        -- Endereço
        c.BuildingNumber AS BuildingNumber,
        c.StreetName AS StreetName,
        c.AddressLine AS AddressDetail,
        c.City AS City,
        c.PostalCode AS PostalCode,
        c.Region AS Region,
        ISNULL(c.Country, 'PT') AS Country
        
        -- ADICIONE AQUI NOVOS CAMPOS:
        -- c.NIFRepresentante AS NIFRepresentante
        
    FROM Customers c
    WHERE c.Active = 1  -- Apenas clientes ativos
    ORDER BY c.CustomerCode;
END
GO
```

## 2. spGetProductsForSAFTXML

Retorna todos os produtos/artigos.

```sql
CREATE PROCEDURE spGetProductsForSAFTXML
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        -- Tipo de produto: P=Produto, S=Serviço, O=Outro, E=Outro (S&T)
        CASE 
            WHEN p.IsService = 1 THEN 'S'
            ELSE 'P'
        END AS ProductType,
        
        p.ProductCode AS ProductCode,
        p.ProductGroup AS ProductGroup,
        p.Description AS ProductDescription,
        p.ProductCode AS ProductNumberCode  -- Pode ser código de barras ou outro
        
        -- ADICIONE AQUI NOVOS CAMPOS:
        
    FROM Products p
    WHERE p.Active = 1  -- Apenas produtos ativos
    ORDER BY p.ProductCode;
END
GO
```

## 3. spGetInvoicesForSAFTXML

Retorna faturas/documentos dentro do período fiscal.

```sql
CREATE PROCEDURE spGetInvoicesForSAFTXML
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        -- Identificação do documento
        i.InvoiceNumber AS InvoiceNo,
        i.ATCUD AS ATCUD,  -- Código único de documento (obrigatório desde 2023)
        i.Hash AS Hash,
        i.HashControl AS HashControl,
        
        -- Datas
        i.InvoiceDate AS InvoiceDate,
        i.SystemDate AS SystemEntryDate,
        
        -- Cliente
        i.CustomerCode AS CustomerID,
        
        -- Totais
        i.TaxAmount AS TaxPayable,
        i.NetAmount AS NetTotal,
        i.GrossAmount AS GrossTotal
        
        -- ADICIONE AQUI NOVOS CAMPOS:
        
    FROM Invoices i
    WHERE i.InvoiceDate BETWEEN @StartDate AND @EndDate
        AND i.Status <> 'Cancelled'  -- Excluir documentos cancelados
    ORDER BY i.InvoiceDate, i.InvoiceNumber;
END
GO
```

## 4. spGetInvoiceLinesForSAFTXML

Retorna linhas de uma fatura específica.

```sql
CREATE PROCEDURE spGetInvoiceLinesForSAFTXML
    @InvoiceNo NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        -- Linha
        il.LineNumber AS LineNumber,
        il.ProductCode AS ProductCode,
        il.Description AS ProductDescription,
        
        -- Quantidades
        il.Quantity AS Quantity,
        ISNULL(il.UnitOfMeasure, 'UN') AS UnitOfMeasure,
        il.UnitPrice AS UnitPrice,
        
        -- Impostos
        ISNULL(il.TaxType, 'IVA') AS TaxType,
        ISNULL(il.TaxCountryRegion, 'PT') AS TaxCountryRegion,
        ISNULL(il.TaxCode, 'NOR') AS TaxCode,  -- NOR, RED, INT, ISE
        il.TaxRate AS TaxPercentage,
        il.TaxExemptionReason AS TaxExemptionReason,  -- Para taxas isentas
        
        -- Valor
        il.LineAmount AS CreditAmount  -- Total da linha
        
        -- ADICIONE AQUI NOVOS CAMPOS:
        
    FROM InvoiceLines il
    WHERE il.InvoiceNumber = @InvoiceNo
    ORDER BY il.LineNumber;
END
GO
```

## 5. spGetSAFTHeader

Retorna informações da empresa e configurações SAFT.

```sql
CREATE PROCEDURE spGetSAFTHeader
    @FiscalYear INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP 1
        -- Versão do ficheiro SAFT
        '1.04_01' AS AuditFileVersion,
        
        -- Identificação da empresa
        c.CompanyCode AS CompanyID,
        c.TaxID AS TaxRegistrationNumber,
        'F' AS TaxAccountingBasis,  -- F=Faturação, C=Contabilidade
        c.CompanyName AS CompanyName,
        
        -- Endereço da empresa
        c.BuildingNumber AS BuildingNumber,
        c.StreetName AS StreetName,
        c.AddressLine AS AddressDetail,
        c.City AS City,
        c.PostalCode AS PostalCode,
        c.Region AS Region,
        ISNULL(c.Country, 'PT') AS Country,
        
        -- Moeda
        ISNULL(c.CurrencyCode, 'EUR') AS CurrencyCode,
        
        -- Entidade
        'Global' AS TaxEntity,
        
        -- Software
        c.TaxID AS ProductCompanyTaxID,  -- NIF da empresa produtora do software
        c.SoftwareCertificate AS SoftwareCertificateNumber,  -- Certificado AT
        'SAFTExtractor/PULSE DOMINOS' AS ProductID,
        '1.0' AS ProductVersion
        
    FROM CompanySettings c
    WHERE c.Active = 1;
END
GO
```

## 🔍 Verificação das Stored Procedures

Após criar as SPs, execute os seguintes testes:

```sql
-- Teste 1: Clientes
EXEC spGetCustomersForSAFTXML;

-- Teste 2: Produtos
EXEC spGetProductsForSAFTXML;

-- Teste 3: Faturas de 2024
EXEC spGetInvoicesForSAFTXML 
    @StartDate = '2024-01-01', 
    @EndDate = '2024-12-31';

-- Teste 4: Linhas de uma fatura (substitua pelo número real)
EXEC spGetInvoiceLinesForSAFTXML 
    @InvoiceNo = 'FT 2024/1';

-- Teste 5: Header
EXEC spGetSAFTHeader 
    @FiscalYear = 2024;
```

## 📋 Checklist de Campos Obrigatórios

### Clientes
- ✅ CustomerID
- ✅ AccountID
- ✅ CustomerTaxID (NIF)
- ✅ CompanyName
- ✅ AddressDetail
- ✅ City
- ✅ PostalCode
- ✅ Country

### Produtos
- ✅ ProductType
- ✅ ProductCode
- ✅ ProductDescription
- ✅ ProductNumberCode

### Faturas
- ✅ InvoiceNo
- ✅ InvoiceDate
- ✅ CustomerID
- ✅ TaxPayable
- ✅ NetTotal
- ✅ GrossTotal

### Linhas de Fatura
- ✅ LineNumber
- ✅ ProductCode
- ✅ ProductDescription
- ✅ Quantity
- ✅ UnitPrice
- ✅ TaxType
- ✅ TaxPercentage

## 💡 Dicas

1. **Desempenho**: Se tiver muitos dados, considere adicionar índices nas colunas de data
2. **Dados de Teste**: Comece testando com um período pequeno (1 mês)
3. **Validação**: Sempre valide os dados antes de gerar o SAFT final
4. **ATCUD**: Campo obrigatório desde 2023 - certifique-se de ter este campo preenchido
5. **Hash**: Obrigatório para assinatura de documentos - deve ser calculado pelo seu ERP

## ⚠️ Notas Importantes

- Os nomes das tabelas e colunas são **exemplos**. Adapte conforme sua BD.
- Verifique se todos os campos obrigatórios do SAFT-PT estão mapeados.
- Teste sempre com dados reais antes de submeter à AT.
- Mantenha backup da base de dados antes de criar as SPs.

---

**Para mais informações sobre o schema SAFT-PT, consulte a documentação oficial da AT.**
