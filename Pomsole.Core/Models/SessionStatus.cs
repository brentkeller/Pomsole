using System;

namespace Pomsole.Core.Models
{
    public enum SessionState
    {
        Session,
        Break,
        Completed
    }

    public class SessionStatus
    {
        public SessionState State { get; set; }
        public TimeSpan TimeRemaining { get; set; }
        public Session Session { get; set; }
        public bool PlayAlert { get; set; } = false;
    }
}
