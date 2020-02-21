using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Task
    {
       [Key]
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public string EstStartDate { get; set; }

        public string EstEndDate { get; set; }

        public string ActStartDate { get; set; }

        public string ActEndDate { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Note { get; set; }

        public string Priority { get; set; }

        //representing the many to many of employees and tasks
        public  ICollection<EmployeesXTasks> EmployeesXTasks { get; set; }

    }
}