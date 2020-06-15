using Application.Interfaces;
using Application.ViewModels.Transaction;
using Domain.DomainObjects;
using Domain.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Application.AutoMapper;
using AutoMapper;

namespace Application.Services
{
    public class TransactionAppService : ITransactionAppService
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IOfxService _ofxService;
        private readonly IMapper _mapper;

        public TransactionAppService(ITransactionRepository transactionRepo, IOfxService ofxService, IMapper mapper)
        {
            _transactionRepo = transactionRepo;
            _ofxService = ofxService;
            _mapper = mapper;
        }
        public async Task<UploadResponse> InsertBulkAsync(List<StreamReader> streams)
        {
            var transactions = _ofxService.TranslateToTransactions(streams);
            transactions = await _transactionRepo.DistinctAsync(transactions);

            if (!transactions.Any())
            {
                throw new DomainException("No valid transactions found");
            }

            await _transactionRepo.InsertBulkAsync(transactions);
            return new UploadResponse { FilesRead = streams.Count, TransactionsEntered = transactions.Count };
        }

        public async Task<List<TransactionResponse>> GetAll()
        {
            var transactions = await _transactionRepo.AllAsync();
            return transactions.MapTo<List<TransactionResponse>>(_mapper);
        }
        public void Dispose()
        {
            _transactionRepo?.Dispose();
        }
    }
}
