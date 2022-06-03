--
INSERT INTO Game ( 
	  ID 
	, Title 
	, ConsoleID 
	, NumAchievements 
	, Points 
	, NumLeaderboards 
	, DateModified 
	, ForumTopicID 
	, ImageIcon 
	) VALUES ( 
	  @ID 
	, @Title 
	, @ConsoleID 
	, @NumAchievements 
	, @Points 
	, @NumLeaderboards 
	, @DateModified 
	, @ForumTopicID 
	, @ImageIcon 
);
--