namespace JsonEditorSharp.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;
    using Newtonsoft.Json.Linq;

    public class JsonDataToItemsCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not JToken jToken)
            {
                throw new Exception("Wrong type for this converter");
            }

            switch (jToken.Type)
            {
                case JTokenType.Array:
                    return $"[{jToken.Children().Count()}]";
                case JTokenType.Object:
                    if (jToken.Parent == null)
                    {
                        return $"object {{{jToken.Children().Count()}}}";
                    }
                    else
                    {
                        // Searches for the specified JToken and returns the index of its first occurrence.
                        int indexOf = Array.IndexOf(jToken.Parent.Select(x => x.Path == jToken.Path).ToArray(), true);
                        return $"{indexOf}  {{{jToken.Children().Count()}}}";
                    }
                case JTokenType.Property:
                    return ((JProperty)jToken).Value.Type switch
                    {
                        JTokenType.Object => $"{{{jToken.Children().FirstOrDefault().Children().Count()}}}",
                        _ => $"[{jToken.Children().FirstOrDefault().Children().Count()}]",
                    };
                default:
                    throw new Exception("Type should be JProperty or JArray");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
        }
    }
}
