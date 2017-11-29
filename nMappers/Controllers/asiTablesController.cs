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
        public ActionResult TableInDetails()
        {
            DataSet TableDetails = string.IsNullOrEmpty("") ? new DataSet() : asiSSMSTrans.FillDataSet(new SqlConnection(), "sp_help ", new object[] { "" });
            return View(TableDetails);
        }
    }
}