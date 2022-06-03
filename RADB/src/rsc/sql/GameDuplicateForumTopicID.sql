--
SELECT * FROM Game 
WHERE 1=1 
	AND ForumTopicID IN 
	(
		SELECT ForumTopicID FROM Game 
		Group By ForumTopicID 
		HAVING Count(*) > 1 
	)
	AND ConsoleID NOT IN (100, 101) 
ORDER BY ForumTopicID, ConsoleID ASC 
	