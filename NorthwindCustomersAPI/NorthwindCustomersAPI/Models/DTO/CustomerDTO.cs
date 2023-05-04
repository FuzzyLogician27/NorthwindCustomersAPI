namespace NorthwindCustomersAPI.Models.DTO;

public class CustomerDTO
{

    public string CompanyName { get; set; } = null!;
    public string? FullNameAndTitle { get; set; }
    public string? Location { get; set; }
    public string? Fax { get; set; }
}
