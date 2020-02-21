namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyMany_rel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.employeesXtasks", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.employeesXtasks", "TaskId", "dbo.Tasks");
            DropIndex("dbo.employeesXtasks", new[] { "EmployeeId" });
            DropIndex("dbo.employeesXtasks", new[] { "TaskId" });
            CreateTable(
                "dbo.TaskEmployees",
                c => new
                    {
                        Task_TaskId = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Task_TaskId, t.Employee_EmployeeId })
                .ForeignKey("dbo.Tasks", t => t.Task_TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId, cascadeDelete: true)
                .Index(t => t.Task_TaskId)
                .Index(t => t.Employee_EmployeeId);
            
            DropTable("dbo.employeesXtasks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.employeesXtasks",
                c => new
                    {
                        EmployeesXtasksid = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeesXtasksid);
            
            DropForeignKey("dbo.TaskEmployees", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.TaskEmployees", "Task_TaskId", "dbo.Tasks");
            DropIndex("dbo.TaskEmployees", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.TaskEmployees", new[] { "Task_TaskId" });
            DropTable("dbo.TaskEmployees");
            CreateIndex("dbo.employeesXtasks", "TaskId");
            CreateIndex("dbo.employeesXtasks", "EmployeeId");
            AddForeignKey("dbo.employeesXtasks", "TaskId", "dbo.Tasks", "TaskId", cascadeDelete: true);
            AddForeignKey("dbo.employeesXtasks", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
    }
}
