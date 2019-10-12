using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal InsufficientFundsLimit = 0m;

        public const decimal LowFundsLimit = 500m;

        public const decimal PayInLimit = 4000m;
        
        public const decimal WithdrawalLimit = 2500m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }
    }
}