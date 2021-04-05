namespace JsonEditorSharp.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Windows.Data;

    public class JsonUndefinedToJsonTokenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter is not string methodName)
            {
                return null;
            }

            MethodInfo methodInfo = value.GetType().GetMethod(methodName, Array.Empty<Type>());

            if (methodInfo == null)
            {
                return null;
            }

            object returnValue = methodInfo.Invoke(value, Array.Empty<object>());

            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
        }
    }
}
