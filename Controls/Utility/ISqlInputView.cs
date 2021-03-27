using System;
namespace MasudaManager.Controls
{
    interface ISqlInputView
    {
        void ApplyPreference();
        void ApplySetting();
        string FilePath { get; set; }
        bool IsCommented();
        bool IsCommented(int caretPosition);
        string ZoomRatio { get; }
    }
}
