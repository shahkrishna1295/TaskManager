namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_in_model : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskEmployees", "Task_TaskId", "dbo.Tasks");
            DropForeignKey("dbo.TaskEmployees", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.TaskEmployees", new[] { "Task_TaskId" });
            DropIndex("dbo.TaskEmployees", new[] { "Employee_EmployeeId" });
            CreateTable(
                "dbo.EmployeesXTasks",
                c => new
                    {
                        employeesXtasksid = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.employeesXtasksid)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.TaskId);
            
            DropTable("dbo.TaskEmployees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TaskEmployees",
                c => new
                    {
                        Task_TaskId = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Task_TaskId, t.Employee_EmployeeId });
            
            DropForeignKey("dbo.EmployeesXTasks", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.EmployeesXTasks", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmployeesXTasks", new[] { "TaskId" });
            DropIndex("dbo.EmployeesXTasks", new[] { "EmployeeId" });
            DropTable("dbo.EmployeesXTasks");
            CreateIndex("dbo.TaskEmployees", "Employee_EmployeeId");
            CreateIndex("dbo.TaskEmployees", "Task_TaskId");
            AddForeignKey("dbo.TaskEmployees", "Employee_EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.TaskEmployees", "Task_TaskId", "dbo.Tasks", "TaskId", cascadeDelete: true);
        }
    }
}
