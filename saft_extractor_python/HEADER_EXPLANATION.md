# Como Funciona o Header SAFT - Combinação de 2 SPs

Este documento explica em detalhe como o header SAFT é montado a partir de **2 Stored Procedures** diferentes.

## 🎯 Conceito

O header completo do ficheiro SAFT é construído combinando dados de:

1. **spGetSAFTDetails** → Retorna a maioria dos campos do header
2. **spGetSAFTXMLHeaderDetails** → Retorna o ProductVersion

## 📊 Stored Procedure 1: spGetSAFTDetails

### SQL Original

```sql
ALTER PROCEDURE [dbo].[spGetSAFTDetails]
(
    @LocationCode AS NVARCHAR(8)
)
AS
BEGIN
    SET NOCOUNT ON
    
    SELECT 
        Setting,
        Value 
    FROM GovernmentReceipt..SAFTDetails (NOLOCK)
    WHERE Location_Code = @LocationCode
END
```

### Estrutura da Tabela SAFTDetails

A tabela `GovernmentReceipt..SAFTDetails` tem o formato:

| Location_Code | Setting | Value |
|---------------|---------|-------|
| 01 | AuditFileVersion | 1.04_01 |
| 01 | CompanyID | 12345 |
| 01 | TaxRegistrationNumber | 501234567 |
| 01 | CompanyName | MINHA EMPRESA LDA |
| 01 | AddressDetail | Rua Principal, 123 |
| 01 | City | Lisboa |
| 01 | PostalCode | 1000-001 |
| 01 | Country | PT |
| ... | ... | ... |

### Exemplo de Retorno

```
Setting                     | Value
----------------------------|------------------
AuditFileVersion            | 1.04_01
CompanyID                   | 12345
TaxRegistrationNumber       | 501234567
TaxAccountingBasis          | F
CompanyName                 | MINHA EMPRESA LDA
BuildingNumber              | 123
StreetName                  | Rua Principal
AddressDetail               | Rua Principal, 123
City                        | Lisboa
PostalCode                  | 1000-001
Region                      | Lisboa
Country                     | PT
CurrencyCode                | EUR
TaxEntity                   | Global
ProductCompanyTaxID         | 501234567
SoftwareCertificateNumber   | 1234
ProductID                   | PULSE DOMINOS
Telephone                   | 210000000
Fax                         | 210000001
Email                       | geral@empresa.pt
Website                     | www.empresa.pt
```

## 📊 Stored Procedure 2: spGetSAFTXMLHeaderDetails

### SQL Original

```sql
ALTER PROCEDURE [dbo].[spGetSAFTXMLHeaderDetails]
(
    @LocationCode AS NVARCHAR(8)
)
AS
BEGIN
    SET NOCOUNT ON
    
    SELECT DISTINCT 
        LC.Location_Code AS LocationCode,
        VER.Version AS Version 
    FROM 
        Location_Codes (NOLOCK) AS LC
        INNER JOIN Version (NOLOCK) AS VER ON 
        (
            VER.Location_Code = @LocationCode
        )
    GROUP BY
        LC.Location_Code,
        VER.Version
END
```

### Exemplo de Retorno

```
LocationCode | Version
-------------|----------
01           | 1.2.3.4
```

O campo `Version` será usado como `ProductVersion` no XML.

## 🔧 Como o Código Python Combina as 2 SPs

### Código em saft_xml_details.py

