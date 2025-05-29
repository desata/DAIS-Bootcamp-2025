using BirthdayGifts.Repository.Interfaces.Employee;
using BirthdayGifts.Repository.Interfaces.VotingSession;
using BirthdayGifts.Services.DTOs.VotingSession;
using BirthdayGifts.Services.Interfaces.VotingSession;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace BirthdayGifts.Services.Implementations.VotingSession
{
    public class VotingSessionService : IVotingSessionService
    {
        private readonly IVotingSessionRepository2 _votingSessionRepository;
        private readonly IEmployeeRepository2 _employeeRepository;

        public VotingSessionService(
            IVotingSessionRepository2 votingSessionRepository,
            IEmployeeRepository2 employeeRepository)
        {
            _votingSessionRepository = votingSessionRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<VotingSessionDto> CreateSessionAsync(CreateVotingSessionRequest request)
        {
            if (request.CreatedById == request.BirthdayPersonId)
            {
                throw new ValidationException("You cannot create a voting session for yourself");
            }

            var activeSessionFilter = new VotingSessionFilter
            {
                BirthdayPersonId = new SqlInt32(request.BirthdayPersonId),
                IsActive = SqlBoolean.True
            };

            var hasActiveSession = await _votingSessionRepository
                .RetrieveCollectionAsync(activeSessionFilter)
                .AnyAsync();

            if (hasActiveSession)
            {
                throw new ValidationException("There is already an active voting session for this person");
            }

            var currentYear = DateTime.Now.Year;
            var yearSessionFilter = new VotingSessionFilter
            {
                BirthdayPersonId = new SqlInt32(request.BirthdayPersonId)
            };

            var yearSessions = await _votingSessionRepository
                .RetrieveCollectionAsync(yearSessionFilter)
                .ToListAsync();

            if (yearSessions.Any(s => s.BirthYear == currentYear))
            {
                throw new ValidationException("There is already a voting session for this person this year");
            }

            var session = new Models.VotingSession
            {
                BirthdayPersonId = request.BirthdayPersonId,
                CreatedById = request.CreatedById,
                StartDate = DateTime.Now,
                IsActive = true,
                BirthYear = currentYear
            };

            var sessionId = await _votingSessionRepository.CreateAsync(session);
            return await GetSessionAsync(sessionId);
        }

        public async Task<VotingSessionDto> GetSessionAsync(int sessionId)
        {
            var session = await _votingSessionRepository.RetrieveAsync(sessionId);
            return await MapToDtoAsync(session);
        }

        public async Task<IEnumerable<VotingSessionDto>> GetActiveSessionsAsync()
        {
            var filter = new VotingSessionFilter { IsActive = SqlBoolean.True };
            var sessions = await _votingSessionRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var dtos = new List<VotingSessionDto>();
            foreach (var session in sessions)
            {
                dtos.Add(await MapToDtoAsync(session));
            }
            return dtos;
        }

        public async Task<bool> EndSessionAsync(int sessionId, int requestedByUserId)
        {
            var session = await _votingSessionRepository.RetrieveAsync(sessionId);

            if (session.CreatedById != requestedByUserId)
            {
                throw new ValidationException("Only the creator can end the voting session");
            }

            var update = new VotingSessionUpdate
            {
                IsActive = SqlBoolean.False,
                EndDate = new SqlDateTime(DateTime.Now)
            };

            return await _votingSessionRepository.UpdateAsync(sessionId, update);
        }

        public async Task<VotingSessionDto> GetActiveSessionForEmployeeAsync(int employeeId)
        {
            var filter = new VotingSessionFilter
            {
                BirthdayPersonId = new SqlInt32(employeeId),
                IsActive = SqlBoolean.True
            };

            var sessions = await _votingSessionRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var session = sessions.FirstOrDefault();

            if (session == null) return null;

            return await MapToDtoAsync(session);
        }

        private async Task<VotingSessionDto> MapToDtoAsync(Models.VotingSession session)
        {
            var birthdayPerson = await _employeeRepository.RetrieveAsync(session.BirthdayPersonId);
            var creator = await _employeeRepository.RetrieveAsync(session.CreatedById);

            return new VotingSessionDto
            {
                VotingSessionId = session.VotingSessionId,
                BirthdayPersonId = session.BirthdayPersonId,
                BirthdayPersonName = birthdayPerson.FullName,
                CreatedById = session.CreatedById,
                CreatedByName = creator.FullName,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                IsActive = session.IsActive,
                BirthYear = session.BirthYear
            };
        }
    }
}
