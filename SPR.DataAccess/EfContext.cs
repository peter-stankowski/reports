using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPR.Models.ViewModels;

namespace SPR.DataAccess
{
    public class EfContext : DbContext, IEfContext
    {
        string _defaultSchema = null;
        public string DefaultSchema
        {
            get
            {
                if (_defaultSchema == null) throw new ArgumentNullException("Database schema cannot be empy.");
                return _defaultSchema;
            }
        }

        public IQueryable<SqlStoredProcedureView> SqlSprocView
        {
            get
            {
                return Database.SqlQuery<SqlStoredProcedureView>(@"
                    select		StoredProcedureID		= object_id,
				                StoredProcedureName		= sys.procedures.name,
				                StoredProcedureSchema	= SCHEMA_NAME(schema_id)
	                from		sys.procedures").Where(x => x.StoredProcedureSchema == DefaultSchema).AsQueryable();
            }
        }
        public IQueryable<SqlParametersView> SqlSprocParamView
        {
            get
            {
                return Database.SqlQuery<SqlParametersView>(@"
                    select		StoredProcedureID		= sys.procedures.object_id,
				                StoredProcedureName		= sys.procedures.name,
				                StoredProcedureSchema	= SCHEMA_NAME(schema_id),
				                ParameterID				= parameter_id,
				                ParameterName			= sys.parameters.name,
				                [ParameterDataType]		= type_name(user_type_id),
				                [MaxLength]				= max_length,
				                IsNullable				= is_nullable,
				                IsReadOnly				= is_readonly
	                from		sys.parameters  
	                inner join	sys.procedures on parameters.object_id = procedures.object_id").Where(x => x.StoredProcedureSchema == DefaultSchema).AsQueryable();
            }
        }

        public EfContext(string defaultSchema=null, string defaultConnection= "DefaultConnection") : base(defaultConnection)
        {
            Database.SetInitializer<EfContext>(null);
            _defaultSchema = defaultSchema;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);
        }


        // methods
        public DataSet RunSproc(string sprocName, List<SqlParameter> parameters)
        {
            var paramString = string.Join(",", parameters.Select(x => x.ParameterName.StartsWith("@") ? x.ParameterName : $"@{x.ParameterName}"));

            var cmd = base.Database.Connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = $"{DefaultSchema}.{sprocName}";
            cmd.Parameters.AddRange(parameters.ToArray());

            var ds = new DataSet();
            var dt = new DataTable();

            base.Database.Connection.Open();
            var reader = cmd.ExecuteReader();
            do
            {
                var tb = new DataTable();
                tb.Load(reader);
                ds.Tables.Add(tb);

            } while (!reader.IsClosed);

            return ds;
        }

        public IQueryable<LookupViewModel> GetLookupView(string lookupViewName)
        {
            var cmdString = $"select * from {DefaultSchema}.{lookupViewName}";

            return Database.SqlQuery<LookupViewModel>(cmdString).AsQueryable();
        }
    }
}
