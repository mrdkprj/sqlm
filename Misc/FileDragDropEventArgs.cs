using System;

namespace MasudaManager
{
    public class FileDragDropEventArgs : EventArgs
    {
        public FileDragDropEventArgs() { }

        public FileDragDropEventArgs(string path)
        {
            this.Path = path;
        }

        public FileDragDropEventArgs(string path, string text)
        {
            this.Path = path;
            this.Text = text;
        }

        public string Path { get; set; }
        public string Text { get; set; }
    }
}
