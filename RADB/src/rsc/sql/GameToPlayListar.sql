﻿--
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
	INNER JOIN GameToPlay AS gh ON gh.ID = g.ID 
WHERE 1 = 1 
	