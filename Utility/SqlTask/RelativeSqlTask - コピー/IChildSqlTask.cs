
namespace MasudaManager.Utility
{
    public interface IChildSqlTask : IFilterable
    {
        IParentSqlTask ParentSqlTask { get; set; }
    }
}
