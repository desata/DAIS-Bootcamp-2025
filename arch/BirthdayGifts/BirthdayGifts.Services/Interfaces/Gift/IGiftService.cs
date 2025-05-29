using BirthdayGifts.Services.DTOs.Gift;

namespace BirthdayGifts.Services.Interfaces.Gift
{
    public interface IGiftService
    {
        Task<GiftDto> GetByIdAsync(int giftId);
        Task<IEnumerable<GiftDto>> GetAllAsync();
    }
}
