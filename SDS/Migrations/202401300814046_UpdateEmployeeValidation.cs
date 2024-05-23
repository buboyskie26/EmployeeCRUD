namespace SDS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployeeValidation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ContactNo", c => c.String());
            AddColumn("dbo.Employees", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "EmailAddress");
            DropColumn("dbo.Employees", "ContactNo");
        }
    }
}
