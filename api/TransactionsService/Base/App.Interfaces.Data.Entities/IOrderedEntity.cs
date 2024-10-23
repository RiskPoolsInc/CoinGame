using System;

namespace App.Interfaces.Data.Entities;

public interface IOrderedEntity {
    DateTime CreatedOn { get; set; }
}