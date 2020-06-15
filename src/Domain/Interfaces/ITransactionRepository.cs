using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<List<Transaction>> DistinctAsync(List<Transaction> transactions);
    }
}
