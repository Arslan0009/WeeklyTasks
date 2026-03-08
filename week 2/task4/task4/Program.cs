using System;
using System.Collections.Generic;

class Student
{
    public string Name;
    public int Marks;
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>();
        int choice;

        do
        {
            Console.WriteLine("1.Add Student");
            Console.WriteLine("2.Show Students");
            Console.WriteLine("3.Calculate Average");
            Console.WriteLine("4.Top Student");
            Console.WriteLine("5.Exit");

            Console.Write("Enter Choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Enter a number.");
                continue;
            }

            if (choice == 1)
            {
                Student s = new Student();

                Console.Write("Enter Name: ");
                s.Name = Console.ReadLine();

                Console.Write("Enter Marks: ");

                if (!int.TryParse(Console.ReadLine(), out s.Marks))
                {
                    Console.WriteLine("Invalid marks.");
                    continue;
                }

                students.Add(s);
            }

            else if (choice == 2)
            {
                foreach (Student s in students)
                {
                    Console.WriteLine(s.Name + " " + s.Marks);
                }
            }

            else if (choice == 3)
            {
                if (students.Count == 0)
                {
                    Console.WriteLine("No students available.");
                    continue;
                }

                int sum = 0;
                foreach (Student s in students)
                {
                    sum += s.Marks;
                }

                Console.WriteLine("Average: " + sum / students.Count);
            }

            else if (choice == 4)
            {
                if (students.Count == 0)
                {
                    Console.WriteLine("No students available.");
                    continue;
                }

                Student top = students[0];

                foreach (Student s in students)
                {
                    if (s.Marks > top.Marks)
                        top = s;
                }

                Console.WriteLine("Top Student: " + top.Name);
            }

        } while (choice != 5);
    }
}