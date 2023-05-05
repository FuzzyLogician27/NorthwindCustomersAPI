
using Castle.Core.Resource;
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
    [Category("GetCustomers")]
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
    [Category("GetCustomer")]
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
        var result = await _sut.GetAsync("Test");
        Assert.That(result, Is.InstanceOf<Customer>());
    }

    [Category("Happy Path")]
    [Category("CreateCustomer")]
    [Test]
    public async Task CreateAsync_CreatingValidCustomer_ReturnsTrue()
    {
        var mockRepository = GetRepository();
        var mockLogger = GetLogger();

        var _sut = new NorthwindService(mockLogger, mockRepository);
        var result = await _sut.CreateAsync(Mock.Of<Customer>());
        Assert.That(result, Is.True);
    }

    //[Test]
    //public async Task CreateAsync_CreatingValidCustomer_CallsMethodsCorrectNumberOfTimes()
    //{
    //    var mockRepository = GetRepository();
    //    var mockLogger = GetLogger();
        

    //    List<Customer> customers = new List<Customer> { It.IsAny<Customer>() };

    //    var cnt = customers.Count;
    //    Mock.Get(mockRepository).Setup(cs => cs.Add(Mock.Of<Customer>()));


    //    var _sut = new NorthwindService(mockLogger, mockRepository);
    //     await _sut.CreateAsync(Mock.Of<Customer>());

    //    Assert.That(customers.Count, Is.EqualTo(cnt + 1));


    //    var mockCustomerService = new Mock<INorthwindService>();

    //    mockCustomerService.Verify(mockRepository.Add(Mock.Of<Customer>()), Times.AtLeastOnce);

    //}

}
