using System;

namespace Pomsole.Core.Models
{
    public class Session
    {
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public DateTime? BreakStart { get; set; }
        public DateTime? BreakEnd { get; set; }
        public int SessionLength { get; set; }
        public int BreakLength { get; set; }
        public string Task { get; set; }
        public string Category { get; set; }
    }
}
