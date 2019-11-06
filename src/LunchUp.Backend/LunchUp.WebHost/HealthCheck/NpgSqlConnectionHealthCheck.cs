using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace LunchUp.WebHost.HealthCheck
{
    /// <inheritdoc />
    public class NpgSqlConnectionHealthCheck : IHealthCheck
    {
        private const string DefaultTestQuery = "Select 1";

        /// <inheritdoc />
        public NpgSqlConnectionHealthCheck(string connectionString)
            : this(connectionString, DefaultTestQuery)
        {
        }

        /// <inheritdoc />
        public NpgSqlConnectionHealthCheck(string connectionString, string testQuery)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            TestQuery = testQuery;
        }

        private string ConnectionString { get; }

        private string TestQuery { get; }

        /// <inheritdoc />
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);

                    if (TestQuery != null)
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = TestQuery;

                        await command.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
                catch (DbException)
                {
                    return new HealthCheckResult(context.Registration.FailureStatus);
                }
            }

            return HealthCheckResult.Healthy();
        }
    }
}