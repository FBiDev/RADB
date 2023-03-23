--
SELECT
	  g.ID 
	, Title 
	, g.ConsoleID 
	, c.Name AS ConsoleName 
	, CASE WHEN ci.ConsoleNameShort IS NULL THEN c.Name ELSE ci.ConsoleNameShort END AS ConsoleNameShort 
	, strftime('%Y-%m-%d', rd.ReleasedDate) AS ReleasedDate 
	, ImageIcon 
	, NumAchievements 
	, Points 
	, NumLeaderboards 
	, DateModified 
	, ForumTopicID 
FROM GameData AS g 
	INNER JOIN Console AS c ON c.ID = g.ConsoleID 
	INNER JOIN GameToHide AS gh ON gh.ID = g.ID 
	LEFT JOIN CompanyItems AS ci ON ci.ConsoleID = c.ID 
	LEFT JOIN GameReleasedDate AS rd ON rd.ID = g.ID 
WHERE 1 = 1 
	