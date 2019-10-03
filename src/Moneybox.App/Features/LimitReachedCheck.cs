using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class LimitReacherCheck : ILimitReachedService
    {
        public void LimitReached(decimal limit, decimal amount, string errorMessage)
        {
            if (amount > Account.PayInLimit)
            {
                throw new InvalidOperationException(errorMessage);
            }
        }
    }
}