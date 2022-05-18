--
INSERT INTO game ( 
	  Title 
	, ConsoleID 
	, NumAchievements 
	, NumLeaderboards 
	, Points 
	, ImageIcon
	) VALUES ( 
	  @Title 
	, @ConsoleID 
	, @NumAchievements 
	, @NumLeaderboards 
	, @Points 
	, @ImageIcon 
);
--