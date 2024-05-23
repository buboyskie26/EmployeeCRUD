namespace SDS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequiredfields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Employees", "ContactNo", c => c.String(nullable: false, maxLength: 11));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "ContactNo", c => c.String(maxLength: 11));
            AlterColumn("dbo.Employees", "LastName", c => c.String(maxLength: 15));
            AlterColumn("dbo.Employees", "FirstName", c => c.String(maxLength: 15));
        }
    }
}
