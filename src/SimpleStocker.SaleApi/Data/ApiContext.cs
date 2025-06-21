using Microsoft.EntityFrameworkCore;

namespace SimpleStocker.SaleApi.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> oprions) : base(oprions) { }
    }
}
