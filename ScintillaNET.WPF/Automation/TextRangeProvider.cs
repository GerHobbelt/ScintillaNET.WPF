using System;
using System.Windows.Automation.Provider;
using System.Windows.Automation.Text;

namespace ScintillaNET.WPF.Automation
{
    internal class TextRangeProvider : ITextRangeProvider
    {
        private readonly ScintillaWPF editor;

        public int Position { get; private set; }

        public int EndPosition { get; private set; }

        public TextRangeProvider(ScintillaWPF owner, int position, int endPosition)
        {
            this.editor = owner;
            this.Position = position;
            this.EndPosition = endPosition;
        }

        public ITextRangeProvider Clone()
        {
            return new TextRangeProvider(this.editor, this.Position, this.EndPosition);
        }

        public bool Compare(ITextRangeProvider range)
        {
            if (!(range is TextRangeProvider provider))
            {
                return false;
            }

            return this.Position == provider.Position && this.EndPosition == provider.EndPosition;
        }

        public int CompareEndpoints(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange, TextPatternRangeEndpoint targetEndpoint)
        {
            if (!(targetRange is TextRangeProvider provider))
            {
                return 0;
            }

            var position = endpoint == TextPatternRangeEndpoint.Start ? this.Position : this.EndPosition;
            var targetPosition = targetEndpoint == TextPatternRangeEndpoint.Start ? provider.Position : provider.EndPosition;
            return position - targetPosition;
        }

        public void ExpandToEnclosingUnit(TextUnit unit)
        {
            switch (unit)
            {
                case TextUnit.Character:
                    this.EndPosition = this.Position + 1;
                    break;
                case TextUnit.Line:
                {
                    var line = this.editor.LineFromPosition(this.Position);
                    this.Position = this.editor.Lines[line].Position;
                    this.EndPosition = this.editor.Lines[line].EndPosition - 1;
                    break;
                }
                default:
                    throw new NotImplementedException();
            }
        }

        public ITextRangeProvider FindAttribute(int attribute, object value, bool backward)
        {
            throw new NotImplementedException();
        }

        public ITextRangeProvider FindText(string text, bool backward, bool ignoreCase)
        {
            throw new NotImplementedException();
        }

        public object GetAttributeValue(int attribute)
        {
            throw new NotImplementedException();
        }

        public double[] GetBoundingRectangles()
        {
            throw new NotImplementedException();
        }

        public IRawElementProviderSimple GetEnclosingElement()
        {
            throw new NotImplementedException();
        }

        public string GetText(int maxLength)
        {
            var length = this.EndPosition - this.Position;
            if (maxLength >= 0)
            {
                length = Math.Min(maxLength, length);
            }

            return this.editor.GetTextRange(this.Position, length);
        }

        public int Move(TextUnit unit, int count)
        {
            switch (unit)
            {
                case TextUnit.Character:
                {
                    var previousStart = this.Position;
                    this.Position = Math.Max(0, Math.Min(this.editor.TextLength - 1, this.Position + count));
                    this.EndPosition = this.Position + 1;
                    return this.Position - previousStart;
                }
                case TextUnit.Line:
                {
                    var previous = this.editor.LineFromPosition(this.Position);
                    var newIndex = Math.Max(0, Math.Min(this.editor.Lines.Count - 1, previous + count));
                    var newLine = this.editor.Lines[newIndex];
                    this.Position = newLine.Position;
                    this.EndPosition = newLine.EndPosition - 1;
                    return newIndex - previous;
                }
                default:
                    throw new NotImplementedException();
            }
        }

        public int MoveEndpointByUnit(TextPatternRangeEndpoint endpoint, TextUnit unit, int count)
        {
            switch (unit)
            {
                case TextUnit.Character:
                {
                    if (endpoint == TextPatternRangeEndpoint.Start)
                    {
                        var previous = this.Position;
                        this.Position = Math.Max(0, Math.Min(this.EndPosition, this.Position + count));
                        return this.Position - previous;
                    }
                    else
                    {
                        var previous = this.EndPosition;
                        this.EndPosition = Math.Max(this.Position, Math.Min(this.editor.TextLength, this.EndPosition + count));
                        return this.EndPosition - previous;
                    }
                }
                case TextUnit.Line:
                {
                    var previous = this.editor.LineFromPosition(endpoint == TextPatternRangeEndpoint.Start ? this.Position : this.EndPosition);
                    var newIndex = Math.Max(0, Math.Min(this.editor.Lines.Count - 1, previous + count));
                    var newLine = this.editor.Lines[newIndex];
                    if (endpoint == TextPatternRangeEndpoint.Start)
                    {
                        this.Position = newLine.Position;
                    }
                    else
                    {
                        this.EndPosition = newLine.EndPosition - 1;
                    }

                    return newIndex - previous;
                }
                default:
                    throw new NotImplementedException();
            }
        }

        public void MoveEndpointByRange(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange, TextPatternRangeEndpoint targetEndpoint)
        {
            // TODO
            throw new NotImplementedException();
        }

        public void Select()
        {
            this.editor.SetSelection(this.EndPosition, this.Position);
        }

        public void AddToSelection()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromSelection()
        {
            throw new NotImplementedException();
        }

        public void ScrollIntoView(bool alignToTop)
        {
            throw new NotImplementedException();
        }

        public IRawElementProviderSimple[] GetChildren()
        {
            throw new NotImplementedException();
        }
    }
}
