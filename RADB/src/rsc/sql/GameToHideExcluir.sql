--
DELETE 
FROM GameToHide 
WHERE 1 = 1 
	AND (ID = @ID 
		OR (@ID = 0 OR @ID IS NULL)) 
;
--