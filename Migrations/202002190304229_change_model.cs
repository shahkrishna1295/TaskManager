namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_model : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            RenameColumn(table: "dbo.Employees", name: "CompanyId", newName: "Company_CompanyId");
            AlterColumn("dbo.Employees", "Company_CompanyId", c => c.Int());
            CreateIndex("dbo.Employees", "Company_CompanyId");
            AddForeignKey("dbo.Employees", "Company_CompanyId", "dbo.Companies", "CompanyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Company_CompanyId", "dbo.Companies");
            DropIndex("dbo.Employees", new[] { "Company_CompanyId" });
            AlterColumn("dbo.Employees", "Company_CompanyId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Employees", name: "Company_CompanyId", newName: "CompanyId");
            CreateIndex("dbo.Employees", "CompanyId");
            AddForeignKey("dbo.Employees", "CompanyId", "dbo.Companies", "CompanyId", cascadeDelete: true);
        }
    }
}
