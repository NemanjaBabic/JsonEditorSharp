namespace JsonEditorSharp.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Newtonsoft.Json.Linq;

    public class JValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is JValue jvalue)
            {
                switch (jvalue.Type)
                {
                    case JTokenType.String:
                        return jvalue.Value;
                    case JTokenType.Null:
                        return "Null";
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
        }
    }
}
