using GF.Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig.IntegrationTests
{
    internal static class Helpers
    {
        /// <summary>
        /// Cleans up a unit test.
        /// </summary>
        /// <param name="session">The MongoDb session</param>
        /// <param name="logger">The <see cref="Serilog.ILogger"/> logger used to log the test</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        internal static async Task CleanUpTestAsync(IClientSessionHandle? session, Serilog.ILogger logger, CancellationToken cancellationToken = default)
        {
            logger.Information("Test disposal started.");

            //list to capture the exceptions
            List<Exception> cleanUpExceptions = new();

            //rollback the session
            try
            {
                if (session != null)
                {
                    if (session.IsInTransaction) await session.AbortTransactionAsync(cancellationToken);
                    logger.Information($"Session transaction successfully aborted.");
                }
                else
                {
                    logger.Information($"No MongoDb session passed to {nameof(Helpers.CleanUpTestAsync)}.");
                }
            }
            catch (Exception ex)
            {
                cleanUpExceptions.Add(new Exception("An error has occurred trying to abort the MongoDb transaction.", ex));
            }

            if (cleanUpExceptions.Count > 0)
            {
                throw new AggregateException($"Error(s) have occurred trying to perform the post test clean up at {nameof(Helpers.CleanUpTestAsync)}.", cleanUpExceptions);
            }
        }
    }
}
