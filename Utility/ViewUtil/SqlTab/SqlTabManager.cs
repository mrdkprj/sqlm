using System;
using System.Collections.Generic;

namespace MasudaManager.Utility
{
    public class SqlTabManager : IFileManager
    {
        readonly int _minTabCount = 1;
        List<object> _guidList = new List<object>();
        IFileManager _fileManager = new SqlFileManager();
        object _currentGuid = null;

        public object CurrentGuid { get { return _currentGuid; } }

        public void Add(object guid)
        {
            _guidList.Add(guid);
            _currentGuid = guid;
        }

        public bool Remove(object guid)
        {
            if (!CanRemove(guid))
                return false;

            return _guidList.Remove(guid);
        }

        bool CanRemove(object guid)
        {
            if (!_guidList.Contains(guid))
                return false;

            return _guidList.Count > _minTabCount;
        }

        
        public void Activate(object guid)
        {
            _currentGuid = guid;
        }

        // IFileManager interface
        public string GetFileName(object guid)
        {
            return _fileManager.GetFileName(guid);
        }

        public string GetFilePath(object guid)
        {
            return _fileManager.GetFilePath(guid);
        }

        public bool SaveFile(object guid, string path, string text)
        {
            return _fileManager.SaveFile(guid, path, text);
        }

        public string OpenFile(object guid, string path)
        {
            return _fileManager.OpenFile(guid, path);
        }

        public void ReleaseStream()
        {
            _fileManager.ReleaseStream();
        }

        public void ReleaseStream(object guid)
        {
            _fileManager.ReleaseStream(guid);
        }
    }
}
