using Microsoft.Data.SqlClient;
using SysMetricsAPI.Repository.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using Newtonsoft.Json;
using SysMetricsAPI.Models;
using SysMetricsAPI.Connection;

namespace SysPerfAPI.Repository
{
    public class SystemMetricsRepository : ISystemMetricsRepository
    {

        private readonly SqlConnectionFactory _connectionString;

        public SystemMetricsRepository(SqlConnectionFactory connectionString)
        {
            _connectionString = connectionString;
        }

       
        public string SystemMetricsDataFromAgent(SystemMetricsData monitoringResponse)
        {
            var monresjson = JsonConvert.SerializeObject(monitoringResponse);   

            try
            {
                using (var connection = _connectionString.CreateConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand("InsertSystemMetricsData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@JsonData", monresjson);

                        command.ExecuteNonQuery();
                    }
                }

                return "Success"; 
            }
            catch (Exception)
            {
                throw; 
            }
        }


        public SystemMetricsData GetSystemMetricsData()
        {
            var systemMetricsData = new SystemMetricsData
            {
                ParamsMetricsList = new List<ParamMetrics>() 
            };

            try
            {
                using (var connection = _connectionString.CreateConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand("GetSystemMetricsData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var dataSet = new DataSet();
                        using (var adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataSet);
                        }

                        if (dataSet.Tables.Count > 0)
                        {
                            var systemDetailsTable = dataSet.Tables[0];
                            if (systemDetailsTable.Rows.Count > 0)
                            {
                                var row = systemDetailsTable.Rows[0];
                                systemMetricsData.IPV4Address = row["IPV4Address"].ToString();
                                systemMetricsData.HostName = row["HostName"].ToString();
                                systemMetricsData.OSVersion = row["OSVersion"].ToString();
                                systemMetricsData.RetrievalTimestamp = Convert.ToDateTime(row["RetrievalTimestamp"]).ToString("o");
                            }

                            if (dataSet.Tables.Count > 1)
                            {
                                var paramMetricsTable = dataSet.Tables[1];
                                foreach (DataRow row in paramMetricsTable.Rows)
                                {
                                    var paramMetric = new ParamMetrics
                                    {
                                        ParamName = row["ParamName"].ToString(),
                                        ParamValue = row["ParamValue"].ToString(),
                                        ErrorCode = Convert.ToInt32(row["ErrorCode"]),
                                    };

                                    systemMetricsData.ParamsMetricsList.Add(paramMetric);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("A database error occurred while fetching system metrics data.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while fetching system metrics data.", ex);
            }

            return systemMetricsData;
        }






    }
}


