using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public interface IFileManager
    {
        string GetFileName(object guid);

        string GetFilePath(object guid);

        bool SaveFile(object guid, string path, string text);

        string OpenFile(object guid, string path);

        void ReleaseStream();

        void ReleaseStream(object guid);
    }
}
