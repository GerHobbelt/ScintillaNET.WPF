using System.Windows.Automation.Provider;

namespace ScintillaNET.WPF.Automation
{
    internal class ValueProvider : IValueProvider
    {
        private readonly ScintillaWPF editor;

        public ValueProvider(ScintillaWPF editor)
        {
            this.editor = editor;
        }

        public void SetValue(string value)
        {
            this.Value = value;
        }

        public string Value
        {
            get => this.editor.Text;
            set => this.editor.Text = value;
        }

        public bool IsReadOnly => this.editor.ReadOnly;
    }
}
