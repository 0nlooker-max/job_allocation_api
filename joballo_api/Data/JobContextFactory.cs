using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using joballo_api.Data;

namespace joballo_api.Data
{
    public class JobContextFactory : IDesignTimeDbContextFactory<JobContext>
    {
        public JobContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JobContext>();
            optionsBuilder.UseMySql("server=localhost;port=3306;database=dapperdb;user=root;password=;Allow User Variables=True", 
                                   ServerVersion.AutoDetect("server=localhost;port=3306;database=dapperdb;user=root;password=;Allow User Variables=True"));

            return new JobContext(optionsBuilder.Options);
        }
    }
}