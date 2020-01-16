using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPR.Models.ViewModels
{


    public class SqlStoredProcedureView
    {
        public int StoredProcedureID { get; set; }
        public string StoredProcedureName { get; set; }
        public string StoredProcedureSchema { get; set; }

        public ICollection<SqlParametersView> SqlParameters { get; set; }
    }

    public class SqlParametersView
    {
        public int ParameterID { get; set; }
        public string ParameterName { get; set; }
        public string ParameterDataType { get; set; }
        public Int16 MaxLength { get; set; }
        public bool IsNullable { get; set; }
        public bool IsReadOnly { get; set; }

        public int StoredProcedureID { get; set; }
        public string StoredProcedureName { get; set; }
        public string StoredProcedureSchema { get; set; }

        public string ParameterValue { get; set; }

        public string DisplayName
        {
            get
            {
                return ParameterName.Replace("@", "").Replace("_LookupView", " Lookup");
            }
        }

        public string InputName
        {
            get
            {
                return ParameterName.Replace("@", "");
            }
        }

        public bool IsLookup
        {
            get
            {
                return ParameterName.EndsWith("_LookupView") ? true : false;
            }
        }

        public string LookupName
        {
            get
            {
                return ParameterName.Replace("@", "");
            }
        }

        public List<LookupViewModel> LookupData { get; set; }

    }
}
