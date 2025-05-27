﻿using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}