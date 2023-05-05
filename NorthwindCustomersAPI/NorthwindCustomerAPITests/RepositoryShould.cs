using Microsoft.EntityFrameworkCore;
using NorthwindCustomersAPI.Data.Repositories;
using NorthwindCustomersAPI.Models;

namespace NorthwindCustomerAPITests;

public class RepositoryShould
{
    private NorthwindContext _context;
    private CustomerRepository _sut;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var options = new DbContextOptionsBuilder<NorthwindContext>()
        .UseInMemoryDatabase("NorthwindDB").Options;
        _context = new NorthwindContext(options);

        _sut = new CustomerRepository(_context);
    }

    [SetUp]
    public void SetUp()
    {
        if (_context.Customers != null)
        {
            _context.Customers.RemoveRange(_context.Customers);
        }



        _context.Customers!.AddRange(
        new List<Customer>
        {
                 new Customer
                 {
                         CustomerId = "ANID",
                         CompanyName = "Sparta Global",
                         City = "Birmingham",
                         Country = "UK",
                         ContactName = "Nish Mandal",
                         ContactTitle = "Manager",
                         Region = "SE"
                 },
                 new Customer
                 {
                         CustomerId = "ANOTH",
                         CompanyName = "Nintendo",
                         City = "Tokyo",
                         Country = "Japan",
                         ContactName = "Shigeru Miyamoto",
                         ContactTitle = "CEO",
                         Region = "NW"
                 }
        });
        _context.SaveChanges();
    }

    [Category("Happy Path")]
    [Category("FindAsync")]
    [Test]
    public void FindAsync_GivenValidID_ReturnsCorrectCustomer()
    {
        var result = _sut.FindAsync("ANID").Result;
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<Customer>());
        Assert.That(result.CompanyName, Is.EqualTo("Sparta Global"));
    }
}