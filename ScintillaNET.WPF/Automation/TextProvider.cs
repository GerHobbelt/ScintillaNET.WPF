using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Provider;

namespace ScintillaNET.WPF.Automation
{
    internal class TextProvider : ITextProvider
    {
        private readonly ScintillaWPF editor;

        public TextProvider(ScintillaWPF editor)
        {
            this.editor = editor;
        }

        public ITextRangeProvider[] GetSelection()
        {
            return new ITextRangeProvider[]
            {
                new TextRangeProvider(this.editor, this.editor.SelectionStart, this.editor.SelectionEnd),
            };
        }

        public ITextRangeProvider[] GetVisibleRanges()
        {
            return new[] {this.DocumentRange};
        }

        public ITextRangeProvider RangeFromChild(IRawElementProviderSimple childElement)
        {
            throw new System.NotImplementedException();
        }

        public ITextRangeProvider RangeFromPoint(Point screenLocation)
        {
            var controlPosition = this.editor.PointFromScreen(screenLocation);
            var position = this.editor.CharPositionFromPoint((int) controlPosition.X, (int) controlPosition.Y);
            return new TextRangeProvider(this.editor, position, position);
        }

        public ITextRangeProvider DocumentRange => new TextRangeProvider(this.editor, 0, this.editor.TextLength);

        public SupportedTextSelection SupportedTextSelection { get; } = SupportedTextSelection.Single;
    }
}
