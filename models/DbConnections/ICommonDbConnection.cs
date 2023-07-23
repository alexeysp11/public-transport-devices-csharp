using System.Data; 

namespace PublicTransportDevices.DbConnections
{
    public interface ICommonDbConnection
    {
        DataTable ExecuteSqlCommand(string sqlRequest); 
    }
}