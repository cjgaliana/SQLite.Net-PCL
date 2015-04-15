using SQLite.Net.Interop;
using System.Threading;

namespace SQLite.Net.Platform.Silverlight
{
    public class VolatileServiceSilverlight : IVolatileService
    {
        public void Write(ref int transactionDepth, int depth)
        {
            //Thread.VolatileWrite(ref transactionDepth, depth);

            Interlocked.Increment(ref transactionDepth);
        }
    }
}