--
INSERT INTO game ( 
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