using SysMetricsAPI.Models;

namespace SysMetricsAPI.Repository.Interfaces
{
    public interface ISystemMetricsRepository
    {
        string SystemMetricsDataFromAgent(SystemMetricsData monitoringResponse);
        SystemMetricsData GetSystemMetricsData();
    }
}
