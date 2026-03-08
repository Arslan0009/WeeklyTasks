using System;
using System.Collections.Generic;

class Product
{
    public int ID;
    public string Name;
    public double Price;
    public string Category;
    public string Brand;
    public string Country;
}

class Program
{
    static void Main()
    {
        List<Product> products = new List<Product>();

        Product p = new Product();

        p.ID = 1;
        p.Name = "Laptop";
        p.Price = 800;
        p.Category = "Electronics";
        p.Brand = "Dell";
        p.Country = "USA";

        products.Add(p);

        double total = 0;

        foreach (Product item in products)
        {
            Console.WriteLine(item.ID + " " + item.Name + " " + item.Price);
            total += item.Price;
        }

        Console.WriteLine("Total Store Worth: " + total);
    }
}