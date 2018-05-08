Visual Studio 
Word Wrap = Edit-> Advanced -> Word Wrap
Create property = prop+Tab+Tab
Create property with value = propfull+Tab+Tab
Create constructor = ctor+Tab+Tab


Classes:

	1.- User
	2.- Film
	3.- Comment

Attributes: 
	
	1. Film: 	
		ID -> int
		Title -> string
		Genres -> List<string>
		Synopsis -> string
		Score -> double?
		Comments -> List<Comment>
	2. User:
		ID -> int
		Username -> string
		Password -> string
		Comments -> List<Comment>
	3. Comment:
		ID -> int
		Author -> User
		Film -> Film
		Score -> int?
		Text -> string
		Creation Date -> DateTime
		Modification Date -> DateTime

Database Architecture: 
	Table Film (
		id INT PRIMARY KEY,
		title STRING,
		genres STRING,
		synopsis STRING NULLABLE,
		score DOUBLE NULLABLE
	)
	Table User (
		id INT PRIMARY KEY,
		username STRING,
		password STRING
	)
	Table Comment (
		id INT PRIMARY KEY,
		user_id INT FOREIGN KEY,
		film_id INT FOREIGN KEY,
		score DOUBLE NULLABLE,
		text STRING,
		creation_date DATETIME,
		modification_date DATETIME
	)

Discussion:

	- IDs in general:
		[Alex]
		Should we use a more complex type for IDs?
		Ex: Guid (.NET equivalent to UUID)
		Or do we stick to ints? (easier to associate with database indices)
		=> INT

	- Film Genres:
		[Alex]
		Based on Ale's original suggestion, should we use Enum instead of string?
		Pros: easier to manipulate.
		Possible cons: how do we make it possible to create new genres that aren't listed on the app if the user doesn't find one that corresponds to the film they're adding?
		=> ENUM

	- User class:
		[Alex]
		Based on Ale's original architecture.
		Are those fields necessary?
		- Name -> do we really need the user's full name, or is a username enough?
		- Active -> how/what for would we use this information?

	- User passwords:
		[Alex]
		How do we store them? (Currently: string field in object)
		Do we hash them? How?
		Can be decided later down the line.
		=> HASH

	- Mutual inclusions:
		[Alex]
		They're back!
		Mutual inclusions were a problem for PenelopeF because we were serialising objects locally.
		But since we're storing the data in a database, in our user class we could have
			private List<Comment> comments;
		instead of
			private List<int> commentsIds;
		It will make accessing the objects easier (user.Comments.get(0) rather than globalHashMap.getCommentFromId(user.CommentsIds.get(0)) like we used to do in PenelopeF)
		Or is there a situation where using IDs rather than the objects themselves is better? 
	- Author and Film in Comment:
		[Alex]
		This question is somewhat similar to the previous one. Do we put the IDs, or the actual User and Film as an attribute?
		The readme was ambiguous ("Film ID (Film attribute)" -> so is it an ID, or an attribute of the type "Film" ?), I went with actual objects.
		We can revert to IDs, it all depends on what we need these for.
		=> AGREED

	- Scores:
		[Alex]
		I added an average score for the Film (type: double?), and a score field for Comments (type: int?)
		General idea: people can add a score (ex: 3/5 stars) when commenting a movie, but it's optional (the text is also optional: you can give a score but have nothing to say)
		The average score is calculated based on the user scores. If there are no scores in the comments, then the score is null (rather than 0/5, which would be sad)
		int? = optional int, double? = optional double

	- Updating Average Scores:
		[Alex]
		Continuation of the previous topic
		Currently the method is inside the Film class.
		When and where do we use it?
		When: after adding, updating or deleting a comment.
		Where: ...? DAO, Controller? In the Film class? => FACADE
		Might need to be moved later down the line.

	- More Film Details
		[Alex]
		We should add more details to a film's page: year, director, maybe the cast?
		These things are considered a bonus by the teacher, but they shouldn't be too much work to implement (at least for the year and director, the cast might be a bit much)

	- Score Champ
		[Ale]
		We need to define who is in charge to the Score. We have one score field on Comment and one on Film but It seems to me both scores refers to the same value. Maybe we should make it on FK from one to another? From Comment-> Movie? 