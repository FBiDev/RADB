--
SELECT 
	  c.ID 
	, c.Name 
	, SUM(CASE WHEN g.NumAchievements > 0 THEN 1 ELSE 0 END) NumGames 
	, Count(g.ID) AS TotalGames 
FROM Console AS c 
	LEFT JOIN Game AS g on g.ConsoleID = c.ID 
WHERE 1 = 1 
	AND (c.ID = @ID 
		OR (@ID = 0 OR @ID IS NULL)) 
	AND (c.Name LIKE '%'+@Name+'%' 
		OR (@Name = '' OR @Name IS NULL)) 
GROUP BY c.ID 
	