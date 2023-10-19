using System.Data;

namespace Infrastructure.Persistence
{
    public interface IMyShopContext
    {
        IDbConnection CreateConnection();
        IDbConnection CreateMasterConnection();
        void CreateDatabase(string dbName);
    }
}
