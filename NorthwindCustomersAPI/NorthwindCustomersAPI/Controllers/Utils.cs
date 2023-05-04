using NorthwindCustomersAPI.Models;
using NorthwindCustomersAPI.Models.DTO;

namespace NorthwindCustomersAPI.Controllers
{
    public static class Utils
    {
        public static CustomerDTO CustomerToDTO(Customer customer) => new CustomerDTO
        {
            CompanyName = customer.CompanyName,
            FullNameAndTitle = $"{customer.ContactName} - {customer.ContactTitle}",
            Location = $"{customer.City}, {customer.Region ?? ""}, {customer.Country}",
            Fax = customer.Fax
        };
    }
}
