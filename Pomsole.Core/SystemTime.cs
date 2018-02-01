using System;

namespace Pomsole.Core
{
    public static class SystemTime
    {
        public static Func<DateTime> LocalTimeProvider = () => DateTime.Now;
        public static Func<DateTime> UtcTimeProvider = () => DateTime.UtcNow;

        public static DateTime Now { get { return UtcTimeProvider(); } }
        public static DateTime LocalNow { get { return LocalTimeProvider(); } }
    }
}
