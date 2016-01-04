using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Markdown.Xaml.ValueConverters
{
    [ValueConversion(typeof(string), typeof(FlowDocument))]
    public class TextToMarkdownFlowDocumentConverter : MarkupExtension, IValueConverter
    {
        private static TextToMarkdownFlowDocumentConverter _valueConverterInstance;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _valueConverterInstance ?? (_valueConverterInstance = new TextToMarkdownFlowDocumentConverter());
        }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && !(parameter is XamlMarkdown))
            {
                throw new ArgumentException($"The parameter of this converter must be an instance of the {nameof(XamlMarkdown)} class.");
            }

            if (value == null)
                return null;

            var engine = (XamlMarkdown)parameter ?? defaultMarkdown.Value;
            return engine.Transform(value.ToString());
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported with this ValueConverter.");
        }

        private readonly Lazy<XamlMarkdown> defaultMarkdown = new Lazy<XamlMarkdown>(() => new XamlMarkdown());
    }
}
