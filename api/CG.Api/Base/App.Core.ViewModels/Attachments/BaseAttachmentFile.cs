namespace App.Core.ViewModels.Attachments;

public class BaseAttachmentFile : ExternalFile {
    public string ContentType { get; set; }
    public byte[] File { get; set; }
}