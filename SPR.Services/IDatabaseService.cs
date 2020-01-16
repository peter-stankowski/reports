using SPR.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPR.Services
{
    public interface IDatabaseService
    {
        List<SqlStoredProcedureView> GetSprocList();
        List<SqlParametersView> GetSprocParameters(string storedProcedureName);
        DataSet ExecuteSproc(string storedProcedureName, List<SqlParameter> parameters);
    }
}
