# SAFT Extractor Python - PULSE DOMINOS

Versão Python do extractor SAFT para facilitar testes e modificações.

Esta versão extrai dados SAFT do sistema PULSE DOMINOS usando Python, permitindo modificações rápidas sem necessidade de recompilar código C#.

## 📋 Características

- ✅ **Extração completa de dados SAFT** do PULSE DOMINOS
- ✅ **Header combinado de 2 SPs** (`spGetSAFTDetails` + `spGetSAFTXMLHeaderDetails`)
- ✅ **Compatível com schema SAFT-PT 1.04_01**
- ✅ **Configuração via config.ini** (sem hardcode)
- ✅ **Interface linha de comando** (fácil de usar)
- ✅ **Código Python limpo** (fácil de modificar)
- ✅ **Executa no servidor** (sem necessidade de compilar)

## 🚀 Instalação Rápida

### 1. Instalar Python 3.8+

Certifique-se de ter Python 3.8 ou superior instalado:

```bash
python --version
```

### 2. Instalar Dependências

```bash
cd saft_extractor_python
pip install -r requirements.txt
```

**Dependências necessárias:**
- `pyodbc` - Conexão SQL Server
- `lxml` - Geração XML
- `configparser` - Configuração
- `python-dateutil` - Manipulação de datas

### 3. Instalar Driver ODBC SQL Server

