using System;
using System.Collections.Generic;

namespace Moneybox.App
{
    public class User
    {
        public Guid Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public IEnumerable<Address> Addresses { get; set; }

        public string FullName => $"{Forename} {Surname}";
    }
}
