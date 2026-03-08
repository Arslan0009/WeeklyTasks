using System;

class ClockType
{
    public int Hours;
    public int Minutes;
    public int Seconds;

    public int ElapsedSeconds()
    {
        return Hours * 3600 + Minutes * 60 + Seconds;
    }

    public int RemainingSeconds()
    {
        return 86400 - ElapsedSeconds();
    }

    public void Display()
    {
        Console.WriteLine(Hours.ToString("00") + ":" +
                          Minutes.ToString("00") + ":" +
                          Seconds.ToString("00"));
    }
}

class Program
{
    static void Main()
    {
        ClockType c1 = new ClockType { Hours = 10, Minutes = 30, Seconds = 20 };
        ClockType c2 = new ClockType { Hours = 15, Minutes = 10, Seconds = 5 };
        ClockType c3 = new ClockType { Hours = 20, Minutes = 45, Seconds = 50 };

        c1.Display();
        Console.WriteLine("Elapsed: " + c1.ElapsedSeconds());
        Console.WriteLine("Remaining: " + c1.RemainingSeconds());

        int diff = Math.Abs(c1.ElapsedSeconds() - c2.ElapsedSeconds());
        Console.WriteLine("Difference: " + diff + " seconds");
    }
}