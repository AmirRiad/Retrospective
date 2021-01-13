using Retrospective;
using Retrospective.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace Safeer2.UI.Controllers
{
    public class UploadSaveExcelController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(FormCollection formCollection)
        {
            if (Request != null && formCollection.Count > 0)
            {
                DataTable dt = new DataTable();
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["XlsFilePath"] + fileName);
                    file.SaveAs(path);
                    if (!System.IO.Directory.Exists(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["XlsFilePath"])))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["XlsFilePath"]));
                    }
                    var excelData = new ExcelData(path);
                    var sData = excelData.getData(Request["sheetName"]);
                    
                    dt = sData.CopyToDataTable();
                    var sprint = new Sprint();
                    var sprintComments = new List<SprintComment>();
                    var count = 0;
                    var lastCountForComment = dt.Rows.Count;
                    foreach (DataRow item in dt.Rows)
                    {
                        //sprint
                        if (count == 0)
                        {
                            sprint.Name = item.Table.Columns[0].ToString();
                            sprint.SafeerTeamId = (int)SafeerTeam.SapceX;
                            sprint.StartDate = DateTime.Now;
                            sprint.EndDate = DateTime.Now;
                        }

                        if (item.ItemArray[0].ToString() == "Sprint Score")
                            lastCountForComment = count;

                        //sprint comments
                        else if (count > 0 && count < lastCountForComment)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                var commentName = item.ItemArray[i].ToString();
                                if (!string.IsNullOrEmpty(commentName))
                                {
                                    var sprintComment = new SprintComment();
                                    sprintComment.MemberId = (int)AgileMember.AllSpaceXTeam;
                                    sprintComment.Name = commentName;
                                    sprintComment.SprintId = sprint.Id;
                                    if (i == 0 || i == 1)
                                        sprintComment.SprintCategoryId = (int)SprintCategory.WhatDidWeDoWell;
                                    else if (i == 2 || i == 3)
                                        sprintComment.SprintCategoryId = (int)SprintCategory.WhatDidWeDoWrong;
                                    else if (i == 4 || i == 5)
                                        sprintComment.SprintCategoryId = (int)SprintCategory.WhatCouldBeImproved;

                                    sprintComments.Add(sprintComment);
                                }
                            }
                        }

                        //ActionPlans
                        
                        count++;

                    }
                    sprint.SprintComments = sprintComments;
                    SaveFromExceltoDbAsync(sprint);
                }
            }
            return View("Index");
        }


        internal static RetrospectiveToolsContext GetRetrospectiveToolsContext()
        {
            RetrospectiveToolsContext entities = new RetrospectiveToolsContext();
            entities.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");

            return entities;
        }

        private  void SaveFromExceltoDbAsync(Sprint sprint)
        {
            using (var ctx = GetRetrospectiveToolsContext())
            {
                ctx.Sprints.Add(sprint);
                ctx.SaveChanges();
            }
        }


        public enum SafeerTeam : int
        {
            SapceX = 1,
            Legend=2
        }
        public enum SprintCategory : int
        {
            WhatDidWeDoWell=1,
            WhatDidWeDoWrong =2,
            WhatCouldBeImproved =3
        }

        public enum AgileMember : int
        {
            AllSpaceXTeam = 1,
            AllLegendTeam = 2,
             
        }
        //public ActionResult Export()
        //{
        //    List<Employee> emps = TempData["EmployeeList"] as List<Employee>;
        //    var grid = new System.Web.UI.WebControls.GridView();
        //    grid.DataSource = emps;
        //    grid.DataBind();
        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=Expectations.xls");
        //    Response.ContentType = "application/ms-excel";
        //    Response.Charset = "";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    grid.RenderControl(htw);
        //    Response.Output.Write(sw.ToString());
        //    Response.Flush();
        //    Response.End();
        //    ViewBag.EmployeeList = emps;
        //    return View("Index");
        //}

    }
}