DELETE FROM CommentSet;
DBCC CHECKIDENT (CommentSet, RESEED, 0);
DELETE FROM UserSet;
DBCC CHECKIDENT (UserSet, RESEED, 0);
DELETE FROM FilmSet;
DBCC CHECKIDENT (FilmSet, RESEED, 0);

INSERT INTO UserSet (Username, Password) VALUES ('aleparedes00', 'Passw0rd');
INSERT INTO UserSet (Username, Password) VALUES ('hyvern_a', '#password#');
INSERT INTO UserSet (Username, Password) VALUES ('troll', 'tr011');

INSERT INTO FilmSet (Title, Genres, Synopsis, Director) VALUES ('The Club', 'Comedy', 'A crisis counselor is sent by the Catholic Church to a small Chilean beach town where disgraced priests and nuns, suspected of crimes ranging from child abuse to baby-snatching from unwed mothers, live secluded, after an incident occurs.', 'Pablo Larrain');
INSERT INTO FilmSet (Title, Genres, Synopsis, Director) VALUES ('The Devil and Father Amorth', 'Documentary', 'Father Gabriele Amorth performs his ninth exorcism on an Italian woman', 'William Friedkin');
INSERT INTO FilmSet (Title, Genres, Synopsis, Director) VALUES ('Godard Mon Amour', 'Biography', 'During the making of one of his films, French film director Jean-Luc Godard falls in love with 17-year old actress Anne Wiazemsky and later marries her.', 'Michel Hazanavicius');
INSERT INTO FilmSet (Title, Genres, Synopsis, Score, Director) VALUES ('Isle of Dogs', 'Comedy', 'Set in Japan, Isle of Dogs follows a boy s odyssey in search of his lost dog.', 4.4, 'Wes Anderson');
INSERT INTO FilmSet (Title, Genres, Synopsis, Score, Director) VALUES ('Call Me by Your Name', 'Drama', 'In 1980s Italy, a romance blossoms between a seventeen year-old student and the older man hired as his father s research assistant.', 4.7, 'Luca Guadagnino');

INSERT INTO CommentSet (Score, Text, CreationDate, ModificationDate, User_Id, Film_Id) VALUES (4.5, 'Great movie', GETDATE(), GETDATE(), 1, 1);
INSERT INTO CommentSet (Score, Text, CreationDate, ModificationDate, User_Id, Film_Id) VALUES (1, 'I hate this movie', GETDATE(), GETDATE(), 1, 2);
INSERT INTO CommentSet (Score, Text, CreationDate, ModificationDate, User_Id, Film_Id) VALUES (3, 'Interesting biopic', GETDATE(), GETDATE(), 2, 3);
INSERT INTO CommentSet (Text, CreationDate, ModificationDate, User_Id, Film_Id) VALUES ('I really want to see this movie when it comes out!', GETDATE(), GETDATE(), 2, 4);
INSERT INTO CommentSet (Score, Text, CreationDate, ModificationDate, User_Id, Film_Id) VALUES (0, 'Snore', GETDATE(), GETDATE(), 2, 5);
INSERT INTO CommentSet (Score, Text, CreationDate, ModificationDate, User_Id, Film_Id) VALUES (5, 'Great fun for the whole family', GETDATE(), GETDATE(), 3, 2);

SELECT * FROM UserSet;
SELECT * FROM FilmSet;
SELECT * FROM CommentSet;