# FileToJsonConverter

Aplicação console .NET 10 que converte arquivos de diversos formatos em arquivos `.json` contendo o conteúdo codificado em **Base64**, juntamente com metadados de identificação do tipo de documento (`TemplateDocumentTypeId`).

Útil para enviar documentos binários a APIs que esperam um payload JSON com o arquivo em Base64.

## Formatos suportados

| Categoria   | Extensões                                          |
|-------------|-----------------------------------------------------|
| Documentos  | `.docx`, `.pdf`                                     |
| Planilhas   | `.xlsx`                                              |
| Imagens     | `.png`, `.jpg`, `.jpeg`, `.bmp`, `.gif`, `.tiff`, `.webp`, `.svg` |
| Dados       | `.json`                                              |

## Estrutura do JSON gerado

### Tipos padrão (ID ≥ 1)

```json
{
    "FileName": "MinutaPropostaApolice_76",
    "FileExtension": "pdf",
    "Base64Data": "UEsDBBQABgAIAAAAIQAAAAAAA...",
    "TemplateDocumentTypeId": 3
}
```

### Tipo Geral (ID = 0)

Quando o tipo **Geral (0)** é selecionado, o comportamento é diferente dos demais:

- `FileName` recebe o **nome original do arquivo** selecionado (sem extensão), em vez do nome do enum.
- `TemplateDocumentTypeId` é **omitido** do JSON gerado.

```json
{
    "FileName": "meu-documento",
    "FileExtension": "pdf",
    "Base64Data": "UEsDBBQABgAIAAAAIQAAAAAAA..."
}
```

O arquivo `.json` é salvo no **mesmo diretório** do arquivo original.

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) instalado
- Sistema operacional **Windows** (utiliza explorador de arquivos nativo para seleção)

## Como executar

### Via terminal

```bash
cd src
dotnet run
```

### Via executável publicado

```bash
cd src
dotnet publish -c Release -o ../publish
../publish/FileToJsonConverter.exe
```

## Fluxo de uso

1. A aplicação exibe uma mensagem de apresentação e um menu com as opções:
   - `[1] Iniciar operação` — inicia o processo de conversão.
   - `[2] Ler documentação` — exibe este arquivo README diretamente no console.
   - `[0] Sair` — encerra a aplicação.
2. Ao iniciar a operação, o explorador de arquivos do Windows é aberto para selecionar o arquivo (DOCX, PDF, XLSX, JSON, imagens).
3. Os tipos de documento disponíveis são listados no console.
4. O usuário digita o número do `TemplateDocumentTypeId` desejado.
5. O arquivo `.json` é gerado no mesmo diretório do arquivo selecionado.

<img width="1483" height="761" alt="image" src="https://github.com/user-attachments/assets/a32d096c-6266-46a8-b291-989a602fbc84" />

<img width="1481" height="758" alt="image" src="https://github.com/user-attachments/assets/f774744a-cbe8-4582-934c-3c21716dedeb" />

<img width="1476" height="760" alt="image" src="https://github.com/user-attachments/assets/06d1a345-9598-4657-bc96-bbccefca3cff" />

## Tipos de documento disponíveis

| ID | Tipo | Observação |
|----|------|------------|
| 0 | Geral | `FileName` = nome do arquivo original; `TemplateDocumentTypeId` omitido do JSON |
| 1 | MinutaPropostaApolice_75 | |
| 2 | ContratoContraGarantia | |
| 3 | MinutaPropostaApolice_76 | |
| 4 | MinutaPropostaEndosso_75 | |
| 5 | MinutaPropostaEndosso_76 | |
| 6 | CCGCustomizado | |
| 7 | LaudoDeEmissao | |
| 8 | LaudoDeEmissaoEndosso | |
| 9 | MinutaPropostaEndosso477_75 | |
| 10 | MinutaPropostaEndosso477_76 | |
| 11 | MinutaPropostaApoliceModeloMapfre | |
| 12 | DemonstrativoRestituicaoEndossoCancelamento | |
| 13 | EndossoComContinuacaoObjetoClausulasParticulares_662 | |
| 14 | EndossoComContinuacaoObjetoSemClausulasParticulares_662 | |
| 15 | EndossoSemContinuacaoObjetoClausulasParticulares_662 | |
| 16 | EndossoSemContinuacaoObjetoSemClausulasParticulares_662 | |
| 17 | EndossoComContinuacaoObjetoClausula_477_75 | |
| 18 | EndossoComContinuacaoObjetoSemClausula_477_75 | |
| 19 | EndossoSemContinuacaoObjetoClausula_477_75 | |
| 20 | EndossoSemContinuacaoObjetoSemClausula_477_75 | |
| 21 | EndossoComContinuacaoObjetoClausula_477_76 | |
| 22 | EndossoComContinuacaoObjetoSemClausula_477_76 | |
| 23 | EndossoSemContinuacaoObjetoClausula_477_76 | |
| 24 | EndossoSemContinuacaoObjetoSemClausula_477_76 | |
| 25 | DocumentoApolice_75 | |
| 26 | DocumentoApolice_76 | |
| 27 | CartaCredito | |
