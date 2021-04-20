namespace JsonEditorSharp.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Data;
    using Newtonsoft.Json.Linq;

    public class JsonArrayObjectToNestedDataConverter : IValueConverter
    {
        /// <summary>
        ///     This converter is only used by JProperty tokens whose Value is Array/Object.
        /// </summary>
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

            object invocationResult = methodInfo.Invoke(value, Array.Empty<object>());
            var jTokens = (IEnumerable<JToken>)invocationResult;

            return (jTokens ?? Array.Empty<JToken>()).First().Children();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
        }
    }
}
