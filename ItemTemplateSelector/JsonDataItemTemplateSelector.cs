namespace JsonEditorSharp.ItemTemplateSelector
{
    using System.Windows;
    using System.Windows.Controls;
    using Newtonsoft.Json.Linq;

    public class JsonDataItemTemplateSelector : DataTemplateSelector
    {
        ////  Provided that a JSON  is "officially" valid, it will only contain the following types when parsing the file: JArray, JObject, JProperty, JValue.
        ////
        ////        JToken Hierarchy:
        ////
        ////           JToken                           - abstract base class
        ////             |
        ////             | --->   JContainer            - abstract base class of JTokens that can contain other JTokens     
        ////             |            |
        ////             |            |---> JArray      - represents a JSON array(contains an ordered list of JTokens)
        ////             |            |---> JObject     - represents a JSON object (contains a collection of JProperties)
        ////             |            |---> JProperty   - represents a JSON property (a name/JToken pair inside a JObject)
        ////             | --->   JValue                - represents a primitive JSON value (string, number, boolean, null)

        /// <summary>
        ///     The JSON primitive property data template (string, number, boolean, null).
        /// </summary>
        public DataTemplate JsonPrimitivePropertyDataTemplate { get; set; }

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

            // JProperty management.
            if (item is JProperty jProperty)
            {
                // Get the JProperty value type.
                return jProperty.Value.Type switch
                {
                    // JObject.
                    JTokenType.Object => frameworkElement.FindResource("JsonObjectDataTemplate") as DataTemplate,
                    // JArray.
                    JTokenType.Array => frameworkElement.FindResource("JsonArrayDataTemplate") as DataTemplate,
                    // JProperty or JValue.
                    _ => frameworkElement.FindResource("JsonPrimitivePropertyDataTemplate") as DataTemplate
                };
            }

            // JArray, JObject and JValue management.
            var key = new DataTemplateKey(item.GetType());
            return frameworkElement.FindResource(key) as DataTemplate;
        }
    }
}
