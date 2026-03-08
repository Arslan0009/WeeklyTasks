using System;
using System.Collections.Generic;

class ATM
{
    double balance;
    List<string> history = new List<string>();

    public ATM(double initialBalance)
    {
        balance = initialBalance;
    }

    public void Deposit(double amount)
    {
        balance += amount;
        history.Add("Deposited: " + amount);
    }

    public void Withdraw(double amount)
    {
        if (amount <= balance)
        {
            balance -= amount;
            history.Add("Withdrawn: " + amount);
        }
        else
        {
            Console.WriteLine("Insufficient Balance");
        }
    }

    public void CheckBalance()
    {
        Console.WriteLine("Balance: " + balance);
    }

    public void ShowHistory()
    {
        foreach (string h in history)
        {
            Console.WriteLine(h);
        }
    }
}

class Program
{
    static void Main()
    {
        ATM atm = new ATM(1000);

        atm.Deposit(500);
        atm.Withdraw(200);
        atm.CheckBalance();
        atm.ShowHistory();
    }
}