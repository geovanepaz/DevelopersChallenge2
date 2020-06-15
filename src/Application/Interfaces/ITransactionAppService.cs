using Application.ViewModels.Transaction;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITransactionAppService : IDisposable
    {
        Task<UploadResponse> InsertBulkAsync(List<StreamReader> streams);
        Task<List<TransactionResponse>> GetAll();
    }
}
