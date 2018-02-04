using Newtonsoft.Json;

namespace Pomsole.Core.Config
{
    public class AppConfig
    {
        [JsonProperty(PropertyName = "alertFilePath")]
        public string AlertFilePath { get; set; }
        [JsonProperty(PropertyName = "dataFilePath")]
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
