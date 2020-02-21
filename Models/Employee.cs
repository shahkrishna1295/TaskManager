using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        //can further divide into city, postal code, province
        public string Address { get; set; }

        public string Phone { get; set; }

        public DateTime JoinDate { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Companies { get; set; }

        //representing the many to many of employees and tasks
        public ICollection<EmployeesXTasks> EmployeesXTasks { get; set; }
    }
}