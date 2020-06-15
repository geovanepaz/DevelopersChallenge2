using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Domain.Interfaces
{
    public interface IOfxService
    {
        List<Transaction> TranslateToTransactions(StreamReader streamReader);
        List<Transaction> TranslateToTransactions(List<StreamReader> streams);
    }
}
