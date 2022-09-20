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
	AND NumAchievements > 0 
	AND c.ID <> 100 AND c.ID <> 101 
	AND (g.ConsoleID = @ConsoleID 
		OR (@ConsoleID = 0 OR @ConsoleID IS NULL)) 
	