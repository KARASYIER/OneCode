using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OneCode.Data
{
    /* This is used if database provider does't define
     * IOneCodeDbSchemaMigrator implementation.
     */
    public class NullOneCodeDbSchemaMigrator : IOneCodeDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}