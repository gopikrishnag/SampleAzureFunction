using System.Threading.Tasks;

namespace ClearData.Service
{
    public interface IDatabaseService
    {
        Task<byte> CleanDatabaseTables(string email);
    }
}
