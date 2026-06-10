using System.Text.Json;
using FileToJsonConverter.Enums;
using FileToJsonConverter.Models;

namespace FileToJsonConverter.Services;

public class FileConverterService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public string Convert(string filePath, int templateDocumentTypeId)
    {
        var fileBytes = File.ReadAllBytes(filePath);
        var base64Data = System.Convert.ToBase64String(fileBytes);

        var filePathName = Path.GetFileNameWithoutExtension(filePath);
        var fileExtension = Path.GetExtension(filePath).TrimStart('.');
        var isGeral = templateDocumentTypeId == (int)ETemplateDocumentType.Geral;

        string fileName;
        int? templateIdOutput;

        if (isGeral)
        {
            fileName = filePathName;
            templateIdOutput = null;
        }
        else
        {
            fileName = Enum.GetName(typeof(ETemplateDocumentType), templateDocumentTypeId) ?? filePathName;
            templateIdOutput = templateDocumentTypeId;
        }

        var output = new DocumentOutput
        {
            FileName = fileName,
            FileExtension = fileExtension,
            Base64Data = base64Data,
            TemplateDocumentTypeId = templateIdOutput
        };

        var json = JsonSerializer.Serialize(output, JsonOptions);

        var directory = Path.GetDirectoryName(filePath)!;
        var outputPath = Path.Combine(directory, $"{fileName}.json");

        File.WriteAllText(outputPath, json, System.Text.Encoding.UTF8);

        return outputPath;
    }
}