```python
def get_header(self, fiscal_year: int, start_date: datetime, end_date: datetime) -> SAFTHeader:
    """
    Obtém header combinando dados de 2 SPs:
    1. spGetSAFTDetails - Retorna Setting/Value (quase todo o header)
    2. spGetSAFTXMLHeaderDetails - Retorna ProductVersion
    """
    header = SAFTHeader()
    header.FiscalYear = fiscal_year
    header.StartDate = start_date
    header.EndDate = end_date
    header.DateCreated = datetime.now()
    
    conn = PulseConnection.create_connection()
    cursor = conn.cursor()
    
    try:
        # ==========================================
        # PASSO 1: Obter dados do spGetSAFTDetails
        # ==========================================
        print(f"Executando spGetSAFTDetails com LocationCode: {self.location_code}")
        cursor.execute(
            "{CALL dbo.spGetSAFTDetails(?)}",
            self.location_code
        )
        
        # A SP retorna pares Setting/Value
        # Vamos construir um dicionário
        saft_details = {}
        for row in cursor.fetchall():
            setting = row[0]  # Setting
            value = row[1]    # Value
            saft_details[setting] = value
            print(f"  {setting} = {value}")
        
        # Mapear campos do spGetSAFTDetails para o header
        header.AuditFileVersion = saft_details.get('AuditFileVersion', '1.04_01')
        header.CompanyID = saft_details.get('CompanyID', '')
        header.TaxRegistrationNumber = saft_details.get('TaxRegistrationNumber', '')
        header.TaxAccountingBasis = saft_details.get('TaxAccountingBasis', 'F')
        header.CompanyName = saft_details.get('CompanyName', '')
        header.CurrencyCode = saft_details.get('CurrencyCode', 'EUR')
        header.TaxEntity = saft_details.get('TaxEntity', 'Global')
        header.ProductCompanyTaxID = saft_details.get('ProductCompanyTaxID', '')
        header.SoftwareCertificateNumber = saft_details.get('SoftwareCertificateNumber', '')
        header.ProductID = saft_details.get('ProductID', 'PULSE DOMINOS')
        
        # Campos opcionais
        header.Telephone = saft_details.get('Telephone')
        header.Fax = saft_details.get('Fax')
        header.Email = saft_details.get('Email')
        header.Website = saft_details.get('Website')
        
        # Endereço da empresa
        address = CompanyAddress()
        address.BuildingNumber = saft_details.get('BuildingNumber')
        address.StreetName = saft_details.get('StreetName')
        address.AddressDetail = saft_details.get('AddressDetail', '')
        address.City = saft_details.get('City', '')
        address.PostalCode = saft_details.get('PostalCode', '')
        address.Region = saft_details.get('Region')
        address.Country = saft_details.get('Country', 'PT')
        header.BusinessAddress = address
        
        # ================================================
        # PASSO 2: Obter ProductVersion do spGetSAFTXMLHeaderDetails
        # ================================================
        print(f"\nExecutando spGetSAFTXMLHeaderDetails com LocationCode: {self.location_code}")
        cursor.execute(
            "{CALL dbo.spGetSAFTXMLHeaderDetails(?)}",
            self.location_code
        )
        
        row = cursor.fetchone()
        if row:
            location_code_ret = row[0]  # LocationCode
            version = row[1]            # Version
            header.ProductVersion = version  # <-- AQUI ESTÁ O PONTO CRÍTICO
            print(f"  LocationCode: {location_code_ret}")
            print(f"  Version (ProductVersion): {version}")
        else:
            print("  AVISO: spGetSAFTXMLHeaderDetails não retornou dados")
            header.ProductVersion = "1.0"
        
        print(f"\nHeader montado com sucesso!")
        print(f"  CompanyName: {header.CompanyName}")
        print(f"  ProductID: {header.ProductID}")
        print(f"  ProductVersion: {header.ProductVersion}")
        
    except pyodbc.Error as e:
        print(f"\nERRO SQL ao obter header: {e}")
        raise Exception(f"Erro ao obter header SAFT: {e}")
    finally:
        cursor.close()
        conn.close()
    
    return header
```

## 📝 Mapeamento Completo

### Campos do spGetSAFTDetails → Header XML

| Setting (BD) | Campo Python | Elemento XML |
|--------------|--------------|--------------|
| AuditFileVersion | header.AuditFileVersion | `<AuditFileVersion>` |
| CompanyID | header.CompanyID | `<CompanyID>` |
| TaxRegistrationNumber | header.TaxRegistrationNumber | `<TaxRegistrationNumber>` |
| TaxAccountingBasis | header.TaxAccountingBasis | `<TaxAccountingBasis>` |
| CompanyName | header.CompanyName | `<CompanyName>` |
| BuildingNumber | address.BuildingNumber | `<BusinessAddress><BuildingNumber>` |
| StreetName | address.StreetName | `<BusinessAddress><StreetName>` |
| AddressDetail | address.AddressDetail | `<BusinessAddress><AddressDetail>` |
| City | address.City | `<BusinessAddress><City>` |
| PostalCode | address.PostalCode | `<BusinessAddress><PostalCode>` |
| Region | address.Region | `<BusinessAddress><Region>` |
| Country | address.Country | `<BusinessAddress><Country>` |
| CurrencyCode | header.CurrencyCode | `<CurrencyCode>` |
| TaxEntity | header.TaxEntity | `<TaxEntity>` |
| ProductCompanyTaxID | header.ProductCompanyTaxID | `<ProductCompanyTaxID>` |
| SoftwareCertificateNumber | header.SoftwareCertificateNumber | `<SoftwareCertificateNumber>` |
| ProductID | header.ProductID | `<ProductID>` |
| Telephone | header.Telephone | `<Telephone>` |
| Fax | header.Fax | `<Fax>` |
| Email | header.Email | `<Email>` |
| Website | header.Website | `<Website>` |

### Campo do spGetSAFTXMLHeaderDetails → Header XML

| Campo (BD) | Campo Python | Elemento XML |
|------------|--------------|--------------|
| Version | header.ProductVersion | `<ProductVersion>` |

### Campos Calculados

| Campo Python | Valor | Elemento XML |
|--------------|-------|--------------|
| header.FiscalYear | startDate.year | `<FiscalYear>` |
| header.StartDate | Parâmetro da função | `<StartDate>` |
| header.EndDate | Parâmetro da função | `<EndDate>` |
| header.DateCreated | datetime.now() | `<DateCreated>` |

## 📄 Exemplo de XML Resultante

