using System;

namespace App.Interfaces.Data.Entities;

public interface ITaskStep
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public int Number { get; set; }
    public string Text { get; set; }
}