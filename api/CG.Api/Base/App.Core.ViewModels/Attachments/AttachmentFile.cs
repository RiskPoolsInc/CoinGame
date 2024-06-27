namespace App.Core.ViewModels.Attachments; 

public class AttachmentFile : ExternalFile {
    public string ContentType { get; set; }
    public byte[] File { get; set; }
}