using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Models.ViewModels;
using System.Diagnostics;
using System.IO;

namespace TaskManager.Controllers
{
    public class CompanyController : Controller
    {
        private TaskManagerContext db = new TaskManagerContext();
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        //get the list of the companies but here I am considering only 1 company
        //future scope can be adding more comapnies and their related tasks and employees
        public ActionResult List()
        {
            //get all the companies
            string query = "Select * from companies";

            //pass the comapnies query and show as a list
            List<Company> cmp = db.Companies.SqlQuery(query).ToList();

            return View(cmp);

        }


        //more deatiled view of the company
        public ActionResult Show(int id)
        {
            //getting the comapny using primary key of company
            //writing query in parameterized format
            string main_query = "select * from companies where companyid = @id";

            //sql parameter to pass pk
            var pk_parameter = new SqlParameter("@id", id);
            Company cmp = db.Companies.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            //query to get the employees associated with the company
            string aside_query = "select * from employees inner join companies on employees.companyid = companies.companyid where companies.companyid=@id";
            
            //sql parameter to pass fk
            var fk_parameter = new SqlParameter("@id", id);
            List<Employee> isemp = db.Employees.SqlQuery(aside_query, fk_parameter).ToList();

            //passing the retrived values of company and associated employes to the view model that has company and employees relation
            //one to many relation
            ShowCompany viewmodel = new ShowCompany();
            viewmodel.company = cmp;
            viewmodel.employees = isemp;

            return View(viewmodel);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}