```xml
<?xml version='1.0' encoding='UTF-8'?>
<AuditFile xmlns="urn:OECD:StandardAuditFile-Tax:PT_1.04_01" 
           xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Header>
    <!-- Campos do spGetSAFTDetails -->
    <AuditFileVersion>1.04_01</AuditFileVersion>
    <CompanyID>12345</CompanyID>
    <TaxRegistrationNumber>501234567</TaxRegistrationNumber>
    <TaxAccountingBasis>F</TaxAccountingBasis>
    <CompanyName>MINHA EMPRESA LDA</CompanyName>
    
    <BusinessAddress>
      <BuildingNumber>123</BuildingNumber>
      <StreetName>Rua Principal</StreetName>
      <AddressDetail>Rua Principal, 123</AddressDetail>
      <City>Lisboa</City>
      <PostalCode>1000-001</PostalCode>
      <Region>Lisboa</Region>
      <Country>PT</Country>
    </BusinessAddress>
    
    <!-- Campos calculados -->
    <FiscalYear>2024</FiscalYear>
    <StartDate>2024-01-01</StartDate>
    <EndDate>2024-12-31</EndDate>
    <CurrencyCode>EUR</CurrencyCode>
    <DateCreated>2026-02-19</DateCreated>
    
    <!-- Mais campos do spGetSAFTDetails -->
    <TaxEntity>Global</TaxEntity>
    <ProductCompanyTaxID>501234567</ProductCompanyTaxID>
    <SoftwareCertificateNumber>1234</SoftwareCertificateNumber>
    <ProductID>PULSE DOMINOS</ProductID>
    
    <!-- Campo do spGetSAFTXMLHeaderDetails -->
    <ProductVersion>1.2.3.4</ProductVersion>
    
    <!-- Campos opcionais do spGetSAFTDetails -->
    <Telephone>210000000</Telephone>
    <Fax>210000001</Fax>
    <Email>geral@empresa.pt</Email>
    <Website>www.empresa.pt</Website>
  </Header>
  
  <!-- ... MasterFiles e SourceDocuments ... -->
</AuditFile>
```

## 🔍 Output do Console

Quando executar o script Python, verá:

```
============================================================
SAFT Extractor - PULSE DOMINOS
============================================================
Data início: 01/01/2024
Data fim: 31/12/2024
Ficheiro saída: output/SAFT_20240101_20241231.xml
LocationCode: 01
============================================================

Executando spGetSAFTDetails com LocationCode: 01
  AuditFileVersion = 1.04_01
  CompanyID = 12345
  TaxRegistrationNumber = 501234567
  TaxAccountingBasis = F
  CompanyName = MINHA EMPRESA LDA
  BuildingNumber = 123
  StreetName = Rua Principal
  AddressDetail = Rua Principal, 123
  City = Lisboa
  PostalCode = 1000-001
  Region = Lisboa
  Country = PT
  CurrencyCode = EUR
  TaxEntity = Global
  ProductCompanyTaxID = 501234567
  SoftwareCertificateNumber = 1234
  ProductID = PULSE DOMINOS
  Telephone = 210000000
  Email = geral@empresa.pt

Executando spGetSAFTXMLHeaderDetails com LocationCode: 01
  LocationCode: 01
  Version (ProductVersion): 1.2.3.4

Header montado com sucesso!
  CompanyName: MINHA EMPRESA LDA
  ProductID: PULSE DOMINOS
  ProductVersion: 1.2.3.4
```

## 🛠️ Como Adicionar Novo Campo ao Header

### Passo 1: Adicionar à Tabela SAFTDetails

```sql
INSERT INTO GovernmentReceipt..SAFTDetails (Location_Code, Setting, Value)
VALUES ('01', 'MeuNovoCampo', 'Valor do novo campo')
```

### Passo 2: Ler no Python

Edite `services/saft_xml_details.py`:

```python
# No método get_header(), após ler saft_details:
header.MeuNovoCampo = saft_details.get('MeuNovoCampo')
```

### Passo 3: Adicionar ao Modelo

Edite `models/saft_models.py`:

```python
@dataclass
class SAFTHeader:
    # ... campos existentes ...
    MeuNovoCampo: Optional[str] = None
```

### Passo 4: Adicionar ao XML

Edite `services/saft_xml_object.py`:

```python
# No método _write_header():
if header.MeuNovoCampo:
    etree.SubElement(header_elem, "MeuNovoCampo").text = header.MeuNovoCampo
```

### Passo 5: Executar

```bash
python saft_extractor.py --start 2024-01-01 --end 2024-12-31
```

**Pronto!** O novo campo aparecerá no XML. 🎉

## ✅ Resumo

1. **spGetSAFTDetails** fornece quase todos os campos do header via Setting/Value
2. **spGetSAFTXMLHeaderDetails** fornece o ProductVersion
3. O código Python **combina** os dados das 2 SPs em um único objeto SAFTHeader
4. O SAFTHeader é serializado para XML conforme schema SAFT-PT 1.04_01

Esta abordagem permite:
- ✅ Separação clara de responsabilidades
- ✅ Flexibilidade para adicionar campos
- ✅ Manutenção fácil (adiciona na BD, lê no código)
- ✅ Sem hardcode de dados da empresa

---

**Versão:** 1.0.0  
**Data:** 2026-02-19  
**Compatível com:** SAFT-PT 1.04_01
