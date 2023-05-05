using NorthwindCustomersAPI.Models;

namespace NorthwindCustomersAPI.Data.Repositories;

public interface ICustomerRepository
{
    bool IsNull { get; }
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> FindAsync(string id);
    void Add(Customer customer);
    void AddRange(IEnumerable<Customer> customers);
    void Update(Customer customer);
    void Remove(Customer customer);
    Task SaveAsync();
}
