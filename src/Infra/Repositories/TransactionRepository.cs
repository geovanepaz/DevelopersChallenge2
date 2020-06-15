using Domain;
using Domain.Interfaces;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DefaultContext context) : base(context)
        {
        }
        public async Task<List<Transaction>> DistinctAsync(List<Transaction> transactions)
        {
            if (transactions == null || !transactions.Any())
            {
                return transactions;
            }

            transactions = transactions.OrderBy(x => x.Date).ToList();
            var start = transactions.First().Date;
            var end = transactions.Last().Date;

            var result = await Query()
                                .Where(x => x.Date >= start && x.Date <= end)
                                .ToListAsync();

            return transactions.Where(x => !result.Contains(x)).ToList();
        }
    }
}
