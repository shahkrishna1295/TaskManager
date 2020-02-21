namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowing_nulls : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "EstStartDate", c => c.String());
            AlterColumn("dbo.Tasks", "EstEndDate", c => c.String());
            AlterColumn("dbo.Tasks", "ActStartDate", c => c.String());
            AlterColumn("dbo.Tasks", "ActEndDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "ActEndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tasks", "ActStartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tasks", "EstEndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tasks", "EstStartDate", c => c.DateTime(nullable: false));
        }
    }
}
