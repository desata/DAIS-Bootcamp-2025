using BirthdayGifts.Services.DTOs.VotingSession;

namespace BirthdayGifts.Services.Interfaces.VotingSession
{
    public interface IVotingSessionService
    {
        Task<VotingSessionDto> CreateSessionAsync(CreateVotingSessionRequest request);
        Task<VotingSessionDto> GetSessionAsync(int sessionId);
        Task<IEnumerable<VotingSessionDto>> GetActiveSessionsAsync();
        Task<bool> EndSessionAsync(int sessionId, int requestedByUserId);
        Task<VotingSessionDto> GetActiveSessionForEmployeeAsync(int employeeId);
    }

}
