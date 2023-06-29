using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using StackOverflowProject.DomainModels;
using StackOverflowProject.Repositories;
using StackOverflowProject.ViewModels;

namespace StackOverflowProject.ServiceLayer
{
    public interface IUsersService
    {
        int InsertUser(RegisterViewModel UserVM);
        void UpdateUserDetails(EditUserDetailsViewModel UserVM);
        void UpdateUserPassword(EditUserPasswordViewModel UserVM);
        void DeleteUser(int UserID);
        List<UserViewModel> GetAllUsers();
        UserViewModel GetUsersByEmailAndPassword(string Email, string Password);
        UserViewModel GetUsersByEmail(string Email);
        UserViewModel GetUsersByUserID(int UserID);
    }
    public class UsersService : IUsersService
    {
        IUsersRepository ur;
        public UsersService()
        {
            ur = new UsersRepository();
        }
        public int InsertUser(RegisterViewModel UserVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<RegisterViewModel,User>(UserVM);
            u.Password = SHA256HashGenerator.GenerateHash(u.Password);
            ur.InsertUser(u);
            int UserID = ur.GetLatestUserID();
            return UserID;
        }
        public void UpdateUserDetails(EditUserDetailsViewModel UserVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserDetailsViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<EditUserDetailsViewModel, User>(UserVM);
            ur.UpdateUserDetails(u);
        }
        public void UpdateUserPassword(EditUserPasswordViewModel UserVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserPasswordViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<EditUserPasswordViewModel, User>(UserVM);
            u.Password = SHA256HashGenerator.GenerateHash(u.Password);
            ur.UpdateUserPassword(u);
        }
        public void DeleteUser(int UserID)
        {
            ur.DeleteUser(UserID);
        }
        public List<UserViewModel> GetAllUsers()
        {
            List<User> u = ur.GetAllUsers();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> UserVM = mapper.Map<List<User>, List<UserViewModel>>(u);
            return UserVM;
        }
        public UserViewModel GetUsersByEmailAndPassword(string Email, string Password)
        {
            User u = ur.GetUsersByEmailAndPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            UserViewModel UserVM = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                UserVM = mapper.Map<User, UserViewModel>(u);

            }
            return UserVM;
        }
        public UserViewModel GetUsersByEmail(string Email)
        {
            User u = ur.GetUsersByEmail(Email).FirstOrDefault();
            UserViewModel UserVM = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                UserVM = mapper.Map<User, UserViewModel>(u);

            }
            return UserVM;
        }
        public UserViewModel GetUsersByUserID(int UserID)
        {
            User u = ur.GetUsersByUserID(UserID).FirstOrDefault();
            UserViewModel UserVM = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                UserVM = mapper.Map<User, UserViewModel>(u);

            }
            return UserVM;
        }
    }
}
