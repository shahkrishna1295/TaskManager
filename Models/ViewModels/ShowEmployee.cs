using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models.ViewModels
{
    public class ShowEmployee
    {
        //for the individual employee
        public virtual Employee employee { get; set; }

        //tasks associated with the employee
        public List<Task> tasks { get; set; }

        //all the tasks of the database
        public List<Task> all_tasks { get; set; }
    }
}