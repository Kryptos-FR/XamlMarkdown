using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Markdown.Xaml
{
    public class TextToMarkdownFlowDocumentConverter : DependencyObject, IValueConverter
    {
        public XamlMarkdown XamlMarkdown
        {
            get { return (XamlMarkdown)GetValue(XamlMarkdownProperty); }
            set { SetValue(XamlMarkdownProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Markdown.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XamlMarkdownProperty =
            DependencyProperty.Register("XamlMarkdown", typeof(XamlMarkdown), typeof(TextToMarkdownFlowDocumentConverter), new PropertyMetadata(null));

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var text = (string)value;

            var engine = XamlMarkdown ?? defaultMarkdown.Value;

            return engine.Transform(text);
        }

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private readonly Lazy<XamlMarkdown> defaultMarkdown = new Lazy<XamlMarkdown>(() => new XamlMarkdown());
    }
}
