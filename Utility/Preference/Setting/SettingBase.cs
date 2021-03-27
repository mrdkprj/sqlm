using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.Preference.Setting
{
    public abstract class SettingBase : ApplicationSettingsBase
    {
        public abstract void RestoreDefault();
    }
}
