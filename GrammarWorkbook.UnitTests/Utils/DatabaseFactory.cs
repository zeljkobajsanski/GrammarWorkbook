using GrammarWorkbook.Data;
using Microsoft.EntityFrameworkCore;

namespace GrammarWorkbook.UnitTests.Utils
{
    public static class DatabaseFactory
    {
        public static DatabaseContext Get()
        {
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase("Database").Options;
            return new DatabaseContext(options);
        }
    }
}