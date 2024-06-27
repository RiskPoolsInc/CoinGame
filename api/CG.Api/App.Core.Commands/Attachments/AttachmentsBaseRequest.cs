using Microsoft.AspNetCore.Http;

namespace App.Core.Commands.Attachments;

public class AttachmentsBaseRequest {
    public IFormFile[]? FileAttachments { get; set; }
}