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
	INNER JOIN GameToHide AS gh ON gh.ID = g.ID 
WHERE 1 = 1 
	