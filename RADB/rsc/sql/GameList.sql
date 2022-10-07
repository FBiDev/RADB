--
SELECT
	  g.ID 
	, Title 
	, ConsoleID 
	, c.Name AS ConsoleName 
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
	AND (g.ID = @ID OR COALESCE(@ID, 0) = 0) 
	AND (g.Title LIKE '%'+@Title+'%' OR COALESCE(@Title, '') = '') 
	AND (g.ConsoleID = @ConsoleID OR COALESCE(@ConsoleID, 0) = 0) 
	