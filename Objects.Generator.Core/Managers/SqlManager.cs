namespace Objects.Generator.Core.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using Objects.Generator.Core.Configuration;
    using Objects.Generator.Core.Configuration.Elements;
    using Objects.Generator.Core.Entities;
    using Objects.Generator.Core.Enumerations;
    using Objects.Generator.Core.Exceptions;
    using Objects.Generator.Core.Localizacion;

    public class SqlManager
    {

        #region · Properties

        private SqlConnection _connection { get; set; }
        private static GeneratorObjectsSection Config { get { return GeneratorObjectsManager.Config; } }
        private GeneratorConnectionElement ConnectionActive { get; set; }

        #endregion Properties

        public SqlManager()
        {
            if (ValidateConnection())
                _connection = new SqlConnection()
                {
                    ConnectionString = string.Format(ConnectionActive.ConectionChain.ConnectionString, 
                                                        ConnectionActive.ConnectionOptions.Server, 
                                                        ConnectionActive.ConnectionOptions.Database, 
                                                        ConnectionActive.ConnectionOptions.Timeout
                                                        )
                };
        }

        #region · Public methods

        public List<Table> GetTableList(int type = 0)
        {
            try
            {
                if(_connection == null) { return null; }

                if(_connection.State != ConnectionState.Open) { _connection.Open(); }

                DataTable table = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter
                {
                    SelectCommand = new SqlCommand(SqlSentencesManager.GetAllTables(type), _connection)
                };

                dataAdapter.Fill(table);

                var tablesList = new List<Table>();

                foreach(DataRow tableName in table.Rows)
                {
                    var tableNew = new Table();

                    if(!tableName.IsNull(TableFields.Table.ToString()))
                    {
                        tableNew.Name = tableName[TableFields.Table.ToString()].ToString();
                        tableNew.TypeObject = tableName[TableFields.Type.ToString()].ToString();

                        DataTable tablaData = new DataTable();

                        dataAdapter.SelectCommand = new SqlCommand(SqlSentencesManager.GetFieldsByTable(tableName[TableFields.Table.ToString()].ToString()), _connection);
                        dataAdapter.Fill(tablaData);

                        tablaData.TableName = tableName[TableFields.Table.ToString()].ToString();

                        foreach(DataRow row in tablaData.Rows)
                        {
                            var field = new Field
                            {
                                Name = row[DatasetFields.Column.ToString()].ToString(),
                                FieldType = row[DatasetFields.TypeData.ToString()].ToString(),
                                Position = Convert.ToInt16(row[DatasetFields.Position.ToString()]),
                                IsNullable = !row[DatasetFields.AlowNulls.ToString()].ToString().Equals("NO")
                            };

                            tableNew.FieldsList.Add(field);
                        }

                        tablaData = new DataTable();

                        dataAdapter.SelectCommand = new SqlCommand(SqlSentencesManager.GetForeigsKeysByTable(tableName[TableFields.Table.ToString()].ToString()), _connection);
                        dataAdapter.Fill(tablaData);

                        foreach(DataRow row in tablaData.Rows)
                        {
                            var refTable = new ReferentialTable
                            {
                                Tablename = row[DatasetFields.Tablename.ToString()].ToString(),
                                Columnname = row[DatasetFields.Columnname.ToString()].ToString(),
                                Foreign = row[DatasetFields.Foreign.ToString()].ToString(),
                                Primary = row[DatasetFields.Primary.ToString()].ToString()
                            };

                            tableNew.ReferentialList.Add(refTable);
                        }
                    }

                    tablesList.Add(tableNew);
                }

                _connection.Close();

                return tablesList;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<Procedure> GetStoredProceduresList()
        {
            try
            {
                if(_connection == null) return null;

                if(_connection.State != ConnectionState.Open) _connection.Open();

                var table = new DataTable();
                var adapter = new SqlDataAdapter
                {
                    SelectCommand = new SqlCommand(SqlSentencesManager.GetAllStored(), _connection)
                };

                adapter.Fill(table);

                var procedureList = new List<Procedure>();

                foreach(DataRow row in table.Rows)
                {
                    var procedure = new Procedure();

                    if(!row.IsNull(ProcedureFields.Name.ToString()))
                    {
                        procedure.Name = row[ProcedureFields.Name.ToString()].ToString();

                        var parameters = new DataTable();

                        adapter.SelectCommand = new SqlCommand(SqlSentencesManager.GetParametersByStoredProcedure(row[ProcedureFields.Name.ToString()].ToString()), _connection);
                        adapter.Fill(parameters);

                        parameters.TableName = row[ProcedureFields.Name.ToString()].ToString();

                        foreach(DataRow parameter in parameters.Rows)
                        {
                            var param = new Parameter
                            {
                                Name = parameter[ParameterFields.Parameter.ToString()].ToString(),
                                Type = parameter[ParameterFields.Type.ToString()].ToString(),
                                Direction = parameter[ParameterFields.Direction.ToString()].ToString(),
                                Order = (int)parameter[ParameterFields.Order.ToString()]
                            };

                            procedure.Parameters.Add(param);
                        }
                    }

                    procedureList.Add(procedure);
                }

                _connection.Close();

                return procedureList;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        #endregion Public methods

        #region · Validations

        private bool ValidateConnection()
        {
            var connectionActive = Config.GeneratorConnections
                                    .Cast<GeneratorConnectionElement>()
                                    .FirstOrDefault(c => c.Enabled);

            if (connectionActive == null)
            {
                throw new GeneratorObjectsException(
                    GeneratorObjectsError.ConnectionNotActive,
                    string.Format(
                        CoreMessages.MsgErrorCode,
                        (long)GeneratorObjectsError.ConnectionNotActive,
                        ExceptionsMessages.MsgConnectionNotActive
                    )
                );
            }

            if (connectionActive.ConectionChain != null)
                ValidateConnectionChain(connectionActive.ConectionChain);

            if (connectionActive.ConnectionOptions != null)
                ValidateConnectionOption(connectionActive.ConnectionOptions);

            ConnectionActive = connectionActive;

            return true;
        }

        private void ValidateConnectionChain(ConectionChainElement options)
        {
            if (string.IsNullOrWhiteSpace(options.ConnectionString))
            {
                throw new GeneratorObjectsException(
                    GeneratorObjectsError.ConnectionStringNotProvided,
                    string.Format(
                        CoreMessages.MsgErrorCode,
                        (long)GeneratorObjectsError.ConnectionStringNotProvided,
                        ExceptionsMessages.MsgConnectionStringNotProvided
                    )
                );
            }
        }

        private void ValidateConnectionOption(ConnectionOptionsElement options)
        {
            if (string.IsNullOrWhiteSpace(options.Server))
            {
                throw new GeneratorObjectsException(
                    GeneratorObjectsError.ServerNotProvided,
                    string.Format(
                        CoreMessages.MsgErrorCode,
                        (long)GeneratorObjectsError.ServerNotProvided,
                        ExceptionsMessages.MsgServerNotProvided
                    )
                );
            }

            if (string.IsNullOrWhiteSpace(options.Database))
            {
                throw new GeneratorObjectsException(
                    GeneratorObjectsError.DatabaseNotProvided,
                    string.Format(
                        CoreMessages.MsgErrorCode,
                        (long)GeneratorObjectsError.DatabaseNotProvided,
                        ExceptionsMessages.MsgDatabaseNotProvided
                    )
                );
            }
        }

        #endregion Validations

    }

}
