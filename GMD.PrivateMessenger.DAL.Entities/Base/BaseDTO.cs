﻿namespace GMD.PrivateMessenger.DAL.Entities.Base;

public class BaseDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
}
