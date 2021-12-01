using System.Threading.Tasks;

namespace OneCode.Data
{
    public interface IOneCodeDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
