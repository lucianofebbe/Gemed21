using IP._EasyAdoSqlServer.EasyAdoSqlServerConfig;
using IP.BaseDomains;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using static IP._EasyAdoSqlServer.EasyAdoSqlServer.EnumExecuteType;

namespace IP._EasyAdoSqlServer.EasyAdoSqlServer
{
    public class EasyAdoSqlServer<T> : IEasyAdoSqlServer<T>
        where T : BaseDomain
    {
        /// <summary>
        /// Contém o retorno dos dados da consulta.
        /// </summary>
        public DataTable dataTable;

        private StringBuilder strBuilder;
        private readonly EasyAdoSqlServerSettings config;
        private readonly ILogsFactory logsFactory;


        public EasyAdoSqlServer(ILogsFactory iGenerateLogs, EasyAdoSqlServerSettings config)
        {
            dataTable = new DataTable();
            this.config = config;
            this.logsFactory = iGenerateLogs;
        }

        /// <summary>
        /// Abre conexao e inicia processo de execução contra a base de dados.
        /// Caso a execução seja para retornar dados, Utilize o comando READER para a leitura
        /// Exemplo:
        /// if (Execute(sql, ExecuteType.ReaderQuery, out feedBack) > 0)
        /// {
        /// while (Reader.Read()){...}
        /// }
        /// </summary>
        /// <param name="commandText">Insira o comando SQL ou o nome da procedure que deseja executar</param>
        /// <param name="executeType">Que tipo de execução contra o banco de dados quer que ocorra. (ReaderQuery), (ReaderProcedure), (NonQuery), (NonProcedure)</param>
        /// <param name="feedBack">Caso o retorno da execução seja igual a 0, será mostrado o motivo do mesmo nesse campo</param>
        /// <param name="transaction">Para força que a execução seja feita com Transaction</param>
        /// <returns></returns>
        public Task<int> Execute(string commandText, List<SqlParameter> parameters, ExecuteType executeType, bool transaction)
        {
            try
            {
                var command = OpenConnection(Task.FromResult(true)).Result;
                int feedExecute = 0;
                if (command.Connection != null)
                {
                    command.CommandText = commandText;

                    if (parameters != null && parameters.Count > 0)
                        command.Parameters.AddRange(parameters.ToArray());

                    switch (executeType)
                    {
                        case ExecuteType.ReaderQuery:
                            command.CommandType = CommandType.Text;
                            if (transaction) command.Transaction = command.Connection.BeginTransaction();
                            dataTable.Load(command.ExecuteReader());
                            feedExecute = dataTable.Rows.Count;
                            break;
                        case ExecuteType.ReaderProcedure:
                            command.CommandType = CommandType.StoredProcedure;
                            if (transaction) command.Transaction = command.Connection.BeginTransaction();
                            dataTable.Load(command.ExecuteReader());
                            feedExecute = dataTable.Rows.Count;
                            break;
                        case ExecuteType.NonQuery:
                            command.CommandType = CommandType.Text;
                            if (transaction) command.Transaction = command.Connection.BeginTransaction();
                            feedExecute = command.ExecuteNonQuery();
                            break;
                        case ExecuteType.NonProcedure:
                            command.CommandType = CommandType.StoredProcedure;
                            if (transaction) command.Transaction = command.Connection.BeginTransaction();
                            feedExecute = command.ExecuteNonQuery();
                            break;
                    }
                    return Task.FromResult(feedExecute);
                }
                else
                    throw new Exception("Não foi possível abrir conexão.");
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
            finally { _ = OpenConnection(Task.FromResult(false)).Result; }
        }

        public virtual Task<int> Insert(Task<T> model)
        {
            try
            {
                var command = OpenConnection(Task.FromResult(true)).Result;
                int idReturn = 0;
                if (model != null)
                {
                    var tipo = model.GetType();
                    strBuilder = new StringBuilder();
                    strBuilder.Append("INSERT INTO ");
                    strBuilder.Append(tipo.Name + " (");

                    foreach (var prop in tipo.GetProperties())
                    {
                        if (prop.GetValue(model) != null &&
                            !string.IsNullOrEmpty(prop.GetValue(model).ToString()) &&
                            prop.Name != "Id")
                        {
                            strBuilder.Append(prop.Name + ",");
                            command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(model));
                        }
                    }
                    strBuilder.Remove(strBuilder.Length - 1, 1);
                    strBuilder.Append(") VALUES (");
                    if (command.Parameters.Count > 0)
                    {
                        foreach (var par in command.Parameters)
                            strBuilder.Append(par.ToString() + ",");
                        strBuilder.Remove(strBuilder.Length - 1, 1);
                        strBuilder.Append("); SELECT SCOPE_IDENTITY()");
                        try
                        {
                            command.CommandText = strBuilder.ToString();
                            command.CommandType = CommandType.Text;
                            idReturn = int.Parse(command.ExecuteScalar().ToString());
                            return Task.FromResult(idReturn);
                        }
                        catch (Exception ex) { throw new Exception("Context Insert", ex); }
                    }
                    else
                        return Task.FromResult(idReturn);
                }
                else
                    return Task.FromResult(idReturn);
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
            finally { _ = OpenConnection(Task.FromResult(false)).Result; }
        }

        /// <summary>
        /// Parametro Model, Objeto que será utilizado para atualizar a base de dados
        /// Tuple: Key do tuple é utilizado na condição Where para determinar a coluna que será utilizada
        /// como regra.
        /// Value: Value do tuple é a condição que a coluna tem que ter para efetuar a atualização do registro.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public virtual Task<bool> Update(Task<T> model, Task<Tuple<string, string>> KeyValue)
        {
            try
            {
                var command = OpenConnection(Task.FromResult(true)).Result;
                int idReturn = 0;
                if (model != null)
                {
                    var type = model.GetType();
                    strBuilder = new StringBuilder();
                    strBuilder.Append("UPDATE ");
                    strBuilder.Append(type.Name + " SET ");

                    foreach (var prop in type.GetProperties())
                    {
                        if (prop.GetValue(model) != null &&
                            !string.IsNullOrEmpty(prop.GetValue(model).ToString()) &&
                            prop.Name != "Id")
                        {
                            strBuilder.Append(prop.Name + "=@" + prop.Name + ",");
                            command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(model));
                        }
                    }
                    strBuilder.Remove(strBuilder.Length - 1, 1);
                    if (command.Parameters.Count > 0)
                    {
                        try
                        {
                            strBuilder.Append(" WHERE " + KeyValue.Result.Item1 + " = " + KeyValue.Result.Item2);
                            command.CommandText = strBuilder.ToString();
                            command.CommandType = CommandType.Text;
                            idReturn = command.ExecuteNonQuery();
                            return Task.FromResult(idReturn > 0);
                        }
                        catch (Exception ex)
                        { throw new Exception("Context Update", ex); }
                    }
                    else
                        return Task.FromResult(false);
                }
                else
                    return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
            finally { _ = OpenConnection(Task.FromResult(false)).Result; }
        }

        /// <summary>
        /// Parametro Model, Objeto que será utilizado como referencia a tabela da base de dados
        /// Tuple: Key do tuple é utilizado na condição Where para determinar a coluna que será utilizada
        /// como regra.
        /// Value: Value do tuple é a condição que a coluna tem que ter para efetuar a exclusão do registro.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public virtual Task<bool> Delete(Task<T> model, Task<Tuple<string, string>> KeyValue)
        {
            try
            {
                var command = OpenConnection(Task.FromResult(true)).Result;
                int idReturn = 0;
                if (model != null)
                {
                    var type = model.GetType();
                    strBuilder = new StringBuilder();
                    strBuilder.Append("DELETE FROM ");
                    strBuilder.Append(type.Name);
                    try
                    {
                        strBuilder.Append(" WHERE " + KeyValue.Result.Item1 + " = " + KeyValue.Result.Item2);
                        command.CommandText = strBuilder.ToString();
                        command.CommandType = CommandType.Text;
                        idReturn = command.ExecuteNonQuery();
                        return Task.FromResult(idReturn > 0);
                    }
                    catch (Exception ex)
                    { throw new Exception("Context Delete", ex); }
                }
                else
                    return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
            finally { _ = OpenConnection(Task.FromResult(false)).Result; }
        }

        public virtual Task<List<T>> GetAll()
        {
            try
            {
                var command = OpenConnection(Task.FromResult(true)).Result;
                var model = Activator.CreateInstance<T>();
                if (model != null)
                {
                    var type = model.GetType();
                    strBuilder = new StringBuilder();
                    strBuilder.Append("SELECT ");

                    foreach (var prop in type.GetProperties())
                        strBuilder.Append(prop.Name + ",");

                    strBuilder.Remove(strBuilder.Length - 1, 1);
                    strBuilder.Append(" FROM " + type.Name);

                    command.CommandText = strBuilder.ToString();
                    command.CommandType = CommandType.Text;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            dataTable.Load(reader);
                    }
                    return ConverterDataTableToList<T>(Task.FromResult(dataTable));
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
            finally { _ = OpenConnection(Task.FromResult(false)).Result; }
        }

        public Task<List<T>> ConverterDataTableToList<T>(Task<DataTable> dataTable)
        {
            try
            {
                var data = new List<T>();
                foreach (DataRow row in dataTable.Result.Rows)
                {
                    T item = GetItem<T>(Task.FromResult(row)).Result;
                    data.Add(item);
                }
                return Task.FromResult(data);
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        private static Task<T> GetItem<T>(Task<DataRow> dataRow)
        {
            try
            {
                Type tipoObjeto = typeof(T);
                T objeto = Activator.CreateInstance<T>();
                foreach (DataColumn coluna in dataRow.Result.Table.Columns)
                {
                    PropertyInfo propInfoColunaObjeto = tipoObjeto.GetProperties().FirstOrDefault(o => o.Name == coluna.ColumnName);
                    Type propColunaObjeto = Nullable.GetUnderlyingType(propInfoColunaObjeto.PropertyType) ?? propInfoColunaObjeto.PropertyType;

                    #region validacao de tipos
                    object valor = null;
                    if (DBNull.Value.Equals(dataRow.Result[coluna.ColumnName]))
                    {
                        switch (Type.GetTypeCode(propColunaObjeto))
                        {
                            case TypeCode.Boolean:
                                valor = null;
                                break;
                            case TypeCode.Byte:
                                valor = 0;
                                break;
                            case TypeCode.Char:
                                valor = "";
                                break;
                            case TypeCode.DateTime:
                                valor = null;
                                break;
                            case TypeCode.DBNull:
                                valor = null;
                                break;
                            case TypeCode.Decimal:
                                valor = 0;
                                break;
                            case TypeCode.Double:
                                valor = 0;
                                break;
                            case TypeCode.Empty:
                                valor = string.Empty;
                                break;
                            case TypeCode.Int16:
                                valor = 0;
                                break;
                            case TypeCode.Int32:
                                valor = 0;
                                break;
                            case TypeCode.Int64:
                                valor = 0;
                                break;
                            case TypeCode.Object:
                                valor = null;
                                break;
                            case TypeCode.SByte:
                                valor = 0;
                                break;
                            case TypeCode.Single:
                                valor = 0;
                                break;
                            case TypeCode.String:
                                valor = string.Empty;
                                break;
                            case TypeCode.UInt16:
                                valor = 0;
                                break;
                            case TypeCode.UInt32:
                                valor = 0;
                                break;
                            case TypeCode.UInt64:
                                valor = 0;
                                break;
                        }
                    }
                    else
                        valor = Convert.ChangeType(dataRow.Result[coluna.ColumnName], propColunaObjeto);
                    #endregion

                    propInfoColunaObjeto.SetValue(objeto, valor, null);
                }
                return Task.FromResult(objeto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private Task<SqlCommand> OpenConnection(Task<bool> openConnection)
        {
            try
            {
                var command = new SqlCommand();
                var connection = new SqlConnection();
                if (openConnection.Result)
                {
                    connection.ConnectionString = config.ConnectionString;
                    command.Connection = connection;
                    command.CommandTimeout = config.TimeOut == 0 ? 180 : config.TimeOut;
                }
                else
                {
                    connection.Close();
                    connection.Dispose();
                    command.Dispose();
                    dataTable.Dispose();
                    dataTable.Clear();
                    GC.SuppressFinalize(this);
                }

                return Task.FromResult(command);

            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }
    }

    public class EnumExecuteType
    {
        public enum ExecuteType
        {
            ReaderQuery,
            ReaderProcedure,
            NonQuery,
            NonProcedure
        }
    }
}
