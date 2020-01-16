using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoredProcedureReports.Controllers
{
    public class BaseController : Controller
    {

        public List<SqlParameter> FormCollectionToSqlParams(FormCollection args)
        {
            var p = new List<SqlParameter>();
            foreach (var key in args.AllKeys)
            {
                string value = args[key];
                if (value != "")
                {
                    p.Add(new SqlParameter(key, value));
                }
                else
                {
                    //p.Add(new SqlParameter(key, DBNull.Value));
                }
            }

            return p;

        }

    }


}