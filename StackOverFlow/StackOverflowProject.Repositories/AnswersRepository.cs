using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer A);
        void UpdateAnswer(Answer A);
        void UpdateAnswerVotesCount(int AnswerId, int UserID, int Value);
        void DeleteAnswer(int AnswerID);
        List<Answer> GetAnswersByQuestionID(int QuestionID);
        List<Answer> GetAnswerByAnswerID(int AnswerID);

    }
    public class AnswersRepository : IAnswersRepository
    {
        StackOverflowDatabaseDbContext db;
        IQuestionsRepository qr;
        IVotesRepository vr;
        public AnswersRepository()
        {
            db = new StackOverflowDatabaseDbContext();
            qr = new QuestionsRepository();
            vr = new VotesRepository();
        }
        public void InsertAnswer(Answer A)
        {
            db.Answers.Add(A);
            db.SaveChanges();
            qr.UpdateQuestionAnswersCount(A.QuestionID, 1);
        }
        public void UpdateAnswer(Answer A)
        {
            Answer Ans = db.Answers.FirstOrDefault(temp => temp.AnswerID == A.AnswerID);
            if(Ans != null)
            {
                Ans.AnswerText = A.AnswerText;
                db.SaveChanges();
            }
        }
        public void UpdateAnswerVotesCount(int AnswerId, int UserID, int Value)
        {
            Answer Ans = db.Answers.FirstOrDefault(temp=> temp.AnswerID == AnswerId);
            if(Ans != null)
            {
                Ans.VotesCount += Value;
                db.SaveChanges();
                qr.UpdateQuestionVotesCount(Ans.QuestionID, Value);
                vr.UpdateVote(AnswerId, UserID, Value);
            }
        }
        public void DeleteAnswer(int AnswerID)
        {
            Answer Ans = db.Answers.FirstOrDefault(temp=> temp.AnswerID == AnswerID);
            if (Ans != null)
            {
                db.Answers.Remove(Ans);
                db.SaveChanges();
                qr.UpdateQuestionAnswersCount(Ans.QuestionID, -1);
            }
        }
        public List<Answer> GetAnswersByQuestionID(int QuestionID)
        {
            List<Answer> AnswerList = db.Answers.Where(temp=> temp.QuestionID == QuestionID).OrderByDescending(temp=> temp.VotesCount).ToList();
            return AnswerList;
        }
        public List<Answer> GetAnswerByAnswerID(int AnswerID)
        {
            List<Answer> AnswerList = db.Answers.Where(temp => temp.AnswerID == AnswerID).ToList();
            return AnswerList;
        }
    }
}
