
using NorthwindCustomersAPI.Models;

namespace NorthwindCustomersAPI.Services;

public interface INorthwindService
{
    Task<bool> CreateAsync(Customer customer);
    Task<bool> UpdateAsync(int id, Customer customer);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Customer>?> GetAllAsync();
    Task<Customer?> GetAsync(int id);
}
