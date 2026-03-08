using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Calculator
    {
        int num1, num2;


        public Calculator(int a, int b)
        {
            num1 = a;
            num2 = b;
        }

        public void Add()
        {
            Console.WriteLine("Addition: " + (num1 + num2));
        }

        public void Subtract()
        {
            Console.WriteLine("Subtraction: " + (num1 - num2));
        }

        public void Multiply()
        {
            Console.WriteLine("Multiplication: " + (num1 * num2));
        }

        public void Divide()
        {
            Console.WriteLine("Division: " + (num1 / num2));
        }
    }

    class Program
    {
        static void Main()
        {
            Calculator c = new Calculator(9, 5);

            c.Add();
            c.Subtract();
            c.Multiply();
            c.Divide();
        }
    }
}