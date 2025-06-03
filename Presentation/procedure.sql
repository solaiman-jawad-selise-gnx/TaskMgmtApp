CREATE PROCEDURE GetTeamUsersWithTasks
AS
BEGIN
    SELECT 
        t.Id AS TeamId,
        t.Name AS TeamName,
        t.Description AS TeamDescription,

        u.Id AS UserId,
        u.FullName AS UserFullName,
        u.Email AS UserEmail,
        u.Role AS UserRole,

        ti.Id AS TaskId,
        ti.Title AS TaskTitle,
        ti.Description AS TaskDescription,
        ti.Status AS TaskStatus,
        ti.DueDate AS TaskDueDate

    FROM Teams t
    LEFT JOIN TaskItems ti ON ti.TeamId = t.Id
    LEFT JOIN Users u ON u.Id = ti.AssignedToUserId
    ORDER BY t.Id, u.Id, ti.Id
END