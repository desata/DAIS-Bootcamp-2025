using BirthdayGifts.Models;
using BirthdayGifts.Repository.Base;
using BirthdayGifts.Repository.Helpers;
using BirthdayGifts.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayGifts.Repository.Implementations
{
    public class VotingSessionRepository : BaseRepository<VotingSession>, IVotingSessionsRepository
    {
        public VotingSessionRepository(IConfiguration configuration)
            : base(configuration, "VoteSession")
        {
        }

        protected override string[] GetColumns()
        {
            return new[]
            {
                "VoteSessionId",
                "InitiatorEmployeeId",
                "BirthdayPersonId",
                "StartDate",
                "EndDate",
                "IsActive",
                "BirthdayYear"
            };
        }

        protected override VotingSession MapToEntity(SqlDataReader reader)
        {
            return new VotingSession
            {
                VotingSessionId = reader.GetInt32(reader.GetOrdinal("VoteSessionId")),
                CreatedById = reader.GetInt32(reader.GetOrdinal("InitiatorEmployeeId")),
                BirthdayPersonId = reader.GetInt32(reader.GetOrdinal("BirthdayPersonId")),
                StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                EndDate = !reader.IsDBNull(reader.GetOrdinal("EndDate"))
                    ? reader.GetDateTime(reader.GetOrdinal("EndDate"))
                    : (DateTime?)null,
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                BirthYear = reader.GetInt32(reader.GetOrdinal("BirthdayYear"))
            };
        }

        protected override Dictionary<string, object> MapToParameters(VotingSession entity)
        {
            return new Dictionary<string, object>
            {
                { "InitiatorEmployeeId", entity.CreatedById },
                { "BirthdayPersonId", entity.BirthdayPersonId },
                { "StartDate", entity.StartDate },
                { "EndDate", entity.EndDate },
                { "IsActive", entity.IsActive },
                { "BirthdayYear", entity.BirthYear }
            };
        }

        public async Task<bool> EndVoteSession(int voteSessionId)
        {
            var update = new Update();
            update.AddChange("IsActive", false);
            update.AddChange("EndDate", DateTime.Now);

            return await Update(voteSessionId, update);
        }

        public async Task<VotingSession> GetActiveVoteSessionForEmployee(int birthdayPersonId)
        {
            var filter = new Filter();
            filter.AddCondition("BirthdayPersonId", birthdayPersonId);
            filter.AddCondition("IsActive", true);

            var sessions = await ReceiveCollection(filter);
            return sessions.FirstOrDefault();
        }

        public async Task<bool> HasVoteSessionForEmployeeInYear(int birthdayPersonId, int year)
        {
            var filter = new Filter();
            filter.AddCondition("BirthdayPersonId", birthdayPersonId);
            filter.AddCondition("BirthdayYear", year);

            var sessions = await ReceiveCollection(filter);
            return sessions.Any();
        }

        public async Task<IEnumerable<VotingSession>> GetActiveVoteSessions()
        {
            var filter = new Filter();
            filter.AddCondition("IsActive", true);

            return await ReceiveCollection(filter);
        }
    }
}