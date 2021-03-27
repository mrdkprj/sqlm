using MasudaManager.Utility.Preference;
using System;

namespace MasudaManager.Controls
{
    public partial class SqlLogView : XRichTextBox, IMsdControl
    {
        readonly string _dateTimeFormat = "[{0}]";
        readonly string _defaultLineText = "-----------------------------------------------------------------------------";
        
        public SqlLogView()
        {
            InitializeComponent();
        }

        public void WriteLogData(string text, DateTime? date, bool writeLine, string lineText = null)
        {
            if (date != null)
                WriteDateLine(date);

            WriteTextLine(text);

            if (writeLine)
                WriteLineSeparator(lineText);

        }

        void WriteDateLine(DateTime? dateTime)
        {
            this.AppendText(String.Format(_dateTimeFormat, dateTime) + Environment.NewLine);
        }

        void WriteTextLine(string text)
        {
            this.AppendText(text + Environment.NewLine);
        }

        void WriteLineSeparator(string lineText)
        {
            if (lineText == null)
                this.AppendText(_defaultLineText + Environment.NewLine);
            else
                this.AppendText(lineText + Environment.NewLine);
        }

        public void ApplyPreference()
        {
            if (!this.Font.Equals(UserPreference.Setting.Output.Font))
                this.Font = UserPreference.Setting.Output.Font;
        }
    }
}
