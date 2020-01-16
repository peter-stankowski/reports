using SPR.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.Mvc.Html;
using System.Text.RegularExpressions;

namespace StoredProcedureReports
{
    public static class HtmlHelpers
    {

        static string helpersRootPath = "~/Views/_Helpers";
        static string helperFullPath(string typeName)
        {
            var path = $"{helpersRootPath}/_{typeName}.cshtml";

            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                return path;
            } else
            {
                return $"{helpersRootPath}/_varchar.cshtml";
            }
        }

        public static IHtmlString RenderParameter(this HtmlHelper helper, SqlParametersView parameter)
        {
            var type = (SqlDbType)Enum.Parse(typeof(SqlDbType), parameter.ParameterDataType, true);
            var typestring = type.ToString().ToLower();

            if (parameter.LookupData != null)
            {
                typestring = "dropdownlist"; 
            }

            // find helper template
            var input = helper.Partial(helperFullPath(typestring), parameter);

            return input;
        }

        public static IHtmlString RenderDataTable(this HtmlHelper helper, DataTable data)
        {
            var typestring = typeof(DataTable).Name.ToString().ToLower();
            
            // find helper template
            var input = helper.Partial(helperFullPath(typestring), data);

            return input;

        }

        public static string GetActiveSprocName(this HtmlHelper helper)
        {
            return helper.ViewContext.RouteData.Values["sprocname"]?.ToString() ?? "";
        }

        public static string SplitPascalCase(this HtmlHelper helper, string stringToSplit)
        {
            return Regex.Replace(stringToSplit, "([A-Z]{1,2}|[0-9]+)", " $1").TrimStart();
        }

    }
}