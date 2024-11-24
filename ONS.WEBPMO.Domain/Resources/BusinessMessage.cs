using Newtonsoft.Json;

namespace ONS.WEBPMO.Domain.Resources
{
    public class BusinessMessage
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }

        private static List<BusinessMessage> Messages;

        private static readonly string JsonFilePath = "businessMessage.json";

        static BusinessMessage()
        {
            LoadMessagesFromFile();
        }

        private static void LoadMessagesFromFile()
        {
            if (File.Exists(JsonFilePath))
            {
                var json = File.ReadAllText(JsonFilePath);
                Messages = JsonConvert.DeserializeObject<List<BusinessMessage>>(json);
            }
            else
            {
                Messages = new List<BusinessMessage>();
            }
        }

        public static BusinessMessage Get(string code)
        {
            return Messages.FirstOrDefault(m => m.Code == code);
        }
    }
}
