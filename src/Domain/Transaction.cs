using Domain.DomainObjects;
using System;

namespace Domain
{
    public class Transaction : Entity
    {
        public string Type { get; private set; }
        public DateTime Date { get; private set; }
        public Decimal Amount { get; private set; }
        public string Description { get; private set; }

        protected Transaction() { }

        public Transaction(string type, DateTime date, decimal amount, string description)
        {
            Type = type;
            Date = date;
            Amount = amount;
            Description = description;
            Validate();
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            var transaction = (Transaction)obj;

            if (Date != transaction.Date) return false;
            if (Description != transaction.Description) return false;
            if (Amount != transaction.Amount) return false;
            if (Type != transaction.Type) return false;

            return true;
        }

        public void Validate()
        {
            Validations.IsEmpty(Type, "Type field cannot be empty");
            Validations.IsEqual(Date, DateTime.MinValue, "Date field cannot be empty");
            Validations.IsEqual(Amount, Decimal.Zero, "Amount field cannot be zero");
            Validations.IsEmpty(Description, "Description field cannot be empty");
        }
    }
}
