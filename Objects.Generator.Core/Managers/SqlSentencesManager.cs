namespace Objects.Generator.Core.Managers
{
    using System.Text;

    internal class SqlSentencesManager
    {
        internal static string GetAllTables(int type)
        {
            var sentence = new StringBuilder();

            sentence.Append("SELECT TABLE_NAME as [Table], ");
            sentence.Append("TABLE_TYPE as [Type] ");
            sentence.Append("FROM INFORMATION_SCHEMA.TABLES ");

            switch(type)
            {
                case 0:  //-- Tablas.
                    sentence.Append("WHERE TABLE_TYPE = 'BASE TABLE' ");
                    sentence.Append("AND TABLE_NAME NOT LIKE 'dt%' ");
                    sentence.Append("AND TABLE_NAME NOT LIKE 'sys%' ");
                    break;
                case 1:  //-- Vistas.
                    sentence.Append("WHERE TABLE_TYPE = 'VIEW' ");
                    sentence.Append("AND TABLE_NAME NOT LIKE 'sys%' ");
                    break;
                default:    //-- Todos: Tablas y Vistas.
                    sentence.Append("WHERE TABLE_NAME NOT LIKE 'dt%' ");
                    sentence.Append("AND TABLE_NAME NOT LIKE 'sys%' ");
                    break;
            }

            sentence.Append(" ORDER BY TABLE_NAME");

            return sentence.ToString();
        }

        internal static string GetFieldsByTable(string table)
        {
            var sentence = new StringBuilder();

            sentence.Append("SELECT TABLE_CATALOG as [Db], ");
            sentence.Append("TABLE_NAME as [Table], ");
            sentence.Append("ORDINAL_POSITION as [Position], ");
            sentence.Append("COLUMN_NAME as [Column], ");
            sentence.Append("IS_NULLABLE as [AlowNulls], ");
            sentence.Append("DATA_TYPE as [TypeData], ");
            sentence.Append("CHARACTER_MAXIMUM_LENGTH as [MaxChar] ");
            sentence.Append("FROM INFORMATION_SCHEMA.COLUMNS ");
            sentence.Append(string.Format("WHERE TABLE_NAME = '{0}' ", table));
            sentence.Append("ORDER BY ORDINAL_POSITION");

            return sentence.ToString();
        }

        internal static string GetForeigsKeysByTable(string tablename)
        {
            var sentence = new StringBuilder();

            sentence.Append("SELECT KP.TABLE_NAME [Tablename], ");
            sentence.Append("KF.COLUMN_NAME [Columnname], ");
            sentence.Append("RC.CONSTRAINT_NAME	[Foreign], ");
            sentence.Append("RC.UNIQUE_CONSTRAINT_NAME [Primary] ");
            sentence.Append("FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC ");
            sentence.Append("JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KF ON RC.CONSTRAINT_NAME = KF.CONSTRAINT_NAME ");
            sentence.Append("JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KP ON RC.UNIQUE_CONSTRAINT_NAME = KP.CONSTRAINT_NAME ");
            sentence.Append(string.Format("WHERE KF.TABLE_NAME = '{0}'", tablename));

            return sentence.ToString();
        }

        internal static string GetAllStored()
        {
            var sentence = new StringBuilder();

            sentence.Append("SELECT ROUTINE_NAME [Name] ");
            sentence.Append("FROM INFORMATION_SCHEMA.ROUTINES ");
            sentence.Append("WHERE ROUTINE_NAME NOT LIKE 'sp_%' ");
            sentence.Append("AND ROUTINE_NAME NOT LIKE 'fn_%' ");
            sentence.Append("ORDER BY ROUTINE_NAME");

            return sentence.ToString();
        }

        internal static string GetParametersByStoredProcedure(string store)
        {
            var sentence = new StringBuilder();

            sentence.Append("SELECT SPECIFIC_NAME [Name], ");
            sentence.Append("PARAMETER_NAME [Parameter], ");
            sentence.Append("DATA_TYPE [Type], ");
            sentence.Append("PARAMETER_MODE [Direction], ");
            sentence.Append("ORDINAL_POSITION [Order] ");
            sentence.Append("FROM INFORMATION_SCHEMA.PARAMETERS ");
            sentence.Append("WHERE SPECIFIC_NAME LIKE 'uSP_%' AND ");
            sentence.Append(string.Format("SPECIFIC_NAME ='{0}' ", store));
            sentence.Append("ORDER BY SPECIFIC_NAME, ");
            sentence.Append("ORDINAL_POSITION");

            return sentence.ToString();
        }

    }

}
