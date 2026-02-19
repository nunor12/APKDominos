# 🔍 Guia de Resolução de Problemas - SAFTExtractor

## Erro: "Não foi possível obter o header SAFT"

Este erro ocorre quando a aplicação não consegue obter os dados da empresa da base de dados PULSE DOMINOS.

### Causas Possíveis e Soluções

#### 1. Stored Procedure Não Encontrada

**Erro:**
```
ERRO: Stored Procedure 'spGetSAFTXMLHeaderDetails' não foi encontrada!
```

**Solução:**
1. Abra SQL Server Management Studio (SSMS)
2. Conecte à base de dados PULSE DOMINOS
3. Execute a query:
   ```sql
   SELECT name 
   FROM sys.procedures 
   WHERE name LIKE '%SAFT%'
   ORDER BY name
   ```
4. Se `spGetSAFTXMLHeaderDetails` não aparecer, crie-a usando o código da pasta `Original SP`

**Ficheiro necessário:** `Original SP/spGetSAFTXMLHeaderDetails.sql`

---

#### 2. LocationCode Incorreto

**Erro:**
```
A Stored Procedure 'spGetSAFTXMLHeaderDetails' não retornou dados.
LocationCode usado: '01'
```

**Solução:**
1. Verifique qual LocationCode existe na base de dados:
   ```sql
   SELECT DISTINCT LocationCode 
   FROM dbo.Locations  -- ou tabela equivalente
   ORDER BY LocationCode
   ```

2. Abra o `App.config` e ajuste:
   ```xml
   <appSettings>
       <add key="LocationCode" value="SEU_CODIGO_AQUI" />
   </appSettings>
   ```

---

#### 3. Base de Dados Não Encontrada

**Erro:**
```
ERRO: Base de dados não encontrada!
Connection String: Data Source=.;Initial Catalog=POS;...
```

**Solução:**
1. Verifique no SSMS qual o nome correto da base de dados PULSE
2. Abra o `App.config` e ajuste:
   ```xml
   <connectionStrings>
       <add name="PulseDB" 
            connectionString="Data Source=SEU_SERVIDOR;Initial Catalog=NOME_CORRETO_BD;..." />
   </connectionStrings>
   ```

**Bases de dados comuns:**
- `POS` - Sistema de ponto de venda
- `PULSE` - Sistema PULSE DOMINOS
- `Dominos` - Sistema Dominos

---

#### 4. Erro de Autenticação

**Erro:**
```
ERRO: Falha na autenticação!
```

**Soluções:**

**Opção A: Usar Autenticação Windows (Recomendado)**
```xml
<add name="PulseDB" 
     connectionString="Data Source=SERVIDOR;Initial Catalog=POS;Integrated Security=True;TrustServerCertificate=True" />
```

**Opção B: Usar Autenticação SQL Server**
```xml
<add name="PulseDB" 
     connectionString="Data Source=SERVIDOR;Initial Catalog=POS;User ID=usuario;Password=senha;TrustServerCertificate=True" />
```

**Verificar permissões:**
```sql
-- Verificar se utilizador tem acesso
EXEC sp_helplogins 'SEU_USUARIO'

-- Dar permissões de leitura (executar como admin)
USE POS
GRANT SELECT TO SEU_USUARIO
GRANT EXECUTE TO SEU_USUARIO
```

---

#### 5. Servidor SQL Não Está em Execução

**Erro:**
```
ERRO ao conectar à base de dados PULSE DOMINOS
```

**Solução:**
1. Verificar se SQL Server está em execução:
   - Windows: Services → SQL Server (MSSQLSERVER)
   - Ou: `services.msc` → procurar SQL Server

2. Iniciar o serviço se estiver parado

3. Testar conexão:
   ```
   sqlcmd -S SERVIDOR -E
   ```

---

#### 6. Nome do Servidor Incorreto

**Como descobrir o nome correto:**

**Método 1: SQL Server Management Studio**
- Ao conectar, o nome aparece no dropdown "Server name"
- Exemplos: `.`, `localhost`, `.\SQLEXPRESS`, `SERVIDOR\INSTANCIA`

**Método 2: Linha de comando**
```cmd
sqlcmd -L
```
Lista todos os servidores SQL na rede

**Método 3: Registry (Windows)**
```
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL
```

---

## Verificar Configuração Atual

### 1. Ver Connection String Configurada

Ao iniciar a aplicação, verá na consola:
```
=== SAFTExtractor - PULSE DOMINOS ===
LocationCode configurado: '01'
Connection String: Data Source=.;Initial Catalog=POS;Integrated Security=True;...

✓ Conexão estabelecida com sucesso!
  Servidor: SERVIDOR\INSTANCIA
  Base de dados: POS
```

ou

```
✗ AVISO: Não foi possível conectar à base de dados PULSE.
Erro SQL: [mensagem de erro]
```

### 2. Verificar Stored Procedures Necessárias

Execute no SSMS:
```sql
-- Lista de SPs necessárias
SELECT 
    CASE 
        WHEN name = 'spGetSAFTXMLHeaderDetails' THEN '✓'
        WHEN name = 'spGetSAFTDetails' THEN '✓'
        WHEN name = 'spGetCustomersForSAFTXML' THEN '✓'
        WHEN name = 'spGetProductsForSAFTXML' THEN '✓'
        WHEN name = 'spGetInvoicesForSAFTXML' THEN '✓'
        ELSE '?'
    END as Status,
    name as StoredProcedure,
    create_date as DataCriacao
FROM sys.procedures 
WHERE name LIKE '%SAFT%'
ORDER BY name
```

