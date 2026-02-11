# 🔧 Guia de Configuração - SAFTExtractor

## Configuração Automática da Conexão

A aplicação **SAFTExtractor** está configurada para detectar automaticamente a base de dados PULSE DOMINOS, sem necessidade de introduzir servidor, base de dados ou credenciais na interface.

## Como Configurar

### 1. Editar App.config

O ficheiro `App.config` está localizado na pasta do projeto. Abra-o e ajuste conforme o seu ambiente:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <connectionStrings>
        <!-- Connection string para a base de dados PULSE DOMINOS -->
        <add name="PulseDB" 
             connectionString="Data Source=.;Initial Catalog=POS;Integrated Security=True;TrustServerCertificate=True" 
             providerName="System.Data.SqlClient" />
    </connectionStrings>
    
    <appSettings>
        <!-- Código da localização/loja (usado em todas as SPs) -->
        <add key="LocationCode" value="01" />
    </appSettings>
</configuration>
```

### 2. Ajustar Connection String

#### Opção 1: Autenticação Windows (Recomendada)
```xml
<add name="PulseDB" 
     connectionString="Data Source=SERVIDOR;Initial Catalog=POS;Integrated Security=True;TrustServerCertificate=True" />
```

#### Opção 2: Autenticação SQL Server
```xml
<add name="PulseDB" 
     connectionString="Data Source=SERVIDOR;Initial Catalog=POS;User ID=usuario;Password=senha;TrustServerCertificate=True" />
```

#### Opção 3: Servidor Local
```xml
<add name="PulseDB" 
     connectionString="Data Source=.;Initial Catalog=POS;Integrated Security=True;TrustServerCertificate=True" />
```

#### Opção 4: SQL Server Express
```xml
<add name="PulseDB" 
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=POS;Integrated Security=True;TrustServerCertificate=True" />
```

### 3. Definir LocationCode

O `LocationCode` identifica a loja/localização e é usado em todas as Stored Procedures:

```xml
<appSettings>
    <add key="LocationCode" value="01" />  <!-- Ajuste para o código da sua loja -->
</appSettings>
```

**Exemplos:**
- Loja 01: `value="01"`
- Loja 02: `value="02"`
- Matriz: `value="MATRIZ"`

## Verificação da Configuração

### Teste Automático no Arranque

Quando a aplicação iniciar, verá uma mensagem na consola:

✅ **Sucesso:**
```
Aplicação SAFTExtractor inicializada. LocationCode: 01
```

❌ **Erro:**
```
AVISO: Não foi possível conectar à base de dados PULSE.
Verifique a connection string no app.config ou configure manualmente.
```

### Como Resolver Erros de Conexão

1. **Verificar nome do servidor**
   - Abra SQL Server Management Studio
   - Veja o nome do servidor ao conectar
   - Use esse nome no `Data Source`

2. **Verificar nome da base de dados**
   - No SSMS, veja o nome da BD PULSE
   - Normalmente é `POS`
   - Ajuste `Initial Catalog` se necessário

3. **Verificar autenticação**
   - Se usa Windows: `Integrated Security=True`
   - Se usa SQL: `User ID=...;Password=...`

4. **Verificar permissões**
   - O utilizador precisa ter permissão de leitura na BD
   - Precisa poder executar as Stored Procedures

## Stored Procedures Necessárias

A aplicação chama estas SPs (já devem existir no PULSE DOMINOS):

1. `spGetSAFTXMLHeaderDetails(@LocationCode)`
2. `spGetSAFTDetails(@LocationCode)`
3. `spGetCustomersForSAFTXML(@LocationCode, @InvoiceNumbers)`
4. `spGetProductsForSAFTXML(@LocationCode, @ProductCodes)`
5. `spGetInvoicesForSAFTXML(@LocationCode, @StartDate, @EndDate)`

**Verificar se existem:**
```sql
SELECT name 
FROM sys.procedures 
WHERE name LIKE '%SAFT%'
ORDER BY name
```

## Interface Simplificada

Após configurar corretamente, a interface só mostra:

```
┌─ Geração de Ficheiro SAFT - PULSE DOMINOS ─┐
│ Data Início: [01/01/2024 ▼]                │
│ Data Fim:    [31/12/2024 ▼]                │
│ Ano Fiscal:  [2024]  (calculado)           │
│ [Gerar Ficheiro SAFT]                      │
└─────────────────────────────────────────────┘
```

**Sem necessidade de:**
- ❌ Introduzir servidor
- ❌ Introduzir base de dados
- ❌ Introduzir utilizador/password
- ❌ Testar conexão

Tudo é automático! 🎉

## Exemplos de Configuração

### Exemplo 1: Ambiente de Produção
```xml
<connectionStrings>
    <add name="PulseDB" 
         connectionString="Data Source=SERVIDOR-PROD;Initial Catalog=POS;Integrated Security=True;TrustServerCertificate=True" />
</connectionStrings>
<appSettings>
    <add key="LocationCode" value="01" />
</appSettings>
```

### Exemplo 2: Ambiente de Testes
```xml
<connectionStrings>
    <add name="PulseDB" 
         connectionString="Data Source=SERVIDOR-TEST;Initial Catalog=POS_TEST;Integrated Security=True;TrustServerCertificate=True" />
</connectionStrings>
<appSettings>
    <add key="LocationCode" value="99" />
</appSettings>
```

### Exemplo 3: Desenvolvimento Local
```xml
<connectionStrings>
    <add name="PulseDB" 
         connectionString="Data Source=localhost;Initial Catalog=POS;User ID=sa;Password=SuaSenha123;TrustServerCertificate=True" />
</connectionStrings>
<appSettings>
    <add key="LocationCode" value="DEV" />
</appSettings>
```

## Troubleshooting

### Erro: "Login failed for user"
**Solução:** Verificar utilizador e password ou usar Integrated Security.

### Erro: "Cannot open database POS"
**Solução:** Verificar se a base de dados existe e o nome está correto.

### Erro: "Could not find stored procedure spGetSAFTXMLHeaderDetails"
**Solução:** Ver pasta `Original SP` e criar as SPs necessárias.

### Erro: "Server not found"
**Solução:** Verificar nome do servidor e se o SQL Server está a correr.

## Vantagens da Configuração Automática

✅ **Simplicidade**: Utilizador apenas seleciona datas  
✅ **Segurança**: Sem passwords na interface  
✅ **Consistência**: Sempre usa a mesma BD configurada  
✅ **Produtividade**: Não precisa configurar toda vez  
✅ **Como o Original**: Comportamento idêntico ao Dp.SAFTXMLAddition

## Migração da Versão Anterior

Se estava a usar a versão com configuração manual:

1. Copie o `App.config` para a pasta da aplicação
2. Configure connection string e LocationCode
3. Compile novamente o projeto
4. Execute - interface estará simplificada

A aplicação agora funciona exatamente como o código original PULSE DOMINOS! 🎯

---

**Versão:** 2.0  
**Data:** 2026-02-11  
**Status:** ✅ Conexão Automática Implementada
