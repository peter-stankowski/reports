using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPR.Models;
using SPR.Services;

namespace StoredProcedureReports.Controllers
{
    public class HomeController : BaseController
    {
        private IDatabaseService service;
        public HomeController()
        {
            service = new DatabaseService();
        }

        [Route("sproc-list")]
        public ActionResult Index()
        {
            var list = service.GetSprocList();

            return View(new ActionResultDetails() { sprocList = list });
        }

        [Route("sproc-details/{sprocname}")]
        public ActionResult Details(string sprocName)
        {
            var sprocParams = service.GetSprocParameters(sprocName);
            var list = service.GetSprocList();

            return View(new ActionResultDetails() { sprocParameters = sprocParams, sprocList = list });
        }
        [HttpPost]
        public ActionResult Details(FormCollection args, string sprocName)
        {
            var postData = FormCollectionToSqlParams(args);

            var result = service.ExecuteSproc(sprocName, postData);
            var sprocParams = service.GetSprocParameters(sprocName);
            var list = service.GetSprocList();

            sprocParams // set parameter/filter values after postback
                .ForEach(p => p.ParameterValue = postData.Find(x => $"@{x.ParameterName}" == p.ParameterName)?
                .Value
                .ToString());

            return View(new ActionResultDetails() { executeData = result, sprocParameters = sprocParams, sprocList = list });
        }

       

    }
}