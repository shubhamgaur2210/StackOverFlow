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
    public interface ICategoriesService
    {
        void InsertCategory(CategoryViewModel CategoryVM);
        void UpdateCategory(CategoryViewModel CategoryVM);
        void DeleteCategory(int CategoryID);
        List<CategoryViewModel> GetAllCategories();
        CategoryViewModel GetCategoryByCategoryID(int CategoryID);
    }
    public class CategoriesService : ICategoriesService
    {
        ICategoriesRepository cr;
        public CategoriesService()
        {
            cr = new CategoriesRepository();
        }

        public void InsertCategory(CategoryViewModel CategoryVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category c = mapper.Map<CategoryViewModel, Category>(CategoryVM);
            cr.InsertCategory(c);
        }
        public void UpdateCategory(CategoryViewModel CategoryVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category c = mapper.Map<CategoryViewModel, Category>(CategoryVM);
            cr.UpdateCategory(c);
        }
        public void DeleteCategory(int CategoryID)
        {
            cr.DeleteCategory(CategoryID);
        }
        public List<CategoryViewModel> GetAllCategories()
        {
            List<Category> c = cr.GetAllCategories();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> CategoryVM = mapper.Map<List<Category>, List< CategoryViewModel>>(c);
            return CategoryVM;
        }
        public CategoryViewModel GetCategoryByCategoryID(int CategoryID)
        {
            Category c = cr.GetCategoryByCategoryID(CategoryID).FirstOrDefault();
            CategoryViewModel CategoryVM = null;
            if (c != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                CategoryVM = mapper.Map<Category, CategoryViewModel>(c);
            }
            return CategoryVM;
        }
    }
}
