using System;
using System.Linq;
using AuthServer.API.Models;

namespace AuthServer.API.Database
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Books.Any() && dbContext.Authors.Any())
            {
                return;
            }

            var books = new []
            {
                new Book
                {
                    Id = Guid.Parse("e62c0ea2-cac2-4d28-8a55-bc0126d90621"),
                    DatePublished = new DateTime(1847, 10, 16),
                    Description =
                        "Fiery love, shocking twists of fate, and tragic mysteries put a lonely governess in jeopardy in JANE EYRE",
                    ISBN = "9781519133977",
                    Stock = 10,
                    Title = "Jane Eyre",
                    Price = new decimal (15.99)
                },
                new Book
                {
                    Id = Guid.Parse("92a0c786-3504-49a0-8236-9cbaf177f982"),
                    Title = "Wuthering Heights",
                    DatePublished = new DateTime().AddYears(1847).AddMonths(12),
                    ISBN = " ‎0-486-29256-8",
                    Description =
                        "Wuthering Heights is a wild, passionate story of the intense and almost demonic love between Catherine Earnshaw and Heathcliff, a foundling adopted by Catherine's father.",
                    Stock = 2,
                    Price = new decimal (11.99)
                },
                new Book
                {
                    Id = Guid.Parse("5113ab45-b8d0-44bf-aa79-000499c487ac"),
                    Title = "Pride and Prejudice",
                    DatePublished = new DateTime(1813,1,28),
                    ISBN = "9781611067095",
                    Description = "Pride and Prejudice is a humorous story of love and life among English gentility during the Georgian era",
                    Stock = 8,
                    Price = new decimal(10.99)
                },
                new Book
                {
                    Id = Guid.Parse("5113ab45-b8d0-44bf-aa79-000499c487a2"),
                    Title = "Don Quixote",
                    DatePublished = new DateTime().AddYears(1615),
                    ISBN = "9780805511963",
                    Description = "Alonso Quixana is an older gentleman who lives in La Mancha, in the Spanish countryside. He has read many of the books of chivalry and as a result, he has lost his wits, and he decides to roam the country as a knight-errant named Don Quixote de La Mancha. ",
                    Stock = 11,
                    Price = new decimal(5.99) 
                },
                new Book
                {
                    Id = Guid.Parse("ab13ab45-b8d0-44bf-aa79-000499c487a2"),
                    Title = "1984",
                    DatePublished = new DateTime(1949, 6, 8),
                    ISBN = "9781471331435",
                    Description = "The novel is set in Airstrip One, formerly Great Britain, a province of the superstate Oceania, whose residents are victims of perpetual war, omnipresent government surveillance and public manipulation",
                    Stock = 14
                },
                new Book
                {
                    Id = Guid.Parse("ab13ab45-b8d0-44bf-aa79-000499c48777"),
                    Title = "The Great Gatsby",
                    DatePublished = new DateTime(1925, 4, 10),
                    ISBN = "9781556513268",
                    Description = "The story primarily concerns the young and mysterious millionaire Jay Gatsby and his quixotic passion and obsession for the beautiful former debutante Daisy Buchanan",
                    Stock = 4,
                    Price = new decimal(12.99)
                },
                new Book
                {
                    Id = Guid.Parse("d0dc49a7-e0a3-4052-93ad-3e734d01aa42"),
                    Title = "Moby-Dick",
                    DatePublished = new DateTime(1851, 10, 18),
                    ISBN = "9781974305032",
                    Description = "The book is sailor Ishmael's narrative of the obsessive quest of Ahab, captain of the whaling ship Pequod, for revenge on Moby Dick, the white whale that on the ship's previous voyage bit off Ahab's leg at the knee",
                    Stock = 1,
                    Price = new decimal(9.99)
                },
                new Book
                {
                    Id = Guid.Parse("164fb3f3-5f28-4734-91aa-31f9bf67916a"),
                    Title = "Frankenstein",
                    DatePublished = new DateTime().AddYears(1823),
                    ISBN = "9781977841438",
                    Description = "Frankenstein follows Victor Frankenstein's triumph as he reanimates a dead body, and then his guilt for creating such a thing.",
                    Stock = 12,
                    Price = new decimal(11.99)
                },
                new Book
                {
                    Id = Guid.Parse("6279d3c3-abc1-498e-bee9-f58f4e781558"),
                    Title = "The Picture of Dorian Gray",
                    DatePublished = new DateTime(1890, 6, 20),
                    Description = "The Picture of Dorian Gray begins on a beautiful summer day in Victorian era England, where Lord Henry Wotton, an opinionated man, is observing the sensitive artist Basil Hallward painting the portrait of Dorian Gray, a handsome young man who is Basil's ultimate muse.",
                    ISBN = "9781546899457",
                    Stock = 4,
                    Price = new decimal(7.99)
                },
                new Book
                {
                    Id = Guid.Parse("e5ae13c8-8e0f-4af0-b967-d265d5debad5"),
                    Title = "Head First Android Development: A Brain-Friendly Guide",
                    DatePublished = new DateTime().AddYears(2015),
                    Description = "In Android Programming, Ryan Hodson provides a useful overview of the Android application lifecycle. Topics ranging from creating a UI to adding widgets and embedding fragments are covered, and he provides plenty of links to Android documentation along the way",
                    ISBN = "1548494658",
                    Stock = 12,
                    Price = new decimal(13.99)
                }
            };

            var authors = new []
            {
                new Author
                {
                    Id = Guid.Parse("695e5328-2539-4917-8a08-f5beacfc8dbe"),
                    FirstName = "Charlotte",
                    LastName = "Brontë"
                },
                new Author
                {
                    Id = Guid.Parse("45b1c1e1-394e-4a9a-9d2a-998bf7844925"),
                    FirstName = "Emily",
                    LastName = "Brontë"
                },
                new Author
                {
                    Id = Guid.Parse("ea1aed23-553d-4f8c-a186-fce35922e60d"),
                    FirstName = "Jane",
                    LastName = "Austen"
                },
                new Author
                {
                    Id = Guid.Parse("ea1aed23-553d-4f8c-a186-fce35922e601"),
                    FirstName = "Miguel",
                    LastName = "de Cervantes"
                },
                new Author
                {
                    //George Orwell
                    Id = Guid.Parse("ab1aed23-553d-4f8c-a186-fce35922e601"),
                    FirstName = "George",
                    LastName = "Orwell"
                },
                new Author
                {
                    Id = Guid.Parse("cd1aed23-553d-4f8c-a186-fce35922e601"),
                    FirstName = "F. Scott",
                    LastName = "Fitzgerald"
                },
                new Author
                {
                    //Herman Melville
                    Id = Guid.Parse("6a4fd0ec-d82c-4a54-a68f-f0b8078fdd41"),
                    FirstName = "Herman",
                    LastName = "Melville"
                },
                new Author
                {
                    Id = Guid.Parse("95d1e5ea-bb2e-4b13-9bbd-c67fe5ef3030"),
                    FirstName = "Mary",
                    LastName = "Shelley"
                },
                new Author
                {
                    Id = Guid.Parse("f133ebe4-440c-4a53-9c2b-e8d8b9e73bd5"),
                    FirstName =  "Oscar",
                    LastName = "Wilde"
                },
                new Author
                {
                    Id = Guid.Parse("1899d8d7-4941-4aa0-9be7-c9c7b90af3c5"),
                    FirstName = "Anthony J.F.",
                    LastName =  "Griffiths"
                },
                new Author
                {
                    Id = Guid.Parse("05b3366b-a4a4-4f1e-a5b9-57716e2c45a8"),
                    FirstName = "David",
                    LastName =  "Griffiths"
                }
            };

            var bookAuthors = new[]
            {
                new BookAuthor
                {
                    BookId = Guid.Parse("e62c0ea2-cac2-4d28-8a55-bc0126d90621"),
                    AuthorId = Guid.Parse("695e5328-2539-4917-8a08-f5beacfc8dbe")
                },
                new BookAuthor
                {
                    BookId = Guid.Parse("92a0c786-3504-49a0-8236-9cbaf177f982"),
                    AuthorId = Guid.Parse("45b1c1e1-394e-4a9a-9d2a-998bf7844925")
                },
                new BookAuthor
                {
                    BookId = Guid.Parse("5113ab45-b8d0-44bf-aa79-000499c487ac"),
                    AuthorId = Guid.Parse("ea1aed23-553d-4f8c-a186-fce35922e60d")
                },
                new BookAuthor
                {
                    BookId = Guid.Parse("5113ab45-b8d0-44bf-aa79-000499c487a2"),
                    AuthorId = Guid.Parse("ea1aed23-553d-4f8c-a186-fce35922e601")
                },
                new BookAuthor
                {
                    BookId = Guid.Parse("ab13ab45-b8d0-44bf-aa79-000499c487a2"),
                    AuthorId = Guid.Parse("ab1aed23-553d-4f8c-a186-fce35922e601")
                },
                new BookAuthor
                {
                    BookId = Guid.Parse("ab13ab45-b8d0-44bf-aa79-000499c48777"),
                    AuthorId = Guid.Parse("cd1aed23-553d-4f8c-a186-fce35922e601")
                },
                new BookAuthor
                {
                    BookId = Guid.Parse("d0dc49a7-e0a3-4052-93ad-3e734d01aa42"),
                    AuthorId = Guid.Parse("6a4fd0ec-d82c-4a54-a68f-f0b8078fdd41")
                },
                new BookAuthor
                {
                    BookId = Guid.Parse("164fb3f3-5f28-4734-91aa-31f9bf67916a"),
                    AuthorId = Guid.Parse("95d1e5ea-bb2e-4b13-9bbd-c67fe5ef3030")
                },
                new BookAuthor
                {
                    BookId = Guid.Parse("6279d3c3-abc1-498e-bee9-f58f4e781558"),
                    AuthorId = Guid.Parse("f133ebe4-440c-4a53-9c2b-e8d8b9e73bd5")
                },
                new BookAuthor
                {
                    BookId = Guid.Parse("e5ae13c8-8e0f-4af0-b967-d265d5debad5"),
                    AuthorId = Guid.Parse("1899d8d7-4941-4aa0-9be7-c9c7b90af3c5")
                },
                new BookAuthor
                {
                    BookId = Guid.Parse("e5ae13c8-8e0f-4af0-b967-d265d5debad5"),
                    AuthorId = Guid.Parse("05b3366b-a4a4-4f1e-a5b9-57716e2c45a8")
                }
            };
            
            dbContext.Authors.AddRange(authors);
            dbContext.SaveChanges();
            dbContext.Books.AddRange(books);
            dbContext.SaveChanges();
            dbContext.BookAuthors.AddRange(bookAuthors);
            dbContext.SaveChanges();
        }
    }
}