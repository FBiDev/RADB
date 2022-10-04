--
DELETE 
FROM GameToPlay 
WHERE 1 = 1 
	AND (ID = @ID OR COALESCE(@ID, 0) = 0) 
;
--