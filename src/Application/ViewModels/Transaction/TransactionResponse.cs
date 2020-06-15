using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels.Transaction
{
    public class TransactionResponse
    {
        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public DateTime Date { get; private set; }
        public Decimal Amount { get; private set; }
        public string Description { get; private set; }
    }
}
