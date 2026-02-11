# ❓ FAQ - Perguntas Frequentes - SAFTExtractor

## 📋 Geral

### O que é o SAFTExtractor?
O SAFTExtractor é uma aplicação Windows Forms em C# que extrai dados do sistema PULSE DOMINOS e gera ficheiros SAFT-PT (Standard Audit File for Tax) conformes com os requisitos da Autoridade Tributária portuguesa.

### O que é SAFT-PT?
SAFT-PT (Standard Audit File for Tax - Portugal) é um formato de ficheiro XML normalizado pela AT (Autoridade Tributária) que contém informações contabilísticas e fiscais de uma empresa.

### É obrigatório ter SAFT?
Sim, em Portugal todas as empresas que usam sistemas de faturação informatizados são obrigadas a ter capacidade de gerar ficheiros SAFT-PT.

## 🔧 Instalação e Configuração

### Que requisitos preciso?
- Windows 7 ou superior
- .NET 8.0 Runtime ou superior
- SQL Server (onde está o PULSE DOMINOS)
- Permissões de leitura na base de dados

### Como instalo a aplicação?
1. Faça download do projeto
2. Compile no Visual Studio 2022 ou execute `dotnet build`
3. Execute o ficheiro `SAFTExtractor.exe`

### Preciso instalar algo na base de dados?
Sim, precisa criar as 5 Stored Procedures descritas no ficheiro `STORED_PROCEDURES.md`.

### Como sei se as SPs estão corretas?
Execute cada SP manualmente no SQL Server Management Studio e verifique se retornam dados.

## 💾 Base de Dados

### A aplicação modifica dados na base de dados?
**NÃO**. A aplicação apenas **lê** dados. Nunca altera, adiciona ou remove dados do PULSE DOMINOS.

### Posso usar com outras bases de dados além do PULSE DOMINOS?
Sim! A aplicação funciona com qualquer base de dados SQL Server, desde que crie as Stored Procedures adequadas e adapte os nomes das tabelas.

### Como adapto para a minha estrutura de base de dados?
Edite as Stored Procedures no ficheiro `STORED_PROCEDURES.md` para mapear as suas tabelas e colunas.

### Preciso dar permissões especiais ao utilizador SQL?
Apenas permissões de leitura (SELECT) e execução das Stored Procedures (EXECUTE).

## 📄 Geração de Ficheiros SAFT

### Quanto tempo demora a gerar o ficheiro?
Depende da quantidade de dados:
- Poucos meses: segundos
- 1 ano com muitos documentos: 1-5 minutos
- Anos anteriores com histórico grande: até 10 minutos

### Posso gerar SAFT de anos anteriores?
Sim! Basta selecionar o ano fiscal desejado na aplicação.

### O ficheiro SAFT fica muito grande?
Depende do volume de dados. Um ano com milhares de faturas pode gerar ficheiros de vários MB.

### Posso gerar SAFT de apenas um mês?
A aplicação gera o ano completo. Para períodos parciais, você pode:
1. Modificar o código para aceitar datas específicas
2. Ou filtrar o XML gerado posteriormente

### Como sei se o ficheiro gerado está correto?
Use o validador oficial da AT disponível no Portal das Finanças.

## 🔐 Segurança

### A aplicação guarda passwords?
**NÃO**. A aplicação não guarda passwords. Você precisa inserir as credenciais cada vez que executar.

### É seguro usar Autenticação Windows?
Sim! É a opção **mais segura** e recomendada.

### Posso partilhar o ficheiro SAFT?
O ficheiro SAFT contém dados sensíveis da empresa. Apenas partilhe com a AT ou auditores autorizados.

## ❌ Erros Comuns

### "Não foi possível conectar à base de dados"
**Soluções:**
- Verifique se o SQL Server está a correr
- Confirme o nome do servidor e base de dados
- Verifique credenciais
- Teste a conexão com SQL Server Management Studio primeiro

### "Could not find stored procedure 'spGetCustomersForSAFTXML'"
**Soluções:**
- Execute os scripts SQL do ficheiro `STORED_PROCEDURES.md`
- Verifique se criou as SPs na base de dados correta
- Confirme que o utilizador tem permissões de execução

### "O ficheiro XML gerado está vazio"
**Soluções:**
- Verifique se há dados no ano fiscal selecionado
- Execute as SPs manualmente para confirmar que retornam dados
- Verifique se as tabelas têm registos

