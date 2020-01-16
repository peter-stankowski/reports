using SPR.Models.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SPR.DataAccess
{
    public interface IEfContext
    {
        string DefaultSchema { get; }

        IQueryable<SqlStoredProcedureView> SqlSprocView { get; }
        IQueryable<SqlParametersView> SqlSprocParamView { get; }
        DataSet RunSproc(string sprocName, List<SqlParameter> parameters);
        IQueryable<LookupViewModel> GetLookupView(string lookupViewName);
    }
}
