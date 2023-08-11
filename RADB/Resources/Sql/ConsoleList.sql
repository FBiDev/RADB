--
SELECT 
	  co.ID
	, c.Name AS Company 
	,(CASE WHEN ci.ConsoleNameComplete IS NULL THEN 
		CASE WHEN co.Name IS NULL THEN c.Name ELSE co.Name END 
	  ELSE ci.ConsoleNameComplete END) AS CName 
	, SUM(CASE WHEN g.NumAchievements > 0 THEN 1 ELSE 0 END) NumGames 
	, Count(g.ID) AS TotalGames 
FROM Company AS c 
	LEFT JOIN CompanyItems AS ci ON ci.CompanyID = c.ID 
	LEFT JOIN Console AS co ON co.ID = ci.ConsoleID 
	LEFT JOIN GameData AS g ON g.ConsoleID = co.ID 
		AND g.ConsoleID <> 100 AND g.ConsoleID <> 101 
WHERE 1 = 1 
	AND ci.Active = 1 
	AND co.ID > 0
	AND (co.ID = @ID OR COALESCE(@ID, 0) = 0) 
	AND (co.Name LIKE '%' + @Name + '%' OR COALESCE(@Name, '') = '') 
GROUP BY co.ID 
ORDER BY c.OrderNum, ci.OrderNum, LOWER(CName) 
	