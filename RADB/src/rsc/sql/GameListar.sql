--
SELECT
	  G.ID 
	, Title 
	, ConsoleID 
	, C.Name as ConsoleName 
	, NumAchievements 
	, Points 
	, NumLeaderboards 
	, DateModified 
	, ForumTopicID 
	, Icon 
FROM GameData AS G 
	LEFT JOIN Console as C on C.ID=G.ConsoleID 
WHERE 1 = 1 
	AND (G.ID = @ID 
		OR (@ID = 0 OR @ID IS NULL)) 
	AND (G.Title LIKE '%'+@Title+'%' 
		OR (@Title = '' OR @Title IS NULL)) 
	AND (G.ConsoleID = @ConsoleID 
		OR (@ConsoleID = 0 OR @ConsoleID IS NULL)) 
	