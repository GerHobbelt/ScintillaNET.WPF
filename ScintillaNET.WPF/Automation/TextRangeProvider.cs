using System;
using System.Windows.Automation.Provider;
using System.Windows.Automation.Text;

namespace ScintillaNET.WPF.Automation
{
    internal class TextRangeProvider : ITextRangeProvider
    {
        private readonly ScintillaWPF editor;

        public int Start { get; private set; }

        public int End { get; private set; }

        public TextRangeProvider(ScintillaWPF owner, int start, int end)
        {
            this.editor = owner;
            this.Start = start;
            this.End = end;
        }

        public ITextRangeProvider Clone()
        {
            return new TextRangeProvider(this.editor, this.Start, this.End);
        }

        public bool Compare(ITextRangeProvider range)
        {
            if (!(range is TextRangeProvider provider))
            {
                return false;
            }

            return this.Start == provider.Start && this.End == provider.End;
        }

        public int CompareEndpoints(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange, TextPatternRangeEndpoint targetEndpoint)
        {
            var position = endpoint == TextPatternRangeEndpoint.Start ? this.Start : this.End;
            var targetPosition = targetEndpoint == TextPatternRangeEndpoint.Start ? this.Start : this.End;
            return position - targetPosition;
        }

        public void ExpandToEnclosingUnit(TextUnit unit)
        {
            throw new System.NotImplementedException();
        }

        public ITextRangeProvider FindAttribute(int attribute, object value, bool backward)
        {
            throw new System.NotImplementedException();
        }

        public ITextRangeProvider FindText(string text, bool backward, bool ignoreCase)
        {
            throw new System.NotImplementedException();
        }

        public object GetAttributeValue(int attribute)
        {
            throw new System.NotImplementedException();
        }

        public double[] GetBoundingRectangles()
        {
            throw new System.NotImplementedException();
        }

        public IRawElementProviderSimple GetEnclosingElement()
        {
            throw new System.NotImplementedException();
        }

        public string GetText(int maxLength)
        {
            var length = this.End - this.Start;
            if (maxLength >= 0)
            {
                length = Math.Min(maxLength, length);
            }

            return this.editor.GetTextRange(this.Start, length);
        }

        public int Move(TextUnit unit, int count)
        {
            if (unit == TextUnit.Character)
            {
                var previousStart = this.Start;
                var degenerate = this.Start == this.End;
                this.Start = Math.Max(0, Math.Min(this.editor.TextLength - 1, this.Start + count));
                this.End = this.Start + (degenerate ? 0 : 1);
                return this.Start - previousStart;
            }

            // TODO
            throw new System.NotImplementedException();
        }

        public int MoveEndpointByUnit(TextPatternRangeEndpoint endpoint, TextUnit unit, int count)
        {
            if (unit == TextUnit.Character)
            {
                if (endpoint == TextPatternRangeEndpoint.Start)
                {
                    var previous = this.Start;
                    this.Start = Math.Max(0, Math.Min(this.End, this.Start + count));
                    return this.Start - previous;
                }
                else
                {
                    var previous = this.End;
                    this.End = Math.Max(this.Start, Math.Min(this.editor.TextLength - 1, this.End + count));
                    return this.End - previous;
                }
            }

            // TODO
            throw new System.NotImplementedException();
        }

        public void MoveEndpointByRange(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange, TextPatternRangeEndpoint targetEndpoint)
        {
            // TODO
            throw new System.NotImplementedException();
        }

        public void Select()
        {
            this.editor.SetSelection(this.End, this.Start);
        }

        public void AddToSelection()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveFromSelection()
        {
            throw new System.NotImplementedException();
        }

        public void ScrollIntoView(bool alignToTop)
        {
            throw new System.NotImplementedException();
        }

        public IRawElementProviderSimple[] GetChildren()
        {
            throw new System.NotImplementedException();
        }
    }
}
