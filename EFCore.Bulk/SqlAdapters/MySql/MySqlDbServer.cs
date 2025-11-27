using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using MySql.EntityFrameworkCore.Metadata;
using System.Data.Common;

namespace EFCore.Bulk.SqlAdapters.MySql;

/// <inheritdoc/>
public class MySqlDbServer : IDbServer
{
    SqlType IDbServer.Type => SqlType.MySql;

    MySqlAdapter _adapter = new();
    ISqlOperationsAdapter IDbServer.Adapter => _adapter;

    MySqlDialect _dialect = new();
    IQueryBuilderSpecialization IDbServer.Dialect => _dialect;

    /// <inheritdoc/>
    public SqlQueryBuilder QueryBuilder { get; } = new MySqlQueryBuilder();

    string IDbServer.ValueGenerationStrategy => nameof(MySQLValueGenerationStrategy);

    /// <inheritdoc/>
    public DbConnection? DbConnection { get; set; }

    /// <inheritdoc/>
    public DbTransaction? DbTransaction { get; set; }

    bool IDbServer.PropertyHasIdentity(IAnnotation annotation) => (MySQLValueGenerationStrategy?)annotation.Value == MySQLValueGenerationStrategy.IdentityColumn;
}
