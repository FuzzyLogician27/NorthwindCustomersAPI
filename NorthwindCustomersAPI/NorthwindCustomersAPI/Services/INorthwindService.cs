
using NorthwindCustomersAPI.Models;

namespace NorthwindCustomersAPI.Services;

public interface INorthwindService
{
    Task<bool> CreateAsync(Customer customer);
    Task<bool> UpdateAsync(string id, Customer customer);
    Task<bool> DeleteAsync(string id);
    Task<IEnumerable<Customer>?> GetAllAsync();
    Task<Customer?> GetAsync(string id);
}
