using SQLite.Net.Interop;

namespace SQLite.Net.Platform.Silverlight
{
    public class SQLitePlatformSilverlight : ISQLitePlatform
    {
        public ISQLiteApi SQLiteApi { get; private set; }

        public IStopwatchFactory StopwatchFactory { get; private set; }

        public IReflectionService ReflectionService { get; private set; }

        public IVolatileService VolatileService { get; private set; }

        public SQLitePlatformSilverlight()
        {
            SQLiteApi = new SQLiteApiSilverlight();
            VolatileService = new VolatileServiceSilverlight();
            ReflectionService = new ReflectionServiceSilverlight();
            StopwatchFactory = new StopwatchFactorySilverlight();
        }
    }
}