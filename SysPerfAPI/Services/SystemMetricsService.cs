using Microsoft.AspNetCore.Mvc;
using SysMetricsAPI.Models;
using SysMetricsAPI.Repository.Interfaces;
using SysMetricsAPI.Services.Interfaces;
using SysPerfAPI.Repository;

namespace SysPerfAPI.Services
{
    public class SystemMetricsService : ISystemMetricsService
    {
        private readonly ISystemMetricsRepository _systemMetricsRepository;

        public SystemMetricsService(ISystemMetricsRepository sysmetricrepo)
        {
            _systemMetricsRepository = sysmetricrepo;
        }


        public string SystemMetricsDataFromAgent(SystemMetricsData monitoringResponse)
        {
            string status;
            try
            {
                status = _systemMetricsRepository.SystemMetricsDataFromAgent(monitoringResponse);
            }
            catch (Exception)
            {
                throw;
            }
       
            return status;
        }

        public SystemMetricsData GetSystemMetricsData()
        {
            SystemMetricsData sysMetricsData;
            try
            {
                sysMetricsData = _systemMetricsRepository.GetSystemMetricsData();
            }
            catch (Exception)
            {
                throw;
            }

            return sysMetricsData;
        }

    }
}
