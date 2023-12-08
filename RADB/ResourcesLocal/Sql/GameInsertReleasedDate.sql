--
DELETE FROM GameReleasedDate WHERE ID = @ID AND ReleasedDate IS NULL AND @ReleasedDate IS NOT NULL;
INSERT INTO GameReleasedDate (
	  ID
	, ReleasedDate
	, ConsoleID
	) SELECT * FROM (SELECT 
		  @ID AS ID 
		, @ReleasedDate AS ReleasedDate 
		, @ConsoleID AS ConsoleID 
	) AS tmp
WHERE NOT EXISTS (
	SELECT * FROM GameReleasedDate AS GRD WHERE ID = @ID 
);
--