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
    public interface IAnswersService
    {
        void InsertAnswer(NewAnswerViewModel AnswerVM);
        void UpdateAnswer(EditAnswerViewModel AnswerVM);
        void UpdateAnswerVotesCount(int AnswerId, int UserID, int Value);
        void DeleteAnswer(int AnswerID);
        List<AnswerViewModel> GetAnswersByQuestionID(int QuestionID);
        AnswerViewModel GetAnswersByAnswerID(int AnswerID);
    }
    public class AnswersService : IAnswersService
    {
        IAnswersRepository ar;
        public AnswersService()
        {
            ar = new AnswersRepository();
        }
        public void InsertAnswer(NewAnswerViewModel AnswerVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<NewAnswerViewModel, Answer>(AnswerVM);
            ar.InsertAnswer(a);
        }
        public void UpdateAnswer(EditAnswerViewModel AnswerVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<EditAnswerViewModel, Answer>(AnswerVM);
            ar.UpdateAnswer(a);
        }
        public void UpdateAnswerVotesCount(int AnswerId, int UserID, int Value)
        {
            ar.UpdateAnswerVotesCount(AnswerId, UserID, Value);
        }
        public void DeleteAnswer(int AnswerID)
        {
            ar.DeleteAnswer(AnswerID);
        }
        public List<AnswerViewModel> GetAnswersByQuestionID(int QuestionID)
        {
            List<Answer> a = ar.GetAnswersByQuestionID(QuestionID);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> AnswerVM = mapper.Map<List<Answer>, List<AnswerViewModel>>(a);
            return AnswerVM;
        }
        public AnswerViewModel GetAnswersByAnswerID(int AnswerID)
        {
            Answer a = ar.GetAnswerByAnswerID(AnswerID).FirstOrDefault();
            AnswerViewModel AnswerVM = null;
            if (a == null)
            {

                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                AnswerVM = mapper.Map<Answer, AnswerViewModel>(a);

            }
            return AnswerVM;
        }
    }
}
