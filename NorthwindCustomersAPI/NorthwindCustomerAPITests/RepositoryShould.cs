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

    [Category("Sad Path")]
    [Category("FindAsync")]
    [Test]
    public void FindAsync_GivenInValidID_ReturnsNullCustomer()
    {
        var result = _sut.FindAsync("THSTJ").Result;
        Assert.That(result, Is.Null);
        
    }

    [Category("Happy Path")]
    [Category("GetAllAsync")]
    [Test]
    public void GetAllAsync_ReturnsCustomersList()
    {
        var result = _sut.GetAllAsync().Result;
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<Customer>>());
    }

    [Category("Happy Path")]
    [Category("Add")]
    [Test]
    public void Add_AddsCustomerToDatabase()
    {
        var countOfCustomers = _context.Customers.Count();
        var c = new Customer()
        {
            CustomerId = "TESTC",
            CompanyName = "Google",
            City = "LA",
            Country = "murica",
            ContactName = "CEOMAN",
            ContactTitle = "CEO",
            Region = "W"
        };
        _sut.Add(c);
        _context.SaveChanges();
        Assert.That(_context.Customers, Is.Not.Null);
        Assert.That(_context.Customers.ToList(), Has.Member(c));
        Assert.That(_context.Customers.Count(), Is.EqualTo(countOfCustomers + 1));
    }

    //[Category("Sad Path")]
    //[Category("Add")]
    //[Test]
    //public void Add_CustomerWithNoId_DoesNotAdd()
    //{
    //    var countOfCustomers = _context.Customers.Count();
    //    var c = new Customer()
    //    {
    //        CompanyName = "Google",
    //        City = "LA",
    //        Country = "murica",
    //        ContactName = "CEOMAN",
    //        ContactTitle = "CEO",
    //        Region = "W"
    //    };
    //    _sut.Add(c);
    //    _context.SaveChanges();
    //    //Assert.That(_context.Customers.ToList(), Has.No.Member(c));
    //    Assert.That(_context.Customers.Count(), Is.EqualTo(countOfCustomers));
    //}



}