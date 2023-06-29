using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IQuestionsRepository
    {
        void InsertQuestion(Question Q);
        void UpdateQuestionDetails(Question Q);
        void UpdateQuestionVotesCount(int QuestionID, int Value);
        void UpdateQuestionAnswersCount(int QuestionID, int Value);
        void UpdateQuestionViewsCount(int QuestionID, int Value);
        void DeleteQuestion(int QuestionID);
        List<Question> GetAllQuestions();
        List<Question> GetQuestionByQuestionID(int QuestionID);

    }
    public class QuestionsRepository : IQuestionsRepository
    {
        StackOverflowDatabaseDbContext db;
        public QuestionsRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }
        public void InsertQuestion(Question Q)
        {
            db.Questions.Add(Q);
            db.SaveChanges();
        }
        public void UpdateQuestionDetails(Question Q)
        {
            Question Qt = db.Questions.FirstOrDefault(temp => temp.QuestionID == Q.QuestionID);
            if(Qt != null)
            {
                Qt.QuestionText = Q.QuestionText;
                Qt.QuestionDateAndTime = Q.QuestionDateAndTime;
                Qt.CategoryID = Q.CategoryID;
                db.SaveChanges();
            }
        }
        public void UpdateQuestionVotesCount(int QuestionID, int Value)
        {
            Question Qt = db.Questions.FirstOrDefault(temp=> temp.QuestionID == QuestionID);
            if(Qt!=null)
            {
                Qt.VotesCount += Value;
                db.SaveChanges();
            }
        }
        public void UpdateQuestionAnswersCount(int QuestionID, int Value)
        {
            Question Qt = db.Questions.FirstOrDefault(temp => temp.QuestionID == QuestionID);
            if (Qt != null)
            {
                Qt.AnswersCount += Value;
                db.SaveChanges();
            }
        }
        public void UpdateQuestionViewsCount(int QuestionID, int Value)
        {
            Question Qt = db.Questions.FirstOrDefault(temp => temp.QuestionID == QuestionID);
            if (Qt != null)
            {
                Qt.ViewsCount += Value;
                db.SaveChanges();
            }
        }
        public void DeleteQuestion(int QuestionID)
        {
            Question Qt = db.Questions.FirstOrDefault(temp => temp.QuestionID == QuestionID);
            if (Qt != null)
            {
                db.Questions.Remove(Qt);
                db.SaveChanges();
            }
        }
        public List<Question> GetAllQuestions()
        {
            List<Question> Qt = db.Questions.OrderByDescending(temp => temp.QuestionDateAndTime).ToList();
            return Qt;
        }
        public List<Question> GetQuestionByQuestionID(int QuestionID)
        {
            List<Question> Qt = db.Questions.Where(temp => temp.QuestionID == QuestionID).ToList();
            return Qt;
        }
    }
}
