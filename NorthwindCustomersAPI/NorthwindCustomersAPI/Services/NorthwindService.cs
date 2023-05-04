using Microsoft.EntityFrameworkCore;
using NorthwindCustomersAPI.Data.Repositories;
using NorthwindCustomersAPI.Models;

namespace NorthwindCustomersAPI.Services
{
    public class NorthwindService : INorthwindService
    {
        private readonly ILogger _logger;
        private readonly ICustomerRepository _repository;

        public NorthwindService(ILogger<INorthwindService> logger, ICustomerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> CreateAsync(Customer customer)
        {
            if (_repository.IsNull)
            {
                _logger.LogWarning($"{typeof(Customer).FullName} is null");
                return false;
            }
            _repository.Add(customer);
            try
            {
                await _repository.SaveAsync();
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (_repository.IsNull)
            {
                _logger.LogWarning($"{typeof(Customer).FullName} is null");
                return false;
            }
            var customer = await _repository.FindAsync(id);
            if (customer == null)
            {
                return false;
            }
            _repository.Remove(customer);
            try
            {
                await _repository.SaveAsync();
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Customer>?> GetAllAsync()
        {
            if (_repository.IsNull)
            {
                return null;
            }
            var entity = (await _repository.GetAllAsync());
            if (entity == null)
            {
                _logger.LogWarning($"{typeof(Customer).Name} was not found.");
            }
            return entity;
        }

        public async Task<Customer?> GetAsync(string id)
        {
            if (_repository.IsNull)
            {
                return null;
            }
            var customer = (await _repository.FindAsync(id));

            if (customer == null)
            {
                _logger.LogWarning($"{typeof(Customer).Name} with id: {id} was not found.");
                return null;
            }

            _logger.LogInformation($"{typeof(Customer).Name} with id: {id} found.");
            return customer;
        }

        public async Task<bool> UpdateAsync(string id, Customer customer)
        {
            _repository.Update(customer);
            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CustomerExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        private async Task<bool> CustomerExists(string id)
        {
            var supplier = await _repository.FindAsync(id);
            if (supplier is null)
            {
                return false;
            }
            return true;
        }
    }
}
