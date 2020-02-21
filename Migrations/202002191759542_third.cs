namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Company_CompanyId", "dbo.Companies");
            DropIndex("dbo.Employees", new[] { "Company_CompanyId" });
            RenameColumn(table: "dbo.Employees", name: "Company_CompanyId", newName: "CompanyId");
            AlterColumn("dbo.Employees", "CompanyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "CompanyId");
            AddForeignKey("dbo.Employees", "CompanyId", "dbo.Companies", "CompanyId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            AlterColumn("dbo.Employees", "CompanyId", c => c.Int());
            RenameColumn(table: "dbo.Employees", name: "CompanyId", newName: "Company_CompanyId");
            CreateIndex("dbo.Employees", "Company_CompanyId");
            AddForeignKey("dbo.Employees", "Company_CompanyId", "dbo.Companies", "CompanyId");
        }
    }
}
