using BirthdayGifts.Repository.Interfaces.Employee;
using BirthdayGifts.Repository.Interfaces.Gift;
using BirthdayGifts.Repository.Interfaces.Vote;
using BirthdayGifts.Repository.Interfaces.VotingSession;
using BirthdayGifts.Services.DTOs.Vote;
using BirthdayGifts.Services.Interfaces.Vote;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace BirthdayGifts.Services.Implementations.Vote
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository2 _voteRepository;
        private readonly IVotingSessionRepository2 _sessionRepository;
        private readonly IEmployeeRepository2 _employeeRepository;
        private readonly IGiftRepository2 _giftRepository;

        public VoteService(
            IVoteRepository2 voteRepository,
            IVotingSessionRepository2 sessionRepository,
            IEmployeeRepository2 employeeRepository,
            IGiftRepository2 giftRepository)
        {
            _voteRepository = voteRepository;
            _sessionRepository = sessionRepository;
            _employeeRepository = employeeRepository;
            _giftRepository = giftRepository;
        }

        public async Task<VoteDto> CreateVoteAsync(CreateVoteRequest request)
        {
            var session = await _sessionRepository.RetrieveAsync(request.VotingSessionId);
            if (session == null)
            {
                throw new ValidationException("Voting session not found");
            }
            if (!session.IsActive)
            {
                throw new ValidationException("Voting session is not active");
            }

            if (request.VoterId == session.BirthdayPersonId)
            {
                throw new ValidationException("Birthday person cannot vote in their own session");
            }

            var hasVoted = await HasUserVotedInSessionAsync(request.VoterId, request.VotingSessionId);
            if (hasVoted)
            {
                throw new ValidationException("User has already voted in this session");
            }

            var gift = await _giftRepository.RetrieveAsync(request.GiftId);
            if (gift == null)
            {
                throw new ValidationException("Gift not found");
            }

            var vote = new Models.Vote
            {
                VotingSessionId = request.VotingSessionId,
                VoterId = request.VoterId,
                GiftId = request.GiftId,
                VoteDate = DateTime.Now
            };

            var voteId = await _voteRepository.CreateAsync(vote);
            return await GetVoteByIdAsync(voteId);
        }

        public async Task<IEnumerable<VoteDto>> GetVotesForSessionAsync(int sessionId)
        {
            var filter = new VoteFilter { VotingSessionId = new SqlInt32(sessionId) };
            var votes = await _voteRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var dtos = new List<VoteDto>();
            foreach (var vote in votes)
            {
                dtos.Add(await MapToDtoAsync(vote));
            }
            return dtos;
        }

        public async Task<bool> HasUserVotedInSessionAsync(int userId, int sessionId)
        {
            var filter = new VoteFilter
            {
                VoterId = new SqlInt32(userId),
                VotingSessionId = new SqlInt32(sessionId)
            };

            return await _voteRepository.RetrieveCollectionAsync(filter).AnyAsync();
        }

        private async Task<VoteDto> GetVoteByIdAsync(int voteId)
        {
            var vote = await _voteRepository.RetrieveAsync(voteId);
            return await MapToDtoAsync(vote);
        }

        private async Task<VoteDto> MapToDtoAsync(Models.Vote vote)
        {
            var voter = await _employeeRepository.RetrieveAsync(vote.VoterId);
            var gift = await _giftRepository.RetrieveAsync(vote.GiftId);

            return new VoteDto
            {
                VoteId = vote.VoteId,
                VotingSessionId = vote.VotingSessionId,
                VoterId = vote.VoterId,
                VoterName = voter.FullName,
                GiftId = vote.GiftId,
                GiftName = gift.Name,
                VoteDate = vote.VoteDate
            };
        }
    }

}
