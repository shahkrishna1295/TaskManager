namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        JoinDate = c.DateTime(),
                        CompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.employeesXtasks",
                c => new
                    {
                        EmployeesXtasksid = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeesXtasksid)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(),
                        EstStartDate = c.DateTime(nullable: false),
                        EstEndDate = c.DateTime(nullable: false),
                        ActStartDate = c.DateTime(),
                        ActEndDate = c.DateTime(),
                        Description = c.String(),
                        Status = c.String(),
                        Note = c.String(),
                        Priority = c.String(),
                    })
                .PrimaryKey(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.employeesXtasks", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.employeesXtasks", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropIndex("dbo.employeesXtasks", new[] { "TaskId" });
            DropIndex("dbo.employeesXtasks", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            DropTable("dbo.Tasks");
            DropTable("dbo.employeesXtasks");
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
        }
    }
}
