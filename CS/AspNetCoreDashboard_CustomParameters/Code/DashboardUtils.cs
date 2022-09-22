using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Excel;
using DevExpress.DataAccess.Sql;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace AspNetCoreDashboard_CustomParameters {
    public static class DashboardUtils {
        public static DashboardConfigurator CreateDashboardConfigurator(IConfiguration configuration, IFileProvider fileProvider) {
            DashboardConfigurator configurator = new DashboardConfigurator();
            configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(configuration));

            DashboardFileStorage dashboardFileStorage = new DashboardFileStorage(fileProvider.GetFileInfo("Data/Dashboards").PhysicalPath);
            configurator.SetDashboardStorage(dashboardFileStorage);

            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
            DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "sqlDataSource");

            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());

            configurator.AllowExecutingCustomSql = true;

            configurator.CustomParameters += (s, e) => {
                var custIDParameter = e.Parameters.FirstOrDefault(p => p.Name == "CustomerIdDashboardParameter");
                if (custIDParameter != null) {
                    custIDParameter.Value = "ALFKI";
                }
            };

            configurator.ConfigureDataConnection += (s, e) => {
                CustomStringConnectionParameters sqlConnectionParameters =  new CustomStringConnectionParameters();
                string path = fileProvider.GetFileInfo("Data/NWind.mdf").PhysicalPath;
                sqlConnectionParameters.ConnectionString = $"XpoProvider = MSSqlServer; Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = {path}; Integrated Security = True";
                e.ConnectionParameters = sqlConnectionParameters;
            };

            configurator.SetDataSourceStorage(dataSourceStorage);

            return configurator;
        }
    }
}