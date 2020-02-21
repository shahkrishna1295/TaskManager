using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TaskManager.Data
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext() : base("name=TaskManagerContext")
        {
        }

        //reference : https://stackoverflow.com/questions/3600175/the-model-backing-the-database-context-has-changed-since-the-database-was-crea
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
           
          //  Database.SetInitializer<TaskManagerContext>(null);
            
       // }

        public System.Data.Entity.DbSet<TaskManager.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<TaskManager.Models.Task> Tasks { get; set; }
        public System.Data.Entity.DbSet<TaskManager.Models.Employee> Employees { get; set; }
    }
}