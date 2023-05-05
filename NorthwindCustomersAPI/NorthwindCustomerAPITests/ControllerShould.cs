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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Protocol;
using Castle.Components.DictionaryAdapter.Xml;

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

    [Category("Sad Path")]
    [Category("GetCustomers")]
    [Test]
    public async Task GetCustomers_WhenThereAreNoCustomers_ReturnsNotFound()
    {
        var mockService = Mock.Of<INorthwindService>();
        List<Customer> customers = null;
        Mock
            .Get(mockService)
            .Setup(c => c.GetAllAsync().Result)
            .Returns(customers);

        var sut = new CustomersController(mockService);
        var result = await sut.GetCustomers();
        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }

    [Category("Happy Path")]
    [Category("GetCustomer")]
    [Test]
    public async Task GetCustomer_WhenCustomerExists_ReturnsCustomer()
    {
        var mockService = Mock.Of<INorthwindService>();
        string id = "test";
        Customer customer = Mock.Of<Customer>(c => c.CustomerId == id);
        customer.ContactName = "test";
        customer.ContactTitle = "test";
        customer.CompanyName = "test";
        customer.Country = "test";
        customer.City = "test";
        customer.Region = "test";
        customer.Fax = "test";
        Mock
            .Get(mockService)
            .Setup(c => c.GetAsync(id).Result)
            .Returns(customer);

        var sut = new CustomersController(mockService);
        var result = await sut.GetCustomer(id);
        Assert.That(result.Value, Is.TypeOf<CustomerDTO>());
        Assert.That(result.Value.FullNameAndTitle, Is.EqualTo("test - test"));
        Assert.That(result.Value.CompanyName, Is.EqualTo("test"));
        Assert.That(result.Value.Location, Is.EqualTo("test, test, test"));
        Assert.That(result.Value.Fax, Is.EqualTo("test"));
    }
    
    [Category("Sad Path")]
    [Category("GetCustomer")]
    [Test]
    public async Task GetCustomer_WhenCustomerDoesNotExist_ReturnsNotFound()
    {
        var mockService = Mock.Of<INorthwindService>();
        string id = "test";
        Customer customer = Mock.Of<Customer>();
        Mock
            .Get(mockService)
            .Setup(c => c.GetAsync(id).Result)
            .Returns(customer);

        var sut = new CustomersController(mockService);
        var result = await sut.GetCustomer(id);
        Assert.That(result.Result, Is.Null);
    }

    [Category("Happy Path")]
    [Category("PostCustomer")]
    [Test]
    public async Task PostCustomer_WhenCustomerDoesNotExist_ReturnsCreated()
    {
        var mockService = Mock.Of<INorthwindService>();
        string id = "test";
        Customer customer = Mock.Of<Customer>(c => c.CustomerId == id);
        customer.ContactName = "test";
        customer.ContactTitle = "test";
        customer.CompanyName = "test";
        customer.Country = "test";
        customer.City = "test";
        customer.Region = "test";
        customer.Fax = "test";
        Mock
            .Get(mockService)
            .Setup(c => c.CreateAsync(customer).Result)
            .Returns(true);

        var sut = new CustomersController(mockService);
        var result = await sut.PostCustomer(customer);
        Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());
    }

    [Category("Sad Path")]
    [Category("PostCustomer")]
    [Test]
    public async Task PostCustomer_WhenCustomerExists_ReturnsProblem()
    {
        var mockService = Mock.Of<INorthwindService>();
        string id = "test";
        Customer customer = Mock.Of<Customer>(c => c.CustomerId == id);
        customer.ContactName = "test";
        customer.ContactTitle = "test";
        customer.CompanyName = "test";
        customer.Country = "test";
        customer.City = "test";
        customer.Region = "test";
        customer.Fax = "test";
        Mock
            .Get(mockService)
            .Setup(c => c.CreateAsync(customer).Result)
            .Returns(false);

        var sut = new CustomersController(mockService);
        var result = await sut.PostCustomer(customer);
        var testObj = sut.Problem("Customer not created");
        Assert.That(result.Result, Is.TypeOf<ObjectResult>());
        //Assert.That(result.Result, Is.EqualTo(sut.Problem("Customer not created")));
    }

    [Category("Happy Path")]
    [Category("PutCustomer")]
    [Test]
    public async Task PutCustomer_WhenCustomerExists_UpdatesCustomer()
    {
        var mockService = Mock.Of<INorthwindService>();
        string id = "test";
        Customer customer = Mock.Of<Customer>(c => c.CustomerId == id);
        customer.ContactName = "test";
        customer.ContactTitle = "test";
        customer.CompanyName = "test";
        customer.Country = "test";
        customer.City = "test";
        customer.Region = "test";
        customer.Fax = "test";
        Mock
            .Get(mockService)
            .Setup(c => c.GetAsync(id).Result)
            .Returns(customer);
        Mock
            .Get(mockService)
            .Setup(c => c.UpdateAsync(id, customer).Result)
            .Returns(true);

        var sut = new CustomersController(mockService);
        var result = await sut.PutCustomer(id, new Customer() { CustomerId = id, ContactName = "yes", ContactTitle = "yes", CompanyName = "yes", Country = "yes", City = "yes", Region = "yes", Fax = "yes"});
        var customerResult = Utils.CustomerToDTO(customer);
        Assert.That(result, Is.TypeOf<NoContentResult>());
        Assert.That(customerResult.FullNameAndTitle, Is.EqualTo("yes - yes"));
        Assert.That(customerResult.CompanyName, Is.EqualTo("yes"));
        Assert.That(customerResult.Location, Is.EqualTo("yes, yes, yes"));
        Assert.That(customerResult.Fax, Is.EqualTo("yes"));
    }

    [Category("Happy Path")]
    [Category("PutPostCustomer")]
    [Test]
    public async Task PutCustomer_WhenCustomerDoesNotExist_PostsCustomer()
    {
        var mockService = Mock.Of<INorthwindService>();
        string id = "test";
        Customer customer = null;
        Mock
            .Get(mockService)
            .Setup(c => c.GetAsync(id).Result)
            .Returns(customer);

        var sut = new CustomersController(mockService);
        var result = await sut.PutCustomer(id, new Customer() { CustomerId = id, ContactName = "yes", ContactTitle = "yes", CompanyName = "yes", Country = "yes", City = "yes", Region = "yes", Fax = "yes" });
        Assert.That(result, Is.TypeOf<NoContentResult>());
    }
    
    [Category("Sad Path")]
    [Category("PutCustomer")]
    [Test]
    public async Task PutCustomer_WhenCustomerExistsButUpdateFailed_ReturnsProblem()
    {
        var mockService = Mock.Of<INorthwindService>();
        string id = "test";
        Customer customer = Mock.Of<Customer>(c => c.CustomerId == id);
        customer.ContactName = "test";
        customer.ContactTitle = "test";
        customer.CompanyName = "test";
        customer.Country = "test";
        customer.City = "test";
        customer.Region = "test";
        customer.Fax = "test";
        Mock
            .Get(mockService)
            .Setup(c => c.GetAsync(id).Result)
            .Returns(customer);
        Mock
            .Get(mockService)
            .Setup(c => c.UpdateAsync(id, customer).Result)
            .Returns(false);

        var sut = new CustomersController(mockService);
        var result = await sut.PutCustomer(id, new Customer() { CustomerId = id, ContactName = "yes", ContactTitle = "yes", CompanyName = "yes", Country = "yes", City = "yes", Region = "yes", Fax = "yes" });
        Assert.That(result, Is.TypeOf<ObjectResult>());
    }

    [Category("Happy Path")]
    [Category("DeleteCustomer")]
    [Test]
    public async Task DeleteCustomer_WhenCustomerExists_ReturnsNoContent()
    {
        var mockService = Mock.Of<INorthwindService>();
        string id = "test";
        Customer customer = Mock.Of<Customer>(c => c.CustomerId == id);
        Mock
            .Get(mockService)
            .Setup(c => c.DeleteAsync(id).Result)
            .Returns(true);

        var sut = new CustomersController(mockService);
        var result = await sut.DeleteCustomer(id);
        Assert.That(result, Is.TypeOf<NoContentResult>());
    }
}
