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
	LEFT JOIN CompanyItems AS ci ON ci.ConsoleID = c.ID 
	LEFT JOIN GameReleasedDate AS rd ON rd.ID = g.ID 
WHERE 1 = 1 
	AND g.ID != (CASE WHEN @allTables = 1 THEN (SELECT -1) ELSE COALESCE((SELECT ID FROM GameToPlay AS p WHERE p.ID = g.ID), -1) END) 
	AND g.ID != (CASE WHEN @allTables = 1 THEN (SELECT -1) ELSE COALESCE((SELECT ID FROM GameToHide AS h WHERE h.ID = g.ID), -1) END) 
	--AND c.ID <> 100 AND c.ID <> 101 
	AND c.Active = 1 
	AND ci.Active = 1 
	AND c.IsGameSystem = 1 
	AND (COALESCE(@ID, 0) = 0 OR g.ID = @ID) 
	AND (COALESCE(@Title, '') = '' OR g.Title LIKE '%'+@Title+'%') 
	AND (COALESCE(@ConsoleID, 0) = 0 OR g.ConsoleID = @ConsoleID) 
	