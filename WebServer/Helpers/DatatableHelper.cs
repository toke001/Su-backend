namespace WebServer.Helpers
{
    public static class DatatableHelper
    {
        public static Dictionary<string, string> ConvertToDictionaryString(Dictionary<string, object> dict)
        {
            return dict.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
        }
    }
}
