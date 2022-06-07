--
SELECT 
	 co.ID 
	, c.Name AS Company 
	,(CASE WHEN ci.ConsoleNameComplete IS NULL THEN co.Name ELSE ci.ConsoleNameComplete END) AS Name 
	, SUM(CASE WHEN g.NumAchievements > 0 THEN 1 ELSE 0 END) NumGames 
	, Count(g.ID) AS TotalGames 
FROM Company AS c 
	LEFT JOIN CompanyItems AS ci ON ci.CompanyID = c.ID 
		--AND ci.Active = 1 
	LEFT JOIN Console AS co ON co.ID = ci.ConsoleID 
	LEFT JOIN GameData AS g on g.ConsoleID = co.ID OR co.Name IS NULL 
WHERE 1 = 1 
	AND co.Name IS NULL AND @ID = 0 
	OR ci.Active = 1 
	AND (co.ID = @ID 
		OR (@ID = 0 OR @ID IS NULL)) 
	AND (co.Name LIKE '%' + @Name + '%' 
		OR (@Name = '' OR @Name IS NULL)) 
GROUP BY co.ID 
ORDER BY c.OrderNum, ci.OrderNum, Name 
	