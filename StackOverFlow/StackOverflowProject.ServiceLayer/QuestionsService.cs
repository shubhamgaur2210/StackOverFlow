using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;
using StackOverflowProject.ViewModels;
using StackOverflowProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowProject.ServiceLayer
{
    public interface IQuestionsService
    {
        void InsertQuestion(NewQuestionViewModel QuestionVM);
        void UpdateQuestionDetails(EditQuestionViewModel QuestionVM);
        void UpdateQuestionVotesCount(int QuestionID, int Value);
        void UpdateQuestionAnswersCount(int QuestionID, int Value);
        void UpdateQuestionViewsCount(int QuestionID, int Value);
        void DeleteQuestion(int QuestionID);
        List<QuestionViewModel> GetAllQuestions();
        QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID);
    }
    public class QuestionsService : IQuestionsService
    {
        IQuestionsRepository qr;
        public QuestionsService()
        {
            qr = new QuestionsRepository();
        }
        public void InsertQuestion(NewQuestionViewModel QuestionVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewQuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<NewQuestionViewModel, Question>(QuestionVM);
            qr.InsertQuestion(q);
        }
        public void UpdateQuestionDetails(EditQuestionViewModel QuestionVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditQuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<EditQuestionViewModel, Question>(QuestionVM);
            qr.UpdateQuestionDetails(q);
        }
        public void UpdateQuestionVotesCount(int QuestionID, int Value)
        {
            qr.UpdateQuestionVotesCount(QuestionID, Value);
        }
        public void UpdateQuestionAnswersCount(int QuestionID, int Value)
        {
            qr.UpdateQuestionAnswersCount(QuestionID, Value);
        }
        public void UpdateQuestionViewsCount(int QuestionID, int Value)
        {
            qr.UpdateQuestionViewsCount(QuestionID, Value);
        }
        public void DeleteQuestion(int QuestionID)
        {
            qr.DeleteQuestion(QuestionID);
        }
        public List<QuestionViewModel> GetAllQuestions()
        {
            List<Question> q = qr.GetAllQuestions();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Question, QuestionViewModel>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> QuestionVM = mapper.Map<List<Question>, List<QuestionViewModel>>(q);
            return QuestionVM;
        }
        public QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID)
        {
            Question q = qr.GetQuestionByQuestionID(QuestionID).FirstOrDefault();
            QuestionViewModel QuestionVM = null;
            if(q != null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Question, QuestionViewModel>();
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.CreateMap<Vote, VoteViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                QuestionVM = mapper.Map<Question, QuestionViewModel>(q);

                foreach(var Answer in QuestionVM.Answers)
                {
                    Answer.CurrentUserVoteType = 0;
                    VoteViewModel vote = Answer.Votes.Where(temp => temp.UserID == UserID).FirstOrDefault();
                    if(vote != null)
                    {
                        Answer.CurrentUserVoteType = vote.VoteValue;
                    }
                }
            }
            return QuestionVM;
        }
    }
}
