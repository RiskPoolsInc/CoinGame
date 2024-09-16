using App.Data.Entities.Core;

namespace App.Data.Entities.Senders;

public class ServiceProfile : ArchivableEntity {
    public string Name { get; set; }
    public string ApiKey { get; set; }
    public string? Description { get; set; }
}