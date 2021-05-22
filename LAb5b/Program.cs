using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAb5b
{
    class Program
    {

        static Media[] allMedia = new Media[100];
        static int numberOfMedia = 0;

        public Program()
        {

            if (!read())
            {
                //Display Error message diaglog box.                 
            }
            
        }


        /// <summary>
        /// Reads the data file and populates the media array.
        /// </summary>
        /// <returns></returns>
        private bool read()
        {

            bool successful = true;
            string fileName = "Data.txt";

            try {
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                string[] delimiter = { "-----" };
                string[] mediaData = streamReader.ReadToEnd().Split(delimiter, System.StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < mediaData.Length; i++) {
                    string theMedia = mediaData[i].Trim();
                    string identifier = theMedia.Substring(0, 5);

                    int lengthOfProperties;
                    if (identifier.Contains("SONG")) {
                        lengthOfProperties = theMedia.Length;
                    } else {
                        lengthOfProperties = theMedia.IndexOf("\r\n");
                    }

                    string[] theProperties = theMedia.Substring(0, lengthOfProperties).Split('|');
                    string EncryptedSummary = theMedia.Substring(lengthOfProperties).Trim();
                    int year;
                    int.TryParse(theProperties[2], out year);

                    if (identifier.Contains("BOOK")) {
                        Book theBook = new Book( theProperties[1], year, theProperties[3], EncryptedSummary);
                        allMedia[numberOfMedia] = theBook;
                    } else if (identifier.Contains("SONG")) {
                        Song theSong = new Song( theProperties[1], year, theProperties[3], theProperties[4]);
                        allMedia[numberOfMedia] = theSong;
                    } else if (identifier.Contains("MOVIE")) {
                        Movie theMovie = new Movie( theProperties[1], year, theProperties[3], EncryptedSummary);
                        allMedia[numberOfMedia] = theMovie;
                    }
                    numberOfMedia++;
                }
                streamReader.Close();
                fileStream.Close();
            } catch (Exception e) {
                successful = false;
                if (e.GetType().ToString() == "System.IO.FileNotFoundException") {
                    Console.WriteLine(fileName + " not fonud");
                } else if (e.GetType().ToString() == "System.IndexOutOfRangeException") {
                    Console.WriteLine("Too many records in " + fileName);
                } else if (e.GetType().ToString() == "System.ArgumentOutOfRangeException") {
                    Console.WriteLine("The records are not well formatted");
                } else {
                    Console.WriteLine(e.GetType().ToString());
                }
            }
            return successful;
        }

        public string[,] Search(string searchTerm ) 
        {
            
            int count = 0;
            foreach (var theMedia in allMedia)
            {
                if (theMedia != null && theMedia.Search(searchTerm))
                {
                    count++;
                }
            }


            int two = 2;
            string[,] lst = new string[count, two];
            int b = 0;
            for (int i = 0; i < allMedia.Length; i++)
            {
                if (allMedia[i] != null && allMedia[i].Search(searchTerm))
                {
                    
                    if (allMedia[i] is Book)
                    {
                        Book theBook = allMedia[i] as Book;
                        lst[b, 0] += "Book Title: " + theBook.Title + " (" + theBook.Year + ") \n";
                        lst[b, 0] += "Author: " + theBook.Author;
                        lst[b, 1] = theBook.Decrypt();                     
                    }

                    if (allMedia[i] is Movie)
                    {
                        Movie theMovie = allMedia[i] as Movie;
                        lst[b, 0] += "Movie Title: " + theMovie.Title + " (" + theMovie.Year + ") \n";
                        lst[b, 0] += "Director: " + theMovie.Director;
                        lst[b, 1] = theMovie.Decrypt();
                     
                    }
                    if (allMedia[i] is Song)
                    {
                        Song theSong = allMedia[i] as Song;
                        lst[b, 0] += "Song Title: " + theSong.Title + " (" + theSong.Year + ") \n";
                        lst[b, 0] += "Album: " + theSong.Album + "  Artist: " + theSong.Artist;
                        lst[b, 1] = null;
                     
                    }
                    b++;

                }
            }



            return lst;            
        }


        /// <summary>
        /// Produce the list of books.
        /// </summary>
        public string[ , ] ListBooks()
        {
            int count = 0;
            for (int i = 0; i < allMedia.Length; i++)
            {
                if (allMedia[i] is Book)
                { count++; }
            }

                    int two = 2;
            string[ , ] lst = new string[count, two ];
            int b = 0;
            for( int i = 0; i < allMedia.Length; i++)
            {
                if (allMedia[i] is Book)
                {
                    Book theBook = allMedia[i] as Book;
                    lst[b , 0] += "Book Title: " + theBook.Title + " (" + theBook.Year + ") \n";
                    lst[b , 0] += "Author: " + theBook.Author;
                    lst[b , 1] = theBook.Decrypt();                    
                    b++;                    
                }
            }
            return lst;
        }

        /// <summary>
        /// Produce the list of movies.
        /// </summary>
        public string[,] ListMovies()
        {
            int count = 0;
            for (int i = 0; i < allMedia.Length; i++)
            {
                if (allMedia[i] is Movie)
                { count++; }
            }


            int two = 2;
            string[,] lst = new string[count, two];
            int b = 0;
            for (int i = 0; i < allMedia.Length; i++)
            {
                if (allMedia[i] is Movie)
                {
                    Movie theMovie = allMedia[i] as Movie;            
                    lst[b, 0] += "Movie Title: " + theMovie.Title + " (" + theMovie.Year + ") \n";
                    lst[b, 0] += "Director: " + theMovie.Director;
                    lst[b, 1] = theMovie.Decrypt();
                    b++;
                }
            }
            return lst;
        }

        /// <summary>
        /// Produce the list of songs.
        /// </summary>
        public string[,] ListSongs()
        {
            int count = 0;
            for (int i = 0; i < allMedia.Length; i++)
            {
                if (allMedia[i] is Book)
                { count++; }
            }



            int two = 2;
            string[,] lst = new string[count, two];
            int b = 0;
            for (int i = 0; i < allMedia.Length; i++)
            {
                if (allMedia[i] is Song)
                {
                    Song theSong = allMedia[i] as Song;
                    lst[b, 0] += "Song Title: " + theSong.Title + " (" + theSong.Year + ") \n";
                    lst[b, 0] += "Album: " + theSong.Album + "  Artist: " + theSong.Artist;                   
                    lst[b, 1] = null;
                    b++;
                }

            }
            return lst;
        }

        /// <summary>
        /// Produce the list of all the media.
        /// </summary>
        public string[,] ListAll()
        {
            int two = 2;
            string[,] lst = new string[allMedia.Length, two];
            int b = 0;
            for (int i = 0; i < allMedia.Length; i++)
            {
                if (allMedia[i] is Song)
                {
                    Song theSong = allMedia[i] as Song;
                    lst[b, 0] += "Song Title: " + theSong.Title + " (" + theSong.Year + ") \n";
                    lst[b, 0] += "Album: " + theSong.Album + "  Artist: " + theSong.Artist;
                    lst[b, 1] = null;
                    b++;
                }
                if (allMedia[i] is Movie)
                {
                    Movie theMovie = allMedia[i] as Movie;
                    lst[b, 0] += "Movie Title: " + theMovie.Title + " (" + theMovie.Year + ") \n";
                    lst[b, 0] += "Director: " + theMovie.Director;
                    lst[b, 1] = theMovie.Decrypt();
                    b++;
                }
                if (allMedia[i] is Book)
                {
                    Book theBook = allMedia[i] as Book;
                    lst[b, 0] += "Book Title: " + theBook.Title + " (" + theBook.Year + ") \n";
                    lst[b, 0] += "Author: " + theBook.Author;
                    lst[b, 1] = theBook.Decrypt();
                    b++;
                }
            }
            return lst;
        }
    }
}
