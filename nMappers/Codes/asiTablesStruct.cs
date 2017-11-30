using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using asi.Data;
namespace nMappers.Codes
{
    public class asiTablesStruct
    {
        public static void ScriptExecution(ServerDetails LineDetails)
        {
            StringBuilder sbSQLScript = new StringBuilder();
            sbSQLScript.AppendLine(@"   IF OBJECT_ID(N'app@Tables', N'U') IS NULL
                                        BEGIN
                                            CREATE TABLE app@Tables
                                            (
                                                rowIdentity		timestamp	NOT NULL,
                                                aTIdentity		bigint		NOT NULL PRIMARY KEY IDENTITY,
                                                [object_id]		int			NOT NULL,
                                                [name]			sysname		NOT NULL,
                                                [create_date]	datetime	NOT NULL,
                                                [modify_date]	datetime	NOT NULL,
                                                CreatedOn		datetime	NOT NULL DEFAULT(GETDATE()),
                                                IsActive		bit			NOT NULL DEFAULT(1),
                                                IsBlock			bit			NOT NULL DEFAULT(0)
                                            )  
                                        END");
            sbSQLScript.AppendLine(@"   IF OBJECT_ID(N'sp$app@Tables$ReadAll', N'P') IS NULL
                                        BEGIN
	                                        CREATE PROCEDURE sp$app@Tables$ReadAll
	                                        @IsBlock BIT = NOT NULL
	                                        AS
	                                        BEGIN
		                                        SELECT	rowIdentity, aTIdentity, [object_id], [name], [create_date],[modify_date] 
		                                        FROM	app@Tables
		                                        WHERE	IsActive = 1 AND IsBlock = @IsBlock
	                                        END
                                        END");
            asiSSMSTrans.ScriptExecutor(ServerDetails.sConnection(LineDetails), sbSQLScript.ToString());
        }
    }
}