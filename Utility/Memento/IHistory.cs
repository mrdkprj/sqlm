
namespace MasudaManager.Utility
{
    public interface IHistory<T>
    {
        bool CanRedo { get; }

        bool CanUndo { get; }

        void BeginDo();

        void EndDo();

        void CancelDo();

        void Do(IOriginator<T> originater);

        void Undo(IOriginator<T> originater);

        void Redo(IOriginator<T> originater);

        void Clear();

        void Restore(IOriginator<T> originater);

    }
}
