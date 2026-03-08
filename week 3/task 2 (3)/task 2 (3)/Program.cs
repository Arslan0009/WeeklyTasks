using System;
using System.Collections.Generic;

class Product
{
    public string Name;
    public double Price;
    public int Stock;
    public double TaxRate;
}

class Program
{
    static void Main()
    {
        Console.WriteLine("--- STORE SYSTEM STARTED ---");

        List<Product> products = new List<Product>();

        for (int i = 0; i < 5; i++)
        {
            Product p = new Product();

            Console.Write("Enter Product Name: ");
            p.Name = Console.ReadLine();

            Console.Write("Enter Price: ");
            p.Price = double.Parse(Console.ReadLine());

            Console.Write("Enter Stock: ");
            p.Stock = int.Parse(Console.ReadLine());

            Console.Write("Enter Tax Rate: ");
            p.TaxRate = double.Parse(Console.ReadLine());

            products.Add(p);
            Console.WriteLine();
        }

        double totalTax = 0;
        Product mostExpensive = products[0];

        Console.WriteLine("Low Stock Products:");

        foreach (Product p in products)
        {
            if (p.Stock > 0)
                totalTax += p.Price * p.TaxRate;

            if (p.Stock > 0 && p.Stock < 10)
                Console.WriteLine(p.Name + " (Stock: " + p.Stock + ")");

            if (p.Price > mostExpensive.Price)
                mostExpensive = p;
        }

        Console.WriteLine();
        Console.WriteLine("Total Store Tax: " + totalTax);

        Console.WriteLine();
        Console.WriteLine("Most Expensive Product:");
        Console.WriteLine(mostExpensive.Name + " → Price: " + mostExpensive.Price);

        Console.WriteLine("--- STORE REPORT GENERATED ---");
    }
}