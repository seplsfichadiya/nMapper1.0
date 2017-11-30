using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asi.Data
{
    class asiSQLStruct
    {
        public static bool IsProcedure(ServerDetails LineDetails, string ProcedureName)
        {
            return asiDataDefault.getInt64(asiSSMSTrans.QueryFlag(ServerDetails.sConnection(LineDetails), "SELECT COUNT(*) FROM sys.objects WHERE type = 'P' and name = '" + ProcedureName + "'"), 0) == 1;
        }
        public static bool IsTable(ServerDetails LineDetails, string TableName)
        {
            return asiDataDefault.getInt64(asiSSMSTrans.QueryFlag(ServerDetails.sConnection(LineDetails), "SELECT COUNT(*) FROM sys.objects WHERE type = 'U' and name = '" + TableName + "'"), 0) == 1;
        }
    }
}
