namespace JsonEditorSharp.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    using Newtonsoft.Json.Linq;

    public class JPropertyToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is JProperty jproperty)
            {
                switch (jproperty.Value.Type)
                {
                    case JTokenType.String:
                        return new BrushConverter().ConvertFrom("#388e3c");
                    case JTokenType.Float:
                    case JTokenType.Integer:
                        return new BrushConverter().ConvertFrom("#d32f2f");
                    case JTokenType.Boolean:
                        return new BrushConverter().ConvertFrom("#f57c00");
                    case JTokenType.Null:
                        return new BrushConverter().ConvertFrom("#1976d2");
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
