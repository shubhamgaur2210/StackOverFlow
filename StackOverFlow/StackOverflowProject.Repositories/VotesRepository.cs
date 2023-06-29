using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int AnswerID, int UserID, int Value);
    }
    public class VotesRepository : IVotesRepository
    {
        StackOverflowDatabaseDbContext db;
        public VotesRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }

        public void UpdateVote(int AnswerID, int UserID, int Value)
        {
            int UpdatedValue = Value == 0 ? 0 : (Value > 0 ? 1 : -1);

            Vote Vt = db.Votes.FirstOrDefault(temp => temp.AnswerID == AnswerID && temp.UserID == UserID);
            if(Vt != null)
            {
                Vt.VoteValue = UpdatedValue;
            }
            else
            {
                Vote NewVote = new Vote();
                NewVote.AnswerID = AnswerID;
                NewVote.UserID = UserID;
                NewVote.VoteValue = UpdatedValue;

                db.Votes.Add(NewVote);
            }
            db.SaveChanges();
        }
    }
}
