using SPR.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPR.Models
{
    public class ActionResultDetails
    {
        public List<SqlStoredProcedureView> sprocList { get; set; }
        public List<SqlParametersView> sprocParameters { get; set; }
        public DataSet executeData { get; set; }

        public string AsJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(executeData);

        }
    }
}
