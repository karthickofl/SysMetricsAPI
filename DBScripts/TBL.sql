CREATE TABLE SystemDetails (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IPV4Address NVARCHAR(255),
    HostName NVARCHAR(255),
    OSVersion NVARCHAR(255),
    RetrievalTimestamp DATETIME,
	CreatedOnTimestamp  DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

CREATE TABLE ParamMetrics (
    Id INT PRIMARY KEY IDENTITY(1,1),
    SystemDetailsId INT FOREIGN KEY REFERENCES SystemDetails(Id),
    ParamName NVARCHAR(255),
    ParamValue NVARCHAR(255),
	ErrorCode  INT,
	CreatedOnTimestamp  DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

SELECT GETDATE()
select * from SystemDetails(nolock)
select * from ParamMetrics(nolock)

delete from ParamMetrics

delete from SystemDetails

--DROP table SystemDetails
--DROP table ParamMetrics



   


delete from SystemDetails
delete  from ParamMetrics