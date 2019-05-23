// SELECT ALL GENRES :

// SELECT * FROM Genre;


//INSERT AN ARTIST:

// INSERT INTO Artist (ArtistName, YearEstablished) VALUES ('Kendrick Lamar', 2004);


// INSERT AN ALBUM:

// INSERT INTO Album (Title, ReleaseDate, AlbumLength, Label, ArtistId, GenreId) VALUES ('DAMN.', '4/14/2017', 2268, 'Aftermath', (SELECT Id FROM Artist WHERE Artist.ArtistName = 'Kendrick Lamar'), (SELECT Id FROM Genre where Genre.Label = 'Rap'));


//INSERT A SONG:

// INSERT INTO Song (Title, SongLength, ReleaseDate, GenreId, ArtistId, AlbumId) VALUES ('DNA.', 305, '4/14/2017', (SELECT Id FROM Genre where Genre.Label = 'Rap'),(SELECT Id FROM Artist WHERE Artist.ArtistName = 'Kendrick Lamar'),(SELECT Id FROM Album where Album.Title = 'DAMN.'));


//LIST NEW ALBUM WITH CREATED SONGS AND ARTIST NAME:

//SELECT a.Title AS 'Album Title',
//        s.Title AS 'Song Title', ar.ArtistName AS 'Artist Name'
//   FROM [Album] a
//   JOIN Song s ON a.Id = s.AlbumId
//   JOIN Artist ar ON ar.Id = s.ArtistId  WHERE ar.ArtistName = 'Kendrick Lamar'
// ORDER BY s.Id


//LIST ALL ALBUMS WITH SONG COUNT:

// SELECT a.Title, Count(s.AlbumId) AS 'Number of Songs per Album'
//  FROM Song s
//  LEFT JOIN Album a ON a.Id = s.AlbumId
//  GROUP BY a.Title;


//LIST ALL SONGS PER GENRE:

//  SELECT g.Label, Count(s.Id) AS 'Number of Songs per Genre'
//  FROM Song s
//  LEFT JOIN Genre g ON g.Id = s.GenreId
//  GROUP BY g.Label;


//LIST LONGEST PLAYTIME ALBUM:

// SELECT TOP 1 a.Title, a.AlbumLength
// FROM Album a
// ORDER BY a.AlbumLength DESC;


//LIST LONGEST SONG:

//  SELECT TOP 1 s.Title, s.SongLength
//  FROM song s
//  ORDER BY s.SongLength DESC;

// LIST LONGEST SONG AND ARTIST:

//  SELECT TOP 1 s.Title AS 'Song Title', s.SongLength AS 'Song Length', a.ArtistName AS 'Artist Name'
//  FROM Song s
//  RIGHT JOIN Artist a ON a.Id = s.ArtistId
//  ORDER BY s.SongLength DESC;