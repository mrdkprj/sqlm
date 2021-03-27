using System.Data;

namespace MasudaManager.Utility
{
    public interface IDmlDbCommandBuilder : IDbCommandBuilder
    {
        DbCommandSet CreateCommand(string tablename, SqlResult bindingset);
        DbCommandSet CreateCommand(string tablename, SqlResult valuebindingset, SqlResult wherebindingset);
    }
}
