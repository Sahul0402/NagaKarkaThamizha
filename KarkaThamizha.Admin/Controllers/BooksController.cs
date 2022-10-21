using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.CacheData;
using KarkaThamizha.Repository.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace KarkaThamizha.Admin.Controllers
{
    public class BooksController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Books()
        {
            return View("Books");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAllBooks(string sortOrder, string currentFilter, string txtSearch, int? page, Int16? CategoryID, string ddlAuthorID,string directionSort)
        {
            IEnumerable<BooksModels> bookList = null;
            int pageNumber = 1;
            if (CategoryID == null)
            {
                if (page == null)
                    TempData["Multiple"] = "";

                bookList = LoadBooksList(sortOrder, currentFilter, txtSearch, page, ref pageNumber, ddlAuthorID, directionSort);
            }
            else if (CategoryID > 0)
            {
                BooksModels mdlBooks = new BooksModels();
                DataCaching caching = new DataCaching();
                List<UserModels> lstUser = new List<UserModels>();
                UserModels mdlUser = new UserModels();

                //lstUser = caching.GetUserByAuthorType();
                mdlUser.lstAuthors = lstUser.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.UserName.ToString() }).OrderBy(x => x.Text).ToList();
                mdlBooks.Users = mdlUser;

                return View(bookList.ToPagedList(pageNumber, 12));
            }
            //KarkaFramework.Utilities.KarkaLog.Info("KT Home Controller Index Ended.");
            //KarkaFramework.Utilities.KarkaLog.Error("No error Found");

            return PartialView("_Books", bookList.ToPagedList(pageNumber, 12));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Books(string sortOrder, string currentFilter, string txtSearch, int? page, Int16? CategoryID, string ddlAuthorID, FormCollection formcollection)
        {
            IEnumerable<BooksModels> bookList = new List<BooksModels>();
            int pageNumber = 1;

            if (page == null)
                TempData["Multiple"] = "";

            if (!string.IsNullOrEmpty(formcollection["MultiSelectedUserId"]))
            {
                string selectedUserID = Convert.ToString(formcollection["MultiSelectedUserId"]);
                if (!string.IsNullOrWhiteSpace(selectedUserID))
                {
                    ViewBag.SelectedUserId = selectedUserID;
                    TempData["Multiple"] = selectedUserID;
                    TempData.Keep("Multiple");

                    bookList = LoadBooksList(sortOrder, currentFilter, txtSearch, page, ref pageNumber, selectedUserID, "");
                    pageNumber = page.HasValue ? Convert.ToInt32(page.Value) : 1;
                }
                else
                    bookList = LoadBooksList(sortOrder, currentFilter, txtSearch, page, ref pageNumber, selectedUserID, "");
            }
            else if (!string.IsNullOrEmpty(formcollection["hdnBookListUserID"]) && formcollection["hdnBookListUserID"] == "BookList")
            {
                ViewBag.SelectedUserId = Convert.ToInt32(formcollection["UserID"]);
                bookList = LoadBooksList(sortOrder, currentFilter, txtSearch, page, ref pageNumber, formcollection["UserID"], "");
                pageNumber = page.HasValue ? Convert.ToInt32(page.Value) : 1;
            }
            else if (!string.IsNullOrEmpty(txtSearch))
            {
                bookList = LoadBooksList(sortOrder, currentFilter, txtSearch.Trim(), page, ref pageNumber, formcollection["UserID"], "");
                pageNumber = page.HasValue ? Convert.ToInt32(page.Value) : 1;
            }
            return View(bookList.ToPagedList(pageNumber, 12));
        }
        private IEnumerable<BooksModels> LoadBooksList(string sortOrder, string currentFilter, string txtSearch, int? page, ref int pageNumber, string userID,string directionSort)
        {
            List<BooksModels> bookList = new List<BooksModels>();
            DataCaching caching = new DataCaching();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "" : "Name";

            ViewBag.CreatedDate = sortOrder == "CreatedDate" ? "CreatedDate_Desc" : "CreatedDate";

            if (!string.IsNullOrWhiteSpace(Convert.ToString(TempData["Multiple"])))
            {
                userID = Convert.ToString(TempData["Multiple"]);
                TempData.Keep("Multiple");
            }
            if (!string.IsNullOrEmpty(txtSearch) && userID != "")
                page = 1;
            else
                txtSearch = currentFilter;

            if (!string.IsNullOrEmpty(userID))
            {
                ViewBag.SelectedUserId = userID;
                if (userID == "0")
                    bookList = caching.GetBooksList();
                else
                    bookList = caching.GetBooksList().Where(x => x.Users.UserID == Convert.ToInt32(userID)).ToList();

                if (!string.IsNullOrEmpty(txtSearch))
                {
                    ViewBag.CurrentFilter = txtSearch.Replace(";", "").Trim();
                    string search = Convert.ToString(ViewBag.CurrentFilter);
                    bookList = bookList.Where(x => x.Book.Contains(search)).ToList();
                }
            }
            else if (!string.IsNullOrEmpty(txtSearch))
            {
                ViewBag.CurrentFilter = txtSearch.Replace(";", "").Trim();
                bookList = caching.GetBooksList().Where(p => p.Book.Contains(ViewBag.CurrentFilter)).ToList();
            }
            else
                bookList = caching.GetBooksList();
            List<UserModels> authors = null;
            //List<UserModels> authors = caching.GetUserByAuthorType().OrderBy(x => x.UserName).ToList();
            ViewBag.Author = new SelectList(authors.AsEnumerable(), "UserID", "UserName", ViewBag.SelectedUserId);
            switch (sortOrder)
            {
                case "Name":
                    bookList = directionSort == "ASC" ? bookList.OrderBy(p => p.Book).ToList() : bookList.OrderByDescending(p => p.Book).ToList();
                    break;
                case "Name_Desc":
                    bookList = bookList.OrderByDescending(p => p.Book).ToList();
                    break;
                case "CreatedDate":
                    bookList =directionSort== "ASC" ? bookList.OrderBy(p => p.CreatedDate).ToList(): bookList.OrderByDescending(p => p.CreatedDate).ToList();
                    break;
                case "CreatedDate_Desc":
                    bookList = bookList.OrderByDescending(p => p.CreatedDate).ToList();
                    break;
                default:  // BookId Descending 
                    bookList = bookList.OrderByDescending(p => p.BookID).ToList();//.Where(x => x.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.Author || x.Users.UserType.UserTypeID == null).ToList();
                    break;
            }
            if (page == -4)
            {
                //pageNumber =(int)Math.Round(bookList.Count / 12);
            }
            pageNumber = page.HasValue ? Convert.ToInt32(page.Value) : 1;
            return bookList;
        }

        public JsonResult AutoCompleteBooks(string searchPrefix)
        {
            DataCaching caching = new DataCaching();

            List<string> items = searchPrefix.Split(';').ToList();
            items = items.Select(s => s.Trim()).ToList();

            //Getting extract text to be searched from the list
            string searchItem = items.LastOrDefault().ToString().Trim();

            //Return same if search text is blank or empty
            if (string.IsNullOrEmpty(searchItem))
            {
                return Json(new string[0], JsonRequestBehavior.AllowGet);
            }

            //Populate the items that need to be filtered out
            List<string> excludeItem = new List<string>();
            if (items.Count > 1)
            {
                items.RemoveAt(items.Count - 1);
                excludeItem = items;
            }

            var data = caching.GetBooksList().Where(x => x.Book.Contains(searchItem)).Where(x => !excludeItem.Contains(x.Book)).Select(i => i.Book).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddBooks(BooksModels mdl)
        {
            BooksModels mdlBooks = new BooksModels();
            DataCaching caching = new DataCaching();
            List<UserModels> lstUser = new List<UserModels>();
            UserModels mdlUser = null;
            List<CategoryModels> lstCategory = new List<CategoryModels>();
            UserController ctrlUsers = new UserController();

            //lstUser = caching.GetUserByAuthorType();
            lstCategory = caching.GetAllCategories().Where(x => x.ParentID == 0).ToList();
            mdlBooks.Category = new CategoryModels();
            if (lstUser != null)
            {
                mdlUser = new UserModels();
                mdlUser.lstAuthors = lstUser.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.UserName.ToString() }).OrderBy(x => x.Text).ToList();
                foreach (var singleCat in lstCategory)
                {
                    mdlBooks.Category.sliCategory.Add(new SelectListItem { Value = singleCat.CategoryID.ToString(), Text = singleCat.Category.ToString() });
                }
                mdlBooks.Users = mdlUser;

                UserModels mdlAuthorOthers = new UserModels
                {
                    SelectedMultiAuthorId = new List<int>(),
                    SelectedAuthorsLst = new List<UserModels>()
                };

                // Loading drop down lists.
                this.ViewBag.AuthorList = ctrlUsers.GetAuthorList();
                return View(mdlBooks);
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddBooks(BooksModels lstbook, FormCollection formcollection, string sortOrder, string currentFilter, string txtSearch, int? page)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Add Book","Error in AddBooks method.");
            }

            try
            {
                BooksModels mdlBooks = new BooksModels();
                if (!string.IsNullOrEmpty(formcollection["hdnBookListUserID"]) && formcollection["hdnBookListUserID"] == "BookList")
                {
                    IEnumerable<BooksModels> bookList = new List<BooksModels>();
                    int pageNumber = 1;
                    bookList = LoadBooksList(sortOrder, currentFilter, txtSearch, page, ref pageNumber, formcollection["UserID"], "");
                    pageNumber = page.HasValue ? Convert.ToInt32(page.Value) : 1;
                    ViewBag.ResultData = bookList;
                    return View(bookList.ToPagedList(pageNumber, 12));
                }
                else if (!string.IsNullOrEmpty(formcollection["hdnBookAddUserID"]) && formcollection["hdnBookAddUserID"] == "BookAdd")
                {
                    mdlBooks = FillSubCategory(lstbook.Category.CategoryID);
                    return View(mdlBooks);
                }
                else
                {
                    //KarkaFramework.Utilities.Log.Info("Add Books Called.");
                    DataCaching caching = new DataCaching();
                    UserModels mdlAuthor = new UserModels();
                    List<UserModels> lstAuthor = new List<UserModels>();

                    DataTable dtAuthor = new DataTable();
                    DataRow dr;

                    dtAuthor.Columns.Add("BookID", typeof(int));
                    dtAuthor.Columns.Add("AuthorID", typeof(int));
                    dtAuthor.Columns.Add("UserTypeID", typeof(int));

                    //lstAuthor = caching.GetUserByAuthorType();
                    if (lstAuthor != null)
                    {
                        mdlBooks = new BooksModels();
                        try
                        {
                            mdlAuthor.lstAuthors = lstAuthor.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.UserName.ToString() }).ToList();
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        if (mdlAuthor.lstAuthors != null)
                        {
                            if (lstbook.Users != null)
                            {
                                //Add Authors
                                if (lstbook.Users.AuthorIds != null)
                                {
                                    mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.AuthorIds.Contains(p.UserID)).Select(q => q).ToList();

                                    foreach (var selectedItem in mdlAuthor.SelectedAuthorsLst)
                                    {
                                        dr = dtAuthor.NewRow();
                                        dr["BookID"] = lstbook.BookID;
                                        dr["AuthorID"] = selectedItem.UserID;
                                        dr["UserTypeID"] = (Byte)EnumCode.UserType.Author;

                                        dtAuthor.Rows.Add(dr);
                                    }
                                }

                                //Add Translator Authors
                                if (lstbook.Users.TranslatorAuthorId != null)
                                {
                                    mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.TranslatorAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                    foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                    {
                                        dr = dtAuthor.NewRow();
                                        dr["BookID"] = lstbook.BookID;
                                        dr["AuthorID"] = item.UserID;
                                        dr["UserTypeID"] = (Int16)EnumCode.UserType.Translator;

                                        dtAuthor.Rows.Add(dr);
                                    }
                                }

                                //Add Text Writer Authors
                                if (lstbook.Users.TextWriterAuthorId != null)
                                {
                                    mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.TextWriterAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                    foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                    {
                                        dr = dtAuthor.NewRow();
                                        dr["BookID"] = lstbook.BookID;
                                        dr["AuthorID"] = item.UserID;
                                        dr["UserTypeID"] = (Int16)EnumCode.UserType.TextWriter;

                                        dtAuthor.Rows.Add(dr);
                                    }
                                }

                                //Add Collector Authors
                                if (lstbook.Users.CollectAuthorId != null)
                                {
                                    mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.CollectAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                    foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                    {
                                        dr = dtAuthor.NewRow();
                                        dr["BookID"] = lstbook.BookID;
                                        dr["AuthorID"] = item.UserID;
                                        dr["UserTypeID"] = (Int16)EnumCode.UserType.Collector;

                                        dtAuthor.Rows.Add(dr);
                                    }
                                }

                                //Add Editor Authors
                                if (lstbook.Users.EditorAuthorId != null)
                                {
                                    mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.EditorAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                    foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                    {
                                        dr = dtAuthor.NewRow();
                                        dr["BookID"] = lstbook.BookID;
                                        dr["AuthorID"] = item.UserID;
                                        dr["UserTypeID"] = (Int16)EnumCode.UserType.Editor;

                                        dtAuthor.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }

                    if (dtAuthor != null && dtAuthor.Rows.Count > 0)
                    {
                        var errors = ModelState
                                     .Where(x => x.Value.Errors.Count > 0)
                                     .Select(x => new { x.Key, x.Value.Errors }).ToArray();

                        BooksRepository model = new BooksRepository();
                        int bookId = model.AddBooks(lstbook);
                        //KarkaFramework.Utilities.Log.Info("Books Added");
                        if (bookId > 0)
                        {
                            //KarkaFramework.Utilities.Log.Info("Add Book author");
                            UserRepository repoUser = new UserRepository();
                            dtAuthor.Columns.Remove("BookID");
                            dtAuthor.Columns.Add("BookID", typeof(int), Convert.ToString(bookId));

                            string authorResult = repoUser.AddBooksAuthor(dtAuthor);
                            //KarkaFramework.Utilities.Log.Info("Book Added");

                            if (authorResult == "Success")
                            {
                                System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKS);
                                System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKDETAILS);

                                //KarkaFramework.Utilities.Log.Info("Started to add Category.");
                                SaveSubCategory(lstbook, bookId);
                                //KarkaFramework.Utilities.Log.Info("Category Added");
                            }

                            ViewData["Result"] = bookId;
                            ModelState.Clear();
                        }
                        else if (bookId == -2)
                        {
                            ModelState.AddModelError("New Book", "Book already added.");
                        }
                        //KarkaFramework.Utilities.Log.Info("Book Added..");
                        return RedirectToAction("Books");
                    }
                    //KarkaFramework.Utilities.Log.Info("Book Added");
                    return RedirectToAction("Books");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveSubCategory(BooksModels lstbook, int bookId)
        {
            if (lstbook.lstSubCategories != null)
            {
                DataTable dtCategory = new DataTable();
                DataRow row;

                dtCategory.Columns.Add("BookID", typeof(int));
                dtCategory.Columns.Add("CategoryID", typeof(int));

                foreach (var item in lstbook.lstSubCategories)
                {
                    if (item.IsChecked)
                    {
                        row = dtCategory.NewRow();
                        row["BookID"] = bookId;
                        row["CategoryID"] = item.CategoryID;

                        dtCategory.Rows.Add(row);
                    }
                }
                CategoryRepository repoCategory = new CategoryRepository();
                repoCategory.AddCategory(dtCategory);
            }
        }

        [HttpGet]
        public BooksModels FillSubCategory(Int16 categoryid)
        {
            BooksModels mdlBooks = new BooksModels();
            DataCaching caching = new DataCaching();
            List<UserModels> lstUser = new List<UserModels>();            
            List<CategoryModels> lstCategory = new List<CategoryModels>();
            UserModels mdlUser = null;
            UserController ctrlUsers = null;

            //lstUser = caching.GetUserByAuthorType();
            lstCategory = caching.GetAllCategories().Where(x => x.ParentID == 0).ToList();
            mdlBooks.Category = new CategoryModels();
            if (lstUser != null)
            {
                ctrlUsers = new UserController();
                mdlUser = new UserModels();

                mdlUser.lstAuthors = lstUser.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.UserName.ToString() }).OrderBy(x => x.Text).ToList();
                foreach (var singleCat in lstCategory)
                {
                    mdlBooks.Category.sliCategory.Add(new SelectListItem { Value = singleCat.CategoryID.ToString(), Text = singleCat.Category.ToString() });
                }
                mdlBooks.Users = mdlUser;

                UserModels mdlAuthorOthers = new UserModels
                {
                    SelectedMultiAuthorId = new List<int>(),
                    SelectedAuthorsLst = new List<UserModels>()
                };

                // Loading drop down lists.
                this.ViewBag.AuthorList = ctrlUsers.GetAuthorList();

                mdlBooks.lstSubCategories = caching.GetAllCategories().Where(x => x.ParentID == categoryid).ToList();
            }
            return mdlBooks;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddBooks1(BooksModels lstbook, string save)
        {
            DataCaching caching = null;
            if (string.IsNullOrEmpty(save))
            {
                BooksModels mdlBooks = new BooksModels();
                caching = new DataCaching();
                List<UserModels> lstUser = new List<UserModels>();
                UserModels mdlUser = null;
                List<CategoryModels> lstCategory = new List<CategoryModels>();
                UserController ctrlUsers = new UserController();

                //lstUser = caching.GetUserByAuthorType();
                lstCategory = caching.GetAllCategories().Where(x => x.ParentID == 0).ToList();
                mdlBooks.Categories = new CategoryModels();
                if (lstUser != null)
                {
                    mdlUser = new UserModels();
                    mdlUser.lstAuthors = lstUser.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.UserName.ToString() }).OrderBy(x => x.Text).ToList();

                    mdlBooks.Categories.lstCategories = lstCategory.Select(x => new SelectListItem { Value = x.CategoryID.ToString(), Text = x.Category.ToString() }).OrderBy(x => x.Text).ToList();
                    mdlBooks.Users = mdlUser;

                    UserModels mdlAuthorOthers = new UserModels
                    {
                        SelectedMultiAuthorId = new List<int>(),
                        SelectedAuthorsLst = new List<UserModels>()
                    };

                    // Loading drop down lists.
                    this.ViewBag.AuthorList = ctrlUsers.GetAuthorList();

                    if (lstbook != null && lstbook.Categories != null && lstbook.Categories.CategoryID > 0)
                    {
                        mdlBooks.Categories.CategoryModelsList = caching.GetAllCategories().Where(x => x.ParentID == lstbook.Categories.CategoryID).ToList();
                    }

                    return View(mdlBooks);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                caching = new DataCaching();
                UserModels mdlAuthor = new UserModels();
                List<UserModels> lstAuthor = new List<UserModels>();
                BooksModels mdlBooks;
                DataTable dtAuthor = new DataTable();
                DataRow dr;

                dtAuthor.Columns.Add("BookID", typeof(int));
                dtAuthor.Columns.Add("AuthorID", typeof(int));
                dtAuthor.Columns.Add("UserTypeID", typeof(string));

                //lstAuthor = caching.GetUserByAuthorType();
                if (lstAuthor != null)
                {
                    mdlBooks = new BooksModels();
                    try
                    {
                        mdlAuthor.lstAuthors = lstAuthor.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.UserName.ToString() }).ToList();
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    if (mdlAuthor.lstAuthors != null)
                    {
                        if (lstbook.Users != null)
                        {
                            if (lstbook.Users.AuthorIds != null)
                            {
                                mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.AuthorIds.Contains(p.UserID)).Select(q => q).ToList();

                                foreach (var selectedItem in mdlAuthor.SelectedAuthorsLst)
                                {
                                    dr = dtAuthor.NewRow();
                                    dr["BookID"] = lstbook.BookID;
                                    dr["AuthorID"] = selectedItem.UserID;
                                    dr["UserTypeID"] = (Byte)EnumCode.UserType.Author;

                                    dtAuthor.Rows.Add(dr);
                                }

                                //Add Translator Authors
                                if (lstbook.Users.TranslatorAuthorId != null)
                                {
                                    mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.TranslatorAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                    foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                    {
                                        dr = dtAuthor.NewRow();
                                        dr["BookID"] = lstbook.BookID;
                                        dr["AuthorID"] = item.UserID;
                                        dr["UserTypeID"] = (Int16)EnumCode.UserType.Translator;

                                        dtAuthor.Rows.Add(dr);
                                    }
                                }

                                //Add Text Writer Authors
                                if (lstbook.Users.TextWriterAuthorId != null)
                                {
                                    mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.TextWriterAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                    foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                    {
                                        dr = dtAuthor.NewRow();
                                        dr["BookID"] = lstbook.BookID;
                                        dr["AuthorID"] = item.UserID;
                                        dr["UserTypeID"] = (Int16)EnumCode.UserType.TextWriter;

                                        dtAuthor.Rows.Add(dr);
                                    }
                                }

                                //Add Collector Authors
                                if (lstbook.Users.CollectAuthorId != null)
                                {
                                    mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.CollectAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                    foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                    {
                                        dr = dtAuthor.NewRow();
                                        dr["BookID"] = lstbook.BookID;
                                        dr["AuthorID"] = item.UserID;
                                        dr["UserTypeID"] = (Int16)EnumCode.UserType.Collector;

                                        dtAuthor.Rows.Add(dr);
                                    }
                                }

                                //Add Editor Authors
                                if (lstbook.Users.EditorAuthorId != null)
                                {
                                    mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.EditorAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                    foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                    {
                                        dr = dtAuthor.NewRow();
                                        dr["BookID"] = lstbook.BookID;
                                        dr["AuthorID"] = item.UserID;
                                        dr["UserTypeID"] = (Int16)EnumCode.UserType.Editor;

                                        dtAuthor.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }
                }

                if (dtAuthor != null && dtAuthor.Rows.Count > 0)
                {
                    var errors = ModelState
                                 .Where(x => x.Value.Errors.Count > 0)
                                 .Select(x => new { x.Key, x.Value.Errors }).ToArray();

                    BooksRepository model = new BooksRepository();

                    int bookId = model.AddBooks(lstbook);
                    if (bookId > 0)
                    {
                        UserRepository repoUser = new UserRepository();
                        dtAuthor.Columns.Remove("BookID");
                        dtAuthor.Columns.Add("BookID", typeof(int), Convert.ToString(bookId));

                        string authorResult = repoUser.AddBooksAuthor(dtAuthor);

                        if (authorResult == "Success")
                        {
                            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKS);
                            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKDETAILS);

                            //Add Editor Authors
                            if (lstbook.Categories.SelectedMultiCategoryId != null)
                            {
                                DataTable dtCategory = new DataTable();
                                DataRow row;

                                dtCategory.Columns.Add("BookID", typeof(int));
                                dtCategory.Columns.Add("CategoryID", typeof(int));

                                CategoryModels mdlCategory = new CategoryModels();
                                List<CategoryModels> lstCategory = new List<CategoryModels>();
                                lstCategory = caching.GetAllCategories().Where(x => x.ParentID.Equals(0)).ToList();
                                mdlCategory.CategoryModelsList = lstCategory.Where(p => lstbook.Categories.SelectedMultiCategoryId.Contains(p.CategoryID)).Select(q => q).ToList();

                                foreach (var item in mdlCategory.CategoryModelsList)
                                {
                                    row = dtCategory.NewRow();
                                    row["BookID"] = bookId;
                                    row["CategoryID"] = item.CategoryID;

                                    dtCategory.Rows.Add(row);
                                }
                                CategoryRepository repoCategory = new CategoryRepository();
                                repoCategory.AddCategory(dtCategory);
                            }
                        }

                        ViewData["Result"] = bookId;
                        ModelState.Clear();
                    }
                    else if (bookId == -2)
                    {
                        ModelState.AddModelError("New Book", "Book already added.");
                    }
                    return RedirectToAction("Books", "Books");
                }
                return RedirectToAction("Books", "Books");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AutoComplete(string Prefix)
        {
            DataCaching cach = new DataCaching();
            List<BooksModels> mdlBooks = new List<BooksModels>();
            mdlBooks = cach.GetBooksList().ToList();

            //Searching records from list using LINQ query  
            var book = (from B in mdlBooks
                        where B.Book.StartsWith(Prefix)
                        select new { B.Book });
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        //GET:Books / EditBooks/17
        [HttpGet]
        public ActionResult UpdateBook(int bookID)
        {
            BooksModels mdlBooksUpdate = new BooksModels();
            UserController authors = null;
            List<UserModels> lstUser = new List<UserModels>();
            DataCaching cachedData = new DataCaching();
            CategoryController ctrlCategory = null;

            mdlBooksUpdate.BookID = bookID;
            //lstUser = cachedData.GetUserByAuthorType();  // It show all authors
            mdlBooksUpdate = cachedData.GetBooksList().Find(p => p.BookID == bookID);

            if (lstUser != null)
            {
                authors = new UserController();
                ctrlCategory = new CategoryController();

                // Get Selected Authors
                List<BooksModels> mdlBooksAuthor = authors.GetSelectedAuthor(bookID);
                var lstAuthor = authors.GetAuthorList().Select(p => new SelectListItem { Value = p.Value.ToString(), Text = p.Text.ToString() }).OrderBy(x => x.Text).ToList();

                #region Authors

                var selectedAuthor = mdlBooksAuthor.Where(x => x.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.Author).ToList();

                var selectedAuthor1 = from b in mdlBooksAuthor
                                      where b.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.Author
                                      select new { b.Users.UserID };
                if (selectedAuthor != null && selectedAuthor.Count > 0)
                {
                    //foreach (var selectedvalue in selectedAuthor)
                    for (int i = 0; i < selectedAuthor.Count(); i++)
                    {
                        this.ViewBag.AuthorList = new MultiSelectList(lstAuthor, "Value", "Text", selectedAuthor.Select(x => x.Users.UserID));
                    }
                }
                else
                    this.ViewBag.AuthorList = new SelectList(lstAuthor, "Value", "Text");
                #endregion

                #region Translators
                var selectedTranslators = mdlBooksAuthor.Where(x => x.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.Translator).FirstOrDefault();
                if (selectedTranslators != null && selectedTranslators.Users != null)
                    this.ViewBag.TranslatorList = new SelectList(lstAuthor, "Value", "Text", selectedTranslators.Users.UserID);
                else
                    this.ViewBag.TranslatorList = new SelectList(lstAuthor, "Value", "Text");
                #endregion

                #region Writers
                var selectedWriters = mdlBooksAuthor.Where(x => x.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.TextWriter).FirstOrDefault();
                if (selectedWriters != null && selectedWriters.Users != null)
                    this.ViewBag.WritersList = new SelectList(lstAuthor, "Value", "Text", selectedWriters.Users.UserID);
                else
                    this.ViewBag.WritersList = new SelectList(lstAuthor, "Value", "Text");
                #endregion

                #region Collectors
                var selectedCollectors = mdlBooksAuthor.Where(x => x.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.Collector).FirstOrDefault();
                if (selectedCollectors != null && selectedCollectors.Users != null)
                    this.ViewBag.CollectorsList = new SelectList(lstAuthor, "Value", "Text", selectedCollectors.Users.UserID);
                else
                    this.ViewBag.CollectorsList = new SelectList(lstAuthor, "Value", "Text");
                #endregion

                #region Editors
                var selectedEditors = mdlBooksAuthor.Where(x => x.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.Editor).FirstOrDefault();
                if (selectedEditors != null && selectedEditors.Users != null)
                    this.ViewBag.EditorsList = new SelectList(lstAuthor, "Value", "Text", selectedEditors.Users.UserID);
                else
                    this.ViewBag.EditorsList = new SelectList(lstAuthor, "Value", "Text");
                #endregion

                #region Fill Category
                //List<CategoryModels> mdlListCategory = new List<CategoryModels>();
                List<CategoryModels> selectedItems = new List<CategoryModels>();
                List<CategoryModels> lstCategory = new List<CategoryModels>();

                mdlBooksUpdate.Category = new CategoryModels();
                //mdlListCategory = cachedData.GetAllCategories().Where(x => x.ParentID == 0).ToList();
                selectedItems = ctrlCategory.GetCategoryByBookID(bookID);
                //var parentCategoryID = selectedItems.Select(x => x.ParentID).FirstOrDefault();
                mdlBooksUpdate.Category.CategoryID = selectedItems.Select(x => x.ParentID).FirstOrDefault();                
                lstCategory = cachedData.GetAllCategories().Where(x => x.ParentID == 0).ToList();

                foreach (var singleCat in lstCategory)
                {
                    if (singleCat.CategoryID.ToString().Equals(mdlBooksUpdate.Category.CategoryID.ToString()))
                        mdlBooksUpdate.Category.sliCategory.Add(new SelectListItem { Text = singleCat.Category.ToString(), Value = singleCat.CategoryID.ToString(), Selected = true });
                    else
                        mdlBooksUpdate.Category.sliCategory.Add(new SelectListItem { Text = singleCat.Category.ToString(), Value = singleCat.CategoryID.ToString() });
                }

                #endregion

                #region Sub Categories

                try
                {
                    ctrlCategory = new CategoryController();
                    var selectedCategory = ctrlCategory.GetSelectedCategory(bookID);

                    if (selectedCategory != null && selectedCategory.Rows.Count > 0)
                    {
                        mdlBooksUpdate.lstSubCategories = cachedData.GetAllCategories().Where(x => x.ParentID == mdlBooksUpdate.Category.CategoryID).ToList();

                        foreach (var item in mdlBooksUpdate.lstSubCategories)
                        {
                            for (int i = 0; i < selectedCategory.Rows.Count; i++)
                            {
                                if (item.CategoryID.Equals(selectedCategory.Rows[i]["CategoryID"]))
                                {
                                    //mdlBooksUpdate.lstSubCategories[i].IsChecked = true;
                                    item.IsChecked = true;
                                    break;
                                }
                                else
                                {
                                    item.IsChecked = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                #endregion
                return View("UpdateBook", mdlBooksUpdate);
            }
            return View("UpdateBook", mdlBooksUpdate);
        }

        private List<UserModels> GetAuthorByBookID(int bookID)
        {
            UserRepository mdlUser = new UserRepository();
            List<UserModels> bookAuthor = mdlUser.GetAuthorByBookID(bookID);
            return bookAuthor;
        }

        //POST:Books / EditBooks/8
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateBook(BooksModels lstbook, FormCollection formcollection)
        {
            BooksRepository repoBooks = null;
            BooksModels mdlBooks = null;
            UserRepository repoUser = null;
            UserModels mdlAuthor = null;
            DataCaching caching = null;
            List<UserModels> lstAuthor = null;
            DataTable dtAuthor = new DataTable();
            DataRow dr;
            bool flag = false;

            if (!string.IsNullOrEmpty(formcollection["hdnBookAddUserID"]) && formcollection["hdnBookAddUserID"] == "BookAdd" && formcollection["save"] != "Save")
            {
                lstbook.lstSubCategories = null;
                System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_CATEGORY);
                mdlBooks = FillSubCategory(lstbook.Category.CategoryID);

                FillAllAuthorTypes(lstbook.BookID);
                return View(mdlBooks);
            }
            else if (!string.IsNullOrEmpty(formcollection["save"]) && formcollection["save"] == "Save")
            {
                caching = new DataCaching();
                lstAuthor = new List<UserModels>();
                dtAuthor.Columns.Add("AuthorID", typeof(int));
                dtAuthor.Columns.Add("UserTypeID", typeof(int));
                dtAuthor.Columns.Add("BookID", typeof(int));

                //lstAuthor = caching.GetUserByAuthorType();

                if (lstAuthor != null)
                {
                    mdlAuthor = new UserModels();
                    mdlBooks = new BooksModels();
                    try
                    {
                        mdlAuthor.lstAuthors = new MultiSelectList(lstAuthor, "Name", "UserID").ToList();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    #region Add Author List

                    if (mdlAuthor.lstAuthors != null)
                    {
                        if (lstbook.Users != null)
                        {
                            if (lstbook.Users.AuthorIds != null)
                            {
                                mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.AuthorIds.Contains(p.UserID)).Select(q => q).ToList();

                                foreach (var selectedItem in mdlAuthor.SelectedAuthorsLst)
                                {
                                    dr = dtAuthor.NewRow();
                                    dr["BookID"] = lstbook.BookID;
                                    dr["AuthorID"] = selectedItem.UserID;
                                    dr["UserTypeID"] = (Byte)EnumCode.UserType.Author;
                                    dtAuthor.Rows.Add(dr);
                                }
                            }
                            //Add Translator Authors
                            if (lstbook.Users.TranslatorAuthorId != null)
                            {
                                mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.TranslatorAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                {
                                    dr = dtAuthor.NewRow();
                                    dr["BookID"] = lstbook.BookID;
                                    dr["AuthorID"] = item.UserID;
                                    dr["UserTypeID"] = (Int16)EnumCode.UserType.Translator;

                                    dtAuthor.Rows.Add(dr);
                                }
                            }

                            //Add Text Writer Authors
                            if (lstbook.Users.TextWriterAuthorId != null)
                            {
                                mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.TextWriterAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                {
                                    dr = dtAuthor.NewRow();
                                    dr["BookID"] = lstbook.BookID;
                                    dr["AuthorID"] = item.UserID;
                                    dr["UserTypeID"] = (Int16)EnumCode.UserType.TextWriter;

                                    dtAuthor.Rows.Add(dr);
                                }
                            }

                            //Add Collector Authors
                            if (lstbook.Users.CollectAuthorId != null)
                            {
                                mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.CollectAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                {
                                    dr = dtAuthor.NewRow();
                                    dr["BookID"] = lstbook.BookID;
                                    dr["AuthorID"] = item.UserID;
                                    dr["UserTypeID"] = (Int16)EnumCode.UserType.Collector;

                                    dtAuthor.Rows.Add(dr);
                                }
                            }

                            //Add Editor Authors
                            if (lstbook.Users.EditorAuthorId != null)
                            {
                                mdlAuthor.SelectedAuthorsLst = lstAuthor.Where(p => lstbook.Users.EditorAuthorId.Contains(p.UserID)).Select(q => q).ToList();

                                foreach (var item in mdlAuthor.SelectedAuthorsLst)
                                {
                                    dr = dtAuthor.NewRow();
                                    dr["BookID"] = lstbook.BookID;
                                    dr["AuthorID"] = item.UserID;
                                    dr["UserTypeID"] = (Int16)EnumCode.UserType.Editor;

                                    dtAuthor.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    #endregion
                }

                #region Update Books
                if (dtAuthor != null && dtAuthor.Rows.Count > 0)
                {
                    var errors = ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .Select(x => new { x.Key, x.Value.Errors }).ToArray();

                    repoBooks = new BooksRepository();
                    lstbook.Status = "A";
                    flag = repoBooks.UpdateBook(lstbook.BookID, lstbook);
                    if (flag)
                    {
                        try
                        {
                            repoUser = new UserRepository();
                            string result = repoUser.AddBooksAuthor(dtAuthor);
                            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKS);
                            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKDETAILS);

                            //Add Editor Authors
                            if (result == "Success" && lstbook.lstSubCategories != null && lstbook.lstSubCategories.Count > 0)
                            {
                                SaveSubCategory(lstbook, lstbook.BookID);
                            }

                            ViewData["Result"] = lstbook.BookID;
                            ModelState.Clear();

                            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKS);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
                #endregion
            }

            return RedirectToAction("Books");
        }

        private void FillAllAuthorTypes(int bookid)
        {
            DataCaching caching = new DataCaching();
            UserController authors = new UserController();
            List<UserModels> lstUser = new List<UserModels>();
            //lstUser = caching.GetUserByAuthorType();  // It show all authors

            // Get Selected Authors
            List<BooksModels> mdlBooksAuthor = authors.GetSelectedAuthor(bookid);

            var lstAuthor = authors.GetAuthorList().Select(p => new SelectListItem { Value = p.Value.ToString(), Text = p.Text.ToString() }).OrderBy(x => x.Text).ToList();

            #region Authors

            var selectedAuthor = mdlBooksAuthor.Where(x => x.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.Author).ToList();

            var selectedAuthor1 = from b in mdlBooksAuthor
                                  where b.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.Author
                                  select new { b.Users.UserID };
            if (selectedAuthor != null && selectedAuthor.Count > 0)
            {
                //foreach (var selectedvalue in selectedAuthor)
                for (int i = 0; i < selectedAuthor.Count(); i++)
                {
                    this.ViewBag.AuthorList = new MultiSelectList(lstAuthor, "Value", "Text", selectedAuthor.Select(x => x.Users.UserID));
                }
            }
            else
                this.ViewBag.AuthorList = new SelectList(lstAuthor, "Value", "Text");
            #endregion

            #region Translators
            var selectedTranslators = mdlBooksAuthor.Where(x => x.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.Translator).FirstOrDefault();
            if (selectedTranslators != null && selectedTranslators.Users != null)
                this.ViewBag.TranslatorList = new SelectList(lstAuthor, "Value", "Text", selectedTranslators.Users.UserID);
            else
                this.ViewBag.TranslatorList = new SelectList(lstAuthor, "Value", "Text");
            #endregion

            #region Writers
            var selectedWriters = mdlBooksAuthor.Where(x => x.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.TextWriter).FirstOrDefault();
            if (selectedWriters != null && selectedWriters.Users != null)
                this.ViewBag.WritersList = new SelectList(lstAuthor, "Value", "Text", selectedWriters.Users.UserID);
            else
                this.ViewBag.WritersList = new SelectList(lstAuthor, "Value", "Text");
            #endregion

            #region Collectors
            var selectedCollectors = mdlBooksAuthor.Where(x => x.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.Collector).FirstOrDefault();
            if (selectedCollectors != null && selectedCollectors.Users != null)
                this.ViewBag.CollectorsList = new SelectList(lstAuthor, "Value", "Text", selectedCollectors.Users.UserID);
            else
                this.ViewBag.CollectorsList = new SelectList(lstAuthor, "Value", "Text");
            #endregion

            #region Editors
            var selectedEditors = mdlBooksAuthor.Where(x => x.Users.UserType.UserTypeID == (Byte)EnumCode.UserType.Editor).FirstOrDefault();
            if (selectedEditors != null && selectedEditors.Users != null)
                this.ViewBag.EditorsList = new SelectList(lstAuthor, "Value", "Text", selectedEditors.Users.UserID);
            else
                this.ViewBag.EditorsList = new SelectList(lstAuthor, "Value", "Text");
            #endregion
        }

        //GET Books / DeleteBooks/8
        [HttpGet]//
        public ActionResult DeleteBook(int bookID)
        {
            BooksRepository model = new BooksRepository();
            bool flag = model.DeleteBook(bookID);
            if (flag)
            {
                System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKS);
                return RedirectToAction("Books");
            }
            return View();
        }

        public ActionResult Reading()
        {
            DataCaching cach = new DataCaching();
            List<BooksModels> lstBooks = cach.GetBooksList();
            return View(lstBooks);
        }

        public ActionResult AddReadingBooks(int[] id)
        {
            return View("Reading");
        }
        public ActionResult NewRelease()
        {
            return View("NewRelease");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CheckBookExist(string bookname)
        {
            DataCaching cach = new DataCaching();
            bool isValid = cach.GetBooksList().ToList().Exists(p => p.Book.Contains(bookname));
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Refresh()
        {
            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKS);
            return RedirectToAction("Books");
        }

        public JsonResult GetSubCategory(int categoryid)
        {
            List<CategoryModels> mdlListCategory = new List<CategoryModels>();
            DataCaching cachedData = new DataCaching();
            mdlListCategory = cachedData.GetAllCategories().Where(x => x.ParentID == categoryid).ToList();
            return Json(mdlListCategory, JsonRequestBehavior.AllowGet);
        }

        #region Books with its Category
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult BooksCategory(string sortOrder, string currentFilter, string txtSearch, int? page, string type)
        {
            List<BooksModels> lstUser = new List<BooksModels>();
            BooksRepository repoBooks = new BooksRepository();

            ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";
            //ViewBag.EngNameSort = String.IsNullOrEmpty(sortOrder) ? "Name - English_Desc" : "Name - English";
            //ViewBag.CreatedDate = String.IsNullOrEmpty(sortOrder) ? "Facebook_asc" : "Facebook";
            //ViewBag.CreatedDate = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "Created Date";

            if (txtSearch != null)
            {
                page = 1;
            }
            else
            {
                txtSearch = currentFilter;
            }
            if (!string.IsNullOrEmpty(txtSearch))
                ViewBag.CurrentFilter = txtSearch.Trim().Replace(";", "").Trim();

            lstUser = repoBooks.GetAllBooksWithCategory();

            if (!string.IsNullOrEmpty(ViewBag.CurrentFilter))
            {
                lstUser = lstUser.Where(p => p.Book.Contains(ViewBag.CurrentFilter)).ToList();
            }

            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        lstUser = lstUser.OrderByDescending(p => p.Name).ToList();
            //        break;
            //    case "Name - English":
            //        lstUser = lstUser.OrderBy(p => p.Name).ToList();
            //        break;
            //    case "Name - English_Desc":
            //        lstUser = lstUser.OrderByDescending(p => p.Name).ToList();
            //        break;
            //    case "Facebook_asc":
            //        lstUser = lstUser.OrderByDescending(p => p.UserDetail.FaceBook).ToList();
            //        break;
            //    case "Created Date":
            //        lstUser = lstUser.OrderByDescending(p => p.CreatedDate).ToList();
            //        break;
            //    default:  // Name ascending 
            //        lstUser = lstUser.OrderBy(p => p.Name).ToList();
            //        break;
            //}
            int pageSize = 12;
            int pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;

            return View(lstUser.ToPagedList(pageNumber, pageSize));
        } 
        #endregion
    }
}