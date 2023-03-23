--
SELECT ID, ConsoleID FROM GameData AS g
WHERE (
	g.ConsoleID = @ConsoleID OR COALESCE(@ConsoleID, 0) = 0) 
	AND NOT EXISTS(
		SELECT ID FROM GameReleasedDate AS gr WHERE ID = g.ID 
);
--