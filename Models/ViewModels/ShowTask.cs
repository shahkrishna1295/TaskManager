using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models.ViewModels
{
    public class ShowTask
    {
        //for the individiual task
        public virtual Task task { get; set; }

        //employees associated with the task
        public List<Employee> employees { get; set; }

        //all the employees of the database
        public List<Employee> all_employees { get; set; }
    }
}