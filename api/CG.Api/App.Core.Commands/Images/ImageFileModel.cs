using Microsoft.AspNetCore.Http;

namespace App.Core.Commands.Images {

public class ImageFileModel
{
    public IFormFile File { get; set; }
    public IFormFile Preview { get; set; }
}
}