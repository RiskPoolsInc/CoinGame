using MimeTypeMap.List;

namespace App.Data.Storage; 

public class CustomMimeMapping : MimeMapping {
    public string GetContentType(FileInfo fileInfo) {
        var d = Mappings[fileInfo.Extension];
        return d.FirstOrDefault() ?? string.Empty;
    }
}