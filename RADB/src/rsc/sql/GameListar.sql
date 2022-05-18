--
SELECT 
	  Title 
	, ConsoleID 
	, NumAchievements 
	, NumLeaderboards 
	, Points 
	, ImageIcon 
FROM game  
WHERE 1 = 1 
	AND (Title LIKE '%'+@Title+'%' 
		OR (@Title = '' OR @Title IS NULL)) 
	AND (ConsoleID = @ConsoleID 
		OR (@ConsoleID = 0 OR @ConsoleID IS NULL)) 
	