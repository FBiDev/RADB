--
INSERT INTO Game ( 
	  ID 
	, Title 
	, ConsoleID 
	, NumAchievements 
	, NumLeaderboards 
	, Points 
	, ImageIcon
	) VALUES ( 
	  @ID 
	, @Title 
	, @ConsoleID 
	, @NumAchievements 
	, @NumLeaderboards 
	, @Points 
	, @ImageIcon 
);
--