using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }

        //representing the many employees in one to many relation of employees and company
        public ICollection<Employee> Employees { get; set; }
       
       
    }
}