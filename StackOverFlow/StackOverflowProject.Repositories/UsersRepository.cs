using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User U);
        void UpdateUserDetails(User U);
        void UpdateUserPassword(User U);
        void DeleteUser(int UserID);
        List<User> GetAllUsers();
        List<User> GetUsersByEmailAndPassword(string Email, string Password);
        List<User> GetUsersByEmail(string Email);
        List<User> GetUsersByUserID(int UserID);
        int GetLatestUserID();
    }
    public class UsersRepository : IUsersRepository
    {
        StackOverflowDatabaseDbContext db;
        public UsersRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }
        public void InsertUser(User U)
        {
            db.Users.Add(U);
            db.SaveChanges();
        }
        public void UpdateUserDetails(User U)
        {
            User Us = db.Users.FirstOrDefault(temp => temp.UserID == U.UserID);
            if(Us != null)
            {
                Us.Email = U.Email;
                Us.Name = U.Name;
                Us.Mobile = U.Mobile;
                db.SaveChanges();
            }
        }
        public void UpdateUserPassword(User U)
        {
            User Us = db.Users.FirstOrDefault(temp => temp.UserID == U.UserID);
            if(Us != null)
            {
                Us.Password = U.Password;
                db.SaveChanges();
            }
        }
        public void DeleteUser(int UserID)
        {
            User Us = db.Users.FirstOrDefault(temp => temp.UserID == UserID);
            if(Us!= null)
            {
                db.Users.Remove(Us);
                db.SaveChanges();
            }
        }
        public List<User> GetAllUsers()
        {
            List<User> Us = db.Users.Where(temp=> temp.IsAdmin == false).OrderBy(temp=> temp.Name).ToList();
            return Us;
        }
        public List<User> GetUsersByEmailAndPassword(string Email, string Password)
        {
            List<User> Us = db.Users.Where(temp => temp.Email == Email && temp.Password == Password).ToList();
            return Us;
        }
        public List<User> GetUsersByEmail(string Email)
        {
            List<User> Us = db.Users.Where(temp=> temp.Email == Email).ToList();
            return Us;
        }
        public List<User> GetUsersByUserID(int UserID)
        {
            List<User> Us = db.Users.Where(temp => temp.UserID == UserID).ToList();
            return Us;
        }
        public int GetLatestUserID()
        {
            int UserID = db.Users.Select(temp => temp.UserID).Max();
            return UserID;
        }
    }
}
