using Microsoft.AspNetCore.Mvc;
using SysMetricsAPI.Models;

namespace SysMetricsAPI.Services.Interfaces
{
    public interface ISystemMetricsService
    {
        string SystemMetricsDataFromAgent(SystemMetricsData monitoringResponse);
        SystemMetricsData GetSystemMetricsData();

    }
}
