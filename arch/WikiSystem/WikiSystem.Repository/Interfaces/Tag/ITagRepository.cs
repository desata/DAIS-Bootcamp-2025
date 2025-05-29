using WikiSystem.Repository.Base;

namespace WikiSystem.Repository.Interfaces.Tag
{
    public interface ITagRepository : IBaseRepository<Models.Tag, TagFilter, TagUpdate>
    {
    }
}
