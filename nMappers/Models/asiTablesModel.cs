using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using nMappers.Codes;
using asi;
using asi.Data;


namespace nMappers.Models
{
    public class asiSQLConnection
    {
        public static SqlConnection getConnection(ServerDetails LineServer)
        {
            return ServerDetails.sConnection(LineServer);
        }
    }
    public class asiTablesListModel
    {
        public long objectID { get; set; }
        public string tableName { get; set; }

        public static List<asiTablesListModel> ReadAll(ServerDetails LineDetails)
        {
            asiTablesStruct.ScriptExecution(LineDetails);
            return (from DataRow dr in (asiSSMSTrans.FillDataTable(ServerDetails.sConnection(LineDetails), "sp$app@Tables$ReadAll", new object[] { false })).Rows
                    select new asiTablesListModel
                    {
                        objectID = asiDataDefault.getInt64(dr["object_id"]),
                        tableName = asiDataDefault.getString(dr["name"])
                    }).ToList();
        }
    }
    public class asiTableDetailsModel
    {
        public DataSet tableDetails { get; set; }
    }
}