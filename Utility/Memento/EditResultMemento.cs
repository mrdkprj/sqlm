
namespace MasudaManager.Utility
{
    public struct EditResultMemento : IMemento<EditData>
    {
        EditData _editData;

        public EditResultMemento(EditData editData)
        {
            _editData = editData;
        }

        public EditData Restore()
        {
            return _editData;
        } 
    }
}
