using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.CacheData;
using KarkaThamizha.Repository.DAL;
using KarkaThamizha.Web.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Admin.Controllers
{
    public class UserController : Controller
    {
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult User(string sortOrder, string currentFilter, string txtSearch, int? page, string type)
        {
            DataCaching cache = new DataCaching();
            List<UserModels> lstUser = new List<UserModels>();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";
            ViewBag.EngNameSort = String.IsNullOrEmpty(sortOrder) ? "Name - English_Desc" : "Name - English";
            ViewBag.CreatedDate = String.IsNullOrEmpty(sortOrder) ? "Facebook_asc" : "Facebook";
            ViewBag.CreatedDate = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "Created Date";

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

            if (type == "User")
                lstUser = cache.GetAllUsers().Where(x => x.UserType.UserTypeID > 0 && x.UserType.UserTypeID != Convert.ToByte(EnumCode.UserType.Author)).OrderBy(x => x.UserName).ToList();
            else 
                lstUser = cache.GetAllUsers().Where(x => x.UserType.UserTypeID > 0 && x.UserType.UserTypeID == Convert.ToByte(EnumCode.UserType.Author)).OrderBy(x => x.UserName).ToList();

            if (!string.IsNullOrEmpty(ViewBag.CurrentFilter))
            {
                lstUser = lstUser.Where(p => p.UserName.Contains(ViewBag.CurrentFilter)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lstUser = lstUser.OrderByDescending(p => p.UserName).ToList();
                    break;
                case "Name - English":
                    lstUser = lstUser.OrderBy(p => p.Name).ToList();
                    break;
                case "Name - English_Desc":
                    lstUser = lstUser.OrderByDescending(p => p.Name).ToList();
                    break;
                case "Facebook_asc":
                    lstUser = lstUser.OrderByDescending(p => p.UserDetail.FaceBook).ToList();
                    break;
                case "Created Date":
                    lstUser = lstUser.OrderByDescending(p => p.CreatedDate).ToList();
                    break;
                default:  // Name ascending 
                    lstUser = lstUser.OrderBy(p => p.UserName).ToList();
                    break;
            }
            int pageSize = 12;
            int pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;

            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }

        #region Add/Edit User

        [HttpGet]
        public ActionResult AddUser()
        {
            UserModels mdlUser = new UserModels();
            UserTypeModels mdlUserType = new UserTypeModels();
            UserTypeController ctrlUserType = new UserTypeController();
            MasterController master = new MasterController();

            MasterModels.CountryModels mdlCountry = GetAllCountries();
            mdlUserType = ctrlUserType.GetUserType();
            mdlUser.Profession = master.GetAllProfession();
            mdlUser.SpecialName = master.GetAllSpecialName();

            mdlUser.UserType = mdlUserType;
            mdlUser.Country = mdlCountry;
            return View(mdlUser);
        }

        private MasterModels.CountryModels GetAllCountries()
        {
            DataCaching caching = new DataCaching();
            MasterModels.CountryModels mdlCountry = new MasterModels.CountryModels();
            mdlCountry.LstCountry = new SelectList(caching.GetAllCountries().AsEnumerable(), "CountryID", "Country", 0);
            return mdlCountry;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddUser(UserModels objUser, IEnumerable<HttpPostedFileBase> imgFile)
        {
            string message = AmendUser(objUser, imgFile);
            ViewBag.Message = message;
            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_USER);
            return RedirectToAction("AddUser");
        }

        private string AmendUser(UserModels objUser, IEnumerable<HttpPostedFileBase> imgFile)
        {
            UserRepository repoUser = new UserRepository();
            UserDetailsController ctrlUserDetails = null;
            var InputFileName = "";
            string message = "";
            int result = 0;
            Int16 i = 0;
            Int16 fileUploadCount = 0;
                        
            objUser.Name = StringExtension.TitleCase(objUser.Name);

            result = repoUser.AddUser(objUser);
            
            if (result > 0)
            {
                //iterating through multiple file collection   
                if (imgFile != null)
                {
                    foreach (HttpPostedFileBase file in imgFile)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            InputFileName = Path.GetFileName(file.FileName);
                            //Checking file is available to save.  
                            if (file != null && file.ContentLength > 0)
                            {
                                bool flag = ImageProcess.checkValidation(file);
                                if (flag)
                                {
                                    bool flagSave = SaveFiles(file, InputFileName, objUser);
                                    AddToUserModels(i, InputFileName, ref objUser);
                                    fileUploadCount++;
                                }
                            }
                            ////assigning file uploaded status to ViewBag for showing message to user.  
                            //ViewBag.UploadStatus = imgFile.Count().ToString() + " files uploaded successfully.";
                        }
                        i++;
                    }
                }
                else
                {
                    fileUploadCount = 0;
                    HttpPostedFileBaseUpload(ref objUser, ref InputFileName, ref i, ref fileUploadCount);
                }

                if (fileUploadCount >= 1)
                { }
                else
                {
                    HttpPostedFileBaseUpload(ref objUser, ref InputFileName, ref i, ref fileUploadCount);
                }

                if (!string.IsNullOrEmpty(objUser.UserDetail.Website))
                {
                    string Website = StringExtension.RemoveString(objUser.UserDetail.Website);
                    objUser.UserDetail.Website = Website;
                }

                if (!string.IsNullOrEmpty(objUser.UserDetail.Blog))
                {
                    string blog = StringExtension.RemoveString(objUser.UserDetail.Blog);
                    objUser.UserDetail.Blog = blog;
                }

                if (!string.IsNullOrEmpty(objUser.UserDetail.FaceBook))
                {
                    string FaceBook = StringExtension.RemoveString(objUser.UserDetail.FaceBook);
                    objUser.UserDetail.FaceBook = FaceBook;
                }
                if (!string.IsNullOrEmpty(objUser.UserDetail.Twitter))
                {
                    string Twitter = StringExtension.RemoveString(objUser.UserDetail.Twitter);
                    objUser.UserDetail.Twitter = Twitter;
                }
                if (!string.IsNullOrEmpty(objUser.UserDetail.Instagram))
                {
                    objUser.UserDetail.Instagram = StringExtension.RemoveString(objUser.UserDetail.Instagram);
                }
                if (!string.IsNullOrEmpty(objUser.UserDetail.Pinterest))
                {
                    objUser.UserDetail.Pinterest = StringExtension.RemoveString(objUser.UserDetail.Pinterest);
                }
                if (!string.IsNullOrEmpty(objUser.UserDetail.YouTube))
                {
                    objUser.UserDetail.YouTube = StringExtension.RemoveString(objUser.UserDetail.YouTube.Replace("/", "\\"));
                }

                if (!string.IsNullOrEmpty(objUser.UserDetail.Wikipedia))
                {
                    objUser.UserDetail.Wikipedia = StringExtension.RemoveString(objUser.UserDetail.Wikipedia);
                }

                ctrlUserDetails = new UserDetailsController();
                objUser.UserDetail.UserID = result;
                if (objUser.UserDetail.DOD == null && objUser.UserDetail.OldAuthorDOD == true)
                    objUser.UserDetail.DOD = Convert.ToDateTime("1/1/1753");//DateTime.MinValue;

                message = ctrlUserDetails.AddUserDetails(objUser.UserDetail);
                System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_USER);
                System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_USERBYAUTHOR);
            }
            return message;
        }

        private void HttpPostedFileBaseUpload(ref UserModels objUser, ref string InputFileName, ref short i, ref short fileUploadCount)
        {
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf != null && hpf.ContentLength > 0)
                {
                    InputFileName = Path.GetFileName(hpf.FileName);
                    if (!string.IsNullOrEmpty(InputFileName))
                    {
                        bool flag = ImageProcess.checkValidation(hpf);
                        if (flag)
                        {
                            bool flagSave = SaveFiles(hpf, InputFileName, objUser);
                            AddToUserModels(i, InputFileName, ref objUser);
                            fileUploadCount++;
                        }
                    }
                }
                i++;
            }
        }

        private bool SaveFiles(HttpPostedFileBase file, string inputFileName, UserModels objUser)
        {
            bool flag = true;

            bool isLocal = System.Web.HttpContext.Current.Request.IsLocal;
            string imgPath = "";

            if (isLocal)
            {
                if (objUser.UserType.UserTypeID == Convert.ToByte(EnumCode.UserType.Author))
                    imgPath = @"D:\Sahul\Project\In-House\Karka\VS2019\KarkaThamizha\Images-Author\" + inputFileName;
                else if (objUser.UserType.UserTypeID == Convert.ToByte(EnumCode.UserType.User))
                    imgPath = @"D:\Sahul\Project\In-House\Karka\VS2019\KarkaThamizha\Images-Users\" + inputFileName;
            }
            else
            {
                if (objUser.UserType.UserTypeID == Convert.ToByte(EnumCode.UserType.Author))
                    imgPath = ConfigurationManager.AppSettings["ImgAuthor"] + inputFileName;
                else if (objUser.UserType.UserTypeID == Convert.ToByte(EnumCode.UserType.User))
                    imgPath = ConfigurationManager.AppSettings["ImgUser"] + inputFileName;
            }

            //Save file to server folder
            try
            {
                file.SaveAs(Path.Combine(imgPath));
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
            }

            return flag;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(UserModels user, IEnumerable<HttpPostedFileBase> imgFile)
        {
            if (user.UserID > 0)
            {
                AmendUser(user, imgFile);
            }
            return RedirectToAction("User");
        }

        public ActionResult UpdateUser(int id)
        {
            UserModels mdlUser = new UserModels();
            UserTypeModels mdlUserType = new UserTypeModels();
            MasterModels.StateModels mdlState = new MasterModels.StateModels();
            MasterModels.CityModels mdlCity = new MasterModels.CityModels();
            MasterModels.SpecialNameModels mdlSpecial = new MasterModels.SpecialNameModels();
            MasterModels.ProfessionModels mdlProfession = new MasterModels.ProfessionModels();
            UserTypeController ctrlUserType = new UserTypeController();
            StateController repoState = new StateController();
            MasterRepository repoMaster = new MasterRepository();
            DataCaching caching = new DataCaching();

            MasterModels.CountryModels mdlCountry = GetAllCountries();

            mdlUser = caching.GetAllUsers().Find(x => x.UserID == id);
            if (mdlUser.UserDetail != null)
            {
                mdlState.LstState = new SelectList(repoState.GetStates(mdlUser.UserDetail.CountryID).AsEnumerable(), "StateID", "State", mdlUser.UserDetail.StateID);
            }
            else
            {
                System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_USER);
                mdlState.LstState = new SelectList(repoState.GetStates(mdlUser.UserDetail.CountryID).AsEnumerable(), "StateID", "State", mdlUser.UserDetail.StateID);
            }
            mdlCity.LstCity = new SelectList(repoMaster.GetCityByStateID(mdlUser.UserDetail.StateID).AsEnumerable(), "CityID", "City", mdlUser.UserDetail.CityID);

            mdlProfession.lstProfession = new SelectList(repoMaster.GetAllProfession().AsEnumerable(), "ProfessionID", "Profession");
            mdlProfession.ProfessionID = mdlUser.Profession.ProfessionID;
            mdlUser.Profession = mdlProfession;

            mdlUserType = ctrlUserType.GetUserType();
            mdlUserType.UserTypeID = mdlUser.UserType.UserTypeID;
            mdlUser.UserType = mdlUserType;

            mdlSpecial.lstSpecialName = new SelectList(caching.GetAllSpecialName().AsEnumerable(), "SpecialNameID", "SpecialName");
            mdlSpecial.SpecialNameID = mdlUser.SpecialName.SpecialNameID;
            mdlUser.SpecialName = mdlSpecial;

            mdlUser.UserID = id;
            mdlUser.Country = mdlCountry;
            mdlUser.State = mdlState;
            mdlUser.City = mdlCity;

            if (mdlUser.UserDetail.DOD != null && mdlUser.UserDetail.DOD == Convert.ToDateTime("1753-01-01"))
            {
                mdlUser.UserDetail.DOD = Convert.ToDateTime("1/1/1753");
                mdlUser.UserDetail.OldAuthorDOD = true;
            }

            mdlUser.Reference = mdlUser.Reference;
            return View(mdlUser);
        }
        #endregion

        private void AddToUserModels(int i, string inputFileName, ref UserModels objUser)
        {
            if (i == 0)
            { objUser.UserDetail.ImgComments = inputFileName; }
            else if (i == 1)
            { objUser.UserDetail.ImgProfile = inputFileName; }
        }

        public UserModels GetUserByAuthor()
        {
            DataCaching cache = new DataCaching();
            Int16 userType_ID = GetUserCategory(EnumCode.UserType.Author.ToString());
            List<UserModels> authors = cache.GetAllUsers().Where(x => x.UserType.UserTypeID == userType_ID && x.UserDetail.DOD == null).OrderBy(x => x.UserName).ToList();
            UserModels mdlUser = new UserModels();
            mdlUser.lstUsers = new SelectList(authors.AsEnumerable(), "UserId", "UserName", 0);
            return mdlUser;
        }

        private Int16 GetUserCategory(string userCategory)
        {
            Int16 category;
            switch (userCategory)
            {
                case "Author":
                    category = 1;
                    break;
                case "Publisher":
                    category = 4;
                    break;
                case "User":
                    category = 6;
                    break;
                case "Translator":
                    category = 7;
                    break;
                default:
                    category = 1;
                    break;
            }
            return category;
        }

        /// <summary>
        /// Calling from BooksReview.js
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        public ActionResult GetUsersByTypeID(string userTypeId)
        {
            DataCaching cache = new DataCaching();
            List<UserModels> lstUser = cache.GetAllUsers().Where(x => x.UserType.UserTypeID == UserTypeByID(userTypeId)).OrderBy(x => x.UserName).ToList();
            var userlist = lstUser.Select(c => new { c.UserID, c.UserName });
            return Json(userlist, JsonRequestBehavior.AllowGet);
        }

        private static int UserTypeByID(string userTypeId)
        {
            int userType = 1;
            switch (userTypeId)
            {
                case "1":
                    userType = (int)EnumCode.UserType.Author;
                    break;
                case "2":
                    userType = (int)EnumCode.UserType.TextWriter;
                    break;
                case "3":
                    userType = (int)EnumCode.UserType.Collector;
                    break;
                case "4":
                    userType = (int)EnumCode.UserType.Publisher;
                    break;
                case "5":
                    userType = (int)EnumCode.UserType.Editor;
                    break;
                case "6":
                    userType = (int)EnumCode.UserType.User;
                    break;
                case "7":
                    userType = (int)EnumCode.UserType.Translator;
                    break;
                default:
                    userType = (int)EnumCode.UserType.User;
                    break;
            }

            return userType;
        }

        public JsonResult AutoCompleteUser(string searchPrefix)
        {
            List<string> category = new List<string>();

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
            DataCaching cache = new DataCaching();
            var data = cache.GetAllUsers().Where(x => x.UserName.Contains(searchItem)).Where(x => !excludeItem.Contains(x.UserName)).Select(i => i.UserName).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //Return country for drop down list.
        public IEnumerable<SelectListItem> GetAuthorList()
        {
            //DataCaching caching = new DataCaching();
            //SelectList lstObj = null;
            //var list = null;
            ////var list = caching.GetUserByAuthorType().Select(p => new SelectListItem { Value = p.UserID.ToString(), Text = p.UserName }).OrderBy(x => x.Text).ToList();

            ////setting
            //lstObj = new SelectList(list, "Value", "Text");
            //return lstObj;
            return null;
        }

        [HttpGet]
        public JsonResult CheckUser(string username)
        {
            DataCaching cache = new DataCaching();
            //var data = cache.GetUsersList().Where(x => x.Name.Contains(username)).Where(x => !excludeItem.Contains(x.Name)).Select(i => i.Name).ToList();
            bool isValid = cache.GetAllUsers().ToList().Exists(p => p.UserName.Contains(username));
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }

        public List<BooksModels> GetSelectedAuthor(int bookID)
        {
            List<BooksModels> mdlBooks = new List<BooksModels>();
            BooksRepository repoBook = new BooksRepository();
            mdlBooks = repoBook.GetSelectedAuthor(bookID);
            return mdlBooks;
        }

        public ActionResult Author(string name)
        {
            return Redirect("http://www.karkathamizha.com/Author?Name=" + name);
        }

        public ActionResult AuthorBooks(int id)
        {
            //It is redirect to Karka->AuthorController->AuthorBookList
            return Redirect("http://dev.karkathamizha.com/karka/Author?AuthorID=" + id);
        }

        public ActionResult Refresh()
        {
            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_USER);
            return RedirectToAction("User");
        }
    }
}