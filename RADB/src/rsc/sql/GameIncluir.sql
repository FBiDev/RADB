--
INSERT INTO GameData ( 
	  ID 
	, Title 
	, ConsoleID 
	, NumAchievements 
	, Points 
	, NumLeaderboards 
	, DateModified 
	, ForumTopicID 
	, Icon 
	) VALUES ( 
	  @ID 
	, @Title 
	, @ConsoleID 
	, @NumAchievements 
	, @Points 
	, @NumLeaderboards 
	, @DateModified 
	, @ForumTopicID 
	, @Icon 
);
--