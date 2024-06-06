using App.Shared;
using AutoFixture;
using BenchmarkDotNet.Attributes;

namespace App.Benchs;

[Config(typeof(BenchConfig))]
[BenchmarkCategory(nameof(BenchCategory.Default))]
public class ListCapacityBench
{
    private Company[] _companies;
    
    [Params(100, 1000, 10_000)]
    public int Size { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        var fixture = new Fixture();
        _companies = Enumerable
            .Range(1, Size)
            .Select(_ => fixture.Create<Company>())
            .ToArray();
    }
    
    [Benchmark(Baseline = true)]
    public List<Company> UsingDefaultInitialization()
    {
        var companies = new List<Company>();
        for (var index = 0; index < Size; index++)
        {
            companies.Add(_companies[index]);
        }
        return companies;
    }

    [Benchmark]
    public List<Company> UsingCapacityInitialization()
    {
        var companies = new List<Company>(Size);
        for (var index = 0; index < Size; index++)
        {
            companies.Add(_companies[index]);
        }
        return companies;
    }
    
    public class Company
    {
        public List<Employee> Employees { get; set; }
        public List<Contract> Contracts { get; set; }
    }

    public class Employee
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }	
        public Address Address { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class Contract
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Client Client { get; set; }
    }

    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<Contact> Contacts { get; set; }
    }

    public class Contact
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
    }
}