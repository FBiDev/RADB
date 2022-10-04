--
INSERT INTO GameData ( 
	  ID 
	, Title 
	, ConsoleID 
	, ImageIcon 
	, NumAchievements 
	, Points 
	, NumLeaderboards 
	, DateModified 
	, ForumTopicID 
	) VALUES ( 
	  @ID 
	, @Title 
	, @ConsoleID 
	, @ImageIcon 
	, @NumAchievements 
	, @Points 
	, @NumLeaderboards 
	, @DateModified 
	, @ForumTopicID 
);
--