**Resultado esperado:**
```
✓ spGetCustomersForSAFTXML
✓ spGetInvoicesForSAFTXML
✓ spGetProductsForSAFTXML
✓ spGetSAFTDetails
✓ spGetSAFTXMLHeaderDetails
```

### 3. Testar Stored Procedure Manualmente

```sql
-- Testar se SP retorna dados
EXEC spGetSAFTXMLHeaderDetails @LocationCode = '01'

-- Deve retornar uma linha com:
-- CompanyID, TaxRegistrationNumber, CompanyName, AddressDetail, City, etc.
```

Se não retornar dados:
- O LocationCode não existe
- A tabela de configurações da empresa está vazia
- A SP está a procurar na tabela errada

---

## Checklist de Diagnóstico

Siga esta ordem para diagnosticar problemas:

- [ ] **1. SQL Server está em execução?**
  - Services → SQL Server (MSSQLSERVER) → Estado: Iniciado

- [ ] **2. Nome do servidor está correto?**
  - Testar: `sqlcmd -S NOME_SERVIDOR -E`

- [ ] **3. Base de dados existe?**
  - Verificar no SSMS ou: `sqlcmd -S SERVIDOR -E -Q "SELECT name FROM sys.databases"`

- [ ] **4. Tem permissões?**
  - Consegue conectar no SSMS com as mesmas credenciais?

- [ ] **5. SPs existem?**
  - Ver query na seção "Verificar Stored Procedures Necessárias"

- [ ] **6. LocationCode correto?**
  - Executar: `EXEC spGetSAFTXMLHeaderDetails @LocationCode = 'SEU_CODIGO'`

- [ ] **7. App.config está correto?**
  - Connection string aponta para servidor e BD corretos?
  - LocationCode corresponde ao que existe na BD?

---

## Exemplos de Configuração Correta

### Desenvolvimento Local
```xml
<connectionStrings>
    <add name="PulseDB" 
         connectionString="Data Source=.;Initial Catalog=POS;Integrated Security=True;TrustServerCertificate=True" />
</connectionStrings>
<appSettings>
    <add key="LocationCode" value="01" />
</appSettings>
```

### SQL Server Express
```xml
<connectionStrings>
    <add name="PulseDB" 
         connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=POS;Integrated Security=True;TrustServerCertificate=True" />
</connectionStrings>
<appSettings>
    <add key="LocationCode" value="01" />
</appSettings>
```

### Servidor Remoto (Autenticação Windows)
```xml
<connectionStrings>
    <add name="PulseDB" 
         connectionString="Data Source=SERVIDOR-PROD;Initial Catalog=POS;Integrated Security=True;TrustServerCertificate=True" />
</connectionStrings>
<appSettings>
    <add key="LocationCode" value="LOJA01" />
</appSettings>
```

### Servidor Remoto (Autenticação SQL)
```xml
<connectionStrings>
    <add name="PulseDB" 
         connectionString="Data Source=SERVIDOR-PROD;Initial Catalog=POS;User ID=saft_user;Password=SuaSenha123;TrustServerCertificate=True" />
</connectionStrings>
<appSettings>
    <add key="LocationCode" value="LOJA01" />
</appSettings>
```

---

## Mensagens de Erro Detalhadas

A partir da versão 2.1, a aplicação fornece mensagens de erro muito mais detalhadas:

### Erro de SP Não Encontrada
```
ERRO: Stored Procedure 'spGetSAFTXMLHeaderDetails' não foi encontrada!

A SP precisa existir na base de dados para a aplicação funcionar.

Verifique a pasta 'Original SP' no repositório para obter o código SQL.

Detalhes técnicos: Could not find stored procedure 'spGetSAFTXMLHeaderDetails'.
```

### Erro de Dados Não Retornados
```
A Stored Procedure 'spGetSAFTXMLHeaderDetails' não retornou dados.

LocationCode usado: '01'

Verifique se:
1. O LocationCode '01' existe na base de dados
2. Existem dados de empresa configurados para este LocationCode
3. A SP está a retornar os campos corretos
```

### Erro de Conexão
```
ERRO ao conectar à base de dados PULSE DOMINOS:

Connection String: Data Source=.;Initial Catalog=POS;Integrated Security=True;...
LocationCode: '01'

Verifique:
1. O SQL Server está em execução
2. O App.config tem a connection string correta
3. Tem permissões para aceder à base de dados

Detalhes técnicos: [mensagem técnica]
```

---

## Obter Ajuda

Se após seguir este guia ainda tiver problemas:

1. **Copie a mensagem de erro completa**
   - Inclui connection string (sem password)
   - Inclui LocationCode
   - Inclui detalhes técnicos

2. **Verifique a consola ao iniciar**
   - Mostra informações de diagnóstico
   - Indica se a conexão foi bem sucedida

3. **Execute as queries de diagnóstico**
   - Verify SPs exist
   - Test SP manually
   - Check LocationCode

4. **Verifique ficheiros de log** (se existirem)
   - Podem conter informações adicionais

---

**Versão:** 2.1  
**Data:** 2026-02-11  
**Status:** ✅ Melhorias de Diagnóstico Implementadas
