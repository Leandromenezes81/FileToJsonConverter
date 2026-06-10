using System.Text.Json.Serialization;

namespace FileToJsonConverter.Models;

public class DocumentOutput
{
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public string Base64Data { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TemplateDocumentTypeId { get; set; }
}