### "Invalid column name..."
**Soluções:**
- Adapte as Stored Procedures aos nomes reais das suas colunas
- Verifique a estrutura da sua base de dados
- Consulte o ficheiro `STORED_PROCEDURES.md` para exemplos

### Validador da AT reporta erros no ficheiro
**Soluções comuns:**
- **NIF inválido**: Valide os NIFs na base de dados
- **ATCUD em falta**: Obrigatório desde 2023
- **Código postal inválido**: Formato deve ser XXXX-XXX
- **Campo obrigatório em falta**: Adicione o campo na SP correspondente

## 🆕 Campos Novos

### Como adiciono um novo campo ao SAFT?
Siga os 4 passos no README.md, secção "Como Adicionar Novos Campos":
1. Atualizar Stored Procedures
2. Atualizar Models
3. Atualizar SAFTXMLDetails
4. Atualizar SAFTXMLObject (se necessário)

### Posso adicionar campos personalizados?
Sim, mas certifique-se que estão em conformidade com o schema SAFT-PT da AT.

### O que é o campo ATCUD?
ATCUD (Código Único do Documento) é obrigatório desde 2023 e deve ser gerado pelo seu ERP.

### O que é o Hash?
É uma assinatura digital do documento, calculada pelo ERP, que garante a integridade do documento.

## 🔄 Manutenção

### Como atualizo a aplicação?
1. Faça backup do projeto atual
2. Baixe a nova versão
3. Compile e teste

### Preciso recriar as SPs ao atualizar?
Depende da atualização. Consulte as notas de versão (changelog).

### Como faço backup dos dados?
Faça backup da base de dados SQL Server usando ferramentas nativas do SQL Server.

## 📊 Performance

### A geração está muito lenta
**Soluções:**
- Crie índices nas colunas de data das tabelas
- Otimize as Stored Procedures
- Gere por períodos menores (trimestral)
- Verifique se há queries lentas nas SPs

### Posso gerar vários ficheiros em paralelo?
Não recomendado. Pode sobrecarregar o servidor de BD.

## 🌐 Compatibilidade

### Funciona com SQL Server Express?
Sim!

### Funciona com Azure SQL Database?
Sim!

### Funciona com MySQL ou PostgreSQL?
Não. Atualmente apenas SQL Server. Seria necessário adaptar o código.

### Qual versão do SAFT-PT é gerada?
Versão 1.04_01 (mais recente no momento).

### Funciona com outras versões do .NET?
Foi desenvolvido para .NET 8.0, mas pode ser adaptado para outras versões.

## 📞 Suporte

### Onde encontro mais documentação?
- `README.md` - Documentação completa
- `QUICK_START.md` - Guia de início rápido
- `STORED_PROCEDURES.md` - Guia de SPs
- `CONFIGURATION_EXAMPLES.md` - Exemplos de configuração

### Como reporto um bug?
Abra uma issue no repositório GitHub com:
- Descrição do problema
- Passos para reproduzir
- Mensagem de erro (se houver)
- Versão da aplicação

### Posso contribuir com o projeto?
Sim! Pull requests são bem-vindos. Consulte o README.md para diretrizes.

### A aplicação é gratuita?
Sim, é fornecida "como está" sem garantias.

## 🎓 Aprendizagem

### Onde aprendo mais sobre SAFT-PT?
- [Portal das Finanças](https://info.portaldasfinancas.gov.pt)
- [Documentação oficial SAFT-PT (PDF)](https://info.portaldasfinancas.gov.pt/pt/apoio_contribuinte/Faturacao_e_software/Comunicacao_Dados_Faturacao/Documents/SAF_T_PT.pdf)
- Ordem dos Contabilistas Certificados

### Preciso ser programador para usar?
Não! A interface é simples. Mas para configurar as Stored Procedures, conhecimentos básicos de SQL são úteis.

### Como aprendo a validar ficheiros SAFT?
Use o validador oficial da AT e consulte a documentação do Portal das Finanças.

## 🚀 Próximas Versões

### Que funcionalidades estão planeadas?
- Configuração via ficheiro JSON
- Suporte a múltiplas empresas
- Geração de períodos personalizados
- Validação automática com API da AT
- Exportação para diferentes versões SAFT

### Posso sugerir funcionalidades?
Sim! Abra uma issue no GitHub com o label "feature request".

---

**Não encontrou a resposta? Consulte a documentação completa no README.md ou abra uma issue no GitHub!**
