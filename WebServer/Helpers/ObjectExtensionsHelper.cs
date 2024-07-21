namespace WebServer.Helpers
{
    public static class ObjectExtensionsHelper
    {
        public static bool HasAnyValue(this object obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);
                if (value is int intValue && intValue != 0)
                {
                    return true;
                }
                if (value is string strValue && !string.IsNullOrEmpty(strValue))
                {
                    return true;
                }
                if (value is DateTime dateValue && dateValue != DateTime.MinValue)
                {
                    return true;
                }
                if (value is decimal decimalValue && decimalValue != 0)
                {
                    return true;
                }
                // Добавьте другие типы данных и условия по мере необходимости
            }
            return false;
        }
    }
}
