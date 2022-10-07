--
SELECT 
	  g.ID 
	, g.Title 
	, g.ConsoleID 
	, c.Name AS ConsoleName 
	, g.ImageIcon 
	, g.NumAchievements 
	, g.Points 
	, g.NumLeaderboards 
	, g.DateModified 
	, g.ForumTopicID 
	, gx.Developer 
	, gx.Publisher 
	, gx.Genre 
	, gx.Released 
	, gx.ImageTitle 
	, gx.ImageIngame 
	, gx.ImageBoxArt 
FROM GameDataExtend AS gx 
	LEFT JOIN GameData AS g ON g.ID = gx.ID 
	LEFT JOIN Console AS c ON c.ID = g.ConsoleID 
WHERE 1 = 1 
	AND (g.ID = @ID OR COALESCE(@ID, 0) = 0) 
	AND (g.ConsoleID = @ConsoleID OR COALESCE(@ConsoleID, 0) = 0) 
	