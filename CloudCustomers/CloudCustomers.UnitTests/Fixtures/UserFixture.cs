using CloudCustomers.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UserFixture
    {
        public static List<User> GetTestUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name = "Jane Doe",
                    Address = new Address(){ City = "Babylon", Street = "p1 Main st", ZipCode ="123625"},
                    Email = "JaneDoe@yahhoo.com"
                },
                new User()
                {
                    Id = 2,
                    Name = "Jane Jhonson",
                    Address = new Address(){ City = "Babylon", Street = "p2 Main st", ZipCode ="145625"},
                    Email = "JaneJhonson@yahhoo.com"
                },
                new User()
                {
                    Id = 3,
                    Name = "Dawyne Jhonson",
                    Address = new Address(){ City = "Babylon", Street = "p2 Main st", ZipCode ="145625"},
                    Email = "DawyneJhonson@yahhoo.com"
                },
            };
        }
    }
}
