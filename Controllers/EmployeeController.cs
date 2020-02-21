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


namespace TaskManager.Models
{
    public class EmployeeController : Controller
    {
        private TaskManagerContext db = new TaskManagerContext();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string empsearchkey)
        {
            //access the search key
            //Debug.WriteLine("The search key is " + empsearchkey);

            //getting all the employees
            string query = "Select * from employees";

            //checking the existance of the search input
            if (empsearchkey != "")
            {
                //appending the query to include the search key
                query = query + " where firstName like '%" + empsearchkey + "%'";
                Debug.WriteLine("The query is " + query);
            }

            List<Employee> emp = db.Employees.SqlQuery(query).ToList();
            return View(emp);

        }

        //to get the view of the add new employee
        public ActionResult Add()
        {
            return View();
        }

        //after submiting the form of adding employee
        [HttpPost]
        public ActionResult Add(string firstname, string lastname, string email, string phone, string address, string joindate, string companyid)
        {
            //writing query in parameterized format
            string query = "insert into employees (firstname, lastname, email, address, phone, joindate, companyid ) values (@firstname, @lastname, @email, @address, @phone, @joindate, @companyid)";
            //query before the parameters
            Debug.WriteLine(query);
            
            //insert query parameters
            SqlParameter[] sqlparams = new SqlParameter[7];
            sqlparams[0] = new SqlParameter("@firstname", firstname);
            sqlparams[1] = new SqlParameter("@lastname", lastname);
            sqlparams[2] = new SqlParameter("@email", email);
            sqlparams[3] = new SqlParameter("@address", address);
            sqlparams[4] = new SqlParameter("@phone", phone);
            sqlparams[5] = new SqlParameter("@joindate", joindate);
            sqlparams[6] = new SqlParameter("@companyid", companyid);
            
            //query after the parameters
            Debug.WriteLine(query);

            //we use sql commoand execution for the query execution
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult View(int id)
        {
            //get the data for an inidivial employee
            string main_query = "select * from employees where employeeid = @id";

            //sql parameter to pass pk
            var pk_parameter = new SqlParameter("@id", id);
            Employee employee = db.Employees.SqlQuery(main_query, pk_parameter).FirstOrDefault();

           
            //query to get the employees associated with tasks
            string aside_query = "select * from tasks inner join employeesxtasks on tasks.taskid = employeesxtasks.taskid where employeesxtasks.employeeid=@id";
            var fk_parameter = new SqlParameter("@id", id);

            Debug.WriteLine(aside_query);

            List<Task> taskassign = db.Tasks.SqlQuery(aside_query, fk_parameter).ToList();

            string all_tasks_query = "select * from tasks";
            List<Task> alltask = db.Tasks.SqlQuery(all_tasks_query).ToList();

            //passing the values to the viewmodel retrived from the database
            ShowEmployee viewmodel = new ShowEmployee();
            viewmodel.employee = employee;
            viewmodel.tasks = taskassign;
            viewmodel.all_tasks = alltask;

            return View(viewmodel);
        }

        public ActionResult AttachTask(int id, int taskid)
        {
            Debug.WriteLine("employee id is" + id + " and taskid is " + taskid);

            //first, check if that task is already assigned to that employee
            string check_query = "select * from tasks inner join employeesxtasks on employeesxtasks.taskid = tasks.taskid where tasks.taskid=@taskid and employeeid=@id";
            SqlParameter[] check_params = new SqlParameter[2];
            check_params[0] = new SqlParameter("@id", id);
            check_params[1] = new SqlParameter("@taskid", taskid);
            List<Task> tsk = db.Tasks.SqlQuery(check_query, check_params).ToList();
            

            //check to not repeat the task
            if (tsk.Count <= 0)
            {
                
                string query = "insert into employeesxtasks (taskid, employeeid) values (@taskid, @id)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@id", id);
                sqlparams[1] = new SqlParameter("@taskid", taskid);


                db.Database.ExecuteSqlCommand(query, sqlparams);
            }

            return RedirectToAction("View/" + id);

        }

        [HttpGet]
        public ActionResult DetachTask(int id, int taskid)
        {
            
            Debug.WriteLine("employee id is" + id + " and taskid is " + taskid);

            //query to delete the task associated with the particular employee
            string query = "delete from employeesxtasks where taskid=@taskid and employeeid=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@taskid", taskid);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("View/" + id);
        }
        public ActionResult Update(int id)
        {
            //need information about a particular employee
            Employee selectedemp = db.Employees.SqlQuery("select * from employees where employeeid = @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<Task> tasks = db.Tasks.SqlQuery("select * from tasks").ToList();

            return View(selectedemp);
        }

        [HttpPost]
        public ActionResult Update(int id, string firstname, string lastname, string email, string phone, string address, string joindate, int companyid)
        {
            //updating the employee
            //parameterized sql query
            string query = "update employees set FirstName=@FirstName, LastName=@LastName, Email=@Email, Address=@Address, Phone=@Phone, JoinDate=@JoinDate, CompanyId=@CompanyId where EmployeeId=@id";

            Debug.WriteLine(query);

            // parameters
            SqlParameter[] sqlparams = new SqlParameter[8];
            sqlparams[0] = new SqlParameter("@FirstName", firstname);
            sqlparams[1] = new SqlParameter("@LastName", lastname);
            sqlparams[2] = new SqlParameter("@Email", email);
            sqlparams[3] = new SqlParameter("@Phone", phone);
            sqlparams[4] = new SqlParameter("@Address", address);
            sqlparams[5] = new SqlParameter("@JoinDate", joindate);
            sqlparams[6] = new SqlParameter("@CompanyId", companyid);
            sqlparams[7] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from employees where employeeid = @id";
            SqlParameter param = new SqlParameter("@id", id);
            Employee selectedemp = db.Employees.SqlQuery(query, param).FirstOrDefault();

            return View(selectedemp);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from employees where employeeid = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            ////for the sake of referential integrity, unset the task for the employee
            string refquery = "update employees set employeeid = '' where employeeid=@id";
            db.Database.ExecuteSqlCommand(refquery, param);
            return RedirectToAction("List");
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