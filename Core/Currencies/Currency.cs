using System;

namespace Store.Core.Currencies
{
    public class Currency
    {
        public string Code { get; set; }
        public decimal Rate { get; set; }
        public DateTime Date { get; set; }
    }
}
