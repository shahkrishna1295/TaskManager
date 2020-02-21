using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TaskManager.Models
{
    public class EmployeesXTasks
    {
        public int employeesXtasksid { get; set; }

        public int EmployeeId { get; set; }
        
        public int TaskId { get; set; }
        public virtual Employee Employees { get; set; }

        public virtual Task Task { get; set; }
    }
}