using SQLite.Net.Interop;

namespace SQLite.Net.Platform.Silverlight
{
    public class StopwatchFactorySilverlight : IStopwatchFactory
    {
        public IStopwatch Create()
        {
            return new StopwatchSilverlight();
        }

        private class StopwatchSilverlight : IStopwatch
        {
            private readonly Stopwatch _stopWatch;

            public StopwatchSilverlight()
            {
                _stopWatch = new Stopwatch();
            }

            public void Stop()
            {
                _stopWatch.Stop();
            }

            public void Reset()
            {
                _stopWatch.Reset();
            }

            public void Start()
            {
                _stopWatch.Start();
            }

            public long ElapsedMilliseconds
            {
                get { return _stopWatch.ElapsedMilliseconds; }
            }
        }
    }
}