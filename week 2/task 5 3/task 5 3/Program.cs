using System;
using System.Collections.Generic;

class Book
{
    public string Title;
    public string Author;
    public string Genre;
    public float Rating;

    // Copy constructor
    public Book(Book b)
    {
        Title = b.Title;
        Author = b.Author;
        Genre = b.Genre;
        Rating = b.Rating;
    }

    public Book() { }
}

class Program
{
    static void Main()
    {
        List<Book> books = new List<Book>();
        List<Book> recommended = new List<Book>();

        for (int i = 0; i < 5; i++)
        {
            Book b = new Book();

            Console.Write("Title: ");
            b.Title = Console.ReadLine();

            Console.Write("Author: ");
            b.Author = Console.ReadLine();

            Console.Write("Genre: ");
            b.Genre = Console.ReadLine();

            Console.Write("Rating: ");
            b.Rating = float.Parse(Console.ReadLine());

            books.Add(b);
        }

        Console.WriteLine("Top Rated Books:");

        foreach (Book b in books)
        {
            if (b.Rating > 4.5)
                Console.WriteLine(b.Title);
        }

        Console.Write("Search Genre: ");
        string g = Console.ReadLine();

        foreach (Book b in books)
        {
            if (b.Genre == g)
                Console.WriteLine(b.Title);
        }

        Book top = books[0];

        foreach (Book b in books)
        {
            if (b.Rating > top.Rating)
                top = b;
        }

        recommended.Add(new Book(top));

        Console.WriteLine("Recommended: " + top.Title);
    }
}