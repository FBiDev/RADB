--
DELETE 
FROM GameDataExtend 
WHERE 1 = 1 
	AND (ID = @ID 
		OR (@ID = 0 OR @ID IS NULL)) 
	AND (ConsoleID = @ConsoleID 
		OR (@ConsoleID = 0 OR @ConsoleID IS NULL)) 
;
--