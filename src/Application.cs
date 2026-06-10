using FileToJsonConverter.Enums;
using FileToJsonConverter.Services;

namespace FileToJsonConverter;

public class Application
{
    private readonly FileConverterService _converterService = new();

    public void Run()
    {
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                   FileToJsonConverter                    ║");
        Console.WriteLine("║  Converte arquivos em JSON com conteúdo Base64 para      ║");
        Console.WriteLine("║  envio a APIs que utilizam TemplateDocumentType.         ║");
        Console.WriteLine("║                                                          ║");
        Console.WriteLine("║  Formatos: DOCX, PDF, XLSX, JSON, PNG, JPG, BMP,        ║");
        Console.WriteLine("║  GIF, TIFF, WebP, SVG                                   ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.WriteLine();

        bool running = true;
        while (running)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("  [1] Iniciar operação");
            Console.WriteLine("  [2] Ler documentação");
            Console.WriteLine("  [0] Sair");
            Console.Write("\nOpção: ");
            var opcao = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    RunConversion();
                    running = false;
                    break;
                case "2":
                    ShowReadme();
                    Console.WriteLine();
                    break;
                case "0":
                    Console.WriteLine("Operação cancelada.");
                    running = false;
                    break;
                default:
                    Console.WriteLine("❌ Opção inválida. Tente novamente.\n");
                    break;
            }
        }
    }

    private void RunConversion()
    {
        bool continuar;
        do
        {
            var filePath = SelectFile();
            if (filePath is null)
            {
                Console.WriteLine("Nenhum arquivo selecionado.");
            }
            else
            {
                Console.WriteLine($"Arquivo selecionado: {filePath}\n");

                ListDocumentTypes();

                var templateId = ReadTemplateDocumentTypeId();
                if (templateId is not null)
                {
                    try
                    {
                        var outputPath = _converterService.Convert(filePath, templateId.Value);
                        Console.WriteLine($"\n✅ Arquivo JSON gerado com sucesso: {outputPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n❌ Erro ao converter o arquivo: {ex.Message}");
                    }
                }
            }

            Console.WriteLine();
            Console.Write("Deseja converter mais algum documento? (S/N): ");
            var novaResposta = Console.ReadLine()?.Trim().ToUpper();
            continuar = novaResposta == "S";
            Console.WriteLine();
        }
        while (continuar);

        Console.WriteLine("Operação encerrada.");
    }

    private static void ShowReadme()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        string? readmePath = null;

        while (dir is not null)
        {
            var candidate = Path.Combine(dir.FullName, "README.md");
            if (File.Exists(candidate))
            {
                readmePath = candidate;
                break;
            }
            dir = dir.Parent;
        }

        if (readmePath is null)
        {
            Console.WriteLine("❌ Arquivo README.md não encontrado.");
            return;
        }

        Console.WriteLine(new string('═', 60));
        Console.WriteLine(File.ReadAllText(readmePath, System.Text.Encoding.UTF8));
        Console.WriteLine(new string('═', 60));
    }

    private static string? SelectFile()
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Selecione o arquivo para converter",
            Filter = "Todos os formatos suportados|*.docx;*.pdf;*.xlsx;*.json;*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tiff;*.webp;*.svg"
                + "|Documentos (*.docx, *.pdf)|*.docx;*.pdf"
                + "|Planilhas (*.xlsx)|*.xlsx"
                + "|Imagens (*.png, *.jpg, *.bmp, *.gif, *.tiff, *.webp, *.svg)|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tiff;*.webp;*.svg"
                + "|JSON (*.json)|*.json"
                + "|Todos os arquivos (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true
        };

        return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
    }

    private static void ListDocumentTypes()
    {
        Console.WriteLine("Selecione o TemplateDocumentTypeId:");

        foreach (var value in Enum.GetValues<ETemplateDocumentType>().OrderBy(e => (int)e))
        {
            Console.WriteLine($"  {(int)value,2} - {value}");
        }
    }

    private static int? ReadTemplateDocumentTypeId()
    {
        Console.Write("\nDigite o número correspondente ao TemplateDocumentTypeId: ");
        var input = Console.ReadLine()?.Trim();

        if (!int.TryParse(input, out var templateId))
        {
            Console.WriteLine("❌ Valor inválido.");
            return null;
        }

        if (!Enum.IsDefined(typeof(ETemplateDocumentType), templateId))
        {
            Console.WriteLine("❌ TemplateDocumentTypeId inválido.");
            return null;
        }

        return templateId;
    }
}
