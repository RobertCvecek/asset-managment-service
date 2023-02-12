using System.Transactions;

namespace SolveX.Framework.TestingUtility.DependencyInjectionTests;

public class TransactionedTests : InjectionBasedTest, IDisposable
{
     private TransactionScope transaction;

    public TransactionedTests()
    {
        transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadUncommitted,
                Timeout = TransactionManager.MaximumTimeout
            },
            TransactionScopeAsyncFlowOption.Enabled);
    }

    public void Dispose()
    {
        transaction.Dispose();
    }
}
