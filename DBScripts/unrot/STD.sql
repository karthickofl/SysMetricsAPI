CREATE OR ALTER PROCEDURE GetAllStudents
AS
BEGIN
    SET NOCOUNT ON;

    SELECT StudentID, StudentName FROM Students;
END;
