using DAIS.WikiSystem.Repository.Interfaces.Tag;
using DAIS.WikiSystem.Services.DTOs.Tag;
using DAIS.WikiSystem.Services.Interfaces.Tag;

namespace DAIS.WikiSystem.Services.Implementation.Tag
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<GetAllTagResponse> GetAllAsync()
        {
            var tags = await _tagRepository.RetrieveCollectionAsync(new TagFilter()).ToListAsync();
            var allTags = new GetAllTagResponse
            {
                Tags = tags.Select(MapToTagInfo).ToList(),
                Count = tags.Count
            };
            return allTags;
        }

        public async Task<GetTagResponse> GetByIdAsync(int tagId)
        {
            var tag = await _tagRepository.RetrieveAsync(tagId);

            return (GetTagResponse)MapToTagInfo(tag);

        }

        private TagInfo MapToTagInfo(Models.Tag tag)
        {
            return new TagInfo
            {
                TagId = tag.TagId,
                Name = tag.Name
            };
        }
    }
}
