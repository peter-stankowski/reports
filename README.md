*see v1 branch for code*

# Idea

Using `sys.procedures` and `sys.parameter` list all stored procedures under a given schema (default=`dbo`). 
For each sproc, generate relevant html inputs based on sproc parameter types.

### Demo
[DEMO](http://reports.smartpeter.co.uk/sproc-details/CustOrderHist)

### MS Northwind Database
[Norhtind Database](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs)

## Structure

* Database
    * SPR.MsNorthwind
* DataAccess
    * SPR.DataAccess
* Business
    * SPR.Models
    * SPR.Services
* StoredProcedureReports

## Sproc Parameter types

* bit
* datetime
* int
* nvarchar
* uniqueidentifier
* varchar

*Parameter type name must have corresponding razor partial view in the Views/_Helpers folder e.g. Views/_Helpers/**_nvarchar.cshtml***

e.g
```csharp
@model SPR.Models.ViewModels.SqlParametersView

<div class="form-group col-lg-3 col-md-6 col-sm-12">
    <label>@Model.DisplayName</label>
    <input type="text" id="@Model.InputName" name="@Model.InputName" value="@Model.ParameterValue" class="form-control" />
</div>
```

## Special Sproc Parameters
/Views/_Helpers/**_dropdownlist.cshtml**

Sql parameter that ends with **_LookupView** is rendered as a dropdown list (using the above razor template) if the relevant Sql View exists. The Sql View must return 
`Name` and `Value` fields.

*e.g.*
for sql parameter `@CategoryName_LookupView nvarchar(15)` there should be a corresponding sql view `CategoryName_LookupView` that return a list of data with `Name` and `Value` fields.


## Partial Views

Views/_Partial/**_SqlSprocList.cshtml**
```
Accepts list of SqlStoredProcedureView model and generate html link for each sproc
/sproc-details/<StoredProcedureName>
```

Views/_Partial/**_SqlParametersView.cshtml**
```
Accepts list of SqlParametersView model and generate html based on parameter type
```

Views/_Partial/**_SqlExecuteResult.cshtml**
```
Accepts DataSet and generate a data table 
/sproc-details/<StoredProcedureName>
```


