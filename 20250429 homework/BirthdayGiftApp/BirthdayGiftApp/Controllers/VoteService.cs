using BirthdayGiftApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirthdayGiftApp.Controllers
{
        public class VoteService : IVoteService
        {
            private readonly ApplicationDbContext _context;

            public VoteService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<bool> StartVoteAsync(string startedById, string targetEmployeeId)
            {
                var birthdayYear = DateTime.Now.Year;

                var alreadyExists = await _context.Votes
                    .AnyAsync(v => v.TargetEmployeeId == targetEmployeeId
                                && v.StartDate.Year == birthdayYear
                                && v.EndDate == null);

                if (alreadyExists || startedById == targetEmployeeId)
                    return false;

                var vote = new Vote
                {
                    TargetEmployeeId = targetEmployeeId,
                    StartedById = startedById,
                    StartDate = DateTime.Now,
                    Options = await _context.Gifts.Select(g => new VoteOption
                    {
                        GiftId = g.Id
                    }).ToListAsync()
                };

                _context.Votes.Add(vote);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> VoteAsync(string voterId, int voteOptionId)
            {
                var voteOption = await _context.VoteOptions
                    .Include(vo => vo.Vote)
                    .ThenInclude(v => v.TargetEmployee)
                    .FirstOrDefaultAsync(vo => vo.Id == voteOptionId);

                if (voteOption == null || voteOption.Vote.EndDate != null || voterId == voteOption.Vote.TargetEmployeeId)
                    return false;

                var alreadyVoted = await _context.VoteRecords
                    .AnyAsync(vr => vr.VoterId == voterId && vr.VoteOption.VoteId == voteOption.VoteId);

                if (alreadyVoted)
                    return false;

                _context.VoteRecords.Add(new VoteRecord
                {
                    VoterId = voterId,
                    VoteOptionId = voteOptionId,
                    VotedAt = DateTime.Now
                });

                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> EndVoteAsync(int voteId, string userId)
            {
                var vote = await _context.Votes.FindAsync(voteId);
                if (vote == null || vote.EndDate != null || vote.StartedById != userId)
                    return false;

                vote.EndDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<Vote> GetVoteWithResultsAsync(int voteId)
            {
                return await _context.Votes
                    .Include(v => v.Options)
                        .ThenInclude(o => o.Gift)
                    .Include(v => v.Options)
                        .ThenInclude(o => o.VoteRecords)
                        .ThenInclude(r => r.Voter)
                    .FirstOrDefaultAsync(v => v.Id == voteId);
            }
        }

    }
