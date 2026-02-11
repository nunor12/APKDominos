# 🚀 Guia de Configuração Rápida - SAFTExtractor

Este guia ajuda-o a começar rapidamente com a aplicação SAFTExtractor.

## 📦 Passo 1: Compilar a Aplicação

### Opção A: Visual Studio
1. Abra `APKDominos.sln` no Visual Studio 2022
2. Clique em **Build > Build Solution** (ou pressione `Ctrl+Shift+B`)
3. Execute a aplicação com **Debug > Start Without Debugging** (`Ctrl+F5`)

### Opção B: Linha de Comando
```bash
cd SAFTExtractor
dotnet build
dotnet run
```

## 🗄️ Passo 2: Configurar Base de Dados

### 2.1 Criar as Stored Procedures

Execute os scripts SQL do ficheiro `STORED_PROCEDURES.md` na sua base de dados PULSE DOMINOS.

**Importante**: Adapte os nomes das tabelas e colunas conforme a estrutura da sua BD.

```sql
-- Execute cada SP no SQL Server Management Studio
-- 1. spGetCustomersForSAFTXML
-- 2. spGetProductsForSAFTXML
-- 3. spGetInvoicesForSAFTXML
-- 4. spGetInvoiceLinesForSAFTXML
-- 5. spGetSAFTHeader
```

### 2.2 Testar as Stored Procedures

Antes de usar a aplicação, teste cada SP individualmente:

```sql
-- Teste básico
EXEC spGetCustomersForSAFTXML;
EXEC spGetProductsForSAFTXML;
EXEC spGetSAFTHeader @FiscalYear = 2024;
EXEC spGetInvoicesForSAFTXML 
    @StartDate = '2024-01-01', 
    @EndDate = '2024-01-31';  -- Apenas Janeiro para teste
```

Se as SPs retornarem dados, está pronto para continuar!

## ⚙️ Passo 3: Configurar a Aplicação

### 3.1 Executar a Aplicação

Execute `SAFTExtractor.exe` (ou através do Visual Studio).

### 3.2 Configurar Conexão

Na janela principal:

1. **Servidor**: Nome do servidor SQL (exemplo: `localhost` ou `SERVIDOR\INSTANCIA`)
2. **Base de Dados**: Nome da BD PULSE DOMINOS (exemplo: `PULSEDB`)
3. **Utilizador**: Username SQL (ou marque "Autenticação Windows")
4. **Password**: Password SQL
5. Clique em **"Testar Conexão"**

✅ Se aparecer "Conexão testada com sucesso!", está tudo configurado!

## 📄 Passo 4: Gerar Primeiro Ficheiro SAFT

### 4.1 Teste com Período Pequeno

Para o primeiro teste, use um período pequeno (ex: 1 mês):

1. **Ano Fiscal**: Selecione o ano (exemplo: `2024`)
2. Clique em **"Gerar Ficheiro SAFT"**
3. Escolha onde guardar (exemplo: `C:\SAFT\SAFT_2024_Teste.xml`)
4. Aguarde a geração

### 4.2 Verificar o Ficheiro

Após a geração:

1. ✅ Ficheiro XML foi criado
2. ✅ Abra no Notepad++ ou Visual Studio Code
3. ✅ Verifique se tem a estrutura XML correta
4. ✅ Confirme que tem dados (clientes, produtos, faturas)

## 🔍 Passo 5: Validar o SAFT

### 5.1 Validador Oficial da AT

1. Descarregue o validador oficial da AT:
   - [Portal das Finanças - Validador SAFT](https://info.portaldasfinancas.gov.pt/pt/apoio_contribuinte/Faturacao_e_software/Comunicacao_Dados_Faturacao/)

2. Execute o validador no ficheiro gerado

3. Corrija eventuais erros reportados

### 5.2 Erros Comuns

| Erro | Solução |
|------|---------|
| Campo obrigatório em falta | Adicione o campo na SP correspondente |
| Formato de data inválido | Verifique SAFTDate.cs |
| NIF inválido | Valide CustomerTaxID na base de dados |
| ATCUD em falta | Campo obrigatório desde 2023 |
| Hash em falta | Deve ser calculado pelo ERP |

## 🔧 Resolução de Problemas

### Erro: "Não foi possível conectar à base de dados"

**Soluções:**
- Verifique se o SQL Server está a correr
- Confirme nome do servidor e base de dados
- Teste credenciais no SQL Server Management Studio
- Verifique firewall e permissões

### Erro: "Could not find stored procedure..."

**Soluções:**
- Execute os scripts SQL do `STORED_PROCEDURES.md`
- Verifique se as SPs foram criadas na BD correta
- Confirme permissões de execução

### Erro: "Erro ao gerar ficheiro SAFT"

**Soluções:**
- Verifique os logs de erro (mensagem completa)
- Teste as SPs individualmente no SQL
- Verifique se há dados no período selecionado
- Confirme que todas as SPs retornam dados

### XML gerado está vazio ou incompleto

**Soluções:**
- Verifique se há dados no período fiscal selecionado
- Execute as SPs manualmente para confirmar que retornam dados
- Verifique se os campos estão corretamente mapeados

## 📊 Estrutura do Ficheiro SAFT Gerado

```
SAFT_2024.xml
│
├── Header (Cabeçalho da empresa)
├── MasterFiles
│   ├── Customers (Clientes)
│   └── Products (Produtos)
└── SourceDocuments
    └── SalesInvoices (Faturas)
        └── Invoice (cada fatura)
            ├── Lines (linhas da fatura)
            └── DocumentTotals (totais)
```

## 💡 Dicas Importantes

### 1. Comece Pequeno
- Primeiro teste: 1 mês de dados
- Depois: 1 trimestre
- Por fim: ano completo

### 2. Valide Sempre
- Nunca envie à AT sem validar primeiro
- Use o validador oficial
- Corrija todos os erros e avisos

### 3. Backup
- Faça backup da base de dados antes de qualquer operação
- Guarde cópias dos ficheiros SAFT gerados

### 4. Performance
- Para períodos longos, a geração pode demorar alguns minutos
- Considere gerar por trimestre se tiver muitos dados

### 5. Campos Novos
- Se precisar adicionar campos, consulte o README.md
- Seção "Como Adicionar Novos Campos"

## 📞 Suporte

### Documentação
- `README.md` - Documentação completa
- `STORED_PROCEDURES.md` - Guia de SPs
- Este ficheiro - Guia de configuração rápida

### Recursos Externos
- [Portal das Finanças - SAFT-PT](https://info.portaldasfinancas.gov.pt)
- [Schema SAFT-PT (PDF)](https://info.portaldasfinancas.gov.pt/pt/apoio_contribuinte/Faturacao_e_software/Comunicacao_Dados_Faturacao/Documents/SAF_T_PT.pdf)

## ✅ Checklist Final

Antes de usar em produção:

- [ ] Stored Procedures criadas e testadas
- [ ] Conexão à BD configurada e testada
- [ ] Ficheiro SAFT gerado com sucesso
- [ ] Ficheiro validado com validador da AT
- [ ] Todos os campos obrigatórios presentes
- [ ] Totais conferem com a contabilidade
- [ ] Teste realizado com período completo

## 🎉 Pronto!

Se completou todos os passos, está pronto para usar o SAFTExtractor em produção!

---

**Boa sorte com a extração de dados SAFT do PULSE DOMINOS! 🚀**
