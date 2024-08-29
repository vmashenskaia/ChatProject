namespace TestChat
{
    public interface IDataParser
    {
        public string Serialize<T>(T data) where T : class;
        public T Deserialize<T>(string data) where T : class;
    }
}