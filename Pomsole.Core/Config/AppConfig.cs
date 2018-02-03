
namespace Pomsole.Core.Config
{
    public class AppConfig
    {
        public string AlertFilePath { get; set; }
        public string DataFilePath { get; set; }

        public bool IsEmpty()
        {
            if (string.IsNullOrWhiteSpace(AlertFilePath))
                return true;
            if (string.IsNullOrWhiteSpace(DataFilePath))
                return true;
            return false;
        }
    }
}
