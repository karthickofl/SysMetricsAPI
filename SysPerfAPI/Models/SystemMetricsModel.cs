namespace SysMetricsAPI.Models
{
    public class SystemMetricsModel
    {
    }

    public class SystemMetricsData
    {
        public string IPV4Address { get; set; }
        public string HostName { get; set; }
        public string OSVersion { get; set; }
        public string RetrievalTimestamp { get; set; }

        public List<ParamMetrics> ParamsMetricsList { get; set; }
    }

    public class ParamMetrics
    {
        public string ParamName { get; set; }
        public string ParamValue { get; set; }
        public int ErrorCode { get; set; }
    }

}
