using BirthdayGifts.Services.DTOs.Vote;

namespace BirthdayGifts.Services.Interfaces.Vote
{
    public interface IVoteService
    {
        Task<VoteDto> CreateVoteAsync(CreateVoteRequest request);
        Task<IEnumerable<VoteDto>> GetVotesForSessionAsync(int sessionId);
        Task<bool> HasUserVotedInSessionAsync(int userId, int sessionId);
    }
}