**Windows:**
- O driver já vem instalado na maioria dos sistemas
- Se necessário, baixe de: [Microsoft ODBC Driver for SQL Server](https://learn.microsoft.com/en-us/sql/connect/odbc/download-odbc-driver-for-sql-server)

**Linux:**
```bash
# Ubuntu/Debian
sudo apt-get install unixodbc unixodbc-dev
sudo apt-get install msodbcsql17

# CentOS/RHEL
sudo yum install unixODBC unixODBC-devel
sudo yum install msodbcsql17
```

### 4. Configurar config.ini

Edite o ficheiro `config.ini`:

```ini
[Database]
Server = .
Database = POS
TrustedConnection = True
Driver = ODBC Driver 17 for SQL Server

[Settings]
LocationCode = 01
CommandTimeout = 0

[Output]
DefaultOutputPath = ./output
FilenameFormat = SAFT_{start}_{end}.xml
```

### 5. Testar Conexão

```bash
python saft_extractor.py --test-connection
```

## 📖 Como Usar

### Modo Básico

```bash
# Extrair ano completo de 2024
python saft_extractor.py --start 2024-01-01 --end 2024-12-31

# Ou com formato DD/MM/YYYY
python saft_extractor.py --start 01/01/2024 --end 31/12/2024
```

### Especificar Ficheiro de Saída

```bash
python saft_extractor.py --start 2024-01-01 --end 2024-12-31 --output ./meu_saft.xml
```

### Usar Config Alternativo

```bash
python saft_extractor.py --config config_producao.ini --start 2024-01-01 --end 2024-12-31
```

### Testar Conexão

```bash
python saft_extractor.py --test-connection
```

### Exemplos Práticos

```bash
# Trimestre Q1 2024
python saft_extractor.py --start 2024-01-01 --end 2024-03-31

# Mês de Janeiro 2024
python saft_extractor.py --start 2024-01-01 --end 2024-01-31

# Período personalizado
python saft_extractor.py --start 2024-03-15 --end 2024-09-20
```

## 🔧 Estrutura do Projeto

```
saft_extractor_python/
│
├── saft_extractor.py          # Script principal
├── config.ini                 # Configuração
├── requirements.txt           # Dependências Python
│
├── models/                    # Modelos de dados
│   ├── __init__.py
│   └── saft_models.py         # Classes: SAFTHeader, Customer, Product, Invoice
│
├── services/                  # Lógica de negócio
│   ├── __init__.py
│   ├── saft_xml_details.py    # Extração de dados (SPs)
│   └── saft_xml_object.py     # Geração XML
│
├── utils/                     # Utilitários
│   ├── __init__.py
│   └── pulse_connection.py    # Gestão de conexões SQL
│
└── output/                    # Ficheiros SAFT gerados
```

## 🎯 Como Funciona o Header

O header SAFT é montado combinando dados de **2 Stored Procedures**:

### 1. spGetSAFTDetails (quase todo o header)

Retorna pares `Setting` / `Value`:

```sql
SELECT Setting, Value 
FROM GovernmentReceipt..SAFTDetails
WHERE Location_Code = @LocationCode
```

**Campos esperados:**
- `AuditFileVersion` → 1.04_01
- `CompanyID` → Código da empresa
- `TaxRegistrationNumber` → NIF
- `CompanyName` → Nome da empresa
- `AddressDetail`, `City`, `PostalCode`, etc.
- E outros campos do header...

### 2. spGetSAFTXMLHeaderDetails (ProductVersion)

Retorna `LocationCode` e `Version`:

```sql
SELECT DISTINCT 
    LC.Location_Code AS LocationCode,
    VER.Version AS Version 
FROM Location_Codes AS LC
INNER JOIN Version AS VER ON VER.Location_Code = @LocationCode
```

O campo `Version` vai para `<ProductVersion>` no XML.

### Resultado no XML

```xml
<Header>
    <AuditFileVersion>1.04_01</AuditFileVersion>
    <CompanyID>...</CompanyID>
    <TaxRegistrationNumber>...</TaxRegistrationNumber>
    <CompanyName>...</CompanyName>
    <!-- ... outros campos do spGetSAFTDetails ... -->
    <ProductID>PULSE DOMINOS</ProductID>
    <ProductVersion>1.2.3.4</ProductVersion> <!-- vem do spGetSAFTXMLHeaderDetails -->
</Header>
```

## 🛠️ Modificar o Código

### Adicionar Novo Campo ao Header

1. Edite `services/saft_xml_details.py`:

```python
# No método get_header(), adicione:
header.MeuNovoCampo = saft_details.get('MeuNovoCampo')
```

2. Edite `models/saft_models.py`:

```python
@dataclass
class SAFTHeader:
    # ... campos existentes ...
    MeuNovoCampo: Optional[str] = None
```

3. Edite `services/saft_xml_object.py`:

```python
# No método _write_header(), adicione:
if header.MeuNovoCampo:
    etree.SubElement(header_elem, "MeuNovoCampo").text = header.MeuNovoCampo
```

4. Execute novamente:

```bash
python saft_extractor.py --start 2024-01-01 --end 2024-12-31
```

**Sem necessidade de compilar!** 🎉

### Adicionar Nova SP

Edite `services/saft_xml_details.py` e adicione novo método:

```python
def get_meus_dados(self) -> List[MeuModelo]:
    conn = PulseConnection.create_connection()
    cursor = conn.cursor()
    
    try:
        cursor.execute(
            "{CALL dbo.spMinhaNovaStoredProcedure(?)}",
            self.location_code
        )
        
        dados = []
        for row in cursor.fetchall():
            # Processar dados...
            dados.append(...)
        
        return dados
    finally:
        cursor.close()
        conn.close()
```

## 📊 Stored Procedures Necessárias

O código chama as seguintes SPs (devem existir na base de dados):

| SP | Parâmetros | Descrição |
|----|------------|-----------|
| `spGetSAFTDetails` | @LocationCode | Header (Setting/Value) |
| `spGetSAFTXMLHeaderDetails` | @LocationCode | ProductVersion |
| `spGetCustomersForSAFTXML` | @LocationCode | Clientes |
| `spGetProductsForSAFTXML` | @LocationCode | Produtos |
| `spGetInvoicesForSAFTXML` | @LocationCode, @StartDate, @EndDate | Faturas |

Veja a pasta `../Original SP/` para o código SQL completo.

## 🐛 Troubleshooting

### Erro: "Can't open lib 'ODBC Driver 17 for SQL Server'"

**Solução:** Instale o driver ODBC:
- Windows: [Microsoft ODBC Driver](https://learn.microsoft.com/en-us/sql/connect/odbc/download-odbc-driver-for-sql-server)
- Linux: `sudo apt-get install msodbcsql17`

Ou altere o driver no `config.ini`:
```ini
Driver = SQL Server
```

### Erro: "Login failed for user"

**Solução:** Verifique credenciais no `config.ini`:
- Se usar Autenticação Windows: `TrustedConnection = True`
- Se usar SQL Auth: `TrustedConnection = False` e preencha Username/Password

### Erro: "Stored Procedure not found"

**Solução:** Crie as SPs na base de dados. Veja `../Original SP/` para o código SQL.

### Erro: "spGetSAFTDetails não retornou dados"

**Solução:** 
1. Verifique se o LocationCode no `config.ini` está correto
2. Verifique se existem dados na tabela `GovernmentReceipt..SAFTDetails`
3. Execute manualmente:
   ```sql
   SELECT * FROM GovernmentReceipt..SAFTDetails WHERE Location_Code = '01'
   ```

### Ficheiro XML vazio ou incompleto

**Solução:** Execute com verbose para ver detalhes:
```bash
python saft_extractor.py --start 2024-01-01 --end 2024-12-31 2>&1 | tee log.txt
```

## 🔐 Segurança

- ⚠️ **Nunca commite o config.ini com passwords**
- ⚠️ Use `.gitignore` para excluir `config.ini` e `output/`
- ✅ Em produção, use Autenticação Windows (`TrustedConnection = True`)
- ✅ Dê apenas permissões de leitura à conta SQL

## ✨ Vantagens da Versão Python

| Vantagem | Descrição |
|----------|-----------|
| **Sem compilar** | Modificou? Execute imediatamente |
| **Fácil debug** | Adicione `print()` onde quiser |
| **Portável** | Roda em Windows, Linux, Mac |
| **Configurável** | Tudo no `config.ini` |
| **Open source** | Python é gratuito |
| **Extensível** | Adicione novas funcionalidades facilmente |

## 📝 Comparação: C# vs Python

| Aspecto | C# (SAFTExtractor) | Python (saft_extractor_python) |
|---------|-------------------|--------------------------------|
| **Modificar código** | Requer recompilação | Edita e executa |
| **Dependências** | .NET 8.0 | Python 3.8+ |
| **Performance** | Mais rápido | Suficientemente rápido |
| **Debug** | Visual Studio | print() statements |
| **Distribuição** | .exe (Windows) | .py (multiplataforma) |
| **Facilidade** | Média | Alta |

## 📞 Suporte

Para problemas ou dúvidas:
1. Verifique o `TROUBLESHOOTING.md` na raiz do projeto
2. Execute com `--test-connection` para diagnosticar
3. Verifique logs do SQL Server
4. Consulte documentação das SPs em `../Original SP/`

## 📄 Licença

MIT License - Veja `../LICENSE`

---

**Versão:** 1.0.0  
**Data:** 2026-02-19  
**Compatível com:** SAFT-PT 1.04_01  
**Python:** 3.8+
