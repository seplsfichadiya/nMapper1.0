using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using asi.Data;
using nMappers.Models;
using System.Data.SqlClient;

namespace nMappers.Controllers
{
    public class asiTablesController : Controller
    {
        //
        // GET: /asiTables/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TableList()
        {
            ServerDetails LineDetails = (ServerDetails)HttpContext.Session["ServerDetails"];
            return View(asiTablesListModel.ReadAll(LineDetails));
        }
        public ActionResult TableInDetails(string? Name)
        {
            DataSet TableDetails = string.IsNullOrEmpty(Name.Value) ? new DataSet() : asiSSMSTrans.FillDataSet(new SqlConnection(), "sp_help ", new object[] { Name.Value });
            return View(TableDetails);
        }
    }
}