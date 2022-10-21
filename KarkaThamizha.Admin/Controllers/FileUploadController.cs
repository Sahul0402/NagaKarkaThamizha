using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.SQLObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelApp = Microsoft.Office.Interop.Excel;
using PagedList;
using System.Reflection;

namespace KarkaThamizha.Admin.Controllers
{
    public class FileUploadController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult UploadBookAndDetails()
        //{
        //    return View();
        //}

        public ActionResult FileUpload()
        {
            return View();
        }

        public ActionResult UploadBookAndDetailsUsingOLEDB(string selection)
        {
            string message = "";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    string conString = string.Empty;
                    string fileName = string.Empty;
                    string excelPath = string.Empty;
                    string sheetName = string.Empty;
                    DataTable dtExcelData = null;
                    //string environment = ConfigurationManager.AppSettings["Environment"];

                    //if (environment=="Local")
                    //{
                    //    excelPath = ConfigurationManager.AppSettings["UploadXlFile"];
                    //}
                    //else if(environment == "Dev") {
                        
                    //}
                    //else if(environment == "Prod") {

                    //}

                    excelPath = ConfigurationManager.AppSettings["UploadXlFile"];

                    switch (selection)
                    {
                        case "Books":
                            fileName = Path.GetFileName("BookUploadSheet-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
                            break;
                        case "BookAuthor":
                            fileName = Path.GetFileName("BookAuthors-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
                            break;
                        case "BookDetails":
                            fileName = Path.GetFileName("BookDetails-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
                            break;
                        case "BookCategory":
                            fileName = Path.GetFileName("BookCategory-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
                            break;
                    }

                    var fileExt = System.IO.Path.GetExtension(file.FileName.ToLower()).Substring(1);

                    if (!Directory.Exists(excelPath))
                    {
                        Directory.CreateDirectory(excelPath);
                    }

                    file.SaveAs(Path.Combine(excelPath + fileName));

                    switch (fileExt)
                    {
                        case "xls": //Excel 97-03
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case "xlsx": //Excel 07 or higher
                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                    }

                    conString = string.Format(conString, excelPath + fileName);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        if (excel_con.State == ConnectionState.Closed)
                            excel_con.Open();

                        var excelSheet = excel_con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                        if (selection == "Books") // && item["TABLE_NAME"].ToString() == "Books$")
                        {
                            #region Book
                            dtExcelData = new DataTable();
                            sheetName = excelSheet.Rows[0]["TABLE_NAME"].ToString();

                            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                            dtExcelData.Columns.AddRange(new DataColumn[8] {
                                    new DataColumn("BookID", typeof(Int32)),
                                    new DataColumn("Book", typeof(string)),
                                    new DataColumn("Name", typeof(string)),
                                    new DataColumn("Tanglish", typeof(string)),
                                    new DataColumn("BookDescription", typeof(string)),
                                    new DataColumn("AdminUserID", typeof(Int16)),
                                    new DataColumn("CreatedDate", typeof(DateTime)),
                                    new DataColumn("RecStatus", typeof(string)),
                                });
                            #endregion
                        }
                        else if (selection == "BookAuthor")// && item["TABLE_NAME"].ToString() == "Authors$")
                        {
                            #region Author
                            dtExcelData = new DataTable();
                            sheetName = excelSheet.Rows[0]["TABLE_NAME"].ToString();

                            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                            dtExcelData.Columns.AddRange(new DataColumn[3] {
                                    new DataColumn("BookID", typeof(int)),
                                    new DataColumn("AuthorID", typeof(int)),
                                    new DataColumn("UserTypeID", typeof(int))
                                });
                            #endregion
                        }
                        else if (selection == "BookDetails")// && item["TABLE_NAME"].ToString() == "Details$")
                        {
                            #region Details
                            dtExcelData = new DataTable();
                            sheetName = excelSheet.Rows[0]["TABLE_NAME"].ToString();

                            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                            dtExcelData.Columns.AddRange(new DataColumn[16] {
                                    new DataColumn("BookDetailsID", typeof(int)),
                                    new DataColumn("BookID", typeof(int)),
                                    new DataColumn("BookCode", typeof(string)),
                                    new DataColumn("Price", typeof(Decimal)),
                                    new DataColumn("Pages", typeof(Int16)),
                                    new DataColumn("PublisherID", typeof(Int16)),
                                    new DataColumn("NoofCopy", typeof(Byte)),
                                    new DataColumn("BookFormat", typeof(Byte)),
                                    new DataColumn("ImgBookSmallFS", typeof(string)),
                                    new DataColumn("ImgBookLarge", typeof(string)),
                                    new DataColumn("ISBN13", typeof(string)),
                                    new DataColumn("FirstEdition", typeof(string)),
                                    new DataColumn("CurrentEdition", typeof(string)),
                                    new DataColumn("Dimensions", typeof(string)),
                                    new DataColumn("IsKarkaBook", typeof(Boolean)),
                                    new DataColumn("ImgBookSmallBS", typeof(string))
                                });
                            #endregion
                        }
                        else if (selection == "BookCategory")
                        {
                            #region Category
                            dtExcelData = new DataTable();
                            sheetName = excelSheet.Rows[0]["TABLE_NAME"].ToString();

                            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                            dtExcelData.Columns.AddRange(new DataColumn[2] {
                                    new DataColumn("BookID", typeof(int)),
                                    new DataColumn("CategoryID", typeof(int))
                                });
                            #endregion
                        }

                        string CommandText = "SELECT * From [" + sheetName + "]";
                        using (OleDbDataAdapter oda = new OleDbDataAdapter(CommandText, excel_con))
                        {
                            oda.Fill(dtExcelData);
                        }
                        excel_con.Close();

                        if (dtExcelData != null && dtExcelData.Rows != null && dtExcelData.Rows.Count > 0)
                        {
                            using (SqlConnection con = ConnectionManager.GetConnection())
                            {
                                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con, SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.Default, null))
                                {
                                    if (selection == "Books")
                                    {
                                        #region Books
                                        //Set the database table name
                                        sqlBulkCopy.DestinationTableName = "dbo.Books";

                                        //[OPTIONAL]: Map the Excel columns with that of the database table

                                        sqlBulkCopy.ColumnMappings.Add("BookID", "BookID");
                                        sqlBulkCopy.ColumnMappings.Add("Book", "Book");
                                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                                        sqlBulkCopy.ColumnMappings.Add("Tanglish", "Tanglish");
                                        sqlBulkCopy.ColumnMappings.Add("BookDescription", "BookDescription");
                                        sqlBulkCopy.ColumnMappings.Add("AdminUserID", "AdminUserID");
                                        sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                                        sqlBulkCopy.ColumnMappings.Add("RecStatus", "RecStatus");

                                        #endregion
                                    }
                                    else if (selection == "BookAuthor")
                                    {
                                        #region Authors
                                        //Set the database table name
                                        sqlBulkCopy.DestinationTableName = "dbo.BookAuthor";

                                        //[OPTIONAL]: Map the Excel columns with that of the database table
                                        sqlBulkCopy.ColumnMappings.Add("BookID", "BookID");
                                        sqlBulkCopy.ColumnMappings.Add("AuthorID", "AuthorID");
                                        sqlBulkCopy.ColumnMappings.Add("UserTypeID", "UserTypeID");
                                        #endregion
                                    }
                                    else if (selection == "BookDetails")
                                    {
                                        #region Details
                                        //Set the database table name
                                        sqlBulkCopy.DestinationTableName = "dbo.BooksDetails";

                                        //[OPTIONAL]: Map the Excel columns with that of the database table

                                        sqlBulkCopy.ColumnMappings.Add("BookDetailsID", "BookDetailsID");
                                        sqlBulkCopy.ColumnMappings.Add("BookID", "BookID");
                                        sqlBulkCopy.ColumnMappings.Add("BookCode", "BookCode");
                                        sqlBulkCopy.ColumnMappings.Add("Price", "Price");
                                        sqlBulkCopy.ColumnMappings.Add("Pages", "Pages");
                                        sqlBulkCopy.ColumnMappings.Add("PublisherID", "PublisherID");
                                        sqlBulkCopy.ColumnMappings.Add("NoofCopy", "NoofCopy");
                                        sqlBulkCopy.ColumnMappings.Add("BookFormat", "BookFormat");
                                        sqlBulkCopy.ColumnMappings.Add("ImgBookSmallFS", "ImgBookSmallFS");
                                        sqlBulkCopy.ColumnMappings.Add("ImgBookLarge", "ImgBookLarge");
                                        sqlBulkCopy.ColumnMappings.Add("ISBN13", "ISBN13");
                                        sqlBulkCopy.ColumnMappings.Add("FirstEdition", "FirstEdition");
                                        sqlBulkCopy.ColumnMappings.Add("CurrentEdition", "CurrentEdition");
                                        sqlBulkCopy.ColumnMappings.Add("Dimensions", "Dimensions");
                                        sqlBulkCopy.ColumnMappings.Add("IsKarkaBook", "IsKarkaBook");
                                        sqlBulkCopy.ColumnMappings.Add("ImgBookSmallBS", "ImgBookSmallBS");
                                        #endregion
                                    }
                                    else if (selection == "BookCategory")
                                    {
                                        #region Category
                                        //Set the database table name
                                        sqlBulkCopy.DestinationTableName = "dbo.BookCategory";

                                        //[OPTIONAL]: Map the Excel columns with that of the database table
                                        sqlBulkCopy.ColumnMappings.Add("BookID", "BookID");
                                        sqlBulkCopy.ColumnMappings.Add("CategoryID", "CategoryID");
                                        #endregion
                                    }

                                    con.Open();
                                    try
                                    {
                                        sqlBulkCopy.WriteToServer(dtExcelData);
                                        message = "File uploaded Successfully!";
                                    }
                                    catch (Exception ex)
                                    {
                                        message = ex.Message;
                                        //throw;
                                    }
                                    con.Close();
                                }
                            }
                        }
                        else
                        {
                            message = "No rows found to upload file into SQL";
                        }
                    }
                }
            }
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        public ActionResult UploadExcelsheet()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                List<BooksModels> lstBooks = new List<BooksModels>();
                string filePath = string.Empty;
                if (Request.Files != null)
                {
                    string path = Server.MapPath("~/UploadedFiles/Books/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = path + Path.GetFileName("BookUploadSheet-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
                    string extension = Path.GetExtension("BookUploadSheet-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
                    file.SaveAs(filePath);

                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls": //Excel 97-03.
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx": //Excel 07 and above.
                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                    }

                    conString = string.Format(conString, filePath);

                    using (OleDbConnection connExcel = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                DataTable dt = new DataTable();
                                cmdExcel.Connection = connExcel;

                                //Get the name of First Sheet.
                                connExcel.Open();
                                DataTable dtExcelSchema;
                                string sheetName = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                                connExcel.Close();
                                dtExcelSchema = new DataTable();
                                #region Book
                                if (sheetName == "Books$")
                                {

                                    //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                                    dtExcelSchema.Columns.AddRange(new DataColumn[6] {

                                    new DataColumn("Name", typeof(string)),
                                    new DataColumn("English", typeof(string)),
                                    new DataColumn("Tanglish", typeof(string)),
                                    new DataColumn("AdminUserID", typeof(Int16)),
                                    new DataColumn("RecStatus", typeof(string)),
                                    new DataColumn("BookDescription", typeof(string)),
                                });
                                }
                                #endregion

                                //Read Data from First Sheet.
                                connExcel.Open();
                                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dtExcelSchema);
                                connExcel.Close();

