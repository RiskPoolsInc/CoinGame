using Microsoft.AspNetCore.Http;

namespace App.Core.Commands.Attachments;

public class DirectFileCommand : IRequest<string> {
    public string FullPath { get; set; }
    public IFormFile File { get; set; }
}