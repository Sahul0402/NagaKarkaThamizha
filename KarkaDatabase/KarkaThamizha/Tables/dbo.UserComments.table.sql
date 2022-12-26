CREATE TABLE UserComments(
ID						INT IDENTITY, 
BookID					INT NOT NULL,
AuthorID				INT NOT NULL,
ParentID				VARCHAR(10),
CreatedOn				DATETIME,
ModifiedOn				DATETIME,
CreatorID				INT NOT NULL,
Content					VARCHAR(5000),
FullName				VARCHAR(250),
ProfilePicUrl			VARCHAR(500),
CreatedByAdmin			BIT DEFAULT 0,
CreatedByCurrentUser	BIT DEFAULT 0,
UpVoteCount				INT DEFAULT 0,
UserHasUpVoted			bit DEFAULT 0,
IsNew					BIT DEFAULT 0,
[Status]				BIT  DEFAULT 1
)
 