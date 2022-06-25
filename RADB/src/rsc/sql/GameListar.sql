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
	LEFT JOIN Console AS c ON c.ID = g.ConsoleID 
WHERE 1 = 1 
	AND c.ID <> 100 AND c.ID <> 101 
	AND (g.ID = @ID 
		OR (@ID = 0 OR @ID IS NULL)) 
	AND (g.Title LIKE '%'+@Title+'%' 
		OR (@Title = '' OR @Title IS NULL)) 
	AND (g.ConsoleID = @ConsoleID 
		OR (@ConsoleID = 0 OR @ConsoleID IS NULL)) 
	