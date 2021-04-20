namespace JsonEditorSharp.ItemTemplateSelector
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Newtonsoft.Json.Linq;

    public class JsonDataItemTemplateSelector : DataTemplateSelector
    {
        //// JToken Hierarchy:
        ////
        ////    JToken                           - abstract base class
        ////      |
        ////      | --->   JContainer            - abstract base class of JTokens that can contain other JTokens     
        ////      |            |
        ////      |            |---> JArray      - represents a JSON array(contains an ordered list of JTokens)
        ////      |            |---> JObject     - represents a JSON object (contains a collection of JProperties)
        ////      |            |---> JProperty   - represents a JSON property (a name/JToken pair inside a JObject)
        ////      | --->   JValue                - represents a primitive JSON value (string, number, boolean, null)

        /// <summary>
        ///     The JSON primitive property data type template.
        /// </summary>
        public DataTemplate JsonPrimitivePropertyTemplate { get; set; }

        /// <summary>
        ///     The JSON array data template.
        /// </summary>
        public DataTemplate JsonArrayDataTemplate { get; set; }

        /// <summary>
        ///     The JSON object data template.
        /// </summary>
        public DataTemplate JsonObjectDataTemplate { get; set; }

        /// <inheritdoc/>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }

            if (container is not FrameworkElement frameworkElement)
            {
                return null;
            }

            Type type = item.GetType();
            if (type == typeof(JProperty))
            {
                if (item is JProperty jProperty)
                {
                    return jProperty.Value.Type switch
                    {
                        JTokenType.Object => frameworkElement.FindResource("JsonObjectDataTemplate") as DataTemplate,
                        JTokenType.Array => frameworkElement.FindResource("JsonArrayDataTemplate") as DataTemplate,
                        _ => frameworkElement.FindResource("JsonPrimitivePropertyTemplate") as DataTemplate
                    };
                }
            }

            var key = new DataTemplateKey(type);
            return frameworkElement.FindResource(key) as DataTemplate;
        }
    }
}
