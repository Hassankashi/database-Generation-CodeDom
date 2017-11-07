using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCNHibernate.Entities;
using Microsoft.Build;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using NHibernate.Linq;
using NHibernate;
using System.Collections;

namespace MVCNHibernate.Controllers
{
    public class GenerateDBController : Controller
    {
    
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getDataType()
        {
            using (var session = NHibernate.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var query = session.CreateSQLQuery("select Name from DataType");
                    var result = query.List();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getTable()
        {
            using (var session = NHibernate.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var sql = String.Format("SELECT * FROM information_schema.tables");
                    var query = session.CreateSQLQuery(sql);
                    var result = query.List();
                   
                    List<string> tableName = new List<string>();
                    object tableSpec;

                    IList collection;
                    for (int i = 0; i < result.Count; i++)
                    {
                        tableSpec = result[i];
                        collection = (IList)tableSpec;
                        tableName.Add(collection[2].ToString());
                       
                    }
                  
                    return Json(tableName, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public string Generator(string tableName, string fieldName1, string dataType1, string fieldName2, string dataType2, string Parent)
        {
            try
            {

            if (Parent==null) //=> No Relation
            {
                var projectCollection = ProjectCollection.GlobalProjectCollection;
                string projPath = "~/MVCNHibernate.csproj";

                var p = projectCollection.LoadProject(Server.MapPath(projPath));


                string projItem1 = "~/Entities/" + tableName + ".cs";
                GenerateTable genTable = new GenerateTable(tableName);
                genTable.AddFields( fieldName1,  dataType1,  fieldName2,  dataType2);
                genTable.GenerateCSharpCode(Server.MapPath(projItem1));

                p.AddItem("Compile", Server.MapPath(projItem1));
                p.Save();

                string projItem2 = "~/Mapping/" + tableName + "Map.cs";
                GenerateMap genMap = new GenerateMap(tableName);
                genMap.AddConstructor(fieldName1,fieldName2,tableName);
                genMap.GenerateCSharpCode(Server.MapPath(projItem2));

                p.AddItem("Compile", Server.MapPath(projItem2));
                p.Save();
                ProjectCollection.GlobalProjectCollection.UnloadProject(p);

                p.Build();
               
                NHibernate.OpenSession();
                              
            }
            else if (Parent != null)//=> Relation To Parent
            {
                var projectCollection = ProjectCollection.GlobalProjectCollection;
                string projPath = "~/MVCNHibernate.csproj";

                var p = projectCollection.LoadProject(Server.MapPath(projPath));


                string fileNameEn = "~/Entities/" + tableName + ".cs";
                GenerateTable genTable = new GenerateTable(tableName);
                genTable.RelationalAddFields(tableName, fieldName1, dataType1, fieldName2, dataType2, Parent);
                genTable.GenerateCSharpCode(Server.MapPath(fileNameEn));

                string projItem1 = "~/Entities/" + tableName + ".cs";
                p.AddItem("Compile", Server.MapPath(projItem1));
                p.Save();

                string fileNameMap = "~/Mapping/" + tableName + "Map.cs";
                GenerateMap genMap = new GenerateMap(tableName);
                genMap.RelationalAddConstructor(fieldName1, fieldName2, tableName, Parent);
                genMap.GenerateCSharpCode(Server.MapPath(fileNameMap));

                string projItem2 = "~/Mapping/" + tableName + "Map.cs";
                p.AddItem("Compile", Server.MapPath(projItem2));
                p.Save();
                //ProjectCollection.GlobalProjectCollection.UnloadProject(p);
                ProjectCollection.GlobalProjectCollection.UnloadAllProjects();
                p.Build();
               
                NHibernate.OpenSession();

              
            }
            return "Database generated Successfully ";
              
            }
            catch
            {
                
                return "Database did not generate Successfully ";
            }
                      
        }

    }
}
