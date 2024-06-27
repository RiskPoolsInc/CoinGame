using System;
using System.Collections.Generic;

namespace App.Interfaces.Data.Entities {

public interface ITaskEntity
{
    public Guid Id { get; set; }
    public Guid CreatedById { get; set; }

    public DateTime TaskDate { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }

    public Guid[]? AttachmentIds { get; set; }
    public decimal Award { get; set; }
    public ICollection<ITaskStep>? TaskSteps { get; set; }
    public string? Location { get; set; }
}
}