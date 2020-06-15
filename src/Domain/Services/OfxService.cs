using Domain.DomainObjects;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Domain.Services
{
    public class OfxService : IOfxService
    {
        public List<Transaction> TranslateToTransactions(List<StreamReader> streams)
        {
            List<Transaction> transactions = new List<Transaction>();
            foreach (var stream in streams)
            {
                transactions = TranslateToTransactions(stream, transactions);
            }
            return transactions;
        }
        public List<Transaction> TranslateToTransactions(StreamReader streamReader)
        {
            return TranslateToTransactions(streamReader, new List<Transaction>());
        }
        private List<Transaction> TranslateToTransactions(StreamReader streamReader, List<Transaction> transactions)
        {
            ResetValues(out string type, out string description, out DateTime date, out decimal amount);
            string line, tag; int count = 0;

            while ((line = streamReader.ReadLine()) != null)
            {
                count++;
                line = line.Trim();
                tag = GetTag(line);

                switch (tag.ToUpper())
                {
                    case "<STMTTRN>":
                        ResetValues(out type, out description, out date, out amount);
                        break;

                    case "<TRNTYPE>":
                        type = PopularType(line, count);
                        break;

                    case "<DTPOSTED>":
                        date = PopularDate(line, count);
                        break;

                    case "<TRNAMT>":
                        amount = PopularAmount(line, count);
                        break;

                    case "<MEMO>":
                        description = PopularDescription(line, count);
                        break;

                    case "</STMTTRN>":
                        transactions = AddTransaction(transactions, new Transaction(type, date, amount, description));
                        ResetValues(out type, out description, out date, out amount);
                        break;
                }
            }
            streamReader.Close();
            return transactions;
        }
        private string PopularType(string line, int count)
        {
            string type = GetValueTag(line);
            if (string.IsNullOrEmpty(type))
            {
                throw new DomainException($"TRNTYPE invalid line: {count} content: {line}");
            }
            return type;
        }
        private DateTime PopularDate(string line, int count)
        {
            bool parse = TryParseToDate(GetValueTag(line), out DateTime date);
            if (!parse)
            {
                throw new DomainException($"DTPOSTED invalid line: {count} content: {line}");
            }
            return date;
        }
        private decimal PopularAmount(string line, int count)
        {
            bool parse = TryParseToDecimal(GetValueTag(line), out decimal amount);
            if (!parse)
            {
                throw new DomainException($"TRNAMT invalid line: {count} content: {line}");
            }
            return amount;
        }
        private string PopularDescription(string line, int count)
        {
            string description = GetValueTag(line);
            if (string.IsNullOrEmpty(description))
            {
                throw new DomainException($"MEMO invalid line: {count} content: {line}");
            }
            return description;
        }
        private List<Transaction> AddTransaction(List<Transaction> transactions, Transaction transaction)
        {
            if (!transactions.Any(t => t.Equals(transaction)))
            {
                transactions.Add(transaction);
            }
            return transactions;
        }
        private string GetTag(string value)
        {
            try
            {
                int start = value.IndexOf("<");
                int end = value.IndexOf(">");

                if (start == -1 || end == -1) return string.Empty;

                return value.Substring(start, end + 1);
            }
            catch
            {
                return string.Empty;
            }
        }
        private string GetValueTag(string value)
        {
            int start = value.IndexOf("<");
            int end = value.IndexOf(">");

            if (start == -1 || end == -1) return string.Empty;

            return value.Remove(start, end + 1);
        }
        private static bool TryParseToDecimal(string stringDecimal, out decimal value)
        {
            value = decimal.Zero;
            try
            {
                stringDecimal = stringDecimal.Replace(".", ",");
                CultureInfo provider = new CultureInfo("pt-BR");

                return decimal.TryParse(stringDecimal, NumberStyles.Float, provider, out value);
            }
            catch
            {
                return false;
            }
        }
        private static void ResetValues(out string type, out string description, out DateTime date, out decimal amount)
        {
            type = string.Empty;
            description = string.Empty;
            date = DateTime.MinValue;
            amount = decimal.Zero;
        }

        //Todo: terminar, setar os minutos
        private static bool TryParseToDate(string stringDate, out DateTime date)
        {
            date = DateTime.MinValue;
            var time = string.Empty;
            try
            {
                var yyyy = stringDate.Substring(0, 4);
                var mm = stringDate.Substring(4, 2);
                var dd = stringDate.Substring(6, 2);

                if (stringDate.Length > 6)
                {
                    var h = stringDate.Substring(8, 2);
                    var m = stringDate.Substring(10, 2);
                    var s = stringDate.Substring(12, 2);
                    time = $"T{h}:{m}:{s}";
                }

                return DateTime.TryParse($"{yyyy}-{mm}-{dd}{time}", out date);
            }
            catch
            {
                return true;
            }
        }
    }
}
