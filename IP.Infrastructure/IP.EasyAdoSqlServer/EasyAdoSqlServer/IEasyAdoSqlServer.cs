using IP.BaseDomains;
using System.Data;
using System.Data.SqlClient;
using static IP._EasyAdoSqlServer.EasyAdoSqlServer.EnumExecuteType;

namespace IP._EasyAdoSqlServer.EasyAdoSqlServer
{
    public interface IEasyAdoSqlServer<T>
        where T : BaseDomain
    {
        Task<int> Execute(string commandText, List<SqlParameter> parameters, ExecuteType executeType, bool transaction);
        Task<int> Insert(Task<T> model);
        Task<bool> Update(Task<T> model, Task<Tuple<string, string>> KeyValue);
        Task<bool> Delete(Task<T> model, Task<Tuple<string, string>> KeyValue);
        Task<List<T>> GetAll();
        Task<List<T>> ConverterDataTableToList<T>(Task<DataTable> dataTable);
    }
}
