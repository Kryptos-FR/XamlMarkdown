using System.IO;
using System.Windows;

namespace Markdown.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            EditSource.Text = LoadSample();
        }

        private string LoadSample()
        {
            var subjectType = GetType();
            var subjectAssembly = subjectType.Assembly;
            using (var stream = subjectAssembly.GetManifestResourceStream(subjectType.FullName + ".md"))
            {
                if (stream == null)
                {
                    return $"Could not find sample text *{subjectType.FullName}*.md";
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

    }
}
