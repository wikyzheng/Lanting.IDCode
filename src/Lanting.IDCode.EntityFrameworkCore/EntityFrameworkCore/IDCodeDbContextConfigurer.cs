using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Lanting.IDCode.EntityFrameworkCore
{
    public static class IDCodeDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<IDCodeDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<IDCodeDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
