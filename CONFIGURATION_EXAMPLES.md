# Exemplo de Configuração - SAFTExtractor

## ⚠️ Este ficheiro contém apenas exemplos, NÃO USE em produção!

## Configuração de Conexão à Base de Dados

### Exemplo 1: Autenticação Windows (Recomendado)
```
Servidor: SERVIDOR-SQL\INSTANCIA
Base de Dados: PULSEDOMINOS
Autenticação Windows: ✓ (marcado)
Utilizador: (vazio)
Password: (vazio)
```

### Exemplo 2: Autenticação SQL Server
```
Servidor: 192.168.1.100
Base de Dados: PULSEDOMINOS
Autenticação Windows: ☐ (desmarcado)
Utilizador: saft_user
Password: ********
```

### Exemplo 3: SQL Server Express Local
```
Servidor: localhost\SQLEXPRESS
Base de Dados: PULSEDOMINOS
Autenticação Windows: ✓ (marcado)
```

### Exemplo 4: Azure SQL Database
```
Servidor: meuservidor.database.windows.net
Base de Dados: PULSEDOMINOS
Autenticação Windows: ☐ (desmarcado)
Utilizador: admin@meuservidor
Password: ********
```

## 🔐 Segurança

### Boas Práticas:
1. ✅ Use sempre Autenticação Windows quando possível
2. ✅ Se usar SQL Auth, crie um utilizador específico apenas para SAFT
3. ✅ Dê apenas permissões de leitura (SELECT) e execução das SPs
4. ✅ Nunca partilhe passwords
5. ✅ Use passwords fortes
6. ❌ Nunca guarde passwords em ficheiros de texto

### Permissões SQL Necessárias

```sql
-- Criar utilizador para SAFT (exemplo)
CREATE LOGIN saft_user WITH PASSWORD = 'Password_Forte_123!';
CREATE USER saft_user FOR LOGIN saft_user;

-- Dar permissões apenas nas SPs necessárias
GRANT EXECUTE ON spGetCustomersForSAFTXML TO saft_user;
GRANT EXECUTE ON spGetProductsForSAFTXML TO saft_user;
GRANT EXECUTE ON spGetInvoicesForSAFTXML TO saft_user;
GRANT EXECUTE ON spGetInvoiceLinesForSAFTXML TO saft_user;
GRANT EXECUTE ON spGetSAFTHeader TO saft_user;

-- Permissão de leitura apenas (opcional, se necessário)
GRANT SELECT ON dbo.Customers TO saft_user;
GRANT SELECT ON dbo.Products TO saft_user;
GRANT SELECT ON dbo.Invoices TO saft_user;
GRANT SELECT ON dbo.InvoiceLines TO saft_user;
```

## 📅 Configurações de Período Fiscal

### Exemplos de Períodos:

| Descrição | Ano Fiscal | Período |
|-----------|------------|---------|
| Ano completo 2024 | 2024 | 01/01/2024 - 31/12/2024 |
| Ano completo 2023 | 2023 | 01/01/2023 - 31/12/2023 |
| Apenas Q1 2024 | 2024 | 01/01/2024 - 31/03/2024 |

**Nota**: A aplicação gera automaticamente o período completo do ano fiscal selecionado.

## 📂 Locais Sugeridos para Guardar Ficheiros SAFT

### Windows:
```
C:\SAFT\2024\SAFT_2024.xml
C:\SAFT\2023\SAFT_2023.xml
D:\Backups\SAFT\SAFT_2024_Janeiro.xml
```

### Nomenclatura Recomendada:
- `SAFT_[ANO].xml` - Exemplo: `SAFT_2024.xml`
- `SAFT_[ANO]_[MES].xml` - Exemplo: `SAFT_2024_Janeiro.xml`
- `SAFT_[ANO]_Q[TRIMESTRE].xml` - Exemplo: `SAFT_2024_Q1.xml`

## 🧪 Configuração para Testes

### Base de Dados de Teste
```
Servidor: SERVIDOR-TESTE\SQLEXPRESS
Base de Dados: PULSEDOMINOS_TEST
Autenticação Windows: ✓
```

**Dica**: Use sempre uma base de dados de teste primeiro!

## 📝 Notas de Configuração

### Header SAFT (configurável nas SPs)

Campos importantes a verificar no `spGetSAFTHeader`:

```sql
-- Exemplo de valores no Header
AuditFileVersion: '1.04_01'
TaxAccountingBasis: 'F'  -- F=Faturação, C=Contabilidade
CurrencyCode: 'EUR'
ProductID: 'SAFTExtractor/PULSE DOMINOS'
ProductVersion: '1.0'
SoftwareCertificateNumber: '[NÚMERO DO CERTIFICADO AT]'
```

⚠️ **Importante**: O `SoftwareCertificateNumber` deve ser obtido junto da Autoridade Tributária.

## 🔄 Configurações Avançadas (Futuro)

### Ficheiro de Configuração (config.json) - Exemplo futuro
```json
{
  "Database": {
    "Server": "localhost\\SQLEXPRESS",
    "Database": "PULSEDOMINOS",
    "IntegratedSecurity": true
  },
  "SAFT": {
    "DefaultFiscalYear": 2024,
    "OutputPath": "C:\\SAFT\\",
    "CompanyName": "Minha Empresa Lda",
    "TaxID": "123456789"
  }
}
```

**Nota**: Esta funcionalidade ainda não está implementada na v1.0.

## 📊 Validação de Dados

Antes de gerar o SAFT, confirme:

### Dados da Empresa (Header)
- [ ] Nome da empresa correto
- [ ] NIF correto
- [ ] Morada completa e correta
- [ ] Código postal no formato correto (XXXX-XXX)
- [ ] Certificado de software (se aplicável)

### Clientes
- [ ] Todos têm NIF válido
- [ ] Moradas completas
- [ ] País definido (PT por defeito)

### Produtos
- [ ] Códigos únicos
- [ ] Descrições preenchidas
- [ ] Tipo correto (P=Produto, S=Serviço)

### Faturas
- [ ] Numeração sequencial
- [ ] Datas válidas
- [ ] ATCUD preenchido (obrigatório desde 2023)
- [ ] Hash calculado (se aplicável)
- [ ] Totais corretos

## 💡 Dicas de Configuração

1. **Teste sempre primeiro**: Use dados de teste antes de produção
2. **Backup**: Faça backup antes de qualquer operação
3. **Validação**: Valide o ficheiro gerado antes de enviar à AT
4. **Documentação**: Guarde registo das configurações usadas
5. **Permissões**: Use o princípio do menor privilégio

## 🆘 Em Caso de Problemas

Verifique:
1. Conexão à base de dados
2. Permissões do utilizador SQL
3. Stored Procedures criadas
4. Dados existem no período selecionado
5. Campos obrigatórios estão preenchidos

---

**Para mais informações, consulte README.md e QUICK_START.md**
