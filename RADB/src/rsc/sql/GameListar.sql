--
SELECT
	  g.ID 
	, Title 
	, ConsoleID 
	, C.Name AS ConsoleName 
	, ImageIcon 
	, NumAchievements 
	, Points 
	, NumLeaderboards 
	, DateModified 
	, ForumTopicID 
FROM GameData AS g 
	INNER JOIN Console AS c ON c.ID = g.ConsoleID 
WHERE 1 = 1 
	AND g.ID != (CASE WHEN @allTables = 1 THEN (SELECT -1) ELSE COALESCE((SELECT ID FROM GameToPlay AS p WHERE p.ID = g.ID), -1) END)
	AND g.ID != (CASE WHEN @allTables = 1 THEN (SELECT -1) ELSE COALESCE((SELECT ID FROM GameToHide AS h WHERE h.ID = g.ID), -1) END)
	AND c.ID <> 100 AND c.ID <> 101 
	AND (g.ID = @ID 
		OR (@ID = 0 OR @ID IS NULL)) 
	AND (g.Title LIKE '%'+@Title+'%' 
		OR (@Title = '' OR @Title IS NULL)) 
	AND (g.ConsoleID = @ConsoleID 
		OR (@ConsoleID = 0 OR @ConsoleID IS NULL)) 
	