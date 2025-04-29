using BirthdayGiftApp.Models;

namespace BirthdayGiftApp.Controllers
{
    public interface IVoteService
    {
        Task<bool> StartVoteAsync(string startedById, string targetEmployeeId);
        Task<bool> VoteAsync(string voterId, int voteOptionId);
        Task<bool> EndVoteAsync(int voteId, string userId);
        Task<Vote> GetVoteWithResultsAsync(int voteId);
    }

}
