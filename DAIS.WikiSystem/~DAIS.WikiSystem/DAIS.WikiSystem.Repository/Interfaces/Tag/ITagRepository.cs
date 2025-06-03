using DAIS.WikiSystem.Repository.Base;

namespace DAIS.WikiSystem.Repository.Interfaces.Tag
{
    public interface ITagRepository : IBaseRepository<Models.Tag, TagFilter, TagUpdate>
    {
    }
}
