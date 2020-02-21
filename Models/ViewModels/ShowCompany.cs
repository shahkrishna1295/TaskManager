using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models.ViewModels
{
    public class ShowCompany
    {
        //for the individual employee
        public virtual Company company { get; set; }

        //for the employees associated with the company
        public List<Employee> employees { get; set; }

        //all the employees in the database
        public List<Employee> all_employees { get; set; }

    }
}