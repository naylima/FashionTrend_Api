﻿using System;

public class GetSupplierByEmailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
}

