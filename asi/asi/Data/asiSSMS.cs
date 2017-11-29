using System;
using System.Data;
using System.Data.SqlClient;

namespace asi.Data
{
    public class asiSSMSTrans
    {
        public static bool IsConnect(SqlConnection SSMSConnection)
        {
            try
            {
                ConnectionOpen(SSMSConnection);
                ConnectionClose(SSMSConnection);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void Exec(SqlConnection SSMSConnection, string ProcedureName, object[] ParameterArray)
        {
            ConnectionOpen(SSMSConnection);
            SqlCommand SSMSCmd = new SqlCommand(ProcedureName, SSMSConnection);
            SSMSCmd.CommandType = CommandType.StoredProcedure;
            DataTable ListParamters = getParameters(SSMSConnection, SSMSCmd);
            SSMSCmd = setParameters(SSMSCmd, ListParamters, ParameterArray);
            SSMSCmd.ExecuteNonQuery();
            ConnectionClose(SSMSConnection);
        }
        public static object ExecWithIdentity(SqlConnection SSMSConnection, string ProcedureName, object[] ParameterArray)
        {
            ConnectionOpen(SSMSConnection);
            SqlCommand SSMSCmd = new SqlCommand(ProcedureName, SSMSConnection);
            SSMSCmd.CommandType = CommandType.StoredProcedure;
            DataTable ListParamters = getParameters(SSMSConnection, SSMSCmd);
            SSMSCmd = setParameters(SSMSCmd, ListParamters, ParameterArray);
            object ScalerValue = SSMSCmd.ExecuteScalar();
            ConnectionClose(SSMSConnection);
            return ScalerValue;
        }
        public static object QueryExecutor(SqlConnection SSMSConnection, string Query)
        {
            ConnectionOpen(SSMSConnection);
            SqlCommand SSMSCmd = new SqlCommand(Query, SSMSConnection);
            SSMSCmd.CommandType = CommandType.Text;
            SSMSCmd.ExecuteNonQuery();
            ConnectionClose(SSMSConnection);
            return SSMSCmd.ExecuteNonQuery();
        }
        public static void ScriptExecutor(SqlConnection SSMSConnection, string Query)
        {
            ConnectionOpen(SSMSConnection);
            SqlCommand SSMSCmd = new SqlCommand(Query, SSMSConnection);
            SSMSCmd.CommandType = CommandType.Text;
            SSMSCmd.ExecuteNonQuery();
            ConnectionClose(SSMSConnection);
        }
        public static object QueryFlag(SqlConnection SSMSConnection, string Query)
        {
            ConnectionOpen(SSMSConnection);
            SqlCommand SSMSCmd = new SqlCommand(Query, SSMSConnection);
            SSMSCmd.CommandType = CommandType.Text;
            DataTable dtFill = new DataTable();
            ((SqlDataAdapter)new SqlDataAdapter(SSMSCmd)).Fill(dtFill);
            ConnectionClose(SSMSConnection);
            return dtFill.Rows.Count > 0 ? dtFill.Rows[0][0] : 0;
        }
        public static DataTable FillDataTable(SqlConnection SSMSConnection, string ProcedureName, object[] ParameterArray)
        {
            ConnectionOpen(SSMSConnection);
            SqlCommand SSMSCmd = new SqlCommand(ProcedureName, SSMSConnection);
            SSMSCmd.CommandType = CommandType.StoredProcedure;
            DataTable ListParamters = getParameters(SSMSConnection, SSMSCmd);
            SSMSCmd = setParameters(SSMSCmd, ListParamters, ParameterArray);
            DataTable dtFill = new DataTable();
            ((SqlDataAdapter)new SqlDataAdapter(SSMSCmd)).Fill(dtFill);
            ConnectionClose(SSMSConnection);
            return dtFill;
        }
        public static DataSet FillDataSet(SqlConnection SSMSConnection, string ProcedureName, object[] ParameterArray)
        {
            ConnectionOpen(SSMSConnection);
            SqlCommand SSMSCmd = new SqlCommand(ProcedureName, SSMSConnection);
            SSMSCmd.CommandType = CommandType.StoredProcedure;
            DataTable ListParamters = getParameters(SSMSConnection, SSMSCmd);
            SSMSCmd = setParameters(SSMSCmd, ListParamters, ParameterArray);
            DataSet dsFill = new DataSet();
            ((SqlDataAdapter)new SqlDataAdapter(SSMSCmd)).Fill(dsFill);
            ConnectionClose(SSMSConnection);
            return dsFill;
        }

        private static void ConnectionOpen(SqlConnection SSMSConnection)
        {
            if (SSMSConnection.State == ConnectionState.Closed)
            {
                SSMSConnection.Open();
            }
        }
        private static void ConnectionClose(SqlConnection SSMSConnection)
        {
            if (SSMSConnection.State == ConnectionState.Open)
            {
                SSMSConnection.Close();
            }
        }

        private static DataTable getParameters(SqlConnection SSMSConnection, SqlCommand SSMSCmd)
        {
            SqlCommand SSMSFindPara = new SqlCommand(@" SELECT			C.name AS ParameterName, st.name as ParameterType, C.isoutparam AS ParameterDirection, st.length
														FROM            sys.syscolumns AS C 
														INNER JOIN		sys.sysobjects AS O 
																		ON C.id = O.id 
														INNER JOIN		sys.systypes AS st 
																		ON C.xtype = st.xtype AND st.name <> 'sysname' AND O.xtype = 'P' 
														WHERE O.name = '" + SSMSCmd.CommandText + @"' AND O.category = 0
														ORDER BY C.colorder", SSMSConnection);

            DataTable dtParameters = new DataTable();
            ((SqlDataAdapter)new SqlDataAdapter(SSMSFindPara.CommandText, SSMSConnection)).Fill(dtParameters);
            return dtParameters;
        }
        private static SqlCommand setParameters(SqlCommand SSMSCmd, DataTable ParameterArray, object[] ParameterValues)
        {
            if (ParameterArray.Rows.Count < ParameterValues.Length)
            {
                throw new IndexOutOfRangeException("SQL Parameter " + " Length: " + ParameterArray.Rows.Count.ToString() + " and Value Parameter Length: " + ParameterValues.Length.ToString());
            }
            else
            {
                for (int i = 0; i < ParameterArray.Rows.Count; i++)
                {
                    SqlParameter Parameter = new SqlParameter();
                    Parameter.ParameterName = Convert.ToString(ParameterArray.Rows[i]["ParameterName"]);
                    Parameter.SqlDbType = ParameterType(Convert.ToString(ParameterArray.Rows[i]["ParameterType"]));
                    Parameter.Direction = Convert.ToString(ParameterArray.Rows[i]["ParameterDirection"]) == "0" ? ParameterDirection.Input : ParameterDirection.Output;
                    Parameter.Value = "";
                    SSMSCmd.Parameters.Add(Parameter);
                }
                for (int i = 0; i < ParameterValues.Length; i++)
                {
                    SSMSCmd.Parameters[i].Value = ParameterValues[i];
                }
            }
            return SSMSCmd;
        }
        private static SqlDbType ParameterType(string ParameterType)
        {
            switch (ParameterType)
            {
                case "bigint":
                    return SqlDbType.BigInt;
                case "money":
                    return SqlDbType.Money;
                case "smallmoney":
                    return SqlDbType.SmallMoney;
                case "date":
                    return SqlDbType.Date;
                case "datetime":
                    return SqlDbType.DateTime;
                case "datetime2":
                    return SqlDbType.DateTime2;
                case "datetimeoffset":
                    return SqlDbType.DateTimeOffset;
                case "smalldatetime":
                    return SqlDbType.SmallDateTime;
                case "time":
                    return SqlDbType.Time;
                case "binary":
                    return SqlDbType.Binary;
                case "varbinary":
                    return SqlDbType.VarBinary;
                case "image":
                    return SqlDbType.Image;
                case "sql_variant":
                    return SqlDbType.Variant;
                case "decimal":
                    return SqlDbType.Decimal;
                case "float":
                    return SqlDbType.Float;
                case "numeric":
                    return SqlDbType.Real;
                case "real":
                    return SqlDbType.Real;
                case "bit":
                    return SqlDbType.Bit;
                case "int":
                    return SqlDbType.Int;
                case "smallint":
                    return SqlDbType.SmallInt;
                case "tinyint":
                    return SqlDbType.TinyInt;
                case "timestamp":
                    return SqlDbType.Timestamp;
                case "nchar":
                    return SqlDbType.NChar;
                case "ntext":
                    return SqlDbType.NText;
                case "nvarchar":
                    return SqlDbType.NVarChar;
                case "char":
                    return SqlDbType.Char;
                case "text":
                    return SqlDbType.Text;
                case "varchar":
                    return SqlDbType.VarChar;
                case "xml":
                    return SqlDbType.Xml;
                default:
                    return SqlDbType.VarChar;
            }
        }
    }
    public class ServerDetails
    {
        public string ServerName { get; set; }
        public bool IsWindowAuth { get; set; }
        public string Database { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }

        public static string ResponseSQLString(ServerDetails ServerDetails)
        {
            if (ServerDetails.IsWindowAuth == true)
            {
                return "Data Source=" + ServerDetails.ServerName + ";Initial Catalog=" + ServerDetails.Database + ";Persist Security Info=True;";
            }
            else
            {
                return "Data Source=" + ServerDetails.ServerName + ";Initial Catalog=" + ServerDetails.Database + ";Persist Security Info=True;User ID=" + ServerDetails.UserID + ";Password=" + ServerDetails.Password + "";
            }
        }
        public static SqlConnection sConnection(string sString)
        {
            return new SqlConnection(sString);
        }
        public static SqlConnection sConnection(ServerDetails ServerDetails)
        {
            return new SqlConnection(ResponseSQLString(ServerDetails));
        }
    }
}