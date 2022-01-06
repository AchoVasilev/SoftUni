namespace BookShop
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Data;
    using Initializer;
    using System.Text;
    using BookShop.Models.Enums;
    using System.Globalization;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            //int input = int.Parse(Console.ReadLine());
            //string input = Console.ReadLine();

            //string result = GetMostRecentBooks(db);
            //int result = CountBooks(db, input);

            Console.WriteLine(RemoveBooks(db));

            //IncreasePrices(db);
        }

        //Problem 2

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {

            List<string> books = context.Books
                .AsEnumerable()
                .Where(x => x.AgeRestriction.ToString().ToLower() == command.ToLower())
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToList();

            return String.Join(Environment.NewLine, books);
        }

        //Problem 3

        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .Select(b => new
                {
                    BookId = b.BookId,
                    Titles = b.Title
                })
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Titles);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 4

        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    Title = b.Title,
                    Price = b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 5

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => new
                {
                    b.BookId,
                    b.Title
                })
                .OrderBy(b => b.BookId)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 6
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.ToLower())
                .ToArray();

            List<string> bookTitles = new List<string>();

            foreach (var category in categories)
            {
                List<string> currentBookCategories = context.Books
                    .Where(b => b.BookCategories
                                    .Any(bc => bc.Category.Name.ToLower() == category))
                    .Select(b => b.Title)
                    .ToList();

                bookTitles.AddRange(currentBookCategories);
            }

            bookTitles = bookTitles.OrderBy(bt => bt)
                .ToList();

            return String.Join(Environment.NewLine, bookTitles);
        }

        //Problem 7
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                .Select(b => new
                {
                    Title = b.Title,
                    EditionType = b.EditionType,
                    Price = b.Price,
                    ReleaseDate = b.ReleaseDate
                })
                .OrderByDescending(b => b.ReleaseDate)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 8
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName
                })
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FirstName} {author.LastName}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 9
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            List<string> books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            return String.Join(Environment.NewLine, books);
        }

        //Problem 10
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            input = input.ToLower();

            var books = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input))
                .Select(b => new
                {
                    b.Title,
                    b.Author.FirstName,
                    b.Author.LastName,
                    b.BookId
                })
                .OrderBy(b => b.BookId)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.FirstName} {book.LastName})");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 11
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int books = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return books;
        }

        //Problem 12
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var bookCopies = context.Authors
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    Copies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.Copies)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var authorCopies in bookCopies)
            {
                sb.AppendLine($"{authorCopies.FirstName} {authorCopies.LastName} - {authorCopies.Copies}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 13
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categoryProfits = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalProfit = c.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
                })
                .OrderByDescending(b => b.TotalProfit)
                .ThenBy(b => b.CategoryName)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var cp in categoryProfits)
            {
                sb.AppendLine($"{cp.CategoryName} ${cp.TotalProfit:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 14
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categoryBooks = context.Categories
                .Select(cb => new
                {
                    CategoryName = cb.Name,
                    Books = cb.CategoryBooks
                                .Select(b => new
                                {
                                    b.Book.Title,
                                    b.Book.ReleaseDate.Value
                                })
                                .OrderByDescending(b => b.Value)
                                .Take(3)
                                .ToList()
                })
                .OrderBy(x => x.CategoryName)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var cb in categoryBooks)
            {
                sb.AppendLine($"--{cb.CategoryName}");

                foreach (var books in cb.Books)
                {
                    sb.AppendLine($"{books.Title} ({books.Value.Year})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 15
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(d => d.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        //Problem 16
        public static int RemoveBooks(BookShopContext context)
        {
            var removedBooks = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            context.Books.RemoveRange(removedBooks);

            int removedBooksCount = context.SaveChanges();

            return removedBooks.Count;
        }
    }
}
