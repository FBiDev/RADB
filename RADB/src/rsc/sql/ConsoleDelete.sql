--
DELETE 
FROM Console 
WHERE 1 = 1 
	AND (ID = @ID OR COALESCE(@ID, 0) = 0) 
	AND (Name LIKE '%'+@Name+'%' OR COALESCE(@Name, '') = '') 
;
--