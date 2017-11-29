using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
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
            return (from DataRow dr in (asiSSMSTrans.FillDataTable(ServerDetails.sConnection(LineDetails), "", new object[] { })).Rows
                    select new asiTablesListModel
                    {
                        objectID = asiDataDefault.getInt64(dr[""]),
                        tableName = Convert.ToString(dr[""])
                    }).ToList();
        }
    }
    public class asiTableDetailsModel
    {
        public DataSet tableDetails { get; set; }
    }
}