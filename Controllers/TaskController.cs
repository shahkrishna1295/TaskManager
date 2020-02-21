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


namespace TaskManager.Models
{
    public class TaskController : Controller
    {
        private TaskManagerContext db = new TaskManagerContext();
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string empsearchkey)
        {
            //access the search key

            //Debug.WriteLine("The search key is " + empsearchkey);

            //getting all the tasks
            string query = "Select * from tasks";

            if (empsearchkey != "")
            {
                //appending the query to include the search key
                query = query + " where TaskName like '%" + empsearchkey + "%'";
                Debug.WriteLine("The query is " + query);
            }

            List<Task> task = db.Tasks.SqlQuery(query).ToList();
            return View(task);

        }
        //to get the view of the add new task
        public ActionResult Add()
        {
            return View();
        }
        //after submiting the form of adding task
        [HttpPost]
        public ActionResult Add(string taskname, string eststartdate, string estenddate, string description, string status, string note, string priority)
        {
            //writing query in parameterized format
            string query = "insert into tasks (taskname, eststartdate, estenddate, description, status, note, priority) values (@taskname, @estenddate,@estenddate, @description, @status, @note, @priority)";
            //query before the parameters
            Debug.WriteLine(query);

            //insert query parameters array
            SqlParameter[] sqlparams = new SqlParameter[7];
            sqlparams[0] = new SqlParameter("@taskname", taskname);
            sqlparams[1] = new SqlParameter("@eststartdate", eststartdate);
            sqlparams[2] = new SqlParameter("@estenddate", estenddate);
            sqlparams[3] = new SqlParameter("@description", description);
            sqlparams[4] = new SqlParameter("@status", status);
            sqlparams[5] = new SqlParameter("@note", note);
            sqlparams[6] = new SqlParameter("@priority", priority);


            Debug.WriteLine(query);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult Show(int id)
        {
            //get the data for an inidivial task
            string main_query = "select * from tasks where taskid = @id";

            //sql parameter to pass pk
            var pk_parameter = new SqlParameter("@id", id);
            Task tsk = db.Tasks.SqlQuery(main_query, pk_parameter).FirstOrDefault();

           //getting the employees assigned to the particular task
            string aside_query = "select * from employees inner join employeesxtasks on employees.employeeid = employeesxtasks.employeeid where employeesxtasks.taskid=@id";
            var fk_parameter = new SqlParameter("@id", id);
            List<Employee> taskassign = db.Employees.SqlQuery(aside_query, fk_parameter).ToList();

            //getting all the employees to add more
            string all_employees_query = "select * from employees";
            List<Employee> AllEmployees = db.Employees.SqlQuery(all_employees_query).ToList();

            //passing the values to the viewmodel retrived from the database
            ShowTask viewmodel = new ShowTask();
            viewmodel.task = tsk;
            viewmodel.employees = taskassign;
            viewmodel.all_employees = AllEmployees;

            return View(viewmodel);
        }
        public ActionResult AttachEmployee(int id, int employeeid)
        {
            Debug.WriteLine("task id is" + id + " and employeeid is " + employeeid);

            //adding the employee to the particular task if action of adding is applied
            //cheking all the employees associated with the task
            string check_query = "select * from employees inner join employeesxtasks on employeesxtasks.employeeid = employees.employeeid where employees.employeeid=@employeeid and taskid=@id";
            SqlParameter[] check_params = new SqlParameter[2];
            check_params[0] = new SqlParameter("@id", id);
            check_params[1] = new SqlParameter("@employeeid", employeeid);
            List<Employee> emp = db.Employees.SqlQuery(check_query, check_params).ToList();

            //checks the existance of the employee
            if (emp.Count <= 0)
            {
                //this will attach the employee to the task in the database
                string query = "insert into employeesxtasks (employeeid, taskid) values (@employeeid, @id)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@id", id);
                sqlparams[1] = new SqlParameter("@employeeid", employeeid);


                db.Database.ExecuteSqlCommand(query, sqlparams);
            }

            return RedirectToAction("Show/" + id);

        }

        [HttpGet]
        public ActionResult DetachEmployee(int id, int employeeid)
        {
           
            Debug.WriteLine("task id is" + id + " and employee is " + employeeid);

            //this query will delete the record of an employee from the particular task associated.
            string query = "delete from employeesxtasks where employeeid=@employeeid and taskid=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@employeeid", employeeid);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("Show/" + id);
        }

        public ActionResult Update(int id)
        {
            //need information about a particular task
            Task selectedtsk = db.Tasks.SqlQuery("select * from tasks where taskid = @id", new SqlParameter("@id", id)).FirstOrDefault();

            return View(selectedtsk);
        }

        [HttpPost]
        public ActionResult Update(int id,string taskname, string eststartdate, string estenddate, string description, string status, string note, string priority)
        {
            //updating the task
            //parameterized sql query
            string query = "update tasks set taskname=@taskname, eststartdate=@eststartdate, estenddate=@estenddate, description=@description, status=@status, priority=@priority, note=@note where taskid=@id";

            Debug.WriteLine(query);

            // parameters
            SqlParameter[] sqlparams = new SqlParameter[8];
            sqlparams[0] = new SqlParameter("@taskname", taskname);
            sqlparams[1] = new SqlParameter("@eststartdate", eststartdate);
            sqlparams[2] = new SqlParameter("@estenddate", estenddate);
            sqlparams[3] = new SqlParameter("@description", description);
            sqlparams[4] = new SqlParameter("@status", status);
            sqlparams[5] = new SqlParameter("@note", note);
            sqlparams[6] = new SqlParameter("@priority", priority);
            sqlparams[7] = new SqlParameter("@id", id);

            Debug.WriteLine(query);
            db.Database.ExecuteSqlCommand(query, sqlparams);


            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
           
            string query = "select * from tasks where taskid = @id";
            SqlParameter param = new SqlParameter("@id", id);
            Task selectedtsk = db.Tasks.SqlQuery(query, param).FirstOrDefault();

            Debug.WriteLine(query);

            return View(selectedtsk);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Debug.WriteLine("error");
            string query = "delete from tasks where taskid = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            ////for the sake of referential integrity, unset the task for the employee
            string refquery = "update tasks set taskid = '' where taskid=@id";
            db.Database.ExecuteSqlCommand(refquery, param); 
            return RedirectToAction("List");
        }
    }
}