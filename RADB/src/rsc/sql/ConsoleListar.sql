--
SELECT 
	  ID 
	, Name 
FROM Console 
WHERE 1 = 1 
	AND (ID = @ID 
		OR (@ID = 0 OR @ID IS NULL)) 
	AND (Name LIKE '%'+@Name+'%' 
		OR (@Name = '' OR @Name IS NULL)) 
	