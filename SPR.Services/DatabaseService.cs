using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPR.DataAccess;
using SPR.Models.ViewModels;

namespace SPR.Services
{
    public class DatabaseService : IDatabaseService
    {
        private IEfContext db;
        public DatabaseService()
        {
            db = new EfContext("dbo", "DefaultConnection");
        }

        public List<SqlStoredProcedureView> GetSprocList()
        {
            return db
                    .SqlSprocView
                    .ToList();
        }

        public List<SqlParametersView> GetSprocParameters(string storedProcedureName)
        {
            var data = db
                        .SqlSprocParamView
                        .Where(x => x.StoredProcedureName == storedProcedureName)
                        .ToList();

            foreach(var param in data)
            {
                if (param.IsLookup) // check if parameter is a "lookup" parameter
                {
                    try // catch error, as we want to continue with "standard" templates if something goes wrong
                    {
                        var lookupdata = db.GetLookupView(param.LookupName).ToList(); // fetch view data
                        if(lookupdata != null)
                            data.ForEach(x => { 
                                if (x.ParameterName == param.ParameterName)
                                    x.LookupData = lookupdata; // attach lookup data for matching parameters
                            });
                    }
                    catch (Exception e) { }

                }
            }
            return data;
        }

        public DataSet ExecuteSproc(string storedProcedureName, List<SqlParameter> parameters)
        {
            //var sprocParams = GetSprocParameters(storedProcedureName);
            //foreach(var sprocp in sprocParams)
            //{
            //    var sqlp = parameters.Find(x => "@"+x.ParameterName == sprocp.ParameterName);

            //}

            return db
                    .RunSproc(storedProcedureName, parameters);
        }


    }
}
