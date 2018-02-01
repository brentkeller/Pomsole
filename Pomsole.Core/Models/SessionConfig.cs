using System.Text;

namespace Pomsole.Core.Models
{
    public class SessionConfig
    {
        /// <summary>
        /// Length of session in minutes
        /// </summary>
        public int SessionLength { get; set; }
        /// <summary>
        /// Length of break in minutes
        /// </summary>
        public int BreakLength { get; set; }
        /// <summary>
        /// Name of task category
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Name of task being worked on
        /// </summary>
        public string Task { get; set; }
        /// <summary>
        /// Flag to silence alert sound
        /// </summary>
        public bool Quiet { get; set; }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.AppendLine($"Session: {SessionLength}");
            output.AppendLine($"Break: {BreakLength}");
            output.AppendLine($"Category: {Category}");
            output.AppendLine($"Task: {Task}");
            output.AppendLine($"Quiet: {Quiet}");
            return output.ToString();
        }
    }
}
