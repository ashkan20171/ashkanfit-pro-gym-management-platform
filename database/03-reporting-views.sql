USE AshkanFitPro;
GO
CREATE OR ALTER VIEW dbo.vw_MemberOverview AS
SELECT m.MemberId,m.Name,m.Phone,m.PlanName,m.ExpiryDate,
CASE WHEN m.ExpiryDate < CONVERT(date,GETDATE()) THEN N'Expired' ELSE N'Active' END AS MembershipStatus,
COALESCE(p.TotalPaid,0) AS TotalPaid, COALESCE(a.TotalVisits,0) AS TotalVisits
FROM dbo.Members m
OUTER APPLY (SELECT SUM(Amount) TotalPaid FROM dbo.Payments WHERE MemberId=m.MemberId) p
OUTER APPLY (SELECT COUNT_BIG(*) TotalVisits FROM dbo.Attendance WHERE MemberId=m.MemberId) a;
GO
CREATE OR ALTER VIEW dbo.vw_TrainerWorkload AS
SELECT t.TrainerId,t.Name,t.Specialty,COUNT(DISTINCT w.WorkoutPlanId) AS WorkoutPlanCount,COUNT(DISTINCT c.ClassId) AS ClassCount
FROM dbo.Trainers t LEFT JOIN dbo.WorkoutPlans w ON w.TrainerId=t.TrainerId
LEFT JOIN dbo.GymClasses c ON c.TrainerId=t.TrainerId
GROUP BY t.TrainerId,t.Name,t.Specialty;
GO
