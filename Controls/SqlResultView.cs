using MasudaManager.Utility.Preference;
using System.Windows.Forms;

namespace MasudaManager.Controls
{
    public partial class SqlResultView : XDataGridView, IMsdControl
    {
        public SqlResultView()
        {
            InitializeComponent();

            this.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
        }

        public void ApplyPreference()
        {
            bool invalidateRequired = InvalidateRequired();

            this.Font = UserPreference.Setting.Output.Font;
            this.DisplayRowNumber = UserPreference.Setting.Output.DisplayRowNumber;
            this.DisplaySpaceCharacter = UserPreference.Setting.Output.DisplaySpaceCharacter;
            
            if (invalidateRequired)
                this.Invalidate();
        }

        bool InvalidateRequired()
        {
            if (!this.Font.Equals(UserPreference.Setting.Output.Font))
                return true;

            if (this.DisplayRowNumber != UserPreference.Setting.Output.DisplayRowNumber)
                return true;

            if (this.DisplaySpaceCharacter != UserPreference.Setting.Output.DisplaySpaceCharacter)
                return true;

            return false;
        }
    }
}
