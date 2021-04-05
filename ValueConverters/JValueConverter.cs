namespace JsonEditorSharp.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;
    using Newtonsoft.Json.Linq;

    public class JValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int indexOf = -1;

            if (value is JValue jvalue)
            {
                if (parameter != null && (string) parameter == "true")
                {
                    if (jvalue.Parent != null && jvalue.Parent.GetType().Name == "JArray")
                    {
                        // Searches for the specified JToken and returns the index of its first occurrence.
                        indexOf = Array.IndexOf(jvalue.Parent.Select(x => x.Path == jvalue.Path).ToArray(), true);
                    }
                }

                if (jvalue.Type == JTokenType.Null)
                {
                    return indexOf == -1 ? "Null" : indexOf < 10 ? $"  {indexOf} : " : $"{indexOf} : ";
                }

                return indexOf == -1 ? jvalue.Value : indexOf < 10 ? $"  {indexOf} : " : $"{indexOf} : ";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
        }
    }
}
