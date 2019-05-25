using System;

namespace Asynchronous_Programming
{
    public class Program
    {
        static void Main(string[] args)
        {
            Chronometer chronometer = new Chronometer();

            string command = Console.ReadLine();

            while (command != "exit")
            {

                switch (command)
                {
                    case "start":
                        chronometer.Start();
                        break;
                    case "stop":
                        chronometer.Stop();
                        break;
                    case "lap":
                        Console.WriteLine(chronometer.Lap());
                        break;
                    case "laps":
                        Console.WriteLine(chronometer.DisplayLaps());
                        break;
                    case "time":
                        Console.WriteLine(chronometer.GetTime);
                        break;

                    case "reset":
                        chronometer.Reset();
                        break;
                }

                command = Console.ReadLine();
            }
        }


    }
}

