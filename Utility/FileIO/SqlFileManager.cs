using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MasudaManager.Utility.Preference;

namespace MasudaManager.Utility
{
    class SqlFileManager : IFileManager
    {
        readonly string _invalidPathMessage = "Invalid file path";
        Dictionary<object, FileStream> _openedFiles = new Dictionary<object, FileStream>();

        ~SqlFileManager()
        {
            ReleaseStream();
        }

        public bool SaveFile(object guid, string path, string text)
        {
            try
            {
                FileStream stream = GetFileStream(guid, path, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter writer = new StreamWriter(stream, UserPreference.Reflector.IOEncoding);

                stream.SetLength(0);
                writer.Write(text);
                writer.Flush();

                RegisterStream(guid, stream);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string OpenFile(object guid, string path)
        {
            ThrowIfInvalidPath(path);

            try
            {
                FileStream stream = GetFileStream(guid, path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamReader reader = new StreamReader(stream, UserPreference.Reflector.IOEncoding);
                string text = reader.ReadToEnd();

                RegisterStream(guid, stream);

                return text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetFilePath(object guid)
        {
            if (_openedFiles.ContainsKey(guid))
                return _openedFiles[guid].Name;

            return null;
        }

        public string GetFileName(object guid)
        {
            if (_openedFiles.ContainsKey(guid))
                return Path.GetFileName(_openedFiles[guid].Name);

            return null;
        }
        
        FileStream GetFileStream(object guid, string path, FileMode mode, FileAccess access)
        {
            if (_openedFiles.ContainsKey(guid))
                return _openedFiles[guid];

            return new FileStream(path, mode, access, UserPreference.Reflector.FileShareMode);
        }
        
        void RegisterStream(object guid, FileStream stream)
        {
            if (_openedFiles.ContainsKey(guid))
                _openedFiles[guid] = stream;
            else
                _openedFiles.Add(guid, stream);
        }

        void ThrowIfInvalidPath(string path)
        {
            if (path == null)
                throw new FileNotFoundException(_invalidPathMessage, path);

            if(!File.Exists(path))
                throw new FileNotFoundException(_invalidPathMessage, path);
        }

        public void ReleaseStream(object guid)
        {
            FileStream stream = _openedFiles.GetValueOrDefault(guid);

            if (stream == null)
                return;

            stream.Close();
            _openedFiles.Remove(guid);
        }
        
        public void ReleaseStream()
        {
            foreach (var stream in _openedFiles.Values)
            {
                stream.Close();
            }
            _openedFiles.Clear();
        }
    }
}
