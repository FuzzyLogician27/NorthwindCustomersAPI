using System;
using System.Collections.Generic;

namespace NorthwindCustomersAPI.Models;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;//secret

    public string CompanyName { get; set; } = null!;

    public string? ContactName { get; set; } //title and name together

    public string? ContactTitle { get; set; }

    public string? Address { get; set; }//secret

    public string? City { get; set; } //city, region and country together

    public string? Region { get; set; }

    public string? PostalCode { get; set; }//secret

    public string? Country { get; set; }

    public string? Phone { get; set; }//secret

    public string? Fax { get; set; }
}
