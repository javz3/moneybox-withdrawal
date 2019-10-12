namespace Moneybox.App
{
    public class Address
    {
        public string FirstLine { get; set; }

        public string SecondLine { get; set; }
        
        public string ThirdLine { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public bool IsPrimary { get; set; }
    }
}