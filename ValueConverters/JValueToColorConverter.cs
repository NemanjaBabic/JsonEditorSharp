﻿namespace JsonEditorSharp.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    using Newtonsoft.Json.Linq;

    public class JValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is JValue jValue)
            {
                switch (jValue.Type)
                {
                    case JTokenType.String:
                        return new BrushConverter().ConvertFrom("#4caf50");
                    case JTokenType.Float:
                    case JTokenType.Integer:
                        return new BrushConverter().ConvertFrom("#f44336");
                    case JTokenType.Boolean:
                        return new BrushConverter().ConvertFrom("#ff9800");
                    case JTokenType.Null:
                        return new BrushConverter().ConvertFrom("#2196f3");
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
