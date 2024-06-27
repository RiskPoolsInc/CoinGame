using Microsoft.AspNetCore.Http;

namespace App.Core.Commands.Attachments;

public class AttachTempModel : IFormFile {
    public string Description { get; set; }
    public MemoryStream MemoryStream { get; set; }
    public bool Auto { get; set; } = false;

    public Stream OpenReadStream() {
        MemoryStream.Seek(0, SeekOrigin.Begin);
        return MemoryStream;
    }

    public void CopyTo(Stream target) {
        OpenReadStream();
        MemoryStream.CopyTo(target);
    }

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = new()) {
        OpenReadStream();
        return MemoryStream.CopyToAsync(target, cancellationToken);
    }

    public string ContentType { get; } = null;
    public string ContentDisposition { get; } = null;
    public IHeaderDictionary Headers { get; } = new HeaderDictionary();
    public long Length => MemoryStream.Length;
    public string Name { get; set; }
    public string FileName { get; set; }
}