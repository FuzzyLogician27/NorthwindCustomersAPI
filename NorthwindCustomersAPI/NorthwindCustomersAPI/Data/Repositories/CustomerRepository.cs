using Microsoft.EntityFrameworkCore;
using NorthwindCustomersAPI.Models;

namespace NorthwindCustomersAPI.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly NorthwindContext _context;
        protected readonly DbSet<Customer> _dbSet;

        public CustomerRepository(NorthwindContext context, DbSet<Customer> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public bool IsNull => _dbSet == null;

        public void Add(Customer customer)
        {
            _dbSet.Add(customer);
        }

        public void AddRange(IEnumerable<Customer> customers)
        {
            _dbSet.AddRange(customers);
        }

        public async Task<Customer?> FindAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Remove(Customer customer)
        {
            _dbSet.Remove(customer);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Customer customer)
        {
            _dbSet.Update(customer);
        }
    }
}
