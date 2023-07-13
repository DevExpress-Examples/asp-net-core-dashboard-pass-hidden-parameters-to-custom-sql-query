<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/538484113/2023.1)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1116673)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Dashboard for ASP.NET Core - How to pass a hidden dashboard parameter to a custom SQL query

This example shows how to pass a hidden [dashboard parameter](https://docs.devexpress.com/Dashboard/117062) to a [custom SQL query](https://docs.devexpress.com/Dashboard/117193). In this example, the [`DashboardConfigurator.CustomParameters`](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.CustomParameters) is handled to change the dashboard parameter's default value before it is passed to the query. 

> **Warning**:
> A user can get sensitive information from dashboard parameters. Encode the passed parameter value if possible. Do not store any sensitive information in dashboard parameters that isn't encrypted.

## Example Overview

To pass a hidden dashboard parameter to a custom SQL query, do the following.

### Create a Dashboard Parameter

To [create a dashboard parameter](https://docs.devexpress.com/Dashboard/117547), open the [dashboard menu](https://docs.devexpress.com/Dashboard/117444) and go to the **Parameters** section. In this section, create a parameter and specify its settings. Disable the [*Visible*](https://docs.devexpress.com/Dashboard/js-DevExpress.Dashboard.Model.Parameter#js_devexpress_dashboard_model_parameter_parametervisible) checkbox to hide the parameter from users in Viewer mode. In this example, the dashboard parameter's name is **CountryDashboardParameter** and its default value is _France_:

![Create a Dashboard Parameter](images/create-dashboard-parameter.png)

### Create a Custom Query

Set the [`DashboardConfigurator.AllowExecutingCustomSql`](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.AllowExecutingCustomSql) property to `true` to allow custom SQL query execution on the server. To allow users to edit a custom SQL string in the SQL String editor, call the [`DataSourceWizardOptionBuilder.EnableCustomSql`](https://docs.devexpress.com/Dashboard/DevExpress.DashboardAspNetCore.DataSourceWizardOptionBuilder.EnableCustomSql(System.Boolean)) method and pass `true` as a parameter. 

> **Warning**:
> The use of custom SQL queries can lead to inadvertent or unauthorized modifications to your data/database structure. Ensure that you follow best practices and implement the appropriate user read/write privileges at database level.

You can see the query specified for the data source in the [Data Source Wizard](https://docs.devexpress.com/Dashboard/117680):
 
![Create a Dashboard Parameter](images/data-source-wizard-custom-query.png)

This query contains a query parameter named **CountryParameter**. 

### Bind the Query Parameter to the Dashboard Parameter

To be able to change the query parameter's value, bind it to the **CountryDashboardParameter** dashboard parameter. To do this, on the second page of the Data Source Wizard set the parameter's type to *Expression* and specify the corresponding dashboard parameter in the *Value* field.

![Create a Dashboard Parameter](images/query-parameter-settings.png)

### Change the Default Parameter Value in Code

Handle the [`DashboardConfigurator.CustomParameters`](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.CustomParameters) event and specify the default value: [DashboardUtils.cs](./CS/AspNetCoreDashboard_CustomParameters/Code/DashboardUtils.cs).

As the result, a user sees a dashboard based on the data from the SQL query with the **CountryParameter** query parameter's value specified in the `DashboardConfigurator.CustomParameters` event handler (_Brazil_).

## Files to Review

* [DashboardUtils.cs](./CS/AspNetCoreDashboard_CustomParameters/Code/DashboardUtils.cs)
* [Index.cshtml](./CS/AspNetCoreDashboard_CustomParameters/Pages/Index.cshtml)

## Documentation

- [Dashboard Parameters](https://docs.devexpress.com/Dashboard/117062/web-dashboard/create-dashboards-on-the-web/data-analysis/dashboard-parameters)
- [Query Parameters](https://docs.devexpress.com/Dashboard/117192/web-dashboard/create-dashboards-on-the-web/providing-data/working-with-sql-data-sources/pass-query-parameters)
- [Custom SQL Queries](https://docs.devexpress.com/Dashboard/117193/web-dashboard/create-dashboards-on-the-web/providing-data/working-with-sql-data-sources/custom-sql-queries)

## More Examples

- [Dashboard for Web Forms - How to pass a hidden dashboard parameter to a custom SQL query](https://github.com/DevExpress-Examples/aspxdashboard-how-to-pass-a-hidden-dashboard-parameter-to-a-custom-sql-query-t491903)
- [Dashboard for Web Forms - How to specify dashboard parameter values on the client side](https://github.com/DevExpress-Examples/aspxdashboard-how-to-specify-dashboard-parameter-values-on-the-client-side-t495684)
- [Dashboard for Web Forms - How to update the parameter value when the item's master filter state is changed](https://github.com/DevExpress-Examples/how-to-update-the-parameter-value-when-the-items-master-filter-state-is-changed-t575012)

