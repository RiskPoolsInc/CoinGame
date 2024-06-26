using Microsoft.AspNetCore.Http;

namespace App.Core.Commands.Attachments;

public class AttachFileModel {
    public bool Auto { get; set; }
    public IFormFile File { get; set; }
}