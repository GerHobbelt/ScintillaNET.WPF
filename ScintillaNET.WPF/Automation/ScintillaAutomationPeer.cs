using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace ScintillaNET.WPF.Automation
{
    public class ScintillaAutomationPeer : UserControlAutomationPeer
    {
        private readonly ScintillaWPF editor;

        public ScintillaAutomationPeer(ScintillaWPF owner) : base(owner)
        {
            this.editor = owner;
        }

        protected override string GetClassNameCore()
        {
            return editor.GetType().Name;
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.Value)
            {
                return new ValueProvider(this.editor);
            }

            if (patternInterface == PatternInterface.Text)
            {
                return new TextProvider(this.editor);
            }

            return base.GetPattern(patternInterface);
        }
    }
}