namespace SDS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateemployeedb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "EmpNo", c => c.String());
            AlterColumn("dbo.Employees", "FirstName", c => c.String());
            AlterColumn("dbo.Employees", "LastName", c => c.String());
            AlterColumn("dbo.Employees", "ContactNo", c => c.String());
            AlterColumn("dbo.Employees", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "EmailAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "ContactNo", c => c.String(nullable: false, maxLength: 11));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Employees", "EmpNo", c => c.String(nullable: false, maxLength: 6));
        }
    }
}
