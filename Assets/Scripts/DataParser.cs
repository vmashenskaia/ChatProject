using Newtonsoft.Json;

namespace TestChat
{
    public class DataParser: IDataParser
    {
        public string Serialize<T>(T data) where T: class// в джсон(Т)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T Deserialize<T>(string data) where T : class// из джсон(Т)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}