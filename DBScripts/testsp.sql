-- Define JSON data
DECLARE @JsonData NVARCHAR(MAX);
SET @JsonData = N'{
    "IPV4Address": "192.168.1.1",
    "HostName": "test-computer",
    "OSVersion": "Windows 11",
    "RetrievalTimestamp": "2024-08-21T12:00:00Z",
    "ParamsMetricsList": [
        {
            "ParameterName": "CPUUsage",
            "ParameterValue": "80%"
        },
        {
            "ParameterName": "DiskSpace",
            "ParameterValue": "50GB"
        }
    ]
}';

--SELECT
--    JSON_VALUE(@JsonData, '$.IPV4Address') AS IPV4Address,
--    JSON_VALUE(m.value, '$.ParameterName') AS ParameterName,
--    JSON_VALUE(m.value, '$.ParameterValue') AS ParameterValue
--FROM OPENJSON(@JsonData, '$.ParamsMetricsList') WITH (value NVARCHAR(MAX) '$') m;

-- Execute the stored procedure
EXEC dbo.InsertSystemMetricsData @JsonData;
