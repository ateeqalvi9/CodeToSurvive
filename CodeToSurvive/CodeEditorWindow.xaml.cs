using System.Windows;

namespace CodeToSurvive
{
    public partial class CodeEditorWindow : Window
    {
        public string SourceCode { get; private set; }

        public CodeEditorWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            SourceCode = CodeTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            SourceCode = string.Empty;
            DialogResult = false;
            Close();
        }
    }
}
