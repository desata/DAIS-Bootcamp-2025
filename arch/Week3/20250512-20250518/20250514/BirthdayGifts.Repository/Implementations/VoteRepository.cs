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
    public class VoteRepository : BaseRepository<Vote>, IVoteRepository
    {
        public VoteRepository(IConfiguration configuration)
            : base(configuration, "Vote")
        {
        }

        protected override string[] GetColumns()
        {
            return new[]
            {
                "VoteId",
                "VoteSessionId",
                "VoterId",
                "GiftId",
                "VoteDate"
            };
        }

        protected override Vote MapToEntity(SqlDataReader reader)
        {
            return new Vote
            {
                VoteId = reader.GetInt32(reader.GetOrdinal("VoteId")),
                VotingSessionId = reader.GetInt32(reader.GetOrdinal("VoteSessionId")),
                VoterId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                GiftId = reader.GetInt32(reader.GetOrdinal("GiftId")),
                VoteDate = reader.GetDateTime(reader.GetOrdinal("VoteDate"))
            };
        }

        protected override Dictionary<string, object> MapToParameters(Vote entity)
        {
            return new Dictionary<string, object>
            {
                { "VoteSessionId", entity.VotingSessionId },
                { "EmployeeId", entity.VoterId },
                { "GiftId", entity.GiftId },
                { "VoteDate", entity.VoteDate }
            };
        }

        public async Task<bool> HasEmployeeVotedInSession(int employeeId, int voteSessionId)
        {
            var filter = new Filter();
            filter.AddCondition("EmployeeId", employeeId);
            filter.AddCondition("VoteSessionId", voteSessionId);

            var votes = await ReceiveCollection(filter);
            return votes.Any();
        }

        public async Task<IEnumerable<Vote>> GetVotesBySession(int voteSessionId)
        {
            var filter = new Filter();
            filter.AddCondition("VoteSessionId", voteSessionId);

            return await ReceiveCollection(filter);
        }

        public async Task<Dictionary<int, int>> GetGiftVoteCountBySession(int voteSessionId)
        {
            var votes = await GetVotesBySession(voteSessionId);

            return votes.GroupBy(v => v.GiftId)
                        .ToDictionary(group => group.Key, group => group.Count());
        }

        public async Task<IEnumerable<int>> GetEmployeesWhoVotedInSession(int voteSessionId)
        {
            var votes = await GetVotesBySession(voteSessionId);
            return votes.Select(v => v.VoterId).Distinct();
        }
    }
}