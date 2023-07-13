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
            DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "NWindConnectionString");

            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());

            configurator.AllowExecutingCustomSql = true;

            configurator.CustomParameters += (s, e) => {
                var countryParam = e.Parameters.FirstOrDefault(p => p.Name == "CountryDashboardParameter");
                if (countryParam != null) {
                    countryParam.Value = "Brazil";
                }
            };

            configurator.SetDataSourceStorage(dataSourceStorage);

            return configurator;
        }
    }
}
