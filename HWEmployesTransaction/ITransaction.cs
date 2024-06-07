using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWEmployesTransaction
{
    public interface ITransaction
    {
        string TransactionId { get; }
        decimal Amount { get; }
        DateTime Date { get; }
    }
}
