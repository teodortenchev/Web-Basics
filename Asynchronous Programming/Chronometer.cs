using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronous_Programming
{
    public class Chronometer : IChronometer
    {
        private long milliseconds;

        private bool isRunning;
        public Chronometer()
        {
            Laps = new List<string>();
        }
        public string GetTime => $"{milliseconds / 60000:D2}:{milliseconds / 1000:D2}:{milliseconds % 1000:D4}";

        public List<string> Laps { get; private set; }

        public string Lap()
        {
            string lap = GetTime;
            Laps.Add(lap);
            return lap;
        }

        public void Reset()
        {
            Stop();
            milliseconds = 0;
            Laps.Clear();
        }


        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;

                Task.Run(() =>
                    {
                        while (isRunning)
                        {
                            Thread.Sleep(1);
                            milliseconds++;
                        }
                    }
                );
            }
            else
            {
                Console.WriteLine("Chronometer is already started!");
            }

        }


        public void Stop()
        {
            isRunning = false;
        }

        public string DisplayLaps()
        {

            if (Laps.Count == 0)
            {
                return "No laps were recorded";
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("..:: LAPS ::..");

            for (int i = 0; i < Laps.Count; i++)
            {
                sb.AppendLine($"{i}. {Laps[i]}");
            }


            return sb.ToString().TrimEnd();
        }

    }
}
