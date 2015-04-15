using System;

namespace SQLite.Net.Platform.Silverlight
{
    public class Stopwatch
    {
        private bool isRunning;
        private long startTimeStamp;
        private long elapsed;

        public long ElapsedMilliseconds
        {
            get
            {
                return this.GetElapsedDateTimeTicks() / 10000L;
            }
        }

        public TimeSpan? Elapsed
        {
            get
            {
                return new TimeSpan?(new TimeSpan(this.GetElapsedDateTimeTicks()));
            }
        }

        public static Stopwatch StartNew()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            return stopwatch;
        }

        public static long GetTimestamp()
        {
            return DateTime.Now.Ticks;
        }

        public void Start()
        {
            if (this.isRunning)
                return;
            this.startTimeStamp = Stopwatch.GetTimestamp();
            this.isRunning = true;
        }

        public void Stop()
        {
            if (!this.isRunning)
                return;
            this.elapsed += Stopwatch.GetTimestamp() - this.startTimeStamp;
            this.isRunning = false;
            if (this.elapsed >= 0L)
                return;
            this.elapsed = 0L;
        }

        private long GetElapsedDateTimeTicks()
        {
            long num1 = this.elapsed;
            if (this.isRunning)
            {
                long num2 = Stopwatch.GetTimestamp() - this.startTimeStamp;
                num1 += num2;
            }
            return num1;
        }

        public void Reset()
        {
            this.elapsed = 0L;
        }
    }
}