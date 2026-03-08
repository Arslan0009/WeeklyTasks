using System;
using System.Collections.Generic;

class User
{
    public string Username;
    public string Password;
}

class Program
{
    static void Main()
    {
        List<User> users = new List<User>();
        int choice;

        do
        {
            Console.WriteLine("1. Signup");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Enter Choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid Input");
                continue;
            }

            if (choice == 1)
            {
                User u = new User();

                Console.Write("Enter Username: ");
                u.Username = Console.ReadLine();

                Console.Write("Enter Password: ");
                u.Password = Console.ReadLine();

                users.Add(u);

                Console.WriteLine("Signup Successful");
            }

            else if (choice == 2)
            {
                Console.Write("Enter Username: ");
                string username = Console.ReadLine();

                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                bool found = false;

                foreach (User u in users)
                {
                    if (u.Username == username && u.Password == password)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                    Console.WriteLine("Login Successful");
                else
                    Console.WriteLine("Invalid Username or Password");
            }

        } while (choice != 3);
    }
}