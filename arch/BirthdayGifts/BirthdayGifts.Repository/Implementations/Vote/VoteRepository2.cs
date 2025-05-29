﻿using BirthdayGifts.Repository.Base2;
using BirthdayGifts.Repository.Helpers;
using BirthdayGifts.Repository.Interfaces.Vote;
using Microsoft.Data.SqlClient;

namespace BirthdayGifts.Repository.Implementations.Vote
{
    public class VoteRepository2 : BaseRepository2<Models.Vote>, IVoteRepository2
    {
        private const string IdDbFieldEnumeratorName = "VoteId";

        protected override string GetTableName()
        {
            return "Votes";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "VotingSessionId",
            "VoterId",
            "GiftId",
            "VoteDate"
        };

        protected override Models.Vote MapEntity(SqlDataReader reader)
        {
            return new Models.Vote
            {
                VoteId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                VotingSessionId = Convert.ToInt32(reader["VotingSessionId"]),
                VoterId = Convert.ToInt32(reader["VoterId"]),
                GiftId = Convert.ToInt32(reader["GiftId"]),
                VoteDate = Convert.ToDateTime(reader["VoteDate"])
            };
        }

        public Task<int> CreateAsync(Models.Vote entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.Vote> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Vote> RetrieveCollectionAsync(VoteFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.VoterId is not null)
            {
                commandFilter.AddCondition("VoterId", filter.VoterId);
            }
            if (filter.VotingSessionId is not null)
            {
                commandFilter.AddCondition("VotingSessionId", filter.VotingSessionId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, VoteUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(
                connection,
                "Votes",
                IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("GiftId", update.GiftId);
            updateCommand.AddSetClause("VoteDate", update.VoteDate);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}
