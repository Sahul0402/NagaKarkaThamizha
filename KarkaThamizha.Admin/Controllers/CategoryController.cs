using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KarkaThamizha.Admin.Controllers
{
    public class CategoryController
    {
        internal List<CategoryModels> GetCategoryByBookID(int bookID)
        {
            List<CategoryModels> lstCategory = new List<CategoryModels>();
            CategoryRepository repoCategory = new CategoryRepository();
            lstCategory = repoCategory.GetCategoryByBookID(bookID);
            return lstCategory;
        }

        public DataTable GetSelectedCategory(int bookID)
        {
            DataTable dtCategories = null;
            CategoryRepository repoCategory = new CategoryRepository();
            dtCategories = repoCategory.GetSelectedCategory(bookID);
            return dtCategories;
        }
    }
}