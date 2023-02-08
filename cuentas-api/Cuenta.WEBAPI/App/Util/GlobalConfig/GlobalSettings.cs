using System.Transactions;

namespace Account.WEBAPI.App.GlobalConfig
{
    public interface IGlobalSettings
    {
        TransactionScope GetNewTransactionAsync();
    }

    public class GlobalSettings : IGlobalSettings
    {
        public TransactionScope GetNewTransactionAsync()
        {
            var transactionScopeAsyncOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted,
                Timeout = new TimeSpan(0, 5, 0)
            };

            return new TransactionScope(
                TransactionScopeOption.Required,
                transactionScopeAsyncOptions,
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}