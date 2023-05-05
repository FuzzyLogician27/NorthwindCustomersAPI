using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NorthwindCustomersAPI.Controllers;
using NorthwindCustomersAPI.Models;
using NorthwindCustomersAPI.Models.DTO;
using NorthwindCustomersAPI.Services;

namespace NorthwindCustomerAPITests;

public class ControllerShould
{
    [Category("Happy Path")]
    [Category("GetCustomers")]
    [Test]
    public async Task GetCustomers_WhenThereAreCustomers_ReturnsListOfCustomerDTOs()
    {
        var mockService = Mock.Of<INorthwindService>();
        List<Customer> customers = new List<Customer>() { Mock.Of<Customer>() };
        Mock
            .Get(mockService)
            .Setup(c => c.GetAllAsync().Result)
            .Returns(customers);

        var sut = new CustomersController(mockService);
        var result = await sut.GetCustomers();
        Assert.That(result.Value, Is.InstanceOf<IEnumerable<CustomerDTO>>());
    }
}
