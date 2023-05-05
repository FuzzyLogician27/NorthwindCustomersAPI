
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindCustomersAPI.Data.Repositories;
using NorthwindCustomersAPI.Models;

using NorthwindCustomersAPI.Services;

namespace NorthwindCustomerAPITests;

public class ServiceShould
{
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
    private static ILogger<INorthwindService> GetLogger()
    {
        return Mock.Of<ILogger<INorthwindService>>();
    }

    private static ICustomerRepository GetRepository()
    {
        return Mock.Of<ICustomerRepository>();
    }
}
