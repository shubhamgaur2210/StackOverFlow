using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface ICategoriesRepository
    {
        void InsertCategory(Category C);
        void UpdateCategory(Category C);
        void DeleteCategory(int CategoryID);
        List<Category> GetAllCategories();
        List<Category> GetCategoryByCategoryID(int CategoryID);
    }
    public class CategoriesRepository : ICategoriesRepository
    {
        StackOverflowDatabaseDbContext db;
        public CategoriesRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }
        public void InsertCategory(Category C)
        {
            db.Categories.Add(C);
            db.SaveChanges();
        }
        public void UpdateCategory(Category C)
        {
            Category Ct = db.Categories.FirstOrDefault(temp => temp.CategoryID == C.CategoryID);
            if(Ct != null)
            {
                Ct.CategoryName = C.CategoryName;
                db.SaveChanges();
            }
        }
        public void DeleteCategory(int CategoryID)
        {
            Category Ct = db.Categories.FirstOrDefault(temp => temp.CategoryID == CategoryID);
            if (Ct != null)
            {
                db.Categories.Remove(Ct);
                db.SaveChanges();
            }
        }
        public List<Category> GetAllCategories()
        {
            List<Category> Ct = db.Categories.ToList();
            return Ct;
        }
        public List<Category> GetCategoryByCategoryID(int CategoryID)
        {
            List<Category> Ct = db.Categories.Where(temp=> temp.CategoryID == CategoryID).ToList();
            return Ct;
        }
    }
}
