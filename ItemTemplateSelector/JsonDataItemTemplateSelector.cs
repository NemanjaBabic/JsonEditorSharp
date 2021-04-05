namespace JsonEditorSharp.ItemTemplateSelector
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Newtonsoft.Json.Linq;

    public class JsonDataItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate JsonPrimitivePropertyTemplate { get; set; }

        public DataTemplate JsonArrayPropertyTemplate { get; set; }

        public DataTemplate JsonObjectPropertyTemplate { get; set; }

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
                    return jProperty.Value.Type switch
                    {
                        JTokenType.Object => frameworkElement.FindResource("JsonObjectPropertyTemplate") as DataTemplate,
                        JTokenType.Array => frameworkElement.FindResource("JsonArrayPropertyTemplate") as DataTemplate,
                        _ => frameworkElement.FindResource("JsonPrimitivePropertyTemplate") as DataTemplate
                    };
            }

            var key = new DataTemplateKey(type);
            return frameworkElement.FindResource(key) as DataTemplate;
        }
    }
}
