using BirthdayGifts.Repository.Interfaces.Gift;
using BirthdayGifts.Services.DTOs.Gift;
using BirthdayGifts.Services.Interfaces.Gift;

namespace BirthdayGifts.Services.Implementations.Gift
{
    public class GiftService : IGiftService
    {
        private readonly IGiftRepository2 _giftRepository;

        public GiftService(IGiftRepository2 giftRepository)
        {
            _giftRepository = giftRepository;
        }

        public async Task<GiftDto> GetByIdAsync(int giftId)
        {
            var gift = await _giftRepository.RetrieveAsync(giftId);
            return MapToDto(gift);
        }

        public async Task<IEnumerable<GiftDto>> GetAllAsync()
        {
            var gifts = await _giftRepository.RetrieveCollectionAsync(new GiftFilter()).ToListAsync();
            return gifts.Select(MapToDto);
        }

        private GiftDto MapToDto(Models.Gift gift)
        {
            return new GiftDto
            {
                GiftId = gift.GiftId,
                Name = gift.Name,
                Description = gift.Description,
                Price = gift.Price
            };
        }
    }
}
