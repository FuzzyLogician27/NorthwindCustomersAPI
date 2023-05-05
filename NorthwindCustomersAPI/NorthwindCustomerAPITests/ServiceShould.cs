
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindCustomersAPI.Data.Repositories;
using NorthwindCustomersAPI.Models;

using NorthwindCustomersAPI.Services;

namespace NorthwindCustomerAPITests;

public class ServiceShould
{
    private static ILogger<INorthwindService> GetLogger()
    {
        return Mock.Of<ILogger<INorthwindService>>();
    }

    private static ICustomerRepository GetRepository()
    {
        return Mock.Of<ICustomerRepository>();
    }

    [Category("Happy Path")]
    [Category("GetSuppliers")]
    [Test]
    public async Task GetAllAsync_WhenThereAreCustomers_ReturnsListOfCustomers()
    {
        var mockRepository = GetRepository();
        var mockLogger = GetLogger();
        List<Customer> customers = new List<Customer> { It.IsAny<Customer>() };
        Mock
            .Get(mockRepository)
            .Setup(c => c.GetAllAsync().Result)
            .Returns(customers);
        Mock
            .Get(mockRepository)
            .Setup(c => c.IsNull)
            .Returns(false);

        var _sut = new NorthwindService(mockLogger, mockRepository);
        var result = await _sut.GetAllAsync();
        Assert.That(result, Is.InstanceOf<IEnumerable<Customer>>());
    }

    /*[Category("Sad Path")]
    [Category("GetSuppliers")]
    [Test]
    public async Task GetAllAsync_WhenThereAreNoCustomers_ReturnsLoggerWarning()
    {
        var mockRepository = GetRepository();
        var mockLogger = GetLogger();
        List<Customer> customers = new List<Customer> { It.IsAny<Customer>() };
        Mock
            .Get(mockRepository)
            .Setup(c => c.GetAllAsync().Result)
            .Returns(customers);
        Mock
            .Get(mockRepository)
            .Setup(c => c.IsNull)
            .Returns(false);
        Mock
            .Get(mockLogger)
            .Setup(c => c.LogWarning("No customers found."))

        var _sut = new NorthwindService(mockLogger, mockRepository);
        var result = await _sut.GetAllAsync();
        Assert.That(result, Is.InstanceOf<IEnumerable<Customer>>());


    }*/

    [Category("Happy Path")]
    [Category("GetSuppliers")]
    [Test]
    public async Task GetAsync_WhenThereIsACustomer_ReturnsCustomer()
    {
        var mockRepository = GetRepository();
        var mockLogger = GetLogger();
        var customer = new Customer { CustomerId = "Test", CompanyName = "Test", ContactName = "Test" };

        Mock
            .Get(mockRepository)
            .Setup(c => c.FindAsync("Test").Result).Returns(customer);
        Mock
            .Get(mockRepository)
            .Setup(c => c.IsNull)
            .Returns(false);

        var _sut = new NorthwindService(mockLogger, mockRepository);
        var result = await _sut.GetAllAsync();
        Assert.That(result, Is.InstanceOf<IEnumerable<Customer>>());
    }
}
