--
DELETE 
FROM GameData 
WHERE 1 = 1 
	AND (ID = @ID OR COALESCE(@ID, 0) = 0) 
	AND (ConsoleID = @ConsoleID OR COALESCE(@ConsoleID, 0) = 0) 
;
--