                                CopyToSQL(dtExcelSchema);
                            }
                        }
                    }
                }
                List<BooksModels> _productmaster = new List<BooksModels>();
                ViewBag.maindata = lstBooks;
                return View("UploadBookAndDetails", lstBooks);
            }
            else
            {
                return View();
            }
        }

        private void CopyToSQL(DataTable dtExcelData)
        {
            if (dtExcelData != null && dtExcelData.Rows != null && dtExcelData.Rows.Count > 0)
            {
                using (SqlConnection con = ConnectionManager.GetConnection())
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        #region Books
                        sqlBulkCopy.DestinationTableName = "dbo.NewBooks";

                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("English", "English");
                        sqlBulkCopy.ColumnMappings.Add("Tanglish", "Tanglish");
                        sqlBulkCopy.ColumnMappings.Add("AdminUserID", "AdminUserID");
                        sqlBulkCopy.ColumnMappings.Add("RecStatus", "RecStatus");
                        sqlBulkCopy.ColumnMappings.Add("BookDescription", "BookDescription");

                        #endregion
                        con.Open();
                        try
                        {
                            sqlBulkCopy.WriteToServer(dtExcelData);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        con.Close();
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult FileUpload(int? page, string submit, FormCollection formCollection)
        {
            List<BooksModels> lstBooks = new List<BooksModels>();
            DataTable dtExcel = new DataTable();
            string fileName = "";
            HttpPostedFileBase file;
            int pageSize = 100;
            int pageNumber = page ?? 1;

            dtExcel = (DataTable)TempData["ExcelData"];
            if (dtExcel != null && dtExcel.Rows.Count > 0)
            {
                if (submit == "BulkInsert")
                {
                    string selection = Convert.ToString(TempData["FileType"]);
                    InsertIntoDB(dtExcel, selection);
                }                
            }
            else if (Request.Files.Count > 0)
            {   
                bool isLocal = System.Web.HttpContext.Current.Request.IsLocal;

                HttpFileCollectionBase files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    file = files["xlfile"];
                    if (file != null)
                    {
                        fileName = file.FileName;
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            lstBooks = GetBooksFromExcel(formCollection["Upload"], file);
                        }
                    }
                }
            }

            if (lstBooks != null && lstBooks.Count > 0)
            {
                TempData["FileType"] = formCollection["Upload"];
                return View("FileUpload", lstBooks.ToPagedList(pageNumber, pageSize));
            }
            return View("FileUpload");
        }

        private void InsertIntoDB(DataTable dt, string selection)
        {
            if (dt.Rows.Count > 0)
            {
                string consString = ConfigurationManager.ConnectionStrings["ConnectionKARKA2017"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo." + selection;

                        if (selection == "Books")
                        {
                            //[OPTIONAL]: Map the DataTable columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("BookID", "BookID");
                            sqlBulkCopy.ColumnMappings.Add("Book", "Book");
                            sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                            sqlBulkCopy.ColumnMappings.Add("Tanglish", "Tanglish");
                            sqlBulkCopy.ColumnMappings.Add("BookDescription", "BookDescription");
                            sqlBulkCopy.ColumnMappings.Add("AdminUserID", "AdminUserID");
                        }
                        else if (selection == "BookAuthor")
                        {
                            sqlBulkCopy.ColumnMappings.Add("BookID", "BookID");
                            sqlBulkCopy.ColumnMappings.Add("AuthorID", "AuthorID");
                            sqlBulkCopy.ColumnMappings.Add("UserTypeID", "UserTypeID");
                        }
                        else if (selection == "BookCategory")
                        {
                            sqlBulkCopy.ColumnMappings.Add("BookID", "BookID");
                            sqlBulkCopy.ColumnMappings.Add("CategoryID", "CategoryID");
                        }
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
        }

        private List<BooksModels> GetBooksFromExcel(string selection, HttpPostedFileBase file)
        {
            string conString = string.Empty;
            string fileName = string.Empty;
            string excelPath = string.Empty;
            string sheetName = string.Empty;
            DataRow myNewRow;
            DataTable myTable = null;
            List<BooksModels> lstBooks = null;

            if (Request.Files != null && Request.Files.Count > 0)
            {
                excelPath = ConfigurationManager.AppSettings["UploadXlFile"];

                switch (selection)
                {
                    case "Books":
                        fileName = Path.GetFileName("BookUploadSheet-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + file.FileName);
                        break;
                    case "BookAuthor":
                        fileName = Path.GetFileName("BookAuthors-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + file.FileName);
                        break;
                    case "BookDetails":
                        fileName = Path.GetFileName("BookDetails-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + file.FileName);
                        break;
                    case "BookCategory":
                        fileName = Path.GetFileName("BookCategory-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + file.FileName);
                        break;
                }

                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);

                if (!Directory.Exists(excelPath))
                {
                    Directory.CreateDirectory(excelPath);
                }

                file.SaveAs(Path.Combine(excelPath + fileName));

                //Create COM Objects.
                ExcelApp.Application excelApp = new ExcelApp.Application();

                
                //Notice: Change this path to your real excel file path
                ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(excelPath + fileName);
                ExcelApp._Worksheet excelSheet = excelBook.Sheets[1];
                ExcelApp.Range excelRange = excelSheet.UsedRange;

                int rows = excelRange.Rows.Count;
                int cols = excelRange.Columns.Count;


                if (selection == "Books")
                {
                    //Set DataTable Name and Columns Name
                    myTable = new DataTable(selection);

                    myTable.Columns.Add("BookID", typeof(int));
                    myTable.Columns.Add("Book", typeof(string));
                    myTable.Columns.Add("Name", typeof(string));
                    myTable.Columns.Add("Tanglish", typeof(string));
                    myTable.Columns.Add("BookDescription", typeof(string));
                    myTable.Columns.Add("AdminUserID", typeof(int));
                    myTable.Columns.Add("RecStatus", typeof(string));

                    //first row using for heading, start second row for data
                    for (int i = 2; i <= rows; i++)
                    {
                        myNewRow = myTable.NewRow();
                        myNewRow["BookID"] = Convert.ToInt32(excelRange.Cells[i, 1].Value2.ToString()); //integer
                        myNewRow["Book"] = excelRange.Cells[i, 2].Value2.ToString(); //string
                        var name = Convert.ToString(excelRange.Cells[i, 3].Value2);
                        if (!string.IsNullOrEmpty(name))
                            myNewRow["Name"] = excelRange.Cells[i, 3].Value2.ToString();
                        else
                            myNewRow["Name"] = "";

                        myNewRow["Tanglish"] = excelRange.Cells[i, 4].Value2?.ToString() ?? "";  //string
                        myNewRow["BookDescription"] = excelRange.Cells[i, 5].Value2?.ToString() ?? ""; //string                    
                        myNewRow["AdminUserID"] = Convert.ToInt32(excelRange.Cells[i, 6].Value2.ToString()); //integer
                        myNewRow["RecStatus"] = excelRange.Cells[i, 7].Value2.ToString(); //string

                        myTable.Rows.Add(myNewRow);
                    }
                }
                else if (selection == "BookAuthor")
                {
                    //Set DataTable Name and Columns Name
                    myTable = new DataTable("BookAuthor");

                    myTable.Columns.Add("BookID", typeof(int));
                    myTable.Columns.Add("AuthorID", typeof(int));
                    myTable.Columns.Add("UserTypeID", typeof(int));

                    //first row using for heading, start second row for data
                    for (int i = 2; i <= rows; i++)
                    {
                        myNewRow = myTable.NewRow();
                        myNewRow["BookID"] = Convert.ToInt32(excelRange.Cells[i, 1].Value2.ToString()); //integer
                        myNewRow["AuthorID"] = Convert.ToInt32(excelRange.Cells[i, 2].Value2.ToString()); //integer
                        myNewRow["UserTypeID"] = Convert.ToInt32(excelRange.Cells[i, 3].Value2.ToString()); //integer

                        myTable.Rows.Add(myNewRow);

                    }
                }
                else if (selection == "BookCategory")
                {
                    //Set DataTable Name and Columns Name
                    myTable = new DataTable(selection);

                    myTable.Columns.Add("BookID", typeof(int));
                    myTable.Columns.Add("CategoryID", typeof(int));

                    //first row using for heading, start second row for data
                    for (int i = 2; i <= rows; i++)
                    {
                        myNewRow = myTable.NewRow();
                        myNewRow["BookID"] = Convert.ToInt32(excelRange.Cells[i, 1].Value2.ToString()); //integer
                        myNewRow["CategoryID"] = Convert.ToInt32(excelRange.Cells[i, 2].Value2.ToString()); //integer

                        myTable.Rows.Add(myNewRow);
                    }
                }
                else if (selection == "BookDetails")
                { }

                //after reading, relaase the excel project
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                if (myTable != null && myTable.Rows.Count > 0)
                {
                    TempData["ExcelData"] = myTable;
                    lstBooks = ConvertToListList(myTable, selection);
                }                
            }

            return lstBooks;
        }

        private List<BooksModels> ConvertToListList(DataTable myTable,string selection)
        {
            List<BooksModels> list = new List<BooksModels>();

            if (myTable != null && myTable.Rows.Count > 0)
            {
                if (myTable.TableName == "Books")
                {
                    list = (from DataRow row in myTable.Rows
                             select new BooksModels()
                             {
                                 BookID = Convert.ToInt32(row["BookID"]),
                                 Book = Convert.ToString(row["Book"]),
                                 Name = Convert.ToString(row["Name"]),
                                 Tanglish = Convert.ToString(row["Tanglish"]),
                                 BookDescription = Convert.ToString(row["BookDescription"]),
                                 //AdminUserID = Convert.ToString(row["AdminUserID"]),
                                 Status = Convert.ToString(row["RecStatus"]),
                                 FileUploadType = selection,
                             }).ToList();
                }
                else if (myTable.TableName == "BookAuthor")
                {
                    list = (from DataRow row in myTable.Rows
                            select new BooksModels()
                            {
                                BookID = Convert.ToInt32(row["BookID"]),
                                Users = new UserModels()
                                {
                                    AuthorID = Convert.ToInt32(row["AuthorID"]),
                                    UserType = new UserTypeModels()
                                    { UserTypeID = Convert.ToByte(row["UserTypeID"]), }
                                },
                                FileUploadType = selection,
                            }).ToList();
                }
                if (myTable.TableName == "BookCategory")
                {
                    list = (from DataRow row in myTable.Rows
                            select new BooksModels()
                            {
                                BookID = Convert.ToInt32(row["BookID"]),
                                Category=new CategoryModels()
                                {
                                    CategoryID = Convert.ToInt16(row["CategoryID"]),
                                },
                                FileUploadType = selection,
                            }).ToList();
                }
            }
            return list;
        }

        private DataTable ConverterToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        
        //private DataTable ConverterToDataTable(List<BooksModels> lstBooks)
        //{

        //}

        [HttpPost]
        public ActionResult BindExcelInGrid(int? page)
        {
            List<BooksModels> lstBooks = (List<BooksModels>)TempData["ExcelData"];
            int pageSize = 10;
            int pageNumber = page ?? 1;

            if (lstBooks != null && lstBooks.Count > 0)
            {
                return View("FileUpload", lstBooks.ToPagedList(pageNumber, pageSize));
            }
            return View("FileUpload");
        }

        public ActionResult BulkInsert()
        {
            return View();
        }
    }
}