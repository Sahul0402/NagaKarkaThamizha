using KarkaThamizha.Controller;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    public class BookDetailsController : KarkaThamizhaBaseController
    {
        public ActionResult BookDetails(int? id)
        {
            BooksDetailsModels mdlBookDetails = null;
            BooksDetailsRepository repoBookDetails = null;
            if (id > 0)
            {
                mdlBookDetails = new BooksDetailsModels();
                repoBookDetails = new BooksDetailsRepository();
                mdlBookDetails = repoBookDetails.GetBooksDetailsByBookID(id.Value);
            }
            return View("BookDetails", mdlBookDetails);
        }
